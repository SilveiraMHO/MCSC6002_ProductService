using Application.Interfaces.Messaging;
using MassTransit;

namespace Infrastructure.Messaging
{
    public class EventPublisher : IEventPublisher
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public EventPublisher(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task PublishAsync<T>(T @event, CancellationToken ct = default) where T : class
        {
            await _publishEndpoint.Publish(@event, ct);
        }
    }
}
