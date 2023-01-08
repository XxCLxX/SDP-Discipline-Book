using asp_book.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace asp_book.Models
{
    public class Group
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public Faculty? Faculty { get; set; }
        public ICollection<GroupSubject>? GroupSubjects { get; set; }
        public ICollection<ApplicationUser>? Users { get; set; }
    }
}
