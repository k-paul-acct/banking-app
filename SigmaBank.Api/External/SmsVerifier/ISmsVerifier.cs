namespace SigmaBank.Api.External.SmsVerifier;

public interface ISmsVerifier
{
    public Task<string> Call(string phone);
}