using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SamayaPatra.Business;
using SamayaPatra.Common;
using SamayaPatra.Helper;

namespace SamayaPatra.Controllers
{
    public class ReportController : BaseController
    {
        readonly BALTransaction bALTransaction = new BALTransaction();
        readonly BALVehicle bALVehicle = new BALVehicle();
        public async Task<IActionResult> Index()
        {
            List<ReportInfo> transactions = new List<ReportInfo>();
            var dt = await bALTransaction.GetReportDetails(new ReportInfo() { UserID = UserContext.UserRole.ToUpper() == "Administrator".ToUpper() ? null : (int?)UserContext.UserID });
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    transactions.Add(new Common.ReportInfo()
                    {
                        TransactionID = dr["TrasactionID"].ToInt32(),
                        VehicleNo = dr["VehicleNo"].ToText(),
                        RouteName = dr["RouteName"].ToText(),
                        TotalKM = dr["TotalKM"].ToDecimal(),
                        CheckPointName = dr["CheckPointName"].ToText(),
                        CheckPointOrder = dr["CheckPointOrder"].ToInt32(),
                        EstimatedKM = dr["EstimatedKM"].ToDecimal(),
                        EstimatedArrivalMinutes = dr["EstimatedArrivalMinutes"].ToDecimal(),
                        ArrivalTime = dr["ArrivalTime"].ToDateTime(),
                        ArrivalMinutes = dr["ArrivalMinutes"].ToDecimal()
                    });
                }
            }
            return View(transactions);
        }
    }
}