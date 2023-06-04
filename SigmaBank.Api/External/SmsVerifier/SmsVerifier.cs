namespace SigmaBank.Api.External.SmsVerifier;

public class SmsVerifier : ISmsVerifier
{
    public SmsVerifier()
    {
    }
    
    public Task<string> Call(string phone)
    {
        throw new NotImplementedException();
    }
}