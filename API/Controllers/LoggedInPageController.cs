using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence;

namespace API.Controllers
{
    public class LoggedInPageController : BaseApiController
    {
        private readonly DataContext _context;

        public LoggedInPageController(DataContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public async void LoggedInPage()
        {
            Console.WriteLine("User's logged in Page");
        }
    }
}
