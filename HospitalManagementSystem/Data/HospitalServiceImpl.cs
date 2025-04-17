//6. Define HospitalServiceImpl class and implement all  the methods  IHospitalServiceImpl . 
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using HospitalManagementSystem.entity;
using HospitalManagementSystem.Data;
using HospitalManagementSystem.exception;

namespace HospitalManagementSystem.Data;
internal class HospitalServiceImpl : IHospitalService
{
    SqlConnection con = null;
    SqlCommand command = null;
    public bool AddPatient(Patient patient)
    {
        string query = "insert into Patients (FirstName, LastName, DateOfBirth, Gender, ContactNumber, Address) " +
                       "values (@FirstName, @LastName, @DOB, @Gender, @Contact, @Address)";

        try
        {
            using (con = DBUtility.GetConnection())
            {
                command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@FirstName", patient.FirstName);
                command.Parameters.AddWithValue("@LastName", patient.LastName);
                command.Parameters.AddWithValue("@DOB", patient.DateOfBirth);
                command.Parameters.AddWithValue("@Gender", patient.Gender);
                command.Parameters.AddWithValue("@Contact", patient.ContactNumber);
                command.Parameters.AddWithValue("@Address", patient.Address);
                int rows = command.ExecuteNonQuery();
                return rows > 0;
            }
        }
        catch (SqlException ex)
        {
            throw new Exception("SQL Error: " + ex.Message);
        }
    }
    public bool AddDoctor(Doctor doctor)
    {
        string query = "insert into Doctors (FirstName, LastName, Specialization, ContactNumber) " +
                       "values (@FirstName, @LastName, @Specialization, @ContactNumber)";

        try
        {
            using (con = DBUtility.GetConnection())
            {
                command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@FirstName", doctor.FirstName);
                command.Parameters.AddWithValue("@LastName", doctor.LastName);
                command.Parameters.AddWithValue("@Specialization", doctor.Specialization);
                command.Parameters.AddWithValue("@ContactNumber", doctor.ContactNumber);

                int rows = command.ExecuteNonQuery();
                return rows > 0;
            }
        }
        catch (SqlException ex)
        {
            throw new Exception("SQL Error while adding doctor: " + ex.Message);
        }
    }
    public Appointment GetAppointmentById(int appointmentId)
    {
        Appointment appointment = null;
        string query = "select * from appointments where appointmentid = @appointmentId";

        try
        {
            using (con = DBUtility.GetConnection())
            {
                command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@appointmentId", appointmentId);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    appointment = new Appointment
                    {
                        AppointmentId = (int)reader["appointmentid"],
                        PatientId = (int)reader["patientid"],
                        DoctorId = (int)reader["doctorid"],
                        AppointmentDate = (DateTime)reader["appointmentdate"],
                        Description = (string)reader["description"]
                    };
                }
            }

            if (appointment == null)
            {
                throw new PatientNumberNotFoundException("Appointment with the given ID not found.");
            }
        }
        catch (SqlException ex)
        {
            throw new Exception("SQL Error: " + ex.Message);
        }
        catch (Exception e)
        {
            throw new Exception("Error fetching appointment by ID: " + e.Message);
        }

        return appointment;
    }
    public List<Appointment> GetAppointmentsForPatient(int patientId)
    {
        List<Appointment> appointments = new List<Appointment>();
        string query = "select * from appointments WHERE patientid = @patientId";

        try
        {
            using (con = DBUtility.GetConnection())
            {
                command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@patientId", patientId);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Appointment appointment = new Appointment
                    {
                        AppointmentId = (int)reader["appointmentid"],
                        PatientId = (int)reader["patientid"],
                        DoctorId = (int)reader["doctorid"],
                        AppointmentDate = (DateTime)reader["appointmentdate"],
                        Description = (string)reader["description"]
                    };
                    appointments.Add(appointment);
                }
            }
        }
        catch (SqlException ex)
        {
            throw new Exception("SQL Error: " + ex.Message);
        }

        return appointments;
    }
    public List<Appointment> GetAppointmentsForDoctor(int doctorId)
    {
        List<Appointment> appointments = new List<Appointment>();
        string query = "select * from appointments WHERE doctorid = @doctorId";

        try
        {
            using (con = DBUtility.GetConnection())
            {
                command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@doctorId", doctorId);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Appointment appointment = new Appointment
                    {
                        AppointmentId = (int)reader["appointmentid"],
                        PatientId = (int)reader["patientid"],
                        DoctorId = (int)reader["doctorid"],
                        AppointmentDate = (DateTime)reader["appointmentdate"],
                        Description = (string)reader["description"]
                    };
                    appointments.Add(appointment);
                }
            }
        }
        catch (SqlException ex)
        {
            throw new Exception("SQL Error: " + ex.Message);
        }

        return appointments;
    }

    public bool ScheduleAppointment(Appointment appointment)
    {
        string checkPatientQuery = "select count(*) from Patients where PatientId = @patientId";
        string checkDoctorQuery = "select count(*) from Doctors where DoctorId = @doctorId";
        string checkAppointmentQuery = "select count(*) from Appointments where DoctorId = @doctorId and AppointmentDate = @appointmentDate";
        string insertQuery = "insert into Appointments (PatientId, DoctorId, AppointmentDate, Description) values (@patientId, @doctorId, @appointmentDate, @description)";

        try
        {
            using (con = DBUtility.GetConnection())
            {
            
                command = new SqlCommand(checkPatientQuery, con);
                command.Parameters.AddWithValue("@patientId", appointment.PatientId);
                int patientCount = (int)command.ExecuteScalar();
                if (patientCount == 0)
                {
                    throw new Exception($"Patient ID {appointment.PatientId} does not exist. Cannot schedule appointment.Kindly schedule after 10 minutes.");
                }
                command = new SqlCommand(checkDoctorQuery, con);
                command.Parameters.AddWithValue("@doctorId", appointment.DoctorId);
                int doctorCount = (int)command.ExecuteScalar();
                if (doctorCount == 0)
                {
                    throw new Exception($"Doctor ID {appointment.DoctorId} does not exist. Cannot schedule appointment.");
                }
                command = new SqlCommand(checkAppointmentQuery, con);
                command.Parameters.AddWithValue("@doctorId", appointment.DoctorId);
                command.Parameters.AddWithValue("@appointmentDate", appointment.AppointmentDate);
                int existingAppointments = (int)command.ExecuteScalar();

                if (existingAppointments > 0)
                {
                    throw new Exception("The doctor already has an appointment at the given date and time. Appointment cannot be scheduled.");
                }
                command = new SqlCommand(insertQuery, con);
                command.Parameters.AddWithValue("@patientId", appointment.PatientId);
                command.Parameters.AddWithValue("@doctorId", appointment.DoctorId);
                command.Parameters.AddWithValue("@appointmentDate", appointment.AppointmentDate);
                command.Parameters.AddWithValue("@description", appointment.Description);

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        catch (SqlException ex)
        {
            throw new Exception("SQL Error while scheduling appointment: " + ex.Message);
        }
    }
    public bool UpdateAppointment(Appointment appointment)
    {
        string query = "update appointments set patientid = @patientId, doctorid = @doctorId, appointmentdate = @appointmentDate, description = @description WHERE appointmentid = @appointmentId";
        try
        {
            using (con = DBUtility.GetConnection())
            {
                command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@appointmentId", appointment.AppointmentId);
                command.Parameters.AddWithValue("@patientId", appointment.PatientId);
                command.Parameters.AddWithValue("@doctorId", appointment.DoctorId);
                command.Parameters.AddWithValue("@appointmentDate", appointment.AppointmentDate);
                command.Parameters.AddWithValue("@description", appointment.Description);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    throw new Exception("Failed to update appointment.");
                }
            }
        }
        catch (SqlException ex)
        {
            throw new Exception("SQL Error: " + ex.Message);
        }
    }
    public bool CancelAppointment(int appointmentId)
    {
        string query = "delete from appointments where appointmentid = @appointmentId";
        try
        {
            using (con = DBUtility.GetConnection())
            {
                command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@appointmentId", appointmentId);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    throw new PatientNumberNotFoundException("Failed to cancel appointment.");
                }
            }
        }
        catch (SqlException ex)
        {
            throw new Exception("SQL Error: " + ex.Message);
        }


    }
}
