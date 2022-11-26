using asp_book.Areas.Identity.Data;

namespace asp_book.Models
{
    public class Faculty
    {
        public int FacultyId { get; set; }
        public string FacultyName { get; set; }
        public ICollection<Group> Groups { get; set; }
        public ICollection<ApplicationUser> Users { get; set; }
    }
}
