/*8. Create the exceptions in package myexceptions 
Define the following custom exceptions and throw them in methods whenever needed. Handle all the 
exceptions in main method,
1. PatientNumberNotFoundException :throw this exception when user enters an invalid patient 
number which doesn’t exist in db */
using System;
namespace HospitalManagementSystem.exception
{
    public class PatientNumberNotFoundException : Exception
    {
        public PatientNumberNotFoundException(string message) : base(message) { }
    }
}
