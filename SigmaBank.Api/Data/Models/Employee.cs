namespace SigmaBank.Api.Data.Models;

public class Employee
{
    public Guid Id { get; set; }

    public string FistName { get; set; } = null!;

    public string SecondName { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime SignupTimestamp { get; set; }

    public ICollection<Credit> Credits { get; set; } = new List<Credit>();

    public ICollection<Role> Roles { get; set; } = new List<Role>();
}