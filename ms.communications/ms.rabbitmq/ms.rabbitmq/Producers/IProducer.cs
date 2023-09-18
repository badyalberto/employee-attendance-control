using ms.rabbitmq.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ms.rabbitmq.Producers
{
    public interface IProducer
    {
        void Produce(IRabbitMqEvent rabbitMqEvent);
    }
}
