using System;
using System.Collections.Generic;
using System.Linq;

namespace SISApp
{
    internal class SIS
    {
        public List<Student> Students = new List<Student>();
        public List<Course> Courses = new List<Course>();
        public List<Teacher> Teachers = new List<Teacher>();

        public void AddStudent(Student student)
        {
            if (Students.Any(s => s.StudentId == student.StudentId))
                throw new SISException("Student ID already exists.");
            Students.Add(student);
        }

        public void AddCourse(Course course)
        {
            if (Courses.Any(c => c.CourseId == course.CourseId))
                throw new SISException("Course ID already exists.");
            Courses.Add(course);
        }

        public void AddTeacher(Teacher teacher)
        {
            if (Teachers.Any(t => t.TeacherId == teacher.TeacherId))
                throw new SISException("Teacher ID already exists.");
            Teachers.Add(teacher);
        }

        public void AddEnrollment(Student student, Course course, DateTime enrollmentDate)
        {
            var enrollment = new Enrollment { Student = student, Course = course, EnrollmentDate = enrollmentDate };
            student.Enrollments.Add(enrollment);
            course.Enrollments.Add(enrollment);
        }

        public void AssignCourseToTeacher(Course course, Teacher teacher)
        {
            if (!teacher.AssignedCourses.Contains(course))
            {
                teacher.AssignedCourses.Add(course);
            }
        }

        public void AddPayment(Student student, double amount, DateTime date)
        {
            var payment = new Payment { Student = student, Amount = amount, PaymentDate = date };
            student.Payments.Add(payment);
        }

        public List<Enrollment> GetEnrollmentsForStudent(Student student)
        {
            return student.Enrollments;
        }

        public List<Course> GetCoursesForTeacher(Teacher teacher)
        {
            return teacher.AssignedCourses;
        }

        public List<Student> GetAllStudents()
        {
            return Students;
        }

        public Student GetStudentById(int id) => Students.FirstOrDefault(s => s.StudentId == id)
            ?? throw new SISException("Student not found.");

        public Course GetCourseById(int id) => Courses.FirstOrDefault(c => c.CourseId == id)
            ?? throw new SISException("Course not found.");

        public Teacher GetTeacherById(int id) => Teachers.FirstOrDefault(t => t.TeacherId == id)
            ?? throw new SISException("Teacher not found.");
    }
}
