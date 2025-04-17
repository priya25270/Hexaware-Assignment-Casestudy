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

    // Get appointment by ID
    public bool AddPatient(Patient patient)
    {
        string query = "INSERT INTO Patients (FirstName, LastName, DateOfBirth, Gender, ContactNumber, Address) " +
                       "VALUES (@FirstName, @LastName, @DOB, @Gender, @Contact, @Address)";

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
        string query = "INSERT INTO Doctors (FirstName, LastName, Specialization, ContactNumber) " +
                       "VALUES (@FirstName, @LastName, @Specialization, @ContactNumber)";

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
        string query = "SELECT * FROM appointments WHERE appointmentid = @appointmentId";

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

    // Get appointments for a patient
    public List<Appointment> GetAppointmentsForPatient(int patientId)
    {
        List<Appointment> appointments = new List<Appointment>();
        string query = "SELECT * FROM appointments WHERE patientid = @patientId";

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

    // Get appointments for a doctor
    public List<Appointment> GetAppointmentsForDoctor(int doctorId)
    {
        List<Appointment> appointments = new List<Appointment>();
        string query = "SELECT * FROM appointments WHERE doctorid = @doctorId";

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

    // Schedule a new appointment
    public bool ScheduleAppointment(Appointment appointment)
    {
        string query = "INSERT INTO appointments(patientid, doctorid, appointmentdate, description) VALUES(@patientId, @doctorId, @appointmentDate, @description)";
        try
        {
            using (con = DBUtility.GetConnection())
            {
                command = new SqlCommand(query, con);
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
                    throw new PatientNumberNotFoundException("Failed to schedule appointment.");
                }
            }
        }
        catch (SqlException ex)
        {
            throw new Exception("SQL Error: " + ex.Message);
        }
    }

    // Update appointment
    public bool UpdateAppointment(Appointment appointment)
    {
        string query = "UPDATE appointments SET patientid = @patientId, doctorid = @doctorId, appointmentdate = @appointmentDate, description = @description WHERE appointmentid = @appointmentId";
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

    // Cancel appointment
    public bool CancelAppointment(int appointmentId)
    {
        string query = "DELETE FROM appointments WHERE appointmentid = @appointmentId";
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
