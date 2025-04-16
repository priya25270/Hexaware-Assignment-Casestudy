using System;

namespace SISApp
{
    internal class UserInterface
    {
        public int GetIntInput(string prompt)
        {
            Console.WriteLine(prompt);
            return Convert.ToInt32(Console.ReadLine());
        }

        public string GetStringInput(string prompt)
        {
            Console.WriteLine(prompt);
            return Console.ReadLine();
        }

        public double GetDoubleInput(string prompt)
        {
            Console.WriteLine(prompt);
            return Convert.ToDouble(Console.ReadLine());
        }

        public DateTime GetDateInput(string prompt)
        {
            Console.WriteLine(prompt);
            return Convert.ToDateTime(Console.ReadLine());
        }
    }
}
