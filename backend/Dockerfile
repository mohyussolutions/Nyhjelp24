# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["backend/src/API/API.csproj", "API/"]
COPY ["backend/src/Core/Core.csproj", "Core/"]
RUN dotnet restore "API/API.csproj"
COPY ["backend/src/", "."]
WORKDIR /src/API
RUN dotnet build "API.csproj" -c Release -o /app/build

# Publish Stage
FROM build AS publish
RUN dotnet publish "API.csproj" -c Release -o /app/publish

# Run Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.dll"]
