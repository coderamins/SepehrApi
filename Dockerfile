
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Sepehr.WebApi/Sepehr.WebApi.csproj", "Sepehr.WebApi/"]
COPY ["src/Sepehr.Application/Sepehr.Application.csproj", "Sepehr.Application/"]
COPY ["src/Sepehr.Domain/Sepehr.Domain.csproj", "Sepehr.Domain/"]
COPY ["src/Sepehr.Infrastructure.Persistence/Sepehr.Infrastructure.Persistence.csproj", "Sepehr.Infrastructure.Persistence/"]
COPY ["src/Sepehr.Infrastructure/Sepehr.Infrastructure.csproj", "Sepehr.Infrastructure/"]
COPY ["src/Sepehr.Infrastructure.PersistenceLog/Sepehr.Infrastructure.PersistenceLog.csproj", "Sepehr.Infrastructure.PersistenceLog/"]
COPY ["src/Sepehr.Infrastructure.Shared/Sepehr.Infrastructure.Shared.csproj", "Sepehr.Infrastructure.Shared/"]

RUN dotnet restore "Sepehr.WebApi/Sepehr.WebApi.csproj"
COPY . ../
WORKDIR /src/Sepehr.WebApi
RUN dotnet build "Sepehr.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish --no-restore -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
ENV ASPNETCORE_HTTP_PORTS=3000
EXPOSE 3000
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sepehr.WebApi.dll"]