using System;
using System.Collections.Generic;
using System.Text;

namespace EDCommon.Pulses
{
    public interface IPriceOrderRequest
    {
        string Id { get; set; }
        string LocationCode { get; set; }
    }
}
