//struct BankAccount 
class BankAccount
{
    public void Populate(long number, decimal balance)
    {
        accNo = number;
        accBal = balance;
        accType = AccountType.Checking;
    }
    public decimal Balance( )
    {
        return accBal;
    }
    public long Number( )
    {
        return accNo;
    }
    public string Type( )
    {
        return accType.ToString( );
    }
    private long accNo;
    private decimal accBal;
    private AccountType accType;
}
