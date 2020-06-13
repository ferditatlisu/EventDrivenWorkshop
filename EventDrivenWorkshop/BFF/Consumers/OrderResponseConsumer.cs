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
        public async Task Consume(ConsumeContext<IOrderResponse> context)
        {
            var ss= 3;
            // do somethings ....

            //Socket ile datayi gonder !!!
        }
    }
}
