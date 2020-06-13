using EDCommon.Pulses;
using MassTransit;
using PulseIntegrationService.Procuders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PulseIntegrationService.Consumers
{
    public class PlaceOrderConsumer : IConsumer<IPlaceOrderRequest>
    {
        public async Task Consume(ConsumeContext<IPlaceOrderRequest> context)
        {
            var locationCode = context.Message.LocationCode;
            var a = 3;

            Pulse pulse = new Pulse();
            var response = await pulse.PlaceOrder(context.Message.LocationCode, context.Message);

            PulseOrderProducer priceOrderProducer = new PulseOrderProducer();
            await priceOrderProducer.PlaceOrder(context.Message.LocationCode, response);
        }
    }
}
