using asp_book.Areas.Identity.Data;
using asp_book.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace asp_book.Controllers
{
    public class RoleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoleController(ApplicationDbContext context)
        {
            _context = context;
        }

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

        [Authorize(Policy = Constants.Policies.RequireAdmin)]
        //[Authorize(Roles = $"{Constants.Roles.Administrator},{Constants.Roles.Teacher}")]
        public IActionResult Admin()
        {
            return View();
        }

        [Authorize(Policy = Constants.Policies.RequireStudent)]
        /*public IActionResult Student()
        {
            return View();
        }*/

        // GET: Subjects
        public async Task<IActionResult> Student()
        {
            return View(await _context.Subjects.ToListAsync());
        }
    }
}
