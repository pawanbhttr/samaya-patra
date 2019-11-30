using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using SamayaPatra.Business;
using SamayaPatra.Common;
using SamayaPatra.Helper;

namespace SamayaPatra.Controllers
{
    public class TransactionController : BaseController
    {
        readonly BALTransaction bALTransaction = new BALTransaction();
        readonly BALVehicle bALVehicle = new BALVehicle();
        readonly BALRoute bALRoute = new BALRoute();
        public async Task<IActionResult> Index()
        {
            await LoadVehicleNoAsync();
            await LoadRoutesAsync();
            return View();
        }

        public async Task<IActionResult> List()
        {
            List<Common.Transaction> transactions = new List<Common.Transaction>();
            var dt = await bALTransaction.GetActiveTripAsync();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    transactions.Add(new Common.Transaction()
                    {
                        TrasactionID = dr["TrasactionID"].ToInt32(),
                        VehicleNo = dr["VehicleNo"].ToText(),
                        RouteName = dr["RouteName"].ToText(),
                        TotalKM = dr["TotalKM"].ToDecimal(),
                        LastCheckPoint = dr["LastCheckPoint"].ToText(),
                        LastCheckedDate = dr["LastCheckedDate"].ToDateTime()
                    });
                }
            }
            return View(transactions);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Common.Transaction transaction)
        {
            try
            {
                if (!(transaction.TrasactionID.ToInt32() > 0 || (transaction.VehicleID.ToInt32() > 0 && transaction.RouteID.ToInt32() > 0)))
                {
                    ViewBag.Message = "Vehicle No And Route or Trip No is required to save trip information.";
                    ViewBag.MessageType = "error";
                }
                transaction.CreatedBy = UserContext.UserID;
                var dt = await bALTransaction.SaveTripInfoAsync(transaction);
                if (dt != null && dt.Rows.Count > 0)
                {
                    ModelState.Clear();
                    ViewBag.Message = dt.Rows[0]["Message"].ToText() + (dt.Rows[0]["MessageType"].ToText() == "success" ? $" Route Name: { dt.Rows[0]["RouteName"].ToText() } Check Point Name: { dt.Rows[0]["CheckPointName"].ToText() } Arrived Time: { dt.Rows[0]["ArrivalTime"].ToText() } " : "");
                    ViewBag.MessageType = dt.Rows[0]["MessageType"].ToText();
                }
                else
                {
                    ViewBag.Message = "Error while saving trip info. Please contact sytem administrator.";
                    ViewBag.MessageType = "error";
                }
            }
            catch
            {
                ViewBag.Message = "Error while saving trip info. Please contact sytem administrator.";
                ViewBag.MessageType = "error";
            }
            await LoadVehicleNoAsync();
            await LoadRoutesAsync();
            return View();
        }

        private async Task LoadVehicleNoAsync()
        {
            List<Vehicle> vehicles = new List<Vehicle>();
            var dt = await bALVehicle.GetVehiclesAsync();
            vehicles.Add(new Vehicle()
            {
                VehicleID = 0,
                VehicleNo = "-- Select --",
                IsActive = true
            });
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    vehicles.Add(new Vehicle()
                    {
                        VehicleID = dr["VehicleID"].ToInt32(),
                        VehicleNo = dr["VehicleNo"].ToText(),
                        IsActive = dr["IsActive"].ToBoolean()
                    });
                }
            }
            ViewBag.Vehicles = vehicles.Where(x => x.IsActive == true).ToList();
        }

        private async Task LoadRoutesAsync()
        {
            List<Route> routes = new List<Route>();
            var dt = await bALRoute.GetRoutesAsync();
            routes.Add(new Route()
            {
                RouteID = 0,
                RouteName = "-- Select --",
                IsActive = true
            });
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    routes.Add(new Route()
                    {
                        RouteID = dr["RouteID"].ToInt32(),
                        RouteName = dr["RouteName"].ToText(),
                        IsActive = dr["IsActive"].ToBoolean()
                    });
                }
            }
            ViewBag.Routes = routes.Where(x => x.IsActive == true).ToList();
        }

        public async Task<IActionResult> DeleteTrip(Common.Transaction transaction)
        {
            dynamic model;
            try
            {
                transaction.CreatedBy = UserContext.UserID;
                await bALTransaction.DeleteTripAsync(transaction);
                model = new { MessageType = "success", Message = "Trip Information Deleted Successfully." };
            }
            catch
            {
                model = new { MessageType = "error", Message = "Error while deleting trip info. Please contact sytem administrator." };
            }
            return Json(model);
        }
    }
}