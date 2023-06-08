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

Ddd where to store our data files in docker to -v sqlVolume:/var/opt/mssql, --rm (remove) and 
--name rosterMssql for not getting a random name in container

