using Microsoft.AspNetCore.Identity;
using SigmaBank.Api.Contracts;
using SigmaBank.Api.Contracts.Requests.Auth.Models;
using SigmaBank.Api.Data;
using SigmaBank.Api.Data.Models;
using SigmaBank.Api.DomainTypes.Enums;

namespace SigmaBank.Api.Services;

public class UserAuthService
{
    private readonly SigmaBankContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserAuthService(SigmaBankContext context, IPasswordHasher<User> passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task<User?> SignUp(SignUpModel model)
    {
        EnumSerializers.TryToEnum(model.Sex, out var sex);
        var user = new User
        {
            Id = Guid.NewGuid(),
            FistName = model.FirstName,
            SecondName = model.SecondName,
            Patronymic = model.Patronymic,
            Phone = model.Phone,
            Sex = sex,
            Status = UserStatus.NotVerified,
            CodeExpiration = DateTime.UtcNow.AddMinutes(5),
            PassportNumber = model.Password,
            RegionCode = model.RegionCode,
            SignupTimestamp = DateTime.UtcNow,
            DateOfBirth = model.DateOfBirth
        };
        user.Password = _passwordHasher.HashPassword(user, model.Password);

        _context.Add(user);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }

        return user;
    }

    public async Task<User?> SignIn(SignInModel model)
    {
        throw new NotImplementedException();
    }
}