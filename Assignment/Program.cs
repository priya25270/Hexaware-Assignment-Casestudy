Task 1
using System;
class Program
{
    static void Main()
    {
        Console.Write("Enter your credit score: ");
        int creditScore = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter your annual income: ");
        double annualIncome = Convert.ToDouble(Console.ReadLine());
        if (creditScore > 700 && annualIncome >= 50000)
        {
            Console.WriteLine("Congratulations! You are eligible for a loan.");
        }
        else
        {
            Console.WriteLine("Sorry, you are not eligible for a loan.");
        }
    }
}


Task2
using System;
class Program
{
    static void Main()
    {
        Console.Write("Enter your current balance: ");
        double balance = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("ATM Menu:");
        Console.WriteLine("1. Check Balance");
        Console.WriteLine("2. Withdraw");
        Console.WriteLine("3. Deposit");
        Console.Write("Choose an option (1-3): ");
        int choice = Convert.ToInt32(Console.ReadLine());

        if (choice == 1)
        {
            Console.WriteLine($"Your current balance is: ${balance}");
        }
        else if (choice == 2)
        {
            Console.Write("Enter amount to withdraw: ");
            double withdrawAmount = Convert.ToDouble(Console.ReadLine());

            if (withdrawAmount > balance)
            {
                Console.WriteLine("Insufficient balance.");
            }
            else if (withdrawAmount % 100 != 0 && withdrawAmount % 500 != 0)
            {
                Console.WriteLine("Withdrawal amount must be in multiples of 100 or 500.");
            }
            else
            {
                balance -= withdrawAmount;
                Console.WriteLine($"Withdrawal successful. New balance: ${balance}");
            }
        }
        else if (choice == 3)
        {
            Console.Write("Enter amount to deposit: ");
            double depositAmount = Convert.ToDouble(Console.ReadLine());

            if (depositAmount <= 0)
            {
                Console.WriteLine("Invalid deposit amount.");
            }
            else
            {
                balance += depositAmount;
                Console.WriteLine($"Deposit successful. New balance: ${balance}");
            }
        }
        else
        {
            Console.WriteLine("Invalid choice. Please select a valid option.");
        }
    }
}


Task 3
 using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter the number of customers: ");
        int numCustomers = Convert.ToInt32(Console.ReadLine());
        
        for (int i = 1; i <= numCustomers; i++)
        {
            Console.WriteLine($"Customer {i}:");
            
            Console.Write("Enter initial balance: ");
            double initialBalance = Convert.ToDouble(Console.ReadLine());
            
            Console.Write("Enter annual interest rate (in %): ");
            double annualInterestRate = Convert.ToDouble(Console.ReadLine());
            
            Console.Write("Enter the number of years: ");
            int years = Convert.ToInt32(Console.ReadLine());
            
            double futureBalance = initialBalance * Math.Pow(1 + annualInterestRate / 100, years);
            
            Console.WriteLine($"Future balance after {years} years: ${futureBalance:F2}\n");
        }
    }
}

Task 4
using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter the number of customers: ");
        int numCustomers = Convert.ToInt32(Console.ReadLine());

        string[] accountNumbers = new string[numCustomers];
        double[] balances = new double[numCustomers];

        for (int i = 0; i < numCustomers; i++)
        {
            while (true)
            {
                Console.Write($"Enter account number for Customer {i + 1} (Format: INDB1234): ");
                string accountNumber = Console.ReadLine();

                if (accountNumber.Length == 8 && accountNumber.StartsWith("INDB") && int.TryParse(accountNumber.Substring(4), out _))
                {
                    accountNumbers[i] = accountNumber;
                    break;
                }
                Console.WriteLine("Invalid account number format. Please try again.");
            }

            Console.Write("Enter account balance: ");
            balances[i] = Convert.ToDouble(Console.ReadLine());
        }

        Console.WriteLine("\nBank System: Check Account Balance");
        while (true)
        {
            Console.Write("Enter your account number to check balance (or type 'exit' to quit): ");
            string input = Console.ReadLine();

            if (input.ToLower() == "exit")
                break;

            int index = Array.IndexOf(accountNumbers, input);
            if (index != -1)
            {
                Console.WriteLine($"Your account balance: ${balances[index]:F2}\n");
            }
            else
            {
                Console.WriteLine("Account not found. Please try again.");
            }
        }
    }
}

Task 5

using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        Console.Write("Create a password for your bank account: ");
        string password = Console.ReadLine();

        if (IsValidPassword(password))
        {
            Console.WriteLine("Password is valid. Account created successfully!");
        }
        else
        {
            Console.WriteLine("Invalid password. Ensure it meets the following criteria:");
            Console.WriteLine("- At least 8 characters long");
            Console.WriteLine("- Contains at least one uppercase letter");
            Console.WriteLine("- Contains at least one digit");
        }
    }

    static bool IsValidPassword(string password)
    {
        return password.Length >= 8 &&
               Regex.IsMatch(password, "[A-Z]") &&
               Regex.IsMatch(password, "\\d");
    }
}

Task 6
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<string> transactions = new List<string>();
        double balance = 0;

        while (true)
        {
            Console.WriteLine("\nBank Transaction Menu:");
            Console.WriteLine("1. Deposit");
            Console.WriteLine("2. Withdraw");
            Console.WriteLine("3. Exit and Show Transaction History");
            Console.Write("Choose an option (1-3): ");

            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 3)
            {
                Console.WriteLine("Invalid choice. Please try again.");
                continue;
            }

            if (choice == 3)
                break;

            Console.Write("Enter amount: ");
            double amount;
            if (!double.TryParse(Console.ReadLine(), out amount) || amount <= 0)
            {
                Console.WriteLine("Invalid amount. Please enter a positive number.");
                continue;
            }

            if (choice == 1)
            {
                balance += amount;
                transactions.Add($"Deposited: ${amount:F2}, New Balance: ${balance:F2}");
                Console.WriteLine($"Deposit successful! New balance: ${balance:F2}");
            }
            else if (choice == 2)
            {
                if (amount > balance)
                {
                    Console.WriteLine("Insufficient balance for withdrawal.");
                }
                else
                {
                    balance -= amount;
                    transactions.Add($"Withdrawn: ${amount:F2}, New Balance: ${balance:F2}");
                    Console.WriteLine($"Withdrawal successful! New balance: ${balance:F2}");
                }
            }
        }

        Console.WriteLine("\nTransaction History:");
        foreach (string transaction in transactions)
        {
            Console.WriteLine(transaction);
        }
    }
}
