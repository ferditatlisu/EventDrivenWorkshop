using EDCommon.Model;
using EDCommon.Pulses;
using MassTransit;
using PulseIntegrationService.Procuders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace PulseIntegrationService.Consumers
{
    public class PriceOrderConsumer : IConsumer<IPriceOrderRequest>
    {
        public async Task Consume(ConsumeContext<IPriceOrderRequest> context)
        {
            var locationCode =  context.Message.LocationCode;
            var a = 3;

            Pulse pulse = new Pulse();
            var response = await pulse.PriceOrder(context.Message.LocationCode, context.Message);

            PulseOrderProducer priceOrderProducer = new PulseOrderProducer();
            await priceOrderProducer.PriceOrder(context.Message.LocationCode, response);
        }
    }
}
