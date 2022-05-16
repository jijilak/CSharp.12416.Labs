using System;

namespace Lab2Ex2.StructType
{
    internal partial class Struct
    {
        public enum AccountType { Checking, Deposit }
        public struct BankAccount
        {
            public long accNo;
            public decimal accBal;
            public AccountType accType;
        }
        static void Main (string[] args)
        {
        BankAccount goldAccount;
            goldAccount.accNo = 123;
            goldAccount.accType = AccountType.Checking;
            goldAccount.accBal = (decimal)3200.00;

        Console.WriteLine("*** Account Summary ***");
        Console.WriteLine("Acct Number {0}", goldAccount.accNo);
        Console.WriteLine("Acct Type {0}", goldAccount.accType);
        Console.WriteLine("Acct Balance ${0}", goldAccount.accBal);
        }
    }
}
