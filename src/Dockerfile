FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY ["../src/Sepehr.WebApi/Sepehr.WebApi.csproj","Sepehr.WebApi.csproj/"]
COPY ["../src/Sepehr.Infrastructure.Shared/Sepehr.Infrastructure.Shared.csproj","Sepehr.Infrastructure.Shared.csproj/"]
COPY ["../src/Sepehr.Infrastructure.PersistenceLog/Sepehr.Infrastructure.PersistenceLog.csproj","Sepehr.Infrastructure.PersistenceLog.csproj/"]
COPY ["../src/Sepehr.Infrastructure.Persistence/Sepehr.Infrastructure.Persistence.csproj","Sepehr.Infrastructure.Persistence.csproj/"]
COPY ["../src/Sepehr.Infrastructure/Sepehr.Infrastructure.csproj","Sepehr.Infrastructure.csproj/"]
COPY ["../src/Sepehr.Domain/Sepehr.Domain.csproj","Sepehr.Domain.csproj/"]
COPY ["../src/Sepehr.Application/Sepehr.Application.csproj","Sepehr.Application.csproj/"]

RUN dotnet restore "Sepehr.WebApi.csproj"
RUN dotnet restore "Sepehr.Infrastructure.Shared.csproj"
RUN dotnet restore "Sepehr.Infrastructure.PersistenceLog.csproj"
RUN dotnet restore "Sepehr.Infrastructure.Persistence.csproj"
RUN dotnet restore "Sepehr.Infrastructure.csproj"
RUN dotnet restore "Sepehr.Domain.csproj"
RUN dotnet restore "Sepehr.Application.csproj"

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "Sepehr.WebApi.dll"]