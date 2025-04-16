using System.Collections.Generic;

namespace SISApp
{
    internal class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public List<Payment> Payments { get; set; } = new List<Payment>();

        public override string ToString()
        {
            return $"StudentId: {StudentId}, Name: {Name}";
        }
    }
}