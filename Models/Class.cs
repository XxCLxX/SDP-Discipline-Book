using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace asp_book.Models
{
    public class Class
    {
        
        public int ClassId { get; set; }

        [Display(Name = "Day")]
        public string  DayofClass { get; set; }
        [Display(Name = "Time")]
        public string  TimeofClass { get; set; }
        [Display(Name = "Format")]
        public string  TypeofClass { get; set; }
        [Display(Name = "Building No.")]
        public string   BuildingofClass  { get; set; }
        [Display(Name = "Room No.")]
        public string   RoomofClass { get; set; }

    }
}
