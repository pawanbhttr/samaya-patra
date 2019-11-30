using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SamayaPatra.Common;
using SamayaPatra.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SamayaPatra.Controllers
{
    public class BaseController : Controller
    {
        public User UserContext { get; private set; }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
                context.HttpContext.ForbidAsync();
            SetUserContext();
            base.OnActionExecuting(context);
        }

        void SetUserContext()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var user = new User()
            {
                UserID = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value.ToInt32(),
                UserName = claimsIdentity.FindFirst("UserName").Value.ToText(),
                FullName = claimsIdentity.FindFirst(ClaimTypes.Name).Value.ToText(),
                UserRoleID = claimsIdentity.FindFirst("UserRoleID").Value.ToInt32(),
                Contact = claimsIdentity.FindFirst(ClaimTypes.MobilePhone).Value.ToText(),
                Email = claimsIdentity.FindFirst(ClaimTypes.Email).Value.ToText(),
                Address = claimsIdentity.FindFirst(ClaimTypes.StreetAddress).Value.ToText(),
                UserRole = claimsIdentity.FindFirst("UserRole").Value.ToText()
            };
            UserContext = user;
        }
    }
}
