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
    public class UserController : BaseController
    {
        readonly BALUser bALUser = new BALUser();
        readonly BALVehicle bALVehicle = new BALVehicle();
        public async Task<IActionResult> Index()
        {
            List<User> users = new List<User>();
            var dt = await bALUser.GetUserAsync();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    users.Add(new User()
                    {
                        UserID = dr["UserID"].ToInt32(),
                        FullName = dr["FullName"].ToText(),
                        UserName = dr["UserName"].ToText(),
                        UserRoleID = dr["UserRoleID"].ToInt32(),
                        UserRole = dr["UserRole"].ToText(),
                        Contact = dr["Contact"].ToText(),
                        Email = dr["Email"].ToText(),
                        Address = dr["Address"].ToText(),
                        IsActive = dr["IsActive"].ToBoolean(),
                        VehicleNo = dr["VehicleNo"].ToText()
                    });
                }
            }
            return View(users);
        }

        public async Task<IActionResult> Add()
        {
            await LoadUserRoles();
            await LoadVehicleNoAsync();
            return View();
        }

        private async Task LoadUserRoles()
        {
            var dt = await bALUser.GetUserRoleAsync();
            List<UserRole> userRoles = new List<UserRole>();
            foreach (DataRow dr in dt.Rows)
            {
                userRoles.Add(new UserRole()
                {
                    UserRoleID = dr["UserRoleID"].ToInt32(),
                    UserRoles = dr["UserRole"].ToText()
                });
            }
            ViewBag.UserRoles = userRoles;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(User user)
        {
            try
            {
                user.CreatedBy = UserContext.UserID;
                user.Password = user.Password.ToSHA512();
                await bALUser.SaveOrUpdateUserAsync(user);
                ModelState.Clear();
                ViewBag.Message = user.UserID > 0 ? "User Info Updated Successfully." : "User Info Added Successfully.";
                ViewBag.MessageType = "success";
            }
            catch
            {
                ViewBag.Message = "Error while saving user info. Please contact sytem administrator.";
                ViewBag.MessageType = "error";
            }
            await LoadUserRoles();
            await LoadVehicleNoAsync();
            return View();
        }

        public async Task<IActionResult> Edit(int UserID)
        {
            List<User> users = new List<User>();
            var dt = await bALUser.GetUserAsync(UserID);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    users.Add(new User()
                    {
                        UserID = dr["UserID"].ToInt32(),
                        FullName = dr["FullName"].ToText(),
                        UserName = dr["UserName"].ToText(),
                        UserRoleID = dr["UserRoleID"].ToInt32(),
                        UserRole = dr["UserRole"].ToText(),
                        Contact = dr["Contact"].ToText(),
                        Email = dr["Email"].ToText(),
                        Address = dr["Address"].ToText(),
                        IsActive = dr["IsActive"].ToBoolean(),
                        VehicleID = dr["VehicleID"].ToInt32()
                    });
                }
            }
            await LoadUserRoles();
            await LoadVehicleNoAsync();
            return View("Add", users.FirstOrDefault());
        }

        [HttpPost]
        public async Task<IActionResult> ChangeUserStatus(User user)
        {
            dynamic model;
            try
            {
                user.CreatedBy = UserContext.UserID;
                await bALUser.UpdateUserStatusAsync(user);
                model = new { MessageType = "success", Message = user.IsActive ? "User Activated Successfully" : "User Deactivated Successfully" };
            }
            catch (Exception)
            {
                model = new { MessageType = "error", Message = "Error while changing user status" };
            }
            return new JsonResult(model);
        }
    }
}