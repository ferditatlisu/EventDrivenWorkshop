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

            PulseRequest pulseRequest = new PulseRequest
            {
                Id = "id",
                LocationCode = "Loc",
            };

            var bus = BusConfigurator.ConfigureBus();
            await bus.StartAsync();
            var endPoint = await bus.GetSendEndpoint(new Uri($"{CustomKey.RABBITMQ_BASE_ENDPOINT}/{CustomKey.RABBITMQ_PLACE_ORDER_REQUEST_ENDPOINT}"));
            await endPoint.Send<IPlaceOrderRequest>(pulseRequest);
        }
    }
}
