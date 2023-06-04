namespace SigmaBank.Api.Data.Models;

public class Credit
{
    public Guid Id { get; set; }

    public Guid AccountId { get; set; }

    public Guid? ManagerId { get; set; }

    public decimal Amount { get; set; }

    public int Months { get; set; }

    public int Percent { get; set; }

    public int Status { get; set; }

    public DateTime CreationTimestamp { get; set; }

    public DateTime? ApprovalTimestamp { get; set; }

    public Account Account { get; set; } = null!;

    public Employee? Manager { get; set; }
}