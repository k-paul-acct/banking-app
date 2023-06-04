namespace SigmaBank.Api.Contracts.Responses;

public record TransactionDebitDto
(
    decimal Amount,
    decimal Cashback,
    string Type,
    ShopDto Shop,
    RecipientDto Recipient,
    DateTime Timestamp
);