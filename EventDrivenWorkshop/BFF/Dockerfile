#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["BFF/BFF.csproj", "BFF/"]
RUN dotnet restore "BFF/BFF.csproj"
COPY . .
WORKDIR "/src/BFF"
RUN dotnet build "BFF.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BFF.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BFF.dll"]