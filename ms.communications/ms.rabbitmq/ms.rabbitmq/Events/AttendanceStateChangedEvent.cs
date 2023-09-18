using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ms.rabbitmq.Events
{
    public class AttendanceStateChangedEvent : IRabbitMqEvent
    {
        public Guid EventId => Guid.NewGuid();
        public string UserName { get; set; }
        public string FullName { get; set; }
        public DateTime Date { get; set; }
        public bool Attendance { get; set; }
        public string Notes { get; set; }

        public string Serialize() => JsonSerializer.Serialize(this);
        
    }
}
