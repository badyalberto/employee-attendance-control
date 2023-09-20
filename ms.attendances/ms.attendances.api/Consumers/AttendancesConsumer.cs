using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using ms.attendances.application.Commands;
using ms.attendances.application.Request;
using ms.rabbitmq.Consumers;
using ms.rabbitmq.Events;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;

namespace ms.attendances.api.Consumers
{
    public class AttendancesConsumer : IConsumer
    {
        private readonly IMediator _mediator;
        private IConnection _connection;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AttendancesConsumer(IMediator mediator, IMapper mapper, IConfiguration configuration)
        {
            _mediator = mediator;
            _mapper = mapper;
            _configuration = configuration;
        }

        public void Subscribe()
        {
            var factory = new ConnectionFactory
            {
                HostName = _configuration.GetValue<string>("Communication:EventBus:HostName")
            };

            _connection = factory.CreateConnection();
            var channel = _connection.CreateModel();
            var queue = typeof(AttendanceStateChangedEvent).Name;
            channel.QueueDeclare(queue, durable: true, exclusive: false, autoDelete: false, null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += RecievedEvent;
        }

        private async void RecievedEvent(object sender, BasicDeliverEventArgs e)
        {
            if (e.RoutingKey == typeof(AttendanceStateChangedEvent).Name)
            {
                var message = Encoding.UTF8.GetString(e.Body.Span);
                var attendanceStateChangedEvent = JsonSerializer.Deserialize<AttendanceStateChangedEvent>(message);

                _ = await _mediator.Send(new CreateAttendanceCommand(attendanceStateChangedEvent.UserName,
                    _mapper.Map<CreateAttendanceRequest>(attendanceStateChangedEvent)));
            }
        }

        public void Unsubscribe() => _connection?.Dispose();
        
    }
}
