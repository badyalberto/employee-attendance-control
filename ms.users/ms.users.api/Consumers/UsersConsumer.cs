using AutoMapper;
using Microsoft.Extensions.Configuration;
using MediatR;
using ms.rabbitmq.Consumers;
using RabbitMQ.Client;
using ms.rabbitmq.Events;
using System.Text;
using System.Text.Json;
using ms.users.application.Commands;
using RabbitMQ.Client.Events;

namespace ms.users.api.Consumers
{
    public class UsersConsumer : IConsumer
    {
        private readonly IMediator _mediator;
        private IConnection _connection;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UsersConsumer(IMediator mediator, IMapper mapper, IConfiguration configuration)
        {
            _mediator = mediator;
            _mapper = mapper;
            _configuration = configuration;
        }

        public void Subscribe()
        {
            var factory = new ConnectionFactory()
            {
                HostName = _configuration.GetValue<string>("Communication:EventBus:HostName")
            };

            _connection = factory.CreateConnection();
            var channel = _connection.CreateModel();

            var queue = typeof(EmployeeCreateEvent).Name;

            var consumer = new EventingBasicConsumer(channel); //EventingBasicConsumer
            consumer.Received += RecievedEvent;

            channel.BasicConsume(queue: queue,autoAck: true, consumer: consumer);
        }
        public void Unsubscribe() => _connection?.Dispose();

        private async void RecievedEvent(object sender, BasicDeliverEventArgs e)
        {
            if (e.RoutingKey == typeof(EmployeeCreateEvent).Name)
            {
                var message = Encoding.UTF8.GetString(e.Body.Span);
                var employeeCreatedEvent = JsonSerializer.Deserialize<EmployeeCreateEvent>(message);

                var result = await _mediator.Send(_mapper.Map<CreateUserAccountCommand>(employeeCreatedEvent));
            }
        }
    }
}
