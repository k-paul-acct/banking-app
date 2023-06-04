namespace SigmaBank.Api.Data.Models;

public class AccountDebit
{
    public Guid Id { get; set; }

    public Guid AccountId { get; set; }

    public decimal Amount { get; set; }

    public int Type { get; set; }

    public Guid? ShopId { get; set; }

    public decimal Cashback { get; set; }

    public Guid? RecipientId { get; set; }

    public DateTime Timestamp { get; set; }

    public Account Account { get; set; } = null!;

    public Account? Recipient { get; set; }

    public Shop? Shop { get; set; }
}