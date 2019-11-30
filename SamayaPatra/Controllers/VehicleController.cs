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
    public class VehicleController : BaseController
    {
        BALVehicle bALVehicle = new BALVehicle();
        public async Task<IActionResult> Index()
        {
            List<Vehicle> vehicles = new List<Vehicle>();
            var dt = await bALVehicle.GetVehiclesAsync();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    vehicles.Add(new Vehicle()
                    {
                        VehicleID = dr["VehicleID"].ToInt32(),
                        VehicleNo = dr["VehicleNo"].ToText(),
                        ModelNo = dr["ModelNo"].ToText(),
                        Color = dr["Color"].ToText(),
                        PassengerCapacity = dr["PassengerCapacity"].ToInt32(),
                        OwnerName = dr["OwnerName"].ToText(),
                        OwnerContact = dr["OwnerContact"].ToText(),
                        DriverName = dr["DriverName"].ToText(),
                        DriverContact = dr["DriverContact"].ToText(),
                        IsActive = dr["IsActive"].ToBoolean()
                    });
                }
            }
            return View(vehicles);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Vehicle vehicle)
        {
            try
            {
                vehicle.CreatedBy = UserContext.UserID;
                await bALVehicle.SaveOrUpdateVehicle(vehicle);
                ModelState.Clear();
                ViewBag.Message = vehicle.VehicleID > 0 ? "Vehicle Info Updated Successfully." : "Vehicle Info Added Successfully.";
                ViewBag.MessageType = "success";
            }
            catch
            {
                ViewBag.Message = "Error while saving vehicle info. Please contact sytem administrator.";
                ViewBag.MessageType = "error";
            }
            return View();
        }

        public async Task<IActionResult> Edit(int VehicleID)
        {
            List<Vehicle> vehicles = new List<Vehicle>();
            var dt = await bALVehicle.GetVehiclesAsync(VehicleID);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    vehicles.Add(new Vehicle()
                    {
                        VehicleID = dr["VehicleID"].ToInt32(),
                        VehicleNo = dr["VehicleNo"].ToText(),
                        ModelNo = dr["ModelNo"].ToText(),
                        Color = dr["Color"].ToText(),
                        PassengerCapacity = dr["PassengerCapacity"].ToInt32(),
                        CubicCapacity = dr["CubicCapacity"].ToDecimal(),
                        PurchasedDate = dr["PurchasedDate"].ToDateTime(),
                        OwnerName = dr["OwnerName"].ToText(),
                        OwnerContact = dr["OwnerContact"].ToText(),
                        OwnerAddress = dr["OwnerAddress"].ToText(),
                        DriverName = dr["DriverName"].ToText(),
                        DriverContact = dr["DriverContact"].ToText(),
                        DriverAdddress = dr["DriverAdddress"].ToText(),
                        IsActive = dr["IsActive"].ToBoolean()
                    });
                }
            }
            return View("Add", vehicles.FirstOrDefault());
        }

        [HttpPost]
        public async Task<IActionResult> ChangeVehicleStatus(Vehicle vehicle)
        {
            dynamic model;
            try
            {
                vehicle.CreatedBy = UserContext.UserID;
                await bALVehicle.UpdateVehicleStatus(vehicle);
                model = new { MessageType = "success", Message = vehicle.IsActive ? "Vehicle Activated Successfully" : "Vehicle Deactivated Successfully" };
            }
            catch (Exception)
            {
                model = new { MessageType = "error", Message = "Error while changing vehicle status. Please contact sytem administrator." };
            }
            return new JsonResult(model);
        }
    }
}