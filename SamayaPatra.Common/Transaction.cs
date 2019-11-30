using System;
using System.Collections.Generic;
using System.Text;

namespace SamayaPatra.Common
{
    public class Transaction
    {
        public int? TrasactionID { get; set; }
        public int? VehicleID { get; set; }
        public int RouteID { get; set; }
        public string RouteName { get; set; }
        public decimal TotalKM { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string VehicleNo { get; set; }
        public string LastCheckPoint { get; set; }
        public DateTime LastCheckedDate { get; set; }
    }
}
