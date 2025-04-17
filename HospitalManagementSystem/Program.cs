//9. Create class named MainModule with main method in   package mainmod. Trigger all the methods in service implementation class. 
using System;
using HospitalManagementSystem.entity;
using HospitalManagementSystem.exception;
using HospitalManagementSystem.Data;
using System.Collections.Generic;

namespace HospitalManagementSystem.main
{
    class Program
    {
        static void Main(string[] args)
        {
            IHospitalService service = new HospitalServiceImpl();

            while (true)
            {
                Console.WriteLine("\n--- Hospital Management System ---");
                Console.WriteLine("1. Add Patient");
                Console.WriteLine("2. Add Doctor");
                Console.WriteLine("3. Schedule Appointment");
                Console.WriteLine("4. View Appointment by ID");
                Console.WriteLine("5. View Appointments for Patient");
                Console.WriteLine("6. View Appointments for Doctor");
                Console.WriteLine("7. Update Appointment");
                Console.WriteLine("8. Cancel Appointment");
                Console.WriteLine("9. Exit");
                Console.Write("Choose an option: ");

                int choice = Convert.ToInt32(Console.ReadLine());

                try
                {
                    switch (choice)
                    {
                        case 1:
                            Patient patient = new Patient();
                            Console.Write("First Name: ");
                            patient.FirstName = Console.ReadLine();
                            Console.Write("Last Name: ");
                            patient.LastName = Console.ReadLine();
                            Console.Write("Date of Birth (yyyy-MM-dd): ");
                            patient.DateOfBirth = DateTime.Parse(Console.ReadLine());
                            Console.Write("Gender: ");
                            patient.Gender = Console.ReadLine();
                            Console.Write("Contact Number: ");
                            patient.ContactNumber = Console.ReadLine();
                            Console.Write("Address: ");
                            patient.Address = Console.ReadLine();

                            if (service.AddPatient(patient))
                                Console.WriteLine("Patient added successfully.");
                            else
                                Console.WriteLine("Failed to add patient.");
                            break;

                        case 2:
                            Doctor doctor = new Doctor();
                            Console.Write("First Name: ");
                            doctor.FirstName = Console.ReadLine();
                            Console.Write("Last Name: ");
                            doctor.LastName = Console.ReadLine();
                            Console.Write("Specialization: ");
                            doctor.Specialization = Console.ReadLine();
                            Console.Write("Contact Number: ");
                            doctor.ContactNumber = Console.ReadLine();

                            if (service.AddDoctor(doctor))
                                Console.WriteLine("Doctor added successfully.");
                            else
                                Console.WriteLine("Failed to add doctor.");
                            break;

                        case 3:
                            Appointment appointment = new Appointment();
                            Console.Write("Patient ID: ");
                            appointment.PatientId = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Doctor ID: ");
                            appointment.DoctorId = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Appointment Date (yyyy-MM-dd HH:mm): ");
                            appointment.AppointmentDate = DateTime.Parse(Console.ReadLine());
                            Console.Write("Description: ");
                            appointment.Description = Console.ReadLine();

                            if (service.ScheduleAppointment(appointment))
                                Console.WriteLine("Appointment scheduled successfully.");
                            else
                                Console.WriteLine("Failed to schedule appointment.");
                            break;

                        case 4:
                            Console.Write("Enter Appointment ID: ");
                            int apptId = Convert.ToInt32(Console.ReadLine());
                            Appointment appt = service.GetAppointmentById(apptId);
                            Console.WriteLine($"Appointment ID: {appt.AppointmentId}, Patient ID: {appt.PatientId}, Doctor ID: {appt.DoctorId}, Date: {appt.AppointmentDate}, Description: {appt.Description}");
                            break;

                        case 5:
                            Console.Write("Enter Patient ID: ");
                            int pid = Convert.ToInt32(Console.ReadLine());
                            List<Appointment> pAppointments = service.GetAppointmentsForPatient(pid);
                            foreach (var a in pAppointments)
                            {
                                Console.WriteLine($"ID: {a.AppointmentId}, Doctor ID: {a.DoctorId}, Date: {a.AppointmentDate}, Desc: {a.Description}");
                            }
                            break;

                        case 6:
                            Console.Write("Enter Doctor ID: ");
                            int did = Convert.ToInt32(Console.ReadLine());
                            List<Appointment> dAppointments = service.GetAppointmentsForDoctor(did);
                            foreach (var a in dAppointments)
                            {
                                Console.WriteLine($"ID: {a.AppointmentId}, Patient ID: {a.PatientId}, Date: {a.AppointmentDate}, Desc: {a.Description}");
                            }
                            break;

                        case 7:
                            Appointment updatedAppt = new Appointment();
                            Console.Write("Appointment ID: ");
                            updatedAppt.AppointmentId = Convert.ToInt32(Console.ReadLine());
                            Console.Write("New Patient ID: ");
                            updatedAppt.PatientId = Convert.ToInt32(Console.ReadLine());
                            Console.Write("New Doctor ID: ");
                            updatedAppt.DoctorId = Convert.ToInt32(Console.ReadLine());
                            Console.Write("New Appointment Date (yyyy-MM-dd HH:mm): ");
                            updatedAppt.AppointmentDate = DateTime.Parse(Console.ReadLine());
                            Console.Write("New Description: ");
                            updatedAppt.Description = Console.ReadLine();

                            if (service.UpdateAppointment(updatedAppt))
                                Console.WriteLine("Appointment updated successfully.");
                            else
                                Console.WriteLine("Failed to update appointment.");
                            break;

                        case 8:
                            Console.Write("Enter Appointment ID to cancel: ");
                            int cancelId = Convert.ToInt32(Console.ReadLine());

                            if (service.CancelAppointment(cancelId))
                                Console.WriteLine("Appointment cancelled successfully.");
                            else
                                Console.WriteLine("Failed to cancel appointment.");
                            break;

                        case 9:
                            Console.WriteLine("Exiting...");
                            return;

                        default:
                            Console.WriteLine("Invalid option.");
                            break;
                    }
                }
                catch (PatientNumberNotFoundException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
    }
}
