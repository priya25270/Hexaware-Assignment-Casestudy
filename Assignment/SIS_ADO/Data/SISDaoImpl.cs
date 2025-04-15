using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SIS_ADO.Model;
using AdoConnectedDemo.Data;
using SIS_ADO.Data;

namespace SIS_ADO.Dao
{
    public class SisDaoImpl : SISDao
    {
        private SqlConnection con;
        private SqlCommand cmd;

        public int AddStudent(Student student)
        {
            string query = "INSERT INTO Students (FirstName, LastName, DateOfBirth, Email, PhoneNumber, OutStandingBalance) " +
                           "VALUES (@FirstName, @LastName, @DOB, @Email, @Phone, @OutstandingBalance)";

            using (con = DBUtility.GetConnection())
            {
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
                cmd.Parameters.AddWithValue("@LastName", student.LastName);

                if (student.DateOfBirth >= new DateTime(1753, 1, 1))
                {
                    cmd.Parameters.AddWithValue("@DOB", student.DateOfBirth);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@DOB", DBNull.Value);
                }

                cmd.Parameters.AddWithValue("@Email", student.Email);
                cmd.Parameters.AddWithValue("@Phone", student.PhoneNumber);
                cmd.Parameters.AddWithValue("@OutStandingBalance", student.OutstandingBalance);



                return cmd.ExecuteNonQuery();
            }
        }

        public Student GetStudentById(int id)
        {
            Student student = null;
            string query = "SELECT * FROM Students WHERE StudentId = @Id";
            using (con = DBUtility.GetConnection())
            {
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    student = new Student
                    {
                        StudentId = (int)reader["StudentId"],
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        DateOfBirth = reader["DateOfBirth"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["DateOfBirth"]),
                        Email = reader["Email"].ToString(),
                        PhoneNumber = reader["PhoneNumber"].ToString(),
                       
                    };
                }
            }
            return student;
        }



        public List<Student> GetAllStudents()
        {
            List<Student> students = new List<Student>();
            string query = "SELECT * FROM Students";

            using (con = DBUtility.GetConnection())
            {
                cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    students.Add(new Student
                    {
                        StudentId = (int)reader["StudentId"],
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        DateOfBirth = reader["DateOfBirth"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["DateOfBirth"]),
                        Email = reader["Email"].ToString(),
                        PhoneNumber = reader["PhoneNumber"].ToString(),
                        
                    });
                }
            }
            return students;
        }


        public int AddCourse(Course course)
        {
            string query = "INSERT INTO Courses ( CourseName, CourseCode) " +
                "VALUES (@Name, @Code)";
            using (con = DBUtility.GetConnection())
            {
                cmd = new SqlCommand(query, con);
              
                cmd.Parameters.AddWithValue("@Name", course.CourseName);
                cmd.Parameters.AddWithValue("@Code", course.CourseCode);
                return cmd.ExecuteNonQuery();
            }

        }

        

        public List<Course> GetAllCourses()
        {
            List<Course> list = new List<Course>();
            string query = "SELECT * FROM Courses";
            using (con = DBUtility.GetConnection())
            {
                cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Course
                    {
                        CourseId = (int)reader["CourseId"],
                        CourseName = reader["CourseName"].ToString(),
                        CourseCode = reader["CourseCode"].ToString(),
                        TeacherId = reader["TeacherId"] == DBNull.Value ? null : (int?)reader["TeacherId"]
                    });
                }
            }
            return list;
        }

        public int AddTeacher(Teacher teacher)
        {
            string query = "INSERT INTO Teachers (Name, Email, Expertise) VALUES (@Name, @Email, @Expertise)";
            using (con = DBUtility.GetConnection())
            {
                cmd = new SqlCommand(query, con);
               
                cmd.Parameters.AddWithValue("@Name", teacher.Name);
                cmd.Parameters.AddWithValue("@Email", teacher.Email);
                cmd.Parameters.AddWithValue("@Expertise", teacher.Expertise);
                return cmd.ExecuteNonQuery();
            }
        }

        public Teacher GetTeacherById(int teacherId)
        {
            Teacher teacher = null;
            string query = "SELECT * FROM Teachers WHERE TeacherId = @Id";
            using (con = DBUtility.GetConnection())
            {
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", teacherId);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    teacher = new Teacher
                    {
                        TeacherId = (int)reader["TeacherId"],
                        Name = reader["Name"].ToString(),
                        Email = reader["Email"].ToString(),
                        Expertise = reader["Expertise"].ToString()
                    };
                }
            }
            return teacher;
        }

        public List<Teacher> GetAllTeachers()
        {
            List<Teacher> list = new List<Teacher>();
            string query = "SELECT * FROM Teachers";
            using (con = DBUtility.GetConnection())
            {
                cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Teacher
                    {
                        TeacherId = (int)reader["TeacherId"],
                        Name = reader["Name"].ToString(),
                        Email = reader["Email"].ToString(),
                        Expertise = reader["Expertise"].ToString()
                    });
                }
            }
            return list;
        }

        public int EnrollStudent(int studentId, int courseId, DateTime enrollmentDate)
        {
            string query = "INSERT INTO Enrollments (StudentId, CourseId, EnrollmentDate) VALUES (@SID, @CID, @Date)";
            using (con = DBUtility.GetConnection())
            {
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@SID", studentId);
                cmd.Parameters.AddWithValue("@CID", courseId);
                cmd.Parameters.AddWithValue("@Date", enrollmentDate);
                return cmd.ExecuteNonQuery();
            }
        }



        public List<Enrollment> GetEnrollmentsByCourse(string courseName)
        {
            List<Enrollment> list = new List<Enrollment>();
            string query = @"SELECT e.*, s.FirstName, s.LastName, c.CourseName
                             FROM Enrollments e
                             JOIN Students s ON s.StudentId = e.StudentId
                             JOIN Courses c ON c.CourseId = e.CourseId
                             WHERE c.CourseName = @Name";
            using (con = DBUtility.GetConnection())
            {
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", courseName);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Enrollment
                    {
                        EnrollmentId = (int)reader["EnrollmentId"],
                        StudentId = (int)reader["StudentId"],
                        CourseId = (int)reader["CourseId"],
                        EnrollmentDate = (DateTime)reader["EnrollmentDate"]
                    });
                }
            }
            return list;
        }

        public int AddPayment(Payment payment)
        {
            string query = "INSERT INTO Payments (StudentId, Amount, PaymentDate) VALUES (@SID, @Amount, @Date)";
            using (con = DBUtility.GetConnection())
            {
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@SID", payment.StudentId);
                cmd.Parameters.AddWithValue("@Amount", payment.Amount);
                cmd.Parameters.AddWithValue("@Date", payment.PaymentDate);
                return cmd.ExecuteNonQuery();
            }
        }



        public List<Payment> GetPaymentsByStudentId(int studentId)
        {
            List<Payment> list = new List<Payment>();
            string query = "SELECT * FROM Payments WHERE StudentId = @SID";
            using (con = DBUtility.GetConnection())
            {
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@SID", studentId);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Payment
                    {
                        PaymentId = (int)reader["PaymentId"],
                        StudentId = (int)reader["StudentId"],
                        Amount = (decimal)reader["Amount"],
                        PaymentDate = (DateTime)reader["PaymentDate"]
                    });
                }
            }
            return list;
        }
        public int AssignTeacherToCourse(int courseId, int teacherId)
        {
            string query = "UPDATE Courses SET TeacherId = @TeacherId WHERE CourseId = @CourseId";
            using (con = DBUtility.GetConnection())
            {
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@TeacherId", teacherId);
                cmd.Parameters.AddWithValue("@CourseId", courseId);
                return cmd.ExecuteNonQuery(); 
            }
        }


        

        public bool UpdateStudent(Student student)
        {
            string query = "UPDATE Students SET FirstName = @FirstName, LastName = @LastName, " +
                           "DateOfBirth = @DOB, Email = @Email, PhoneNumber = @Phone " +
                           "WHERE StudentId = @Id";
            using (con = DBUtility.GetConnection())
            {
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
                cmd.Parameters.AddWithValue("@LastName", student.LastName);
                cmd.Parameters.AddWithValue("@DOB", student.DateOfBirth);
                cmd.Parameters.AddWithValue("@Email", student.Email);
                cmd.Parameters.AddWithValue("@Phone", student.PhoneNumber);
                cmd.Parameters.AddWithValue("@Id", student.StudentId);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }


        public bool DeleteStudent(int studentId)
        {
            string query = "DELETE FROM Students WHERE StudentId = @Id";
            using (con = DBUtility.GetConnection())
            {
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", studentId);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }


        bool SISDao.UpdateCourseInstructor(string courseCode, int teacherId)
        {
            return UpdateCourseInstructor(courseCode, teacherId);
        }

        public List<Enrollment> GetEnrollmentsByStudent(int studentId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateOutstandingBalance(int studentId, decimal paymentAmount)
        {
            throw new NotImplementedException();
        }

        public Course GetCourseByCode(string courseCode)
        {
            throw new NotImplementedException();
        }

        internal bool UpdateCourseInstructor(string? courseCode, int teacherId)
        {
            throw new NotImplementedException();
        }

        public List<Payment> GetPaymentsByStudent(int studentId)
        {
            throw new NotImplementedException();
        }
    }
}
