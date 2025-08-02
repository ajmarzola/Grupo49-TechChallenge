# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0.100 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["src/FCG.API/FCG.API.csproj", "FCG.API/"]
COPY ["src/FCG.Application/FCG.Application.csproj", "FCG.Application/"]
COPY ["src/FCG.Domain/FCG.Domain.csproj", "FCG.Domain/"]
COPY ["src/FCG.Infrastructure/FCG.Infrastructure.csproj", "FCG.Infrastructure/"]


RUN dotnet restore "FCG.API/FCG.API.csproj"

COPY ./src .
WORKDIR /src/FCG.API
RUN dotnet build "FCG.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Etapa de publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "FCG.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Etapa final (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8080

ENTRYPOINT ["dotnet", "FCG.API.dll"]
