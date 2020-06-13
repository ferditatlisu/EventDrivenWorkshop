using System;
using System.Collections.Generic;
using System.Text;

namespace EDCommon.Pulses
{
    public class PulseResponse : IPriceOrderResponse, IPlaceOrderResponse
    {
        public string Status { get; set; }
    }
}
