namespace SigmaBank.Api.Data.Models;

public class Mcc
{
    public int Code { get; set; }

    public string Name { get; set; } = null!;

    public ICollection<Shop> Shops { get; set; } = new List<Shop>();
}