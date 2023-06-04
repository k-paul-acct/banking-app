using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SigmaBank.Api.Contracts.Requests.Auth.Models;
using SigmaBank.Api.Contracts.Responses;
using SigmaBank.Api.Options;
using SigmaBank.Api.Services;

namespace SigmaBank.Api.Extensions;

public static class SiteUserEndpoints
{
    public static WebApplication MapSiteUserEndpoints(this WebApplication app)
    {
        app.MapPost("/auth/signup", async (SignUpModel model, UserAuthService authService) =>
        {
            var user = await authService.SignUp(model);
            if (user is null) return Results.BadRequest();

            return Results.Ok(UserDto.MapUser(user, user.RegionCodeNavigation));
        }).AllowAnonymous();

        app.MapPost("/auth/signin", async (SignInModel model, UserAuthService authService,
            IOptions<JwtOptions> options) =>
        {
            var user = await authService.SignIn(model);
            if (user is null) return Results.Unauthorized();

            var key = Encoding.ASCII.GetBytes(options.Value.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var stringToken = tokenHandler.WriteToken(token);

            return Results.Ok(stringToken);
        }).AllowAnonymous();

        app.MapPost("/auth/signout", () => { });

        return app;
    }
}