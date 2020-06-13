using EDCommon;
using EDCommon.Model;
using EDCommon.Pulses;
using EDCommon.RabbitMQ;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.Consumers
{
    public class PlaceOrderConsumer : IConsumer<IPlaceOrderResponse>
    {
        public async Task Consume(ConsumeContext<IPlaceOrderResponse> context)
        {
            // do somethings ....

            OrderResponse ordeResponse = new OrderResponse
            {
                Status = context.Message.Status
            };

            var bus = BusConfigurator.ConfigureBus();
            await bus.StartAsync();
            var endPoint = await bus.GetSendEndpoint(new Uri($"{CustomKey.RABBITMQ_BASE_ENDPOINT}/{CustomKey.RABBITMQ_ORDER_RESPONSE_ENDPOINT}"));
            await endPoint.SendWithHeaders<IOrderResponse>(ordeResponse, context);
        }
    }
}
