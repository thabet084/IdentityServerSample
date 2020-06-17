using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace AuthProject.Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login() => Challenge(new AuthenticationProperties { RedirectUri = "/" });

        public IActionResult AccessDenied()
        {
            return View();
        }
            
    }
}