using System;
using System.Collections.Generic;
using AdoConnectedDemo.Data;
using SIS_ADO.Dao;
using SIS_ADO.Data;
using SIS_ADO.Model;

namespace SIS_ADO
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            SisDaoImpl dao = new SisDaoImpl();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n===== STUDENT INFORMATION SYSTEM MENU =====");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. Get student by id");
                Console.WriteLine("3. View All Students");
                Console.WriteLine("4. Add Course");
                Console.WriteLine("5. View All Courses");
                Console.WriteLine("6. Add Teacher");
                Console.WriteLine("7. Get Teacher by ID");
                Console.WriteLine("8. Get All Teachers");
                Console.WriteLine("9. search enrollment by course");
                Console.WriteLine("10. teacher to course assign");
                Console.WriteLine("11. Record Payment");
                Console.WriteLine("12. View Student Payments");
                Console.WriteLine("13. Update Student");
                Console.WriteLine("14. Delete Student");
                Console.WriteLine("15. Enroll Student in Course");
                Console.WriteLine("16. Exit");


                Console.Write("Enter your choice: ");

                int choice = Convert.ToInt32(Console.ReadLine());

                try
                {
                    switch (choice)
                    {
                        case 1:
                            Student s = new Student();
                            Console.Write("First Name: ");
                            s.FirstName = Console.ReadLine();
                            Console.Write("Last Name: ");
                            s.LastName = Console.ReadLine();
                            Console.Write("DOB (yyyy-mm-dd): ");
                            s.DateOfBirth = DateTime.Parse(Console.ReadLine());
                            Console.Write("Email: ");
                            s.Email = Console.ReadLine();
                            Console.Write("Phone: ");
                            s.PhoneNumber = Console.ReadLine();
                            Console.Write("Outstanding Balance: ");
                            s.OutstandingBalance = decimal.Parse(Console.ReadLine());
                            int result = dao.AddStudent(s);
                            Console.WriteLine(result > 0 ? "Student added!" : "Failed to add student.");
                            break;

                        case 2:
                            Console.Write("Enter Student ID to search: ");
                            if (int.TryParse(Console.ReadLine(), out int studentId))
                            {
                                Student student = dao.GetStudentById(studentId);
                                if (student != null)
                                {
                                    Console.WriteLine("\nStudent Details");
                                    Console.WriteLine($"ID         : {student.StudentId}");
                                    Console.WriteLine($"Name       : {student.FirstName} {student.LastName}");
                                    Console.WriteLine($"DOB        : {student.DateOfBirth:yyyy-MM-dd}");
                                    Console.WriteLine($"Email      : {student.Email}");
                                    Console.WriteLine($"Phone No.  : {student.PhoneNumber}");
                                }
                                else
                                {
                                    Console.WriteLine("Student not found with the given ID.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid ID format. Please enter a valid number.");
                            }
                            break;

                        case 3:
                            var students = dao.GetAllStudents();
                            foreach (var student in students)
                                Console.WriteLine($"{student.StudentId}: {student.FirstName} {student.LastName}, Email: {student.Email}");
                            break;

                        case 4:
                            Course course = new Course();
                            Console.Write("Course Name: ");
                            course.CourseName = Console.ReadLine();
                            Console.Write("Course Code: ");
                            course.CourseCode = Console.ReadLine();
                            int cres = dao.AddCourse(course);
                            Console.WriteLine(cres > 0 ? "Course added!" : "Failed to add course.");
                            break;

                        case 5:
                            var courses = dao.GetAllCourses();
                            foreach (var c in courses)
                                Console.WriteLine($"{c.CourseId}: {c.CourseName} ({c.CourseCode}) - Teacher ID: {(c.TeacherId.HasValue ? c.TeacherId.ToString() : "None")}");
                            break;

                        case 6:
                            Teacher t = new Teacher();
                            Console.Write("Name: ");
                            t.Name = Console.ReadLine();
                            Console.Write("Email: ");
                            t.Email = Console.ReadLine();
                            Console.Write("Expertise: ");
                            t.Expertise = Console.ReadLine();
                            int tRes = dao.AddTeacher(t);
                            Console.WriteLine(tRes > 0 ? "Teacher added!" : "Failed to add teacher.");
                            break;

                        case 7:
                            Console.Write("Enter Teacher ID to retrieve: ");
                            if (int.TryParse(Console.ReadLine(), out int tid))
                            {
                                Teacher teacher = dao.GetTeacherById(tid);
                                if (teacher != null)
                                {
                                    Console.WriteLine($"ID: {teacher.TeacherId}, Name: {teacher.Name}, Email: {teacher.Email}, Expertise: {teacher.Expertise}");
                                }
                                else
                                {
                                    Console.WriteLine("Teacher not found.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid ID.");
                            }
                            break;

                        case 8:
                            List<Teacher> allTeachers = dao.GetAllTeachers();
                            if (allTeachers.Count == 0)
                            {
                                Console.WriteLine("No teachers available.");
                            }
                            else
                            {
                                foreach (var teacher in allTeachers)
                                {
                                    Console.WriteLine($"ID: {teacher.TeacherId}, Name: {teacher.Name}, Email: {teacher.Email}, Expertise: {teacher.Expertise}");
                                }
                            }
                            break;

                        case 9:
                            Console.Write("Enter Course Name: ");
                            string courseName = Console.ReadLine();
                            try
                            {
                                List<Enrollment> enrollments = dao.GetEnrollmentsByCourse(courseName);
                                if (enrollments.Count > 0)
                                {
                                    Console.WriteLine("Enrollments for Course: " + courseName);
                                    foreach (var e in enrollments)
                                    {
                                        Console.WriteLine($"Enrollment ID: {e.EnrollmentId}, Student ID: {e.StudentId}, Course ID: {e.CourseId}, Enrollment Date: {e.EnrollmentDate:d}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("No enrollments found for the specified course.");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error: " + ex.Message);
                            }
                            break;

                        case 10:
                            Console.Write("Enter Course ID: ");
                            int courseId = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Enter Teacher ID: ");
                            int teacherId = Convert.ToInt32(Console.ReadLine());
                            try
                            {
                                int rows = dao.AssignTeacherToCourse(courseId, teacherId);
                                Console.WriteLine(rows > 0 ? "Teacher assigned to course successfully." : "Assignment failed. Check Course ID and Teacher ID.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error: " + ex.Message);
                            }
                            break;

                        case 11:
                            Payment payment = new Payment();
                            Console.Write("Enter Student ID: ");
                            payment.StudentId = int.Parse(Console.ReadLine());
                            Console.Write("Enter Payment Amount: ");
                            payment.Amount = decimal.Parse(Console.ReadLine());
                            payment.PaymentDate = DateTime.Now;

                            int paymentResult = dao.AddPayment(payment);
                            Console.WriteLine(paymentResult > 0 ? "Payment recorded successfully!" : "Failed to record payment.");
                            break;

                        case 12:
                            Console.Write("Enter Student ID to view payments: ");
                            int sid = int.Parse(Console.ReadLine());
                            List<Payment> payments = dao.GetPaymentsByStudentId(sid);
                            if (payments.Count == 0)
                            {
                                Console.WriteLine("No payments found for this student.");
                            }
                            else
                            {
                                Console.WriteLine("Payments for Student ID " + sid + ":");
                                foreach (var p in payments)
                                {
                                    Console.WriteLine($"Payment ID: {p.PaymentId}, Amount: {p.Amount}, Date: {p.PaymentDate.ToShortDateString()}");
                                }
                            }
                            break;



                        case 13:
                            Console.WriteLine("Enter Student ID to update:");
                            int updateId = int.Parse(Console.ReadLine());
                            Student studentToUpdate = dao.GetStudentById(updateId);
                            if (studentToUpdate == null)
                            {
                                Console.WriteLine("Student not found.");
                            }
                            else
                            {
                                Console.WriteLine("Enter updated First Name:");
                                studentToUpdate.FirstName = Console.ReadLine();
                                Console.WriteLine("Enter updated Last Name:");
                                studentToUpdate.LastName = Console.ReadLine();
                                Console.WriteLine("Enter updated Date of Birth (yyyy-MM-dd):");
                                studentToUpdate.DateOfBirth = DateTime.Parse(Console.ReadLine());
                                Console.WriteLine("Enter updated Email:");
                                studentToUpdate.Email = Console.ReadLine();
                                Console.WriteLine("Enter updated Phone Number:");
                                studentToUpdate.PhoneNumber = Console.ReadLine();
                                bool isUpdated = dao.UpdateStudent(studentToUpdate);
                                Console.WriteLine(isUpdated ? "Student updated successfully." : "Update failed.");
                            }
                            break;

                        case 14:
                            Console.WriteLine("Enter Student ID to delete:");
                            int deleteId = int.Parse(Console.ReadLine());
                            Student studentToDelete = dao.GetStudentById(deleteId);
                            if (studentToDelete == null)
                            {
                                Console.WriteLine("Student not found.");
                            }
                            else
                            {
                                bool deleted = dao.DeleteStudent(deleteId);
                                Console.WriteLine(deleted ? "Student deleted successfully." : "Deletion failed.");
                            }
                            break;

                        case 15:
                            Console.Write("Student ID: ");
                            int stid = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Course ID: ");
                            int cid = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Enrollment Date (yyyy-mm-dd): ");
                            DateTime edate = DateTime.Parse(Console.ReadLine());
                            int enr = dao.EnrollStudent(stid, cid, edate);
                            Console.WriteLine(enr > 0 ? "Enrollment successful!" : "Enrollment failed.");
                            break;

                        case 16:
                            exit = true;
                            break;

                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            Console.WriteLine("Exiting the SIS. Goodbye!");
        }
    }
}

