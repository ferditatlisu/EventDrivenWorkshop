using System;
using System.Collections.Generic;
using System.Text;

namespace EDCommon.Model
{
    public class OrderRequest
    {
        public string Id { get; set; }
        public string LocationCode { get; set; }
        public string ConnectionId { get; set; }
    }
}
