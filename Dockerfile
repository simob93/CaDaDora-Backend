# Step 1 - The Build Environment #

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# copy all the layers' csproj files into respective folders

COPY ["src/CaDaDora.Application.Contracts/CaDaDora.Application.Contracts.csproj", "CaDaDora.Application.Contracts/"]
COPY ["src/CaDaDora.Application/CaDaDora.Application.csproj", "CaDaDora.Application/"]
COPY ["src/CaDaDora.AuthServer/CaDaDora.AuthServer.csproj", "CaDaDora.AuthServer/"]
COPY ["src/CaDaDora.Domain/CaDaDora.Domain.csproj", "CaDaDora.Domain/"]
COPY ["src/CaDaDora.Domain.Shared/CaDaDora.Domain.Shared.csproj", "CaDaDora.Domain.Shared/"]
COPY ["src/CaDaDora.EntityFrameworkCore/CaDaDora.EntityFrameworkCore.csproj", "CaDaDora.EntityFrameworkCore/"]
COPY ["src/CaDaDora.HttpApi/CaDaDora.HttpApi.csproj", "CaDaDora.HttpApi/"]
COPY ["src/CaDaDora.HttpApi.Client/CaDaDora.HttpApi.Client.csproj", "CaDaDora.HttpApi.Client/"]
COPY ["src/CaDaDora.HttpApi.Host/CaDaDora.HttpApi.Host.csproj", "CaDaDora.HttpApi.Host/"]
COPY ["src/CaDaDora.DbMigrator/CaDaDora.DbMigrator.csproj", "CaDaDora.DbMigrator/"]

WORKDIR ..

COPY ["test/CaDaDora.Application.Tests/CaDaDora.Application.Tests.csproj", "test/CaDaDora.Application.Tests/"]
COPY ["test/CaDaDora.Domain.Tests/CaDaDora.Domain.Tests.csproj", "test/CaDaDora.Domain.Tests/"]
COPY ["test/CaDaDora.EntityFrameworkCore.Tests/CaDaDora.EntityFrameworkCore.Tests.csproj", "test/CaDaDora.EntityFrameworkCore.Tests/"]
COPY ["test/CaDaDora.HttpApi.Client.ConsoleTestApp/CaDaDora.HttpApi.Client.ConsoleTestApp.csproj", "test/CaDaDora.HttpApi.Client.ConsoleTestApp/"]
COPY ["test/CaDaDora.TestBase/CaDaDora.TestBase.csproj", "test/CaDaDora.TestBase/"]

COPY CaDaDora.sln ./  

# run restore over API project - this pulls restore over the dependent projects as well
#RUN dotnet restore "src/ContainerNinja.API/ContainerNinja.API.csproj"

RUN dotnet restore

#Copy all the source code into the Build Container
COPY . .

# Run dotnet publish in the Build Container
# Generates output available in /app/build
# Since the current directory is /app

RUN dotnet build -c Release -o /app/build

# run publish over the API project
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish  .
ENTRYPOINT ["dotnet", "CaDaDora.HttpApi.Host.dll"]
