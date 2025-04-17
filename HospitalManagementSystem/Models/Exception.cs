using System;

namespace HospitalManagementSystem.exception
{
    public class PatientNumberNotFoundException : Exception
    {
        public PatientNumberNotFoundException(string message) : base(message) { }
    }
}
