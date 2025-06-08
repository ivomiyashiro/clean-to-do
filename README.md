# Clean Architecture To-Do Application

## Overview
A modern To-Do application built using Clean Architecture principles, following SOLID principles and Domain-Driven Design (DDD) practices.

## Architecture Layers

### Domain
The Core (or Domain) layer is the innermost layer and contains your application's business rules. It should have no dependencies on other layers. 

```bash
dotnet new classlib -n To-Do.Domain
dotnet sln add To-Do.Domain
```

#### Key Components
- **Entities**: Core business objects with unique identity
- **Value Objects**: Immutable objects defined by their attributes
- **Domain Events**: Events that occur within the domain
- **Interfaces**: Contracts for external services
- **Enums**: Domain-specific enumerations
- **Exceptions**: Domain-specific exceptions

### Application
The Application layer contains application-specific business rules, use cases, and orchestrates the flow of data. It defines the application's boundaries and serves as an entry point for interacting with the domain.

```bash
dotnet new classlib -n To-Do.Application
dotnet sln add To-Do.Application
dotnet add To-Do.Application reference To-Do.Domain
```

#### Key Components
- **Application Services**: Orchestrate use cases
- **Use Cases**: 
  - Command Handlers (CQRS pattern)
  - Query Handlers (CQRS pattern)
  - Validators (FluentValidation)
- **DTOs**: Data Transfer Objects for API contracts
- **Interfaces**: For external services
- **Mappings**: AutoMapper profiles
- **Behaviors**: MediatR pipeline behaviors

### Infrastructure
The Infrastructure layer provides the implementation details for interfaces defined in the Core and Application layers.

```bash
dotnet new classlib -n To-Do.Infrastructure
dotnet sln add To-Do.Infrastructure
dotnet add To-Do.Infrastructure reference To-Do.Domain
dotnet add To-Do.Infrastructure reference To-Do.Application
```

#### Key Components
- **Persistence**: 
  - Entity Framework Core configurations
  - Repositories implementation
  - Database migrations
- **External Services**: 
  - Email service
  - File storage
  - Third-party API clients
- **Logging**: Serilog implementation
- **Authentication**: JWT implementation
- **Caching**: Redis implementation
- **Background Jobs**: Hangfire implementation

### Presentation
The Presentation layer contains both the API and the React frontend application.

```bash
dotnet new webapi -n To-Do.Presentation
dotnet sln add To-Do.Presentation
dotnet add To-Do.Presentation reference To-Do.Application
dotnet add To-Do.Presentation reference To-Do.Infrastructure
```

#### Key Components
- **API**:
  - Controllers: RESTful endpoints
  - Middleware: Exception handling, request logging, API versioning
  - Filters: Action filters
  - Configuration: App settings
  - Dependency Injection: Service registration
- **Frontend**:
  - React with TypeScript
  - Vite for build tooling
  - Modern UI components
  - State management
  - API integration

## Getting Started

### Prerequisites
- .NET 8.0 SDK
- SQL Server (or your preferred database)
- Visual Studio 2022 / VS Code

### Setup
1. Clone the repository
2. Update connection strings in `appsettings.json`
3. Run database migrations
4. Start the application

### Development
```bash
# Run the application
dotnet run --project To-Do.Presentation

# Run tests
dotnet test

# Build the solution
dotnet build
```

## Best Practices
- Follow CQRS pattern for better separation of concerns
- Use MediatR for implementing CQRS
- Implement proper validation using FluentValidation
- Use AutoMapper for object mapping
- Implement proper logging with Serilog
- Use proper exception handling
- Implement proper API versioning
- Use proper security measures (JWT, HTTPS)
- Implement proper caching strategies
- Use proper database indexing

## Testing
- Unit Tests: xUnit
- Integration Tests: WebApplicationFactory
- E2E Tests: Playwright

## CI/CD
- GitHub Actions for CI/CD
- Docker support
- Azure DevOps pipelines

## Contributing
1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a Pull Request

## License
MIT License