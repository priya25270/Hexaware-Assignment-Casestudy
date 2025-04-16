using System.Collections.Generic;

namespace SISApp
{
    internal class Course
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

        public override string ToString()
        {
            return $"CourseId: {CourseId}, Title: {Title}";
        }
    }
}
