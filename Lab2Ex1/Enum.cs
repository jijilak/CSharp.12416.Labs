using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2Ex1.BankAccount
{
    internal class Enum
    {
        public enum AccountType { Checking, Deposit }
        static void Main(string[] args)
        {
            AccountType goldAccount;
            goldAccount = AccountType.Checking;
            Console.WriteLine("The Customer Account Type is {0}", goldAccount);

            AccountType platinumAccount;
            platinumAccount = AccountType.Deposit;
            Console.WriteLine("The Customer Account Type is {0}", platinumAccount);
        }
    }
}
