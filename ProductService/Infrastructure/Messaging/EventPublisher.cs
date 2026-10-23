using Application.Interfaces.Messaging;
using Application.Settings;
using MassTransit;
using Microsoft.Extensions.Options;

namespace Infrastructure.Messaging
{
    public class EventPublisher : IEventPublisher
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly byte[] _signingKey;

        public EventPublisher(IPublishEndpoint publishEndpoint, IOptions<RabbitMqSettings> rabbitMqSettings)
        {
            _publishEndpoint = publishEndpoint;
            _signingKey = MessageSignature.ParseKey(rabbitMqSettings.Value.SigningKey);
        }

        public async Task PublishAsync<T>(T @event, CancellationToken ct = default) where T : class
        {
            // HMAC + timestamp headers: integrity, replay window.
            var signedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
            var signature = MessageSignature.Compute(@event, signedAt, _signingKey);

            await _publishEndpoint.Publish(@event, context =>
            {
                context.Headers.Set(MessageSignature.SignedAtHeader, signedAt);
                context.Headers.Set(MessageSignature.SignatureHeader, signature);
            }, ct);
        }
    }
}
