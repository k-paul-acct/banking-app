namespace SigmaBank.Api.Data.Models;

public class Shop
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public int MccCode { get; set; }

    public string? AddressUri { get; set; }

    public decimal CashbackPercent { get; set; }

    public DateTime Timestamp { get; set; }

    public ICollection<AccountDebit> AccountDebits { get; set; } = new List<AccountDebit>();

    public Mcc MccCodeNavigation { get; set; } = null!;
}