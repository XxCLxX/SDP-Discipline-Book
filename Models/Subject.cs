using asp_book.Areas.Identity.Data;
using Microsoft.CodeAnalysis.Operations;
using System.Web.Mvc;

namespace asp_book.Models
{
    public class Subject
    {
        public int SubjectId { get; set; }
        public string? SubjectName { get; set; }

        [AllowHtml]
        public string? Description { get; set; }

        [AllowHtml]
        public string? Literature { get; set; }

        public string? Year { get; set; }

        public string? Semester { get; set; } //enum

        public ICollection<GroupSubject>? GroupSubjects { get; set; } 

        public ICollection<ApplicationUser>? Users { get; set; }
    }
}
