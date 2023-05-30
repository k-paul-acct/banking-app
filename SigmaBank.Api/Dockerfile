FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SigmaBank.Api/SigmaBank.Api.csproj", "SigmaBank.Api/"]
RUN dotnet restore "SigmaBank.Api/SigmaBank.Api.csproj"
COPY . .
WORKDIR "/src/SigmaBank.Api"
RUN dotnet build "SigmaBank.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SigmaBank.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SigmaBank.Api.dll"]
