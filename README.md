# Socially

Socially is a platform that encompasses all core characteristics of a modern social media platform. The project conforms to the clean architecture principles and strives to uphold best practices and ensure comprehensive test coverage.


## Built using the following technologies

- ASP.NET Core
- Entity Framework Core
- MediatR
- AutoMapper
- FluentValidation
- NUnit, FluentAssertions and Moq
- Docker
- React
- Vite
- Vitest

## Installation

### Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [Latest Node LTS](https://nodejs.org/en/)
- [Docker](https://www.docker.com/)

### Instructions

1) Clone the repository locally
2) Start the container ```docker-compose up -d```
3) Apply database migration (to create the tables) ```dotnet ef database update --project src/Infrastructure --startup-project src/Web```
4) Navigate to ```src/Web/ClientApp``` and run ```yarn install``` to install project dependencies
5) Navigate to ```src/Web/ClientApp``` and run ```yarn start``` to start development server
6) In a separate terminal run ``` ASPNETCORE_ENVIRONMENT=Development dotnet watch --project src/Web``` to launch back end with hot reloading (ASP.NET Core Web API)

**You can now access the following links**

- **Swagger UI** - [http://localhost:5000/swagger](http://localhost:5000/swagger)
- **Running Application** - [http://localhost:5000](http://localhost:5000)
