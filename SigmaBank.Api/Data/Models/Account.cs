namespace SigmaBank.Api.Data.Models;

public class Account
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public decimal Balance { get; set; }

    public decimal CashbackAmount { get; set; }

    public int CurrencyId { get; set; }

    public string CardNumber { get; set; } = null!;

    public ICollection<AccountCredit> AccountCreditAccounts { get; set; } = new List<AccountCredit>();

    public ICollection<AccountCredit> AccountCreditSenders { get; set; } = new List<AccountCredit>();

    public ICollection<AccountDebit> AccountDebitAccounts { get; set; } = new List<AccountDebit>();

    public ICollection<AccountDebit> AccountDebitRecipients { get; set; } = new List<AccountDebit>();

    public ICollection<Credit> Credits { get; set; } = new List<Credit>();

    public Currency Currency { get; set; } = null!;

    public User User { get; set; } = null!;
}