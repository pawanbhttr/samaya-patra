using System;
using System.Collections.Generic;
using System.Text;

namespace SamayaPatra.Common
{
    public class ReportInfo
    {
        public int TransactionID { get; set; }
        public int VehicleID { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string VehicleNo { get; set; }
        public string RouteName { get; set; }
        public decimal TotalKM { get; set; }
        public string CheckPointName { get; set; }
        public int CheckPointOrder { get; set; }
        public decimal EstimatedKM { get; set; }
        public decimal EstimatedArrivalMinutes { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime PrevArrivalTime { get; set; }
        public decimal ArrivalMinutes { get; set; }
        public int?  UserID { get; set; }
    }
}
