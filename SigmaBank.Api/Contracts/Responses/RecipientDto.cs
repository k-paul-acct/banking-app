namespace SigmaBank.Api.Contracts.Responses;

public record RecipientDto
(
    string FirstName,
    string SecondName,
    string Patronymic,
    string Phone,
    string CardNumber
);