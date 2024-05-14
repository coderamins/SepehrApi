<<<<<<< HEAD
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["./src/Sepehr.WebApi/Sepehr.WebApi.csproj","Sepehr.WebApi.csproj/"]
COPY ["./src/Sepehr.Infrastructure.Shared/Sepehr.Infrastructure.Shared.csproj","Sepehr.Infrastructure.Shared.csproj/"]
COPY ["./src/Sepehr.Infrastructure.PersistenceLog/Sepehr.Infrastructure.PersistenceLog.csproj","Sepehr.Infrastructure.PersistenceLog.csproj/"]
COPY ["./src/Sepehr.Infrastructure.Persistence/Sepehr.Infrastructure.Persistence.csproj","Sepehr.Infrastructure.Persistence.csproj/"]
COPY ["./src/Sepehr.Infrastructure/Sepehr.Infrastructure.csproj","Sepehr.Infrastructure.csproj/"]
COPY ["./src/Sepehr.Domain/Sepehr.Domain.csproj","Sepehr.Domain.csproj/"]
COPY ["./src/Sepehr.Application/Sepehr.Application.csproj","Sepehr.Application.csproj/"]

# run restore over API project - this pulls restore over the dependent projects as well
RUN dotnet restore "/src/Sepehr.WebApi/Sepehr.WebApi.csproj"

COPY . .

# run build over the API project
WORKDIR "/src/Sepehr.WebApi/"
RUN dotnet build -c Release -o /app/build

# run publish over the API project
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS runtime
WORKDIR /app

COPY --from=publish /app/publish .
RUN ls -l
ENTRYPOINT [ "dotnet", "Sepehr.WebApi.dll" ]
=======
# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["../src/Sepehr.WebApi/Sepehr.WebApi.csproj","Sepehr.WebApi.csproj/"]
COPY ["../src/Sepehr.Infrastructure.Shared/Sepehr.Infrastructure.Shared.csproj","Sepehr.Infrastructure.Shared.csproj/"]
COPY ["../src/Sepehr.Infrastructure.PersistenceLog/Sepehr.Infrastructure.PersistenceLog.csproj","Sepehr.Infrastructure.PersistenceLog.csproj/"]
COPY ["../src/Sepehr.Infrastructure.Persistence/Sepehr.Infrastructure.Persistence.csproj","Sepehr.Infrastructure.Persistence.csproj/"]
COPY ["../src/Sepehr.Infrastructure/Sepehr.Infrastructure.csproj","Sepehr.Infrastructure.csproj/"]
COPY ["../src/Sepehr.Domain/Sepehr.Domain.csproj","Sepehr.Domain.csproj/"]
COPY ["../src/Sepehr.Application/Sepehr.Application.csproj","Sepehr.Application.csproj/"]

RUN dotnet restore "Sepehr.WebApi.csproj"
RUN dotnet restore "Sepehr.WebApi.csproj"
RUN dotnet restore "Sepehr.Infrastructure.Shared.csproj"
RUN dotnet restore "Sepehr.Infrastructure.PersistenceLog.csproj"
RUN dotnet restore "Sepehr.Infrastructure.Persistence.csproj"
RUN dotnet restore "Sepehr.Infrastructure.csproj"
RUN dotnet restore "Sepehr.Domain.csproj"
RUN dotnet restore "Sepehr.Application.csproj"

# Stage 2: Create the final image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app/src

# Copy published application files from the build stage
#COPY --from=build /app/src/publish .
RUN dotnet publish "./src/Sepehr.WebApi/Sepehr.WebApi.csproj" -c Release 

# Expose the port your application uses (replace 5000 with your actual port)
EXPOSE 5000

# Set the entry point to run your application
ENTRYPOINT ["dotnet", "Sepehr.WebApi.dll"]
>>>>>>> 3d139d23851805bb0c61fef48e098aa9fcec8218
