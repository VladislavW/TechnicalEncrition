FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["EncryptionApp.EncryptionService/EncryptionApp.EncryptionService.csproj", "EncryptionApp.EncryptionService/"]
RUN dotnet restore "EncryptionApp.EncryptionService/EncryptionApp.EncryptionService.csproj"
COPY . .
WORKDIR "/src/EncryptionApp.EncryptionService"
RUN dotnet build "EncryptionApp.EncryptionService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EncryptionApp.EncryptionService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EncryptionApp.EncryptionService.dll"]
