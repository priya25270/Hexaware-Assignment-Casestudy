using NUnit.Framework;
using System;
using HospitalManagementSystem.entity;
using HospitalManagementSystem.Data;
namespace HospitalManagementSystem.Tests
{
    [TestFixture]
    public class HospitalServiceTests
    {
        private IHospitalService _hospitalService;

        [SetUp]
        public void Setup()
        {
            _hospitalService = new HospitalServiceImpl(); 
        }

        [Test]
        public void Test_AddPatient_ShouldReturnTrue()
        {
            var patient = new Patient
            {
                FirstName = "John",
                LastName = "Smith",
                DateOfBirth = new DateTime(1990, 1, 1),
                Gender = "Male",
                ContactNumber = "1234567890",
                Address = "123 Main St"
            };

            bool result = _hospitalService.AddPatient(patient);

            Assert.IsTrue(result, "Failed to add patient.");
        }

        [Test]
        public void Test_AddDoctor_ShouldReturnTrue()
        {
            var doctor = new Doctor
            {
                FirstName = "Emily",
                LastName = "Clark",
                Specialization = "Dermatology",
                ContactNumber = "0987654321"
            };

            bool result = _hospitalService.AddDoctor(doctor);

            Assert.IsTrue(result, "Failed to add doctor.");
        }

        [Test]
        public void Test_ScheduleAppointment_ShouldReturnTrue()
        {
            var appointment = new Appointment
            {
                PatientId = 1, // Make sure this ID exists in your test DB
                DoctorId = 1,  // Make sure this ID exists in your test DB
                AppointmentDate = DateTime.Now.AddDays(1),
                Description = "General Checkup"
            };

            bool result = _hospitalService.ScheduleAppointment(appointment);

            Assert.IsTrue(result, "Failed to schedule appointment.");
        }

        [Test]
        public void Test_GetAppointmentById_ShouldReturnValidAppointment()
        {
            int testAppointmentId = 2; // Use a valid appointmentId from DB

            Appointment appointment = _hospitalService.GetAppointmentById(testAppointmentId);

            Assert.IsNotNull(appointment, "Appointment not found.");
            Assert.AreEqual(testAppointmentId, appointment.AppointmentId);
        }
    }
}
