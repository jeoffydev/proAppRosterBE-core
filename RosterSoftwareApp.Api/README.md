# Roster App API

Check dotnet --version 7

# Run dotnet 

dotnet build
dotnet run

# Use postman to check APIs

## Starting SQL server

## Go to hub.docker.com 
Search for microsoft-mssql-server
Result and select: Microsoft SQL Server - Ubuntu based images

``` Powershell run the $sa_password as well
$sa_password="[SA PASSWORD HERE]"
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=$sa_password" -p 1433:1433 -v sqlvolumes:/var/opt/mssql -d --rm --name mssql mcr.microsoft.com/mssql/server:2022-latest
```

This is where to store our data files in docker to -v sqlVolume:/var/opt/mssql, --rm (remove) and 
--name rosterMssql for not getting a random name in container

# Secret manager setup for DB password to run into terminal
run first to get the secret ID:
dotnet user-secrets init 

``` run this
$sa_password="[SA PASSWORD HERE]"
dotnet user-secrets set "ConnectionStrings:RosterAppContext" "Server=localhost; Database=RosterAppDB; User Id=sa; Password=$sa_password; TrustServerCertificate=True"
```

Check if working:
dotnet user-secrets list

# Auth0 create token manually via terminal
dotnet user-jwts create --role "Admin/Member" --scope "roster:write/read"

# Install package for Entity Framework Core
dotnet add package Microsoft.EntityFrameworkCore.SqlServer

Tool for EF install it globally and check dotnet-ef --version 
dotnet tool install --global dotnet-ef

Add nuget package to scafold the migrations of EF: 
dotnet add package Microsoft.EntityFrameworkCore.Design

# run Migration EF
dotnet ef migrations add InitialCreate --output-dir Data/Migrations

dotnet ef database update

# Versioning
dotnet add package Asp.Versioning.Http
Check under the csproj
Then change into Endpoint

# API documentation and support for versioning
dotnet add package Swashbuckle.AspNetCore
dotnet add package Asp.Versioning.Mvc.ApiExplorer