using Application.Pipelines;
using Application.Settings;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, CacheSettings cacheSettings)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = cacheSettings.DestinationUrl;
            });

            var assembly = Assembly.GetExecutingAssembly();

            services
                .AddMediatR(cfg =>
                {
                    cfg.RegisterServicesFromAssembly(assembly);
                })
                .AddValidatorsFromAssembly(assembly)
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>))
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(CachePipelineBehavior<,>));

            return services;
        }
    }
}
