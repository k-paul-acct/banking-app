using System.ComponentModel;
using SigmaBank.Api.DomainTypes.Enums;

namespace SigmaBank.Api.Contracts;

public static class EnumSerializers
{
    public static string ToApiString(this Sex sex)
    {
        return sex switch
        {
            Sex.Female => "female",
            Sex.Male => "male",
            _ => throw new InvalidEnumArgumentException()
        };
    }

    public static bool TryToEnum(string sex, out Sex sexEnum)
    {
        switch (sex)
        {
            case "female":
                sexEnum = Sex.Female;
                break;
            case "male":
                sexEnum = Sex.Male;
                break;
            default:
                sexEnum = Sex.Male;
                return false;
        }

        return true;
    }
}