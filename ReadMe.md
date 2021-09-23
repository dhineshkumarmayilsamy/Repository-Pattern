#### .NET 5.0

### Repository pattern - .NET Core API Implementation

* ORM - EFCore - Db First
* Database - MySQL 
* Logger - Serilog
* Test - xunit, Moq & Fluent Assertions

### Command
* MySQL - Scaffold

      dotnet ef dbcontext scaffold "server=localhost; port=3306; database=test; uid=test; password=test" Pomelo.EntityFrameworkCore.MySql -o Model -c AppDbContext -f --no-onconfiguring



