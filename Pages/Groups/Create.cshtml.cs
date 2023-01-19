using asp_book.Areas.Identity.Data;
using asp_book.Models;
using asp_book.Views.Groups;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace asp_book.Pages.Groups
{
    public class CreateModel : FacultyNameViewModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            PopulateFacultiesDropDownList(_context);
            return Page();
        }

        [BindProperty]
        public Group Group { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var emptyGroup = new Group();

            if (await TryUpdateModelAsync<Group>(
             emptyGroup,
             "group",   
             s => s.GroupId, s => s.FacultyId, s => s.GroupName))
            {
                _context.Groups.Add(emptyGroup);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            PopulateFacultiesDropDownList(_context, emptyGroup.FacultyId);
            return Page();
        }
    }
}
