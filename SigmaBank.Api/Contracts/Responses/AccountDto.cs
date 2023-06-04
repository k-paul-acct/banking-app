using SigmaBank.Api.Data.Models;

namespace SigmaBank.Api.Contracts.Responses;

public record AccountDto(Guid Id, decimal Balance, decimal Cashback, string CardNumber)
{
    public static AccountDto Map(Account account)
    {
        return new AccountDto(account.Id, account.Balance, account.CashbackAmount, account.CardNumber);
    }
}