using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SamayaPatra.Business;
using SamayaPatra.Common;
using SamayaPatra.Helper;

namespace SamayaPatra.Controllers
{
    public class RouteController : BaseController
    {
        readonly BALRoute bALRoute = new BALRoute();
        readonly BALCommon bALCommon = new BALCommon();
        public async Task<IActionResult> Index()
        {
            List<Route> routes = await GetRoutesAsync();
            return View(routes);
        }

        private async Task<List<Route>> GetRoutesAsync()
        {
            List<Route> routes = new List<Route>();
            var dt = await bALRoute.GetRoutesAsync();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    routes.Add(new Route()
                    {
                        RouteID = dr["RouteID"].ToInt32(),
                        RouteName = dr["RouteName"].ToText(),
                        TotalKM = dr["TotalKM"].ToDecimal(),
                        IsActive = dr["IsActive"].ToBoolean()
                    });
                }
            }

            return routes;
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Route route)
        {
            dynamic model;
            try
            {
                var routes = await GetRoutesAsync();
                if (route.CheckPoints.GroupBy(x => x.CheckPointID).Select(x => new { CheckPointID = x.Key, Count = x.Count() }).Where(x => x.Count > 1).Any())
                {
                    model = new { MessageType = "error", Message = "Same check point cannot be selected more than once in a single route." };
                }
                else if (routes.Where(x => x.RouteName == route.RouteName).Any())
                {
                    model = new { MessageType = "error", Message = "Route with same name already exits. Please save route with another name." };
                }
                else
                {
                    route.CreatedBy = UserContext.UserID;
                    await bALRoute.SaveOrUpdateRouteAsync(route);
                    model = new { MessageType = "success", Message = route.RouteID > 0 ? "Route Info Updated Successfully." : "Route Info Inserted Successfully." };
                    ModelState.Clear();
                }
            }
            catch (Exception)
            {
                model = new { MessageType = "error", Message = "Error while saving or updating route." };
            }
            return Json(model);
        }

        [HttpGet]
        public IActionResult Edit(int RouteID)
        {
            ViewBag.RouteID = RouteID;
            return View("Add");
        }

        [HttpPost]
        public async Task<IActionResult> RouteDetails(int RouteID)
        {
            Route route = new Route();
            var ds = await bALRoute.GetRouteDetailsAsync(RouteID);
            if (ds != null)
            {
                route.RouteID = ds.Tables[0].Rows[0]["RouteID"].ToInt32();
                route.RouteName = ds.Tables[0].Rows[0]["RouteName"].ToText();
                route.TotalKM = ds.Tables[0].Rows[0]["TotalKM"].ToDecimal();

                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    route.CheckPoints.Add(new RouteCheckPoint()
                    {
                        CheckPointID = dr["CheckPointID"].ToInt32(),
                        CheckPointOrder = dr["CheckPointOrder"].ToInt32(),
                        EstimatedKM = dr["EstimatedKM"].ToDecimal(),
                        EstimatedArrivalMinutes = dr["EstimatedArrivalMinutes"].ToInt32()
                    });
                }
            }
            return Json(route);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeRouteStatus(Route route)
        {
            dynamic model;
            try
            {
                route.CreatedBy = UserContext.UserID;
                await bALRoute.UpdateRouteStatus(route);
                model = new { MessageType = "success", Message = route.IsActive ? "Route Info Activated Successfully" : "Route Info Deactivated Successfully" };
            }
            catch (Exception)
            {
                model = new { MessageType = "error", Message = "Error while changing user status" };
            }
            return Json(model);
        }

        public async Task<IActionResult> LoadCheckPoints()
        {
            dynamic model;
            try
            {
                var selectOption = new SelectOption()
                {
                    TableName = "CheckPoints",
                    TextField = "CheckPointName",
                    ValueField = "CheckPointID",
                    OrderBy = "CheckPointName",
                    Condition = "IsActive=1"
                };
                model = new { Success = true, Data = await bALCommon.GetSelectOptions(selectOption) };
            }
            catch (Exception)
            {
                model = new { Success = false };
            }
            return Json(model);
        }
    }
}