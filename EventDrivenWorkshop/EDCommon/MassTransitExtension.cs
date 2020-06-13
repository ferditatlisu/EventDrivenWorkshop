using EDCommon.RabbitMQ;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EDCommon
{
    public static class MassTransitExtension
    {
        public static async Task SendWithHeaders<T>(this ISendEndpoint sendEndpoint, T data, ConsumeContext consumeContext)
        {
            await sendEndpoint.Send(data, context=>
            {
                var headers = consumeContext.Headers.GetAll();
                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        context.Headers.Set(header.Key, header.Value);
                    }
                }
            });
        }
    }
}
