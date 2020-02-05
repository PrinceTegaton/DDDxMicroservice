YOU ACTUALLY CAME TO READ ME. THOU SHALL PASS
==================================================

Author: Prince Tegaton @princetegaton
Date: Feb 4, 2020
Architecture: Microservice
Pattern: Domain-Driven-Design
Platform: ASPNET-CORE 2.2
Database: MSSQL Server
Test: NUnit (with InMemory and SQLite database)

NOTE
==================================================
This template encapsulate the burden of creating new projects based on Microservice and DDD with a power-packed design.
It is meant to be light, plug-and-play, all you do is focus on writing core application logic and maybe tweak the default services configuration to you taste - which you should obviously do.

Here are a list of what you'll see in this template

- RextHttpClient for HttpCalls @ https://princetegaton.github.io/RextHttpClient
- HealthChecks (SQL Server and custom service checks)
- ExceptionMiddleware (Handles all exception on Controller level without Try...Catch)
- Client Authentication (With the use of ClientKeys to restrict access by default)
- SimpleRepository (a clean generic repository built on EFCore, no UnitOfWork as EFCore handles the co-ordination already)
- AppDbContext (With a generic BaseModel, and configuration to ignore (soft) deleted records)
- UnitTest (With NUnit test, this is already wired to use InMemoryDatabase and SQLiteDatabase to run fast unit tests)
- Swagger (With full configuration which include apikey auth)
- Helper methods
- ExceptionHelper methods


PROJECT STRUCTURE
==================================================
- DDD.Api
	- Controllers
	- Diagnostics
	- Authentication
	- Extensions
- DDD.Core
	- Managers (Add business login here)
- DDD.Domain
	- Models
	- ViewModels
	- Helpers
- DDD.Infrastructure
	- DataAccess
	- Extensions
	- Repository
- DDD.UnitTest
	- Tests


REFERENCES
==================================================
https://github.com/PrinceTegaton/DDDxMicroservice
https://github.com/PrinceTegaton/SimpleClientAuth/blob/master/Startup.cs
https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/ddd-oriented-microservice
https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-3.1&tabs=visual-studio
https://stackoverflow.com/questions/51668952/debugging-swashbuckle-error-failed-to-load-api-definition