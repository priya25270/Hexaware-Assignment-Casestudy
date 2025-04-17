﻿namespace HospitalManagementSystem.entity
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Description { get; set; }
        public Appointment() { }
        public Appointment(int appointmentId, int patientId, int doctorId, DateTime appointmentDate, string description)
        {
            AppointmentId = appointmentId;
            PatientId = patientId;
            DoctorId = doctorId;
            AppointmentDate = appointmentDate;
            Description = description;
        }
        public override string ToString()
        {
            return $"{AppointmentId}, PatientID: {PatientId}, DoctorID: {DoctorId}, {AppointmentDate}, {Description}";
        }
    }
}
