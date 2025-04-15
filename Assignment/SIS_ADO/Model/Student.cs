using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS_ADO.Model
{
    public class Student
    {
        internal object OutstandingBalance;

        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Balance { get; set; }
        public string Phone { get; internal set; }
        public DateTime? DateOfBirth { get; internal set; }
    }
}
