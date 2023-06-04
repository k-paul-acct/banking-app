namespace SigmaBank.Api.Contracts.Responses;

public record TransactionCreditDto
(
    decimal Amount,
    string Type,
    SenderDto Sender,
    DateTime Timestamp
);