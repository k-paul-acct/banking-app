using SigmaBank.Api.Data;
using SigmaBank.Api.Data.Models;

namespace SigmaBank.Api.Services;

public class EmployeeAuthService
{
    private readonly SigmaBankContext _context;

    public EmployeeAuthService(SigmaBankContext context)
    {
        _context = context;
    }

    public async Task<User> SignUp()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> SignIn()
    {
        throw new NotImplementedException();
    }
}