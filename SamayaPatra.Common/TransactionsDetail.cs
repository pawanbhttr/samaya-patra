using System;
using System.Collections.Generic;
using System.Text;

namespace SamayaPatra.Common
{
    public class TransactionsDetail
    {
        public int TransactionDetailID { get; set; }
        public int TransactionID { get; set; }
        public int CheckPointID { get; set; }
        public string CheckPointName { get; set; }
        public int CheckPointOrder { get; set; }
        public decimal EstimatedKM { get; set; }
        public int EstimatedArrivalMinutes { get; set; }
        public DateTime ArrivalTime { get; set; }
    }
}
