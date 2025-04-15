using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIS_ADO.Model;

namespace SIS_ADO.Data
{
    internal interface SISDao
    {
        int AddStudent(Student student);
        Student GetStudentById(int id);
        List<Student> GetAllStudents();
        bool UpdateStudent(Student student);

        
        int AddCourse(Course course);
        Course GetCourseByCode(string courseCode);
        List<Course> GetAllCourses();
        bool UpdateCourseInstructor(string courseCode, int teacherId);

        
        int EnrollStudent(int studentId, int courseId, DateTime enrollmentDate);
        List<Enrollment> GetEnrollmentsByCourse(string courseName);
        List<Enrollment> GetEnrollmentsByStudent(int studentId);

       
        int AddTeacher(Teacher teacher);
        Teacher GetTeacherById(int teacherId);
        List<Teacher> GetAllTeachers();

        
        int AddPayment(Payment payment);
        List<Payment> GetPaymentsByStudent(int studentId);
        bool UpdateOutstandingBalance(int studentId, decimal paymentAmount);
    }
}
