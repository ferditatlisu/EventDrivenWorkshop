using BFF.Hubs;
using EDCommon;
using EDCommon.Model;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BFF.Consumers
{
    public class OrderResponseConsumer : IConsumer<IOrderResponse>
    {

        private readonly OrderHub _orderHub;

        public OrderResponseConsumer(OrderHub orderHub)
        {
            _orderHub = orderHub;
        }
        
        public async Task Consume(ConsumeContext<IOrderResponse> context)
        {
            var connectionId = context.Headers.Get<string>(CustomKey.SIGNALR_CONNECTION_ID);
            if(!string.IsNullOrEmpty(connectionId))
                await _orderHub.SendOrder(connectionId, context.Message.Status);
        }
    }
}
