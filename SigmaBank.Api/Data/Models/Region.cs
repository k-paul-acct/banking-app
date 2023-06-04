namespace SigmaBank.Api.Data.Models;

public class Region
{
    public int Code { get; set; }

    public string Name { get; set; } = null!;

    public ICollection<User> Users { get; set; } = new List<User>();
}