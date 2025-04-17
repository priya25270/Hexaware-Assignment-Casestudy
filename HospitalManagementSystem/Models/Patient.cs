/*2. Implement the following for all model classes. Write default constructors and overload the 
constructor with parameters, getters and setters, method to print all the member variables and 
values.*/
namespace HospitalManagementSystem.entity
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public Patient() { }
        public Patient(int patientId, string firstName, string lastName, DateTime dateOfBirth, string gender, string contactNumber, string address)
        {
            PatientId = patientId;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            ContactNumber = contactNumber;
            Address = address;
        }

        public override string ToString()
        {
            return $"{PatientId}, {FirstName} {LastName}, {DateOfBirth.ToShortDateString()}, {Gender}, {ContactNumber}, {Address}";
        }
    }
}
