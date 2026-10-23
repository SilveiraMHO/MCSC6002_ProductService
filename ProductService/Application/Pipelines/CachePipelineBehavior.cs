using Application.Pipelines.Interfaces;
using Application.Settings;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Application.Pipelines
{
    public class CachePipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, ICacheable
    {
        private readonly IDistributedCache _cache;
        private readonly CacheSettings _cacheSettings;

        public CachePipelineBehavior(IDistributedCache cache, IOptions<CacheSettings> cacheSettings)
        {
            _cache = cache;
            _cacheSettings = cacheSettings.Value;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request.BypassCache) return await next();

            TResponse response;

            string fullCacheKey = $"{_cacheSettings.ApplicationName}:{request.CacheKey}";

            var cacheResponse = await _cache.GetAsync(fullCacheKey, cancellationToken);

            if (cacheResponse is not null)
            {
                response = JsonSerializer.Deserialize<TResponse>(Encoding.UTF8.GetString(cacheResponse), _jsonOptions);
            }
            else
            {
                response = await GetResponseAndWriteToCacheAsync();
            }

            return response;

            #region Helper
            async Task<TResponse> GetResponseAndWriteToCacheAsync()
            {
                response = await next();

                if (response is not null)
                {
                    var slidingExpiration = request.SlidingExpiration ?? TimeSpan.FromMinutes(_cacheSettings.SlidingExpirationInMinutes);

                    var cacheOptions = new DistributedCacheEntryOptions
                    {
                        SlidingExpiration = slidingExpiration,
                        AbsoluteExpiration = DateTime.Now.AddDays(1)
                    };

                    var serializedData = JsonSerializer.SerializeToUtf8Bytes(response, _jsonOptions);

                    await _cache.SetAsync(fullCacheKey, serializedData, cacheOptions, cancellationToken);
                }

                return response;
            }
            #endregion
        }

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            WriteIndented = false
        };
    }
}
