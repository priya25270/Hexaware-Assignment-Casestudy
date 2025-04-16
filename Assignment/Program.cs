
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BankApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("Bank Application Menu:");
                Console.WriteLine("1. Loan Eligibility Check");
                Console.WriteLine("2. ATM Transaction");
                Console.WriteLine("3. Compound Interest Calculator");
                Console.WriteLine("4. Account Balance Checker");
                Console.WriteLine("5. Password Validation");
                Console.WriteLine("6. Bank Transactions History");
                Console.WriteLine("7. Exit");
                Console.Write("Choose an option (1-7): ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Task1_LoanEligibility();
                        break;
                    case "2":
                        Task2_ATMTransaction();
                        break;
                    case "3":
                        Task3_CompoundInterest();
                        break;
                    case "4":
                        Task4_AccountBalanceChecker();
                        break;
                    case "5":
                        Task5_PasswordValidation();
                        break;
                    case "6":
                        Task6_TransactionHistory();
                        break;
                    case "7":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please choose again.");
                        break;
                }
            }
        }

        // Task 1
        static void Task1_LoanEligibility()
        {
            Console.Write("Enter credit score: ");
            int creditScore = int.Parse(Console.ReadLine());

            Console.Write("Enter annual income: ");
            double income = double.Parse(Console.ReadLine());

            if (creditScore > 700 && income >= 50000)
                Console.WriteLine("Eligible for a loan.");
            else
                Console.WriteLine("Not eligible for a loan.");
        }

        // Task 2
        static void Task2_ATMTransaction()
        {
            Console.Write("Enter current balance: ");
            double balance = double.Parse(Console.ReadLine());

            Console.WriteLine("1. Check Balance");
            Console.WriteLine("2. Withdraw");
            Console.WriteLine("3. Deposit");
            Console.Write("Choose transaction: ");
            int option = int.Parse(Console.ReadLine());

            switch (option)
            {
                case 1:
                    Console.WriteLine($"Your balance is: {balance:C}");
                    break;
                case 2:
                    Console.Write("Enter withdrawal amount: ");
                    double withdraw = double.Parse(Console.ReadLine());
                    if (withdraw > balance)
                        Console.WriteLine("Insufficient balance.");
                    else if (withdraw % 100 != 0 && withdraw % 500 != 0)
                        Console.WriteLine("Withdrawal must be in multiples of 100 or 500.");
                    else
                    {
                        balance -= withdraw;
                        Console.WriteLine($"Withdrawn successfully. New balance: {balance:C}");
                    }
                    break;
                case 3:
                    Console.Write("Enter deposit amount: ");
                    double deposit = double.Parse(Console.ReadLine());
                    balance += deposit;
                    Console.WriteLine($"Deposited successfully. New balance: {balance:C}");
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }

        // Task 3
        static void Task3_CompoundInterest()
        {
            Console.Write("Enter number of customers: ");
            int customers = int.Parse(Console.ReadLine());

            for (int i = 1; i <= customers; i++)
            {
                Console.WriteLine($"\nCustomer {i}:");
                Console.Write("Initial balance: ");
                double principal = double.Parse(Console.ReadLine());

                Console.Write("Annual interest rate (%): ");
                double rate = double.Parse(Console.ReadLine());

                Console.Write("Number of years: ");
                int years = int.Parse(Console.ReadLine());

                double futureBalance = principal * Math.Pow((1 + rate / 100), years);
                Console.WriteLine($"Future balance after {years} years: {futureBalance:C}");
            }
        }

        // Task 4
        static void Task4_AccountBalanceChecker()
        {
            Dictionary<string, double> accounts = new Dictionary<string, double>
            {
                {"INDB1234", 1500.00},
                {"INDB5678", 2200.50},
                {"INDB9012", 500.75}
            };

            string account;
            while (true)
            {
                Console.Write("Enter your account number (e.g. INDB1234): ");
                account = Console.ReadLine();

                if (Regex.IsMatch(account, @"^INDB\d{4}$"))
                {
                    if (accounts.ContainsKey(account))
                    {
                        Console.WriteLine($"Your balance is: {accounts[account]:C}");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Account not found. Try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid account format. Must be INDB followed by 4 digits.");
                }
            }
        }

        // Task 5
        static void Task5_PasswordValidation()
        {
            Console.Write("Create a password: ");
            string password = Console.ReadLine();

            bool isLongEnough = password.Length >= 8;
            bool hasUppercase = Regex.IsMatch(password, "[A-Z]");
            bool hasDigit = Regex.IsMatch(password, @"\d");

            if (isLongEnough && hasUppercase && hasDigit)
                Console.WriteLine("Password is valid.");
            else
            {
                Console.WriteLine("Password is invalid. Requirements:");
                if (!isLongEnough) Console.WriteLine("- At least 8 characters.");
                if (!hasUppercase) Console.WriteLine("- At least one uppercase letter.");
                if (!hasDigit) Console.WriteLine("- At least one digit.");
            }
        }

        // Task 6
        static void Task6_TransactionHistory()
        {
            List<string> transactions = new List<string>();
            double balance = 0;
            while (true)
            {
                Console.WriteLine("\n1. Deposit");
                Console.WriteLine("2. Withdraw");
                Console.WriteLine("3. Exit and show history");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.Write("Enter deposit amount: ");
                    double amount = double.Parse(Console.ReadLine());
                    balance += amount;
                    transactions.Add($"Deposited: {amount:C}");
                }
                else if (choice == "2")
                {
                    Console.Write("Enter withdrawal amount: ");
                    double amount = double.Parse(Console.ReadLine());
                    if (amount > balance)
                    {
                        Console.WriteLine("Insufficient funds.");
                        transactions.Add($"Failed Withdrawal Attempt: {amount:C} (Insufficient Funds)");
                    }
                    else
                    {
                        balance -= amount;
                        transactions.Add($"Withdrawn: {amount:C}");
                    }
                }
                else if (choice == "3")
                {
                    Console.WriteLine("\nTransaction History:");
                    foreach (string transaction in transactions)
                    {
                        Console.WriteLine(transaction);
                    }
                    Console.WriteLine($"Final Balance: {balance:C}");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid option.");
                }
            }
        }
    }
}

