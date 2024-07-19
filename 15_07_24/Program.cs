using System;

public abstract class BankAccount
{
    public string AccountNumber { get; }
    public decimal Balance { get; protected set; }

    public BankAccount(string accountNumber, decimal initialBalance)
    {
        AccountNumber = accountNumber;
        Balance = initialBalance;
    }
    public abstract void Deposit(decimal amount);
    public abstract void Withdraw(decimal amount);
}
public class SavingsAccount : BankAccount
{
    public decimal InterestRate { get; set; }

    public SavingsAccount(string accountNumber, decimal initialBalance, decimal interestRate)
        : base(accountNumber, initialBalance)
    {
        InterestRate = interestRate;
    }

    public override void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Suma depusa trebuie sa fie mai mare decat zero.");
        }
        Balance += amount;
    }

    public override void Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Suma retrasa trebuie să fie mai mare decat zero.");
        }
        if (amount > Balance)
        {
            throw new InvalidOperationException("Fonduri insuficiente.");
        }
        Balance -= amount;
    }
}

public class CheckingAccount : BankAccount
{
    public decimal OverdraftLimit { get; set; }

    public CheckingAccount(string accountNumber, decimal initialBalance, decimal overdraftLimit)
        : base(accountNumber, initialBalance)
    {
        OverdraftLimit = overdraftLimit;
    }

    public override void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Suma depusa trebuie sa fie mai mare decat zero.");
        }
        Balance += amount;
    }

    public override void Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Suma retrasa trebuie sa fie mai mare decat zero.");
        }
        if (amount > Balance + OverdraftLimit)
        {
            throw new InvalidOperationException("Depasire limita overdraft.");
        }
        Balance -= amount;
    }
}
public class Program
{
    public static void Main()
    {
        SavingsAccount savings = new SavingsAccount("SA123", 1000m, 0.05m);
        savings.Deposit(500m);
        Console.WriteLine($"SavingsAccount Balance: {savings.Balance}");

        CheckingAccount checking = new CheckingAccount("CA456", 500m, 200m);
        checking.Withdraw(600m);
        Console.WriteLine($"CheckingAccount Balance: {checking.Balance}");
    }
}
