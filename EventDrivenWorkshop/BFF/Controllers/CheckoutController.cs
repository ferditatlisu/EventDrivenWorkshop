using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EDCommon;
using EDCommon.Model;
using EDCommon.RabbitMQ;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BFF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderRequest orderRequest)
        {
            using HttpClient client = new HttpClient();
            var json = JsonConvert.SerializeObject(orderRequest);
            var httpContent = new StringContent(json, Encoding.UTF8, CustomKey.JSON_CONTENT_TYPE);
            await client.PostAsync("http://be/api/order", httpContent);

            return Ok();
        }
    }
}