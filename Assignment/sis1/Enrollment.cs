using System;

namespace SISApp
{
    internal class Enrollment
    {
        public Student Student { get; set; }
        public Course Course { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public override string ToString()
        {
            return $"Student: {Student.Name}, Course: {Course.Title}, Date: {EnrollmentDate.ToShortDateString()}";
        }
    }
}