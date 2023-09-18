using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ms.rabbitmq.Consumers
{
    public interface IConsumer
    {
        void Subscribe();
        void Unsubscribe();
    }
}
