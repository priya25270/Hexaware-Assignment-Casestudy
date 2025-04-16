using System;

namespace SISApp
{
    internal class Payment
    {
        public Student Student { get; set; }
        public double Amount { get; set; }
        public DateTime PaymentDate { get; set; }

        public override string ToString()
        {
            return $"Student: {Student.Name}, Amount: {Amount}, Date: {PaymentDate.ToShortDateString()}";
        }
    }
}