using EDCommon;
using EDCommon.Pulses;
using EDCommon.RabbitMQ;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PulseIntegrationService.Procuders
{
    public class PulseOrderProducer
    {
        private readonly IBusControl _busControl;
        private readonly ConsumeContext _consumeContext;

        public PulseOrderProducer(IBusControl busControl, ConsumeContext consumeContext)
        {
            _busControl = busControl;
            _consumeContext = consumeContext;
        }

        public async Task PriceOrder(string locationCode, IPriceOrderResponse pulseResponse)
        {
            await Task.Delay(3000);
            var endPoint = await _busControl.GetSendEndpoint(new Uri($"{CustomKey.RABBITMQ_BASE_ENDPOINT}/{CustomKey.RABBITMQ_PRICE_ORDER_RESPONSE_ENDPOINT}"));
            await endPoint.SendWithHeaders<IPriceOrderResponse>(pulseResponse, _consumeContext);
        }

        public async Task PlaceOrder(string locationCode, IPlaceOrderResponse orderRequest)
        {
            await Task.Delay(3000);
            var endPoint = await _busControl.GetSendEndpoint(new Uri($"{CustomKey.RABBITMQ_BASE_ENDPOINT}/{CustomKey.RABBITMQ_PLACE_ORDER_RESPONSE_ENDPOINT}"));
            await endPoint.SendWithHeaders<IPlaceOrderResponse>(orderRequest, _consumeContext);
        }
    }
}
