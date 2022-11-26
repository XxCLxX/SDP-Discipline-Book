namespace asp_book.Core
{
    public class Constants
    {
        public static class Roles
        {
            public const string Administrator = "Administrator";
            public const string Teacher = "Teacher";
            public const string Student = "Student";
        }

        public static class Policies
        {
            public const string RequireAdmin = "RequireAdmin";
            public const string RequireTeacher = "RequireTeacher";
        }
    }
}
