using asp_book.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace asp_book.Controllers
{
    public class RoleController : Controller
    {
        //[Authorize(Policy = "EmployeeOnly")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Policy = Constants.Policies.RequireTeacher)]
        public IActionResult Teacher()
        {
            return View();
        }

        //[Authorize(Policy = "RequireAdmin")]
        [Authorize(Roles = $"{Constants.Roles.Administrator},{Constants.Roles.Teacher}")]
        public IActionResult Admin()
        {
            return View();
        }
    }
}
