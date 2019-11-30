using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SamayaPatra.Business;
using SamayaPatra.Common;

namespace SamayaPatra.Controllers
{
    public class CheckPointController : BaseController
    {
        BALCheckPoint bALCheckPoint = new BALCheckPoint();
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetCheckPoint()
        {
            var dt = await bALCheckPoint.GetCheckPoint();
            return Json(dt);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(CheckPoint checkPoint)
        {
            checkPoint.CreatedBy = UserContext.UserID;
            await bALCheckPoint.SaveOrUpdateCheckPoint(checkPoint);
            ModelState.Clear();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ChangeCheckPointStatus(CheckPoint checkPoint)
        {
            dynamic model;
            try
            {
                checkPoint.CreatedBy = UserContext.UserID;
                await bALCheckPoint.UpdateCheckPointStatus(checkPoint);
                model = new { MessageType = "success", Message = checkPoint.IsActive ? "Check Point Activated Successfully" : "Check Point Deactivated Successfully" };
            }
            catch (Exception)
            {
                model = new { MessageType = "error", Message = "Error while changing check point status" };
            }
            return new JsonResult(model);
        }
    }
}