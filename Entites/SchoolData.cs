using asp_book.Entities;
using System.Collections.Generic;

namespace asp_book.Entities
{
    public class SchoolData
    {
        public List<ActivityData> Activities { get; set; }
        public List<string> Lessons { get; set; }
        public List<string> Groups { get; set; }
        public List<string> Rooms { get; set; }
        public List<string> Teachers { get; set; }

        public SchoolData()
        {
            Activities = new List<ActivityData>();
            Lessons = new List<string>();
            Groups = new List<string>();
            Rooms = new List<string>();
            Teachers = new List<string>();
        }

    }
}

