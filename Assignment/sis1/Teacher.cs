using System.Collections.Generic;

namespace SISApp
{
    internal class Teacher
    {
        public int TeacherId { get; set; }
        public string Name { get; set; }
        public List<Course> AssignedCourses { get; set; } = new List<Course>();

        public override string ToString()
        {
            return $"TeacherId: {TeacherId}, Name: {Name}";
        }
    }
}
