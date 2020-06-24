using EDCommon;
using EDCommon.Pulses;
using EDCommon.RabbitMQ;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.Consumers
{
    public class PriceOrderConsumer : IConsumer<IPriceOrderResponse>
    {
        public async Task Consume(ConsumeContext<IPriceOrderResponse> context)
        {
            // do somethings ....

            var pulseRequest = context.Headers.Get<PulseRequest>(CustomKey.ORDER_REQUEST_KEY);
            var bus = BusConfigurator.ConfigureBus();
            await bus.StartAsync();
            var endPoint = await bus.GetSendEndpoint(new Uri($"{CustomKey.RABBITMQ_BASE_ENDPOINT}/{CustomKey.RABBITMQ_PLACE_ORDER_REQUEST_ENDPOINT}"));
            await endPoint.SendWithHeaders<IPlaceOrderRequest>(pulseRequest, context);
        }
    }
}
