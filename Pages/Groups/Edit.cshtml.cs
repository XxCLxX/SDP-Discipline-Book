using asp_book.Areas.Identity.Data;
using asp_book.Models;
using asp_book.Views.Groups;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace asp_book.Pages.Groups
{
    public class EditModel : FacultyNameViewModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

            PopulateFacultiesDropDownList(_context, Group.FacultyId);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groupToUpdate = await _context.Groups.FindAsync(id);

            if (await TryUpdateModelAsync<Group>(
                groupToUpdate,
                "group",
                g => g.GroupName, g => g.FacultyId))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            PopulateFacultiesDropDownList(_context, groupToUpdate.FacultyId);
            return Page();
        }
    }
}
