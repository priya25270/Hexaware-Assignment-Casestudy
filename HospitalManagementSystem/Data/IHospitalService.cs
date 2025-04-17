﻿using HospitalManagementSystem.entity;
using System.Collections.Generic;



namespace HospitalManagementSystem.Data
{
    public interface IHospitalService
    {
        Appointment GetAppointmentById(int appointmentId);
        List<Appointment> GetAppointmentsForPatient(int patientId);
        List<Appointment> GetAppointmentsForDoctor(int doctorId);
        bool ScheduleAppointment(Appointment appointment);
        bool UpdateAppointment(Appointment appointment);
        bool CancelAppointment(int appointmentId);
        bool AddPatient(Patient patient);
        bool AddDoctor(Doctor doctor);

    }
}
