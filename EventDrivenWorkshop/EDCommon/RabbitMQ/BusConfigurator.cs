using MassTransit;
using MassTransit.RabbitMqTransport;
using System;
using System.Collections.Generic;
using System.Text;

namespace EDCommon.RabbitMQ
{
    public static class BusConfigurator
    {
        public static IBusControl ConfigureBus(Action<IRabbitMqBusFactoryConfigurator, IRabbitMqHost> registrationAction = null)
            =>
            Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host("rabbitmq", "/", hst =>
                {
                    hst.Username("test");
                    hst.Password("test");
                });

                registrationAction?.Invoke(cfg, host);
            });
    }
}
