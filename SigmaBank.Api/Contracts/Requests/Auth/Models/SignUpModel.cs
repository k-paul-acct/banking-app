namespace SigmaBank.Api.Contracts.Requests.Auth.Models;

public record SignUpModel
(
    string FirstName,
    string SecondName,
    string Patronymic,
    DateOnly DateOfBirth,
    string Sex,
    string Phone,
    string PassportNumber,
    string Password,
    string PasswordConfirmation,
    int RegionCode
);