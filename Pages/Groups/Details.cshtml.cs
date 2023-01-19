using asp_book.Areas.Identity.Data;
using asp_book.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace asp_book.Pages.Groups
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Group Group { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Group = await _context.Groups
                 .Include(g => g.Faculty).FirstOrDefaultAsync(m => m.GroupId == id);

            if (Group == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}