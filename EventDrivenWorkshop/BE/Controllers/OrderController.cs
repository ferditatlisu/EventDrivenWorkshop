using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EDCommon;
using EDCommon.Model;
using EDCommon.RabbitMQ;
using EDCommon.Pulses;
using MassTransit;

namespace BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderRequest orderRequest)
        {
            PulseRequest pulseRequest = new PulseRequest
            {
                Id = orderRequest.Id,
                LocationCode = orderRequest.LocationCode
            };

            var bus = BusConfigurator.ConfigureBus();
            await bus.StartAsync();
            ISendEndpoint endPoint = await bus.GetSendEndpoint(new Uri($"{CustomKey.RABBITMQ_BASE_ENDPOINT}/{CustomKey.RABBITMQ_PRICE_ORDER_REQUEST_ENDPOINT}"));

            await endPoint.Send<IPriceOrderRequest>(pulseRequest, x=>
            {
                x.Headers.Set(CustomKey.SIGNALR_CONNECTION_ID, orderRequest.ConnectionId);
            });

            return Ok();
        }
    }
}