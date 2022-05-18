
using System;

class CreateAccount
{
    static void Main() 
    {
        BankAccount berts = NewBankAccount();
        Write(berts);
        
        BankAccount freds = NewBankAccount();
        Write(freds);
    }
    
    static BankAccount NewBankAccount()
    {       
        BankAccount created = new BankAccount();
        
        Console.Write("Enter the account balance! : ");
        decimal balance = decimal.Parse(Console.ReadLine());
        
        created.Populate(balance);
        
        return created;
    }
    
    static void Write(BankAccount acc)
    {
        Console.WriteLine("Account number is {0}",  acc.Number());
        Console.WriteLine("Account balance is {0}", acc.Balance());
        Console.WriteLine("Account type is {0}", acc.Type());
    }
}
