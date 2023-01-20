namespace asp_book.Entities
{
    public class ActivityData
    {
        public string Lesson { get; set; }
        public string DayofClass { get; set; }
        public string BuildingofClass { get; set; }
        public int Slot { get; set; }
        public string Teacher { get; set; }
        public string Group { get; set; }

        public ActivityData() { }

        public ActivityData(string lesson, int slot, string day, string building, string group,string teacher)
        {
            this.Lesson = lesson;
            this.DayofClass = day;
            this.BuildingofClass = building;
            this.Slot = slot;
            this.Group = group;
            this.Teacher = teacher;
        }
    }
}