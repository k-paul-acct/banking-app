namespace SigmaBank.Api.Contracts.Responses;

public record ShopDto
(
    string Name,
    int MccCode,
    string MccName,
    string AddressUri,
    string CashbackPercent
);