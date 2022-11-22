using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASPNET_MVC_NET6__Cookie_Auth_2.Controllers
{
    // [Authorize(Roles ="Admin,Manager")]tüm actionlarda oturum açık mı kontrol sadece admin ve manager rolünde olanlar
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
