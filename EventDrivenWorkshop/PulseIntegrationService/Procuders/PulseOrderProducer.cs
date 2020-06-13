using EDCommon;
using EDCommon.Pulses;
using EDCommon.RabbitMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PulseIntegrationService.Procuders
{
    public class PulseOrderProducer
    {
        public async Task PriceOrder(string locationCode, IPriceOrderResponse pulseResponse)
        {
            await Task.Delay(3000);

            var bus = BusConfigurator.ConfigureBus();
            await bus.StartAsync();
            var endPoint = await bus.GetSendEndpoint(new Uri($"{CustomKey.RABBITMQ_BASE_ENDPOINT}/{CustomKey.RABBITMQ_PRICE_ORDER_RESPONSE_ENDPOINT}"));
            await endPoint.Send<IPriceOrderResponse>(pulseResponse);
        }

        public async Task PlaceOrder(string locationCode, IPlaceOrderResponse orderRequest)
        {
            await Task.Delay(3000);

            var bus = BusConfigurator.ConfigureBus();
            await bus.StartAsync();
            var endPoint = await bus.GetSendEndpoint(new Uri($"{CustomKey.RABBITMQ_BASE_ENDPOINT}/{CustomKey.RABBITMQ_PLACE_ORDER_RESPONSE_ENDPOINT}"));
            await endPoint.Send<IPlaceOrderResponse>(orderRequest);
        }
    }
}
