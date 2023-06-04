using SigmaBank.Api.DomainTypes.Enums;

namespace SigmaBank.Api.Data.Models;

public class User
{
    public Guid Id { get; set; }

    public string FistName { get; set; } = null!;

    public string SecondName { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public Sex Sex { get; set; }

    public string? Email { get; set; }

    public string Phone { get; set; } = null!;

    public string PassportNumber { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int RegionCode { get; set; }

    public UserStatus Status { get; set; }

    public string? ConfirmationCode { get; set; }

    public int FailedAttempts { get; set; }

    public DateTime CodeExpiration { get; set; }

    public DateTime SignupTimestamp { get; set; }

    public string? PhotoName { get; set; }

    public Account? Account { get; set; }

    public Region RegionCodeNavigation { get; set; } = null!;
}