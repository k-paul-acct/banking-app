namespace SigmaBank.Api.Data.Models;

public class Currency
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string IsoCode { get; set; } = null!;

    public ICollection<Account> Accounts { get; set; } = new List<Account>();
}