using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace asp_book.Models
{
    public class EditEntryViewModel
    {
        public IEnumerable<SelectListItem> GroupItems { get; set; }
        public IEnumerable<SelectListItem> LessonItems { get; set; }
        public IEnumerable<SelectListItem> TeacherItems { get; set; }

        public string Group { get; set; }
        public string Lesson { get; set; }
        public string Teacher { get; set; }

    }
}