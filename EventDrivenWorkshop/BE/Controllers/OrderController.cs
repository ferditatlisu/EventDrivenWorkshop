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

            //INFO : Make sure to check exchanges to binding to orderRequestQueue
            var bus = BusConfigurator.ConfigureBus();
            await bus.StartAsync();
            var endPoint = await bus.GetSendEndpoint(new Uri($"{CustomKey.RABBITMQ_BASE_ENDPOINT}/{CustomKey.RABBITMQ_PRICE_ORDER_REQUEST_ENDPOINT}"));
            await endPoint.Send<IPriceOrderRequest>(pulseRequest);

            return Ok();
        }
    }
}