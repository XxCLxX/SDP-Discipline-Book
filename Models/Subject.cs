using asp_book.Areas.Identity.Data;
using Microsoft.CodeAnalysis.Operations;

namespace asp_book.Models
{
    public class Subject
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public string Description { get; set; }
        public string Literature { get; set; }
        public string Year { get; set; }
        public string Semester { get; set; } //enum
        public ICollection<GroupSubject> GroupSubjects { get; set; } 
        public ICollection<ApplicationUser> Users { get; set; }
    }
}
