namespace SigmaBank.Api.Data.Models;

public class AccountCredit
{
    public Guid Id { get; set; }

    public Guid AccountId { get; set; }

    public decimal Amount { get; set; }

    public int Type { get; set; }

    public Guid? SenderId { get; set; }

    public DateTime Timestamp { get; set; }

    public Account Account { get; set; } = null!;

    public Account? Sender { get; set; }
}