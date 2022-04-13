FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["EncryptionApp.ApiGateway/EncryptionApp.ApiGateway.csproj", "EncryptionApp.ApiGateway/"]
RUN dotnet restore "EncryptionApp.ApiGateway/EncryptionApp.ApiGateway.csproj"
COPY . .
WORKDIR "/src/EncryptionApp.ApiGateway"
RUN dotnet build "EncryptionApp.ApiGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EncryptionApp.ApiGateway.csproj" -c Release -o /app/publish
RUN dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\aspnetapp.pfx -p password
RUN dotnet dev-certs https --trust

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EncryptionApp.ApiGateway.dll"]
