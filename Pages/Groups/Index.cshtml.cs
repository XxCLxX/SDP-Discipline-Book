using asp_book.Areas.Identity.Data;
using asp_book.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace asp_book.Views.Groups;

public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Group> Groups { get; set; }

    public async Task OnGetAsync()
    {
        Groups = await _context.Groups
            .Include(g => g.Faculty)
            .AsNoTracking()
            .ToListAsync();
    }
}

