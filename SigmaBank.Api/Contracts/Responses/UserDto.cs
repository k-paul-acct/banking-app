using SigmaBank.Api.Data.Models;

namespace SigmaBank.Api.Contracts.Responses;

public record UserDto
(
    Guid Id,
    string FirstName,
    string SecondName,
    string Patronymic,
    string Phone,
    string? Email,
    string PassportNumber,
    DateOnly DateOfBirth,
    string Sex,
    RegionDto Region
)
{
    public static UserDto MapUser(User user, Region region)
    {
        return new UserDto(
            user.Id,
            user.FistName,
            user.SecondName,
            user.Patronymic,
            user.Phone,
            user.Email,
            user.PassportNumber,
            user.DateOfBirth,
            user.Sex.ToApiString(),
            RegionDto.MapRegion(region));
    }
}