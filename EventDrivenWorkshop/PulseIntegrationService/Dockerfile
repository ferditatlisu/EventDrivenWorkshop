#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["PulseIntegrationService/PulseIntegrationService.csproj", "PulseIntegrationService/"]
RUN dotnet restore "PulseIntegrationService/PulseIntegrationService.csproj"
COPY . .
WORKDIR "/src/PulseIntegrationService"
RUN dotnet build "PulseIntegrationService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PulseIntegrationService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PulseIntegrationService.dll"]