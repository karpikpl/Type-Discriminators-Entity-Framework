# About

Type Discriminators in Entity Framework 8.

## Using Type Discriminators

[See EF Documentation](https://learn.microsoft.com/en-us/ef/core/modeling/inheritance#table-per-hierarchy-and-discriminator-configuration)

## Running the sample

1. Run SQL Server in Docker container `docker run -it --rm -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=yourStrongPassword1" -p 1433:1433 mcr.microsoft.com/mssql/server:2022-latest`
2. Run the project with `dotnet run`