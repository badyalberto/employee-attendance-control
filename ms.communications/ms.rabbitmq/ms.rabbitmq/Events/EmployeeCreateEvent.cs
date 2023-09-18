using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ms.rabbitmq.Events
{
    public class EmployeeCreateEvent : IRabbitMqEvent
    {
        public Guid MyProperty => Guid.NewGuid();
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public string Serialize() => JsonSerializer.Serialize(this);
        
    }
}
