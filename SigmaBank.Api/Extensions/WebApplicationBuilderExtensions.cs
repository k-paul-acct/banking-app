namespace SigmaBank.Api.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddValidation(this WebApplicationBuilder builder)
    {
        // builder.Services.AddValidatorsFromAssemblyContaining<SignIn>();

        return builder;
    }
}