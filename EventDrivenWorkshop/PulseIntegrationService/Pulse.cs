using EDCommon;
using EDCommon.Model;
using EDCommon.Pulses;
using EDCommon.RabbitMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PulseIntegrationService
{
    public class Pulse
    {
        public async Task<IPriceOrderResponse> PriceOrder<T>(string locationCode, T orderRequest)
        {
            await Task.Delay(3000);

            //SEND REQUEST TO PULSE AND GET RESPONSE

            return new PulseResponse { Status = "0" };
        }

        public async Task<IPlaceOrderResponse> PlaceOrder<T>(string locationCode, T orderRequest)
        {
            await Task.Delay(3000);

            //SEND REQUEST TO PULSE AND GET RESPONSE

            return new PulseResponse { Status = "1" };
        }
    }
}
