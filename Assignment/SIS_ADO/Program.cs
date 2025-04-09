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
                Console.WriteLine("2. View All Students");
                Console.WriteLine("3. Enroll Student in Course");
                Console.WriteLine("4. Add Course");
                Console.WriteLine("5. View All Courses");
                Console.WriteLine("6. Add Teacher");
                Console.WriteLine("8. Record Payment");
                Console.WriteLine("9. View Student Payments");
                Console.WriteLine("0. Exit");
                Console.WriteLine("11. Get student by id");
                Console.WriteLine("13. Update Student");
                Console.WriteLine("14. Delete Student");


                Console.Write("Enter your choice: ");

                int choice = Convert.ToInt32(Console.ReadLine());

                try
                {
                    switch (choice)
                    {
                        case 1:
                            Student s = new Student();
                            Console.WriteLine("Student id");
                            int StudentId = Convert.ToInt32(Console.ReadLine());
                            Console.Write("First Name: ");
                            s.FirstName = Console.ReadLine();
                            Console.Write("Last Name: ");
                            s.LastName = Console.ReadLine();
                            Console.Write("DOB (yyyy-mm-dd): ");
                            DateTime DateOfBirth = DateTime.Parse(Console.ReadLine());
                            Console.Write("Email: ");
                            s.Email = Console.ReadLine();
                            Console.Write("Phone: ");
                            s.PhoneNumber = Console.ReadLine();

                            int result = dao.AddStudent(s);
                            Console.WriteLine(result > 0 ? "Student added!" : "Failed to add student.");
                            break;

                        case 2:
                            var students = dao.GetAllStudents();
                            foreach (var student in students)
                                Console.WriteLine($"{student.StudentId}: {student.FirstName} {student.LastName}, Email: {student.Email}");
                            break;

                        case 3:
                            Console.Write("Student ID: ");
                            int sid = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Course ID: ");
                            int cid = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Enrollment Date (yyyy-mm-dd): ");
                            DateTime edate = DateTime.Parse(Console.ReadLine());

                            int enr = dao.EnrollStudent(sid, cid, edate);
                            Console.WriteLine(enr > 0 ? "Enrollment successful!" : "Enrollment failed.");
                            break;

                        case 4:
                            Course course = new Course();
                            Console.Write("Course Name: ");
                            course.CourseName = Console.ReadLine();
                            Console.Write("Course Code: ");
                            course.CourseCode = Console.ReadLine();
                            Console.Write("Teacher ID (or leave blank for none): ");
                            string tidInput = Console.ReadLine();
                            course.TeacherId = string.IsNullOrWhiteSpace(tidInput) ? null : int.Parse(tidInput);

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

                        
                        case 8:
                            Payment p = new Payment();
                            Console.Write("Student ID: ");
                            p.StudentId = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Amount: ");
                            p.Amount = Convert.ToDecimal(Console.ReadLine());
                            Console.Write("Payment Date (yyyy-mm-dd): ");
                            p.PaymentDate = DateTime.Parse(Console.ReadLine());

                            int payRes = dao.AddPayment(p);
                            Console.WriteLine(payRes > 0 ? "Payment recorded!" : "Payment failed.");
                            break;

                        case 9:
                            Console.Write("Enter Student ID: ");
                            int paySid = Convert.ToInt32(Console.ReadLine());
                            var payments = dao.GetPaymentsByStudent(paySid);

                            foreach (var pay in payments)
                                Console.WriteLine($"Payment ID: {pay.PaymentId}, Amount: {pay.Amount}, Date: {pay.PaymentDate.ToShortDateString()}");
                            break;

                        case 0:
                            exit = true;
                            break;
                        case 11:
                            Console.Write("Enter Student ID to search: ");
                            if (int.TryParse(Console.ReadLine(), out int studentId))
                            {
                                Student student = dao.GetStudentById(studentId);
                                if (student != null)
                                {
                                    Console.WriteLine("\n--- Student Details ---");
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



                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"⚠️ Error: {ex.Message}");
                }
            }

            Console.WriteLine("Exiting the SIS. Goodbye!");
        }
    }
}
