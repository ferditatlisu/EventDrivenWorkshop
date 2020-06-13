using System;
using System.Collections.Generic;
using System.Text;

namespace EDCommon.Model
{
    public interface IOrderRequest
    {
        string Id { get; set; }
        string LocationCode { get; set; }
    }
}
