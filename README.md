# DDDxMicroservice
A power-packed template for asp.net microservices using DDD pattern

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
