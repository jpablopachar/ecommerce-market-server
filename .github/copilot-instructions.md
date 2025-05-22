# GitHub Copilot Instructions

## Project Overview
This project is an e-commerce market server application built with .NET 8.0. It implements a clean architecture pattern with distinct layers for separation of concerns:
- WebApi: API controllers and endpoints
- BusinessLogic: Services, repositories, and database context
- Core: Domain entities, interfaces, and DTOs

The application provides RESTful API endpoints for managing e-commerce operations including product catalog, user management, shopping cart, and order processing.

## Package List
- Microsoft.EntityFrameworkCore (8.0.16): ORM for database interactions
- Microsoft.EntityFrameworkCore.SqlServer (8.0.16): SQL Server provider for EF Core
- Microsoft.AspNetCore.Identity (2.3.1): Authentication and user management
- Microsoft.AspNetCore.Identity.EntityFrameworkCore (8.0.16): EF Core integration for Identity
- Microsoft.AspNetCore.Authentication.JwtBearer (8.0.16): JWT authentication middleware
- System.IdentityModel.Tokens.Jwt (8.10.0): JWT token generation and validation
- Microsoft.IdentityModel.Tokens (8.10.0): Security token handling
- AutoMapper.Extensions.Microsoft.DependencyInjection (12.0.0): Object-to-object mapping
- StackExchange.Redis (2.8.37): Redis client for distributed caching

## Architecture Guidelines
1. **Clean Architecture**: Follow the onion/clean architecture pattern with clear separation of concerns
   - Core Layer: Contains domain entities, interfaces, and business rules
   - Business Logic Layer: Implements services, repositories, and database interactions
   - Web API Layer: Handles HTTP requests, validation, and controllers

2. **SOLID Principles**: Adhere to SOLID principles throughout the codebase
   - Single Responsibility: Each class should have only one reason to change
   - Open/Closed: Open for extension, closed for modification
   - Liskov Substitution: Derived classes must be substitutable for their base classes
   - Interface Segregation: Clients should not depend on interfaces they don't use
   - Dependency Inversion: Depend on abstractions, not concretions

3. **Repository Pattern**: Use repositories to abstract data access logic
   - Implement generic repository for common CRUD operations
   - Create specific repositories for complex domain-specific queries

4. **Unit of Work Pattern**: Implement Unit of Work to manage transactions
   - Use a single context for multiple repositories to ensure atomic operations
   - Commit changes through the Unit of Work interface

5. **Service Layer**: Business logic should be encapsulated in service classes
   - Services should depend on repositories through interfaces
   - Keep controllers thin by delegating business logic to services

## Folder Structure
```
ecommerce-market-server/
├── .github/                    # GitHub specific files and instructions
├── Core/                       # Domain entities and interfaces
│   ├── Entities/               # Domain models
│   ├── Interfaces/             # Interfaces for services and repositories
│   ├── Specifications/         # Query specifications
│   └── DTOs/                   # Data Transfer Objects
├── BusinessLogic/              # Implementation of business logic
│   ├── Data/                   # Database context and migrations
│   ├── Services/               # Service implementations
│   ├── Repositories/           # Repository implementations
│   └── Infrastructure/         # Cross-cutting concerns
├── WebApi/                     # API controllers and configuration
│   ├── Controllers/            # API endpoints
│   ├── Extensions/             # Extension methods for services configuration
│   ├── Middleware/             # Custom middleware components
│   ├── Filters/                # Action and exception filters
│   ├── Helpers/                # Helper classes
│   └── Mapping/                # AutoMapper profiles
└── Tests/                      # Test projects
    ├── UnitTests/              # Unit tests
    └── IntegrationTests/       # Integration tests
```

## Code Style Guidelines

### Naming Conventions
- **Classes/Interfaces**: PascalCase (e.g., `ProductService`, `IRepository`)
- **Methods**: PascalCase (e.g., `GetProductById`, `CreateOrder`)
- **Variables/Parameters**: camelCase (e.g., `productItem`, `userId`)
- **Private Fields**: _camelCase (e.g., `_dbContext`, `_logger`)
- **Constants**: UPPER_CASE (e.g., `MAX_RETRY_COUNT`, `DEFAULT_PAGE_SIZE`)
- **Interfaces**: Prefix with "I" (e.g., `IProductService`, `IRepository`)
- **DTOs**: Suffix with "Dto" (e.g., `ProductDto`, `CreateOrderDto`)
- **Controllers**: Suffix with "Controller" (e.g., `ProductsController`, `OrdersController`)
- **Extension Methods**: Place in classes named with suffix "Extensions" (e.g., `ServiceCollectionExtensions`)

### Documentation
- **XML Documentation**: All public APIs must have XML documentation in Spanish
  ```csharp
  /// <summary>
  /// Obtiene un producto por su identificador único.
  /// </summary>
  /// <param name="id">El identificador único del producto</param>
  /// <returns>El producto si existe, o null si no se encuentra</returns>
  /// <exception cref="ArgumentException">Se lanza cuando el id es inválido</exception>
  ```
- **README**: Maintain comprehensive README files in Spanish for each project
- **Comments**: Use line and block comments in Spanish to explain complex logic
- **Code Examples**: Include code examples in documentation where appropriate
- **Commit Messages**: Follow conventional commit format (see commit-message.instructions.md)

## Testing Guidelines
- **Unit Tests**: Write unit tests for all business logic
- **Integration Tests**: Cover API endpoints and database operations
- **Test Naming**: Use descriptive names in format `MethodName_Scenario_ExpectedBehavior`
- **AAA Pattern**: Structure tests with Arrange, Act, Assert sections
- **Mock Dependencies**: Use mocking frameworks for dependencies in unit tests

## Important Note
- While the codebase and instructions are in English, all documentation, comments, and user-facing text should be maintained in Spanish.
- XML documentation comments must be written in Spanish.
- Follow conventional commit format for all commit messages as defined in commit-message.instructions.md.