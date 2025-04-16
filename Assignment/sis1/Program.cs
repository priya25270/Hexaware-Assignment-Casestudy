using System;

namespace SISApp
{
    class Program
    {
        static void Main(string[] args)
        {
            SIS sis = new SIS();
            UserInterface ui = new UserInterface();
            bool exit = false;

            // Sample Data Setup
            var student1 = new Student { StudentId = 1, Name = "Alice" };
            var student2 = new Student { StudentId = 2, Name = "Bob" };
            var student3 = new Student { StudentId = 3, Name = "Charlie" };

            var course1 = new Course { CourseId = 101, Title = "Mathematics" };
            var course2 = new Course { CourseId = 102, Title = "Physics" };

            var teacher1 = new Teacher { TeacherId = 201, Name = "Prof. John" };
            var teacher2 = new Teacher { TeacherId = 202, Name = "Prof. Emma" };

            try
            {
                sis.AddStudent(student1);
                sis.AddStudent(student2);
                sis.AddStudent(student3);

                sis.AddCourse(course1);
                sis.AddCourse(course2);

                sis.AddTeacher(teacher1);
                sis.AddTeacher(teacher2);

                sis.AddEnrollment(student1, course1, new DateTime(2025, 4, 1));
                sis.AddEnrollment(student2, course2, new DateTime(2025, 4, 2));

                sis.AssignCourseToTeacher(course1, teacher1);
                sis.AssignCourseToTeacher(course2, teacher2);

                sis.AddPayment(student1, 500, new DateTime(2025, 4, 3));
                sis.AddPayment(student2, 300, new DateTime(2025, 4, 4));
            }
            catch (SISException ex)
            {
                Console.WriteLine("Setup Error: " + ex.Message);
            }

            while (!exit)
            {
                Console.WriteLine("\nStudent Information System");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. Add Course");
                Console.WriteLine("3. Add Teacher");
                Console.WriteLine("4. Enroll Student to Course");
                Console.WriteLine("5. Assign Course to Teacher");
                Console.WriteLine("6. Make Payment");
                Console.WriteLine("7. View Enrollments of Student");
                Console.WriteLine("8. View Courses Assigned to Teacher");
                Console.WriteLine("9. Exit");
                Console.WriteLine("10. View All Students");

                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            var student = new Student
                            {
                                StudentId = ui.GetIntInput("Enter Student ID:"),
                                Name = ui.GetStringInput("Enter Student Name:")
                            };
                            sis.AddStudent(student);
                            Console.WriteLine("Student added.");
                            break;

                        case "2":
                            var course = new Course
                            {
                                CourseId = ui.GetIntInput("Enter Course ID:"),
                                Title = ui.GetStringInput("Enter Course Title:")
                            };
                            sis.AddCourse(course);
                            Console.WriteLine("Course added.");
                            break;

                        case "3":
                            var teacher = new Teacher
                            {
                                TeacherId = ui.GetIntInput("Enter Teacher ID:"),
                                Name = ui.GetStringInput("Enter Teacher Name:")
                            };
                            sis.AddTeacher(teacher);
                            Console.WriteLine("Teacher added.");
                            break;

                        case "4":
                            var studentId = ui.GetIntInput("Enter Student ID:");
                            var courseId = ui.GetIntInput("Enter Course ID:");
                            var date = ui.GetDateInput("Enter Enrollment Date (YYYY-MM-DD):");
                            sis.AddEnrollment(sis.GetStudentById(studentId), sis.GetCourseById(courseId), date);
                            Console.WriteLine("Enrollment successful.");
                            break;

                        case "5":
                            int cid = ui.GetIntInput("Enter Course ID:");
                            int tid = ui.GetIntInput("Enter Teacher ID:");
                            sis.AssignCourseToTeacher(sis.GetCourseById(cid), sis.GetTeacherById(tid));
                            Console.WriteLine("Course assigned to teacher.");
                            break;

                        case "6":
                            int sid = ui.GetIntInput("Enter Student ID:");
                            double amount = ui.GetDoubleInput("Enter Payment Amount:");
                            var payDate = ui.GetDateInput("Enter Payment Date:");
                            sis.AddPayment(sis.GetStudentById(sid), amount, payDate);
                            Console.WriteLine("Payment recorded.");
                            break;

                        case "7":
                            int sId = ui.GetIntInput("Enter Student ID:");
                            var enrollments = sis.GetEnrollmentsForStudent(sis.GetStudentById(sId));
                            Console.WriteLine("\nEnrollments:");
                            enrollments.ForEach(e => Console.WriteLine(e));
                            break;

                        case "8":
                            int tId = ui.GetIntInput("Enter Teacher ID:");
                            var courses = sis.GetCoursesForTeacher(sis.GetTeacherById(tId));
                            Console.WriteLine("\nAssigned Courses:");
                            courses.ForEach(c => Console.WriteLine(c));
                            break;

                        case "9":
                            exit = true;
                            break;

                        case "10":
                            var allStudents = sis.GetAllStudents();
                            Console.WriteLine("\nAll Students:");
                            allStudents.ForEach(s => Console.WriteLine(s + "\n"));
                            break;

                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                }
                catch (SISException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input format.");
                }
            }
        }
    }
}
