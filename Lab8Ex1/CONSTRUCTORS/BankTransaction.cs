using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class BankTransaction
{
    private decimal amount;
    private DateTime when;
    public decimal Amount()
    {
    return amount;
    }
    public DateTime When()
    {
    return when;
    }
    public BankTransaction (decimal tranAmount)
    {
    amount = tranAmount;
    when = DateTime.Now;
    }
}


