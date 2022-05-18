using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Struct
{
    class Struct
    {
        public enum AccountType
        {
            Checking,
            Deposit
        }

        public struct BankAccount
        {
            public long accNo;
            public decimal accBal;
            public AccountType accType;
        }

        static void Main(string[] args)
        {
            BankAccount goldAccount;
            goldAccount.accType = AccountType.Checking;
            goldAccount.accBal = 3200.00M;
            Console.Write("Enter account number: ");
            goldAccount.accNo = long.Parse(Console.ReadLine());
            Console.WriteLine("*** Account Summary ***");
            Console.WriteLine("Acct Number {0}", goldAccount.accNo);
            Console.WriteLine("Acct Type {0}", goldAccount.accType);
            Console.WriteLine("Acct Balance ${0}",goldAccount.accBal);
            Console.Read();
        }
    }
}
