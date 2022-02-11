using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
    /// <summary>
    /// class Account is the base/parent class for classes CheckingAccount and SavingsAccount
    /// </summary>
    public class Account
    {
        private decimal balance;
        
        public Account(decimal accountBalance)
        {
            Balance = accountBalance;
        }

        public decimal Balance 
        {
            get
            {
                return balance;
            }

            set
            {
                if (value >= 0.0m)
                {
                    balance = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, $"{nameof(Balance)} must be greater than or equal to 0");
                }
            }
        }

        public virtual decimal Credit(decimal amountToAdd)
        {
            //decimal amountToAdd;
            // add an amount to the current balance
            //Console.Write("Enter an amount you would like to add: ");
            //amountToAdd = decimal.Parse(Console.ReadLine());

            Balance = Balance + amountToAdd;
            //Console.WriteLine($"New balance is: {Balance}");

            return Balance;
        }

        public virtual decimal Debit(decimal amountOut)
        {
            // withdraw an amount of money
            //decimal amountToWithdraw;
           // Console.Write("Enter an amount you would like to withdraw: ");
            //amountToWithdraw = decimal.Parse(Console.ReadLine());

            while (amountOut > Balance)
            {
                Console.Write("Amount cannot be greater than account balance\nEnter a different amount to withdraw:");
                amountOut = decimal.Parse(Console.ReadLine());
            }

            Balance = Balance - amountOut;
            //Console.WriteLine($"New account balance is: {Balance: C}");

            return Balance;
        }

        public virtual decimal CalculateInterest()
        {
            return Balance;
        }

    }

    /// <summary>
    /// Checking account is derived from class Account and represents a checking account. It overrides functions Debit and Credit
    /// </summary>
    public class CheckingAccount : Account
    {

        private decimal transactionFee;
        public CheckingAccount(decimal initBalance, decimal fee) : base(initBalance)
        {
            transactionFee = fee;
        }

        public override decimal Credit(decimal amountToAdd)
        {
            Balance = base.Credit(amountToAdd) - transactionFee;
            return Balance;
        }

        public override decimal Debit(decimal amountOut)
        {
            Balance = base.Debit(amountOut) - transactionFee;
            return Balance;
        }
    }

    /// <summary>
    /// SavingsAccount is derived from class Account and represents a savings account
    /// </summary>
    public class SavingsAccount : Account
    {
        private decimal interestRate;

        public SavingsAccount(decimal initBalance, decimal intRate) : base(initBalance)
        {
            interestRate = intRate;
        }

        public override decimal CalculateInterest()
        {
            decimal amountEarned;
            amountEarned = Balance * interestRate;
            return amountEarned;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Account[] arrObjects = new Account[4];
            arrObjects[0] = new CheckingAccount(100m, 20m); // checking account with balance $100 and $20 fee
            arrObjects[1] = new CheckingAccount(200m, 20m); // checking account with balance $200 and $20 fee
            arrObjects[2] = new SavingsAccount(150m, 0.1m); // savings account with balance $150 and 10% interest rate
            arrObjects[3] = new SavingsAccount(300m, 0.15m); // savings account with balance $300 and 15% interest rate
           // int choice;

            Console.WriteLine("Please choose a command:");
            Console.WriteLine("1: Deposit in checking account");
            Console.WriteLine("2: Deposit in savings account");
            Console.WriteLine("3: Withdraw from checking account");
            Console.WriteLine("4: Withdraw from savings account");
            //choice = int.Parse(Console.ReadLine());

            /*switch (choice)
            {
                case 1:
                    Console.Write("Enter amount you would like to deposit: ");
                    arrObjects[0].Credit(decimal.Parse(Console.ReadLine()));
                    Console.WriteLine($"Your new checking account balance is: {arrObjects[0].Balance:C}");
                    break;
                case 2:
                    Console.Write("Enter amount you would like to deposit: ");
                    arrObjects[2].Credit(decimal.Parse(Console.ReadLine()));
                    Console.WriteLine($"Your new savings account balance is: {arrObjects[2].Balance:C}");
                    break;
                case 3:
                    Console.Write("Enter amount you would like to withdraw: ");
                    arrObjects[0].Debit(decimal.Parse(Console.ReadLine()));
                    Console.WriteLine($"Your new checking account balance is: {arrObjects[0].Balance:C}");
                    break;
                case 4:
                    Console.Write("Enter amount you would like to withdraw: ");
                    arrObjects[2].Debit(decimal.Parse(Console.ReadLine()));
                    arrObjects[2].Balance = (arrObjects[2].CalculateInterest() + arrObjects[2].Balance);
                    Console.WriteLine($"Your new savings account balance is: {arrObjects[2].Balance:C}");
                    break;
            }*/

            for (int i = 0; i < arrObjects.Length; ++i)
            {
                decimal amountEarned;
                //Withdrawing from account
                Console.WriteLine($"Account {i + 1} balance: {arrObjects[i].Balance:C}");
                Console.Write("Enter an amount to withdraw from this account: ");
                arrObjects[i].Debit(decimal.Parse(Console.ReadLine()));
                Console.WriteLine($"New balance in account {i + 1} is {arrObjects[i].Balance:C}");
                

                //Depositing into account
                Console.Write("\nEnter an amount to deposit into this account: ");
                arrObjects[i].Credit(decimal.Parse(Console.ReadLine()));
                if (i >= 2)
                {
                    amountEarned = arrObjects[i].CalculateInterest();
                    Console.WriteLine($"Amount earned is: {amountEarned}");
                    arrObjects[i].Balance = arrObjects[i].Balance + amountEarned;
                }

                Console.WriteLine($"New balance in account {i + 1} is: {arrObjects[i].Balance:C}");
            }

            Console.Write("Enter any key to exit: ");
            Console.ReadLine();
        }
    }
}
