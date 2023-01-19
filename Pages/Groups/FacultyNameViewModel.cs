using asp_book.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace asp_book.Views.Groups
{
    public class FacultyNameViewModel : PageModel
    {
        public SelectList FacultyNameSL { get; set; }

        public void PopulateFacultiesDropDownList(ApplicationDbContext _context, object selectedFaculty=null)
        {
            var facultiesQuery = from f in _context.Faculties
                                 orderby f.FacultyName
                                 select f;

            FacultyNameSL = new SelectList(facultiesQuery.AsNoTracking(),
                "FacultyId", "FacultyName", selectedFaculty);
        }
    }
}
