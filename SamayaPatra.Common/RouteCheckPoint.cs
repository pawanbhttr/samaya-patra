using System;
using System.Collections.Generic;
using System.Text;

namespace SamayaPatra.Common
{
    public class RouteCheckPoint
    {
        public int RouteCheckPointID { get; set; }
        public int RouteID { get; set; }
        public int CheckPointID { get; set; }
        public int CheckPointOrder { get; set; }
        public decimal EstimatedKM { get; set; }
        public int EstimatedArrivalMinutes { get; set; }
    }
}
