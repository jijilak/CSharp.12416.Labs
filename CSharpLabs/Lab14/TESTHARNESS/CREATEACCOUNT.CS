
using System;
using System.Collections;
using Banking;

// Test harness
class CreateAccount
{
    public static void Main() 
    {
		// Create a bank account and test the DumpToScreen method
		long accNo1 = Bank.CreateAccount(AccountType.Checking, 100);
		BankAccount myAccount = Bank.GetAccount(accNo1);

		// Add a line of code to call DumpToScreen
	}
}
