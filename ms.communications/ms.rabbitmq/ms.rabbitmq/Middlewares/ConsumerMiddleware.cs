using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ms.rabbitmq.Consumers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ms.rabbitmq.Middlewares
{
    public static class ConsumerMiddleware
    {
        private static IConsumer _consumer { get; set; }

        public static IApplicationBuilder UseRabbitConsumer(this IApplicationBuilder app, IConsumer consumer)
        {
            _consumer = consumer;

            IHostApplicationLifetime lifetime = app.ApplicationServices.GetService(typeof(IHostApplicationLifetime)) as IHostApplicationLifetime;

            lifetime.ApplicationStarted.Register(OnStarted);

            lifetime.ApplicationStopping.Register(OnStopping);

            return app;
        }

        private static void OnStarted() => _consumer.Subscribe();
        private static void OnStopping() => _consumer.Unsubscribe();
    }
}
