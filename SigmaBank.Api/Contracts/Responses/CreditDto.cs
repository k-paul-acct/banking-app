namespace SigmaBank.Api.Contracts.Responses;

public record CreditDto
(
    decimal Amount,
    decimal Returned,
    int Months,
    int MonthsPassed,
    decimal MonthPayment,
    decimal Percent,
    string Status,
    DateTime Created,
    DateTime? Approved
);