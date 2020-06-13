using System;
using System.Collections.Generic;
using System.Text;

namespace EDCommon.Pulses
{
    public class PulseRequest : IPriceOrderRequest, IPlaceOrderRequest
    {
        public string Id { get; set; }
        public string LocationCode { get; set; }
    }
}
