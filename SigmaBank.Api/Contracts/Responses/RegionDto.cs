using SigmaBank.Api.Data.Models;

namespace SigmaBank.Api.Contracts.Responses;

public record RegionDto(int Code, string Name)
{
    public static RegionDto MapRegion(Region region)
    {
        return new RegionDto(region.Code, region.Name);
    }
}