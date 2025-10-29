# CleanArchitecture - Template CRUD API

A production-ready .NET 9.0 solution implementing Clean Architecture principles with a complete CRUD API for Template entities.

## 🏗️ Architecture

This project follows Clean Architecture principles with clear separation of concerns:

```
├── src/
│   ├── CleanArchitecture.Domain/          # Enterprise business rules
│   │   ├── Entities/                       # Domain entities
│   │   ├── Events/                         # Domain events
│   │   ├── Exceptions/                     # Domain exceptions
│   │   └── Interfaces/                     # Repository interfaces
│   │
│   ├── CleanArchitecture.Application/      # Application business rules
│   │   ├── Templates/
│   │   │   ├── Commands/                   # CQRS Commands
│   │   │   ├── Queries/                    # CQRS Queries
│   │   │   ├── DTOs/                       # Data Transfer Objects
│   │   │   └── Validators/                 # FluentValidation rules
│   │   └── Mappings/                       # AutoMapper profiles
│   │
│   ├── CleanArchitecture.Infrastructure/   # External concerns
│   │   ├── Data/                           # EF Core DbContext
│   │   ├── Repositories/                   # Repository implementations
│   │   └── Migrations/                     # Database migrations
│   │
│   └── CleanArchitecture.Web/              # Presentation layer
│       ├── Controllers/                    # API Controllers
│       └── Program.cs                      # Application startup
│
└── tests/
    ├── CleanArchitecture.UnitTests/        # Unit tests
    └── CleanArchitecture.IntegrationTests/ # Integration tests
```

## 🚀 Features

### Template Entity CRUD Operations

- **Create Template**: Add new templates with name and description
- **Read Template**: Retrieve single template by ID or list all templates
- **Update Template**: Modify existing template properties
- **Delete Template**: Remove templates from the system

### Technical Features

- **Clean Architecture**: Dependency rule enforcement with clear layer separation
- **CQRS Pattern**: Separate command and query responsibilities using MediatR
- **Validation**: FluentValidation for input validation
- **Mapping**: AutoMapper for object-object mapping
- **Domain Events**: Event-driven architecture support
- **Repository Pattern**: Abstraction over data access
- **Swagger/OpenAPI**: Complete API documentation
- **Unit Tests**: Comprehensive test coverage with xUnit, Moq, and FluentAssertions

## 🛠️ Technologies

- **.NET 9.0**
- **ASP.NET Core Web API**
- **Entity Framework Core 9.0**
- **MediatR** - CQRS implementation
- **FluentValidation** - Input validation
- **AutoMapper** - Object mapping
- **Swagger/Swashbuckle** - API documentation
- **xUnit** - Unit testing framework
- **Moq** - Mocking framework
- **FluentAssertions** - Assertion library

## 📋 Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- SQL Server or SQL Server LocalDB (for production)
- Your favorite IDE (Visual Studio 2022, VS Code, or Rider)

## 🔧 Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/maiconcardozo/CleanArchitecture.git
cd CleanArchitecture
```

### 2. Update the connection string

Edit `src/CleanArchitecture.Web/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=CleanArchitectureDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

### 3. Apply database migrations

```bash
cd src/CleanArchitecture.Infrastructure
dotnet ef database update --startup-project ../CleanArchitecture.Web
```

### 4. Run the application

```bash
cd src/CleanArchitecture.Web
dotnet run
```

The API will be available at `https://localhost:5001` (or the port shown in the console).

### 5. Access Swagger UI

Navigate to `https://localhost:5001/swagger` to explore and test the API endpoints.

## 🧪 Running Tests

### Run all tests

```bash
dotnet test
```

### Run only unit tests

```bash
dotnet test tests/CleanArchitecture.UnitTests/CleanArchitecture.UnitTests.csproj
```

### Run tests with coverage

```bash
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=lcov
```

## 📚 API Endpoints

### Templates

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/templates` | Get all templates |
| GET | `/api/templates/{id}` | Get template by ID |
| POST | `/api/templates` | Create new template |
| PUT | `/api/templates/{id}` | Update existing template |
| DELETE | `/api/templates/{id}` | Delete template |

### Example Requests

#### Create Template

```bash
curl -X POST https://localhost:5001/api/templates \
  -H "Content-Type: application/json" \
  -d '{
    "name": "My Template",
    "description": "A sample template"
  }'
```

#### Get All Templates

```bash
curl -X GET https://localhost:5001/api/templates
```

#### Update Template

```bash
curl -X PUT https://localhost:5001/api/templates/1 \
  -H "Content-Type: application/json" \
  -d '{
    "id": 1,
    "name": "Updated Template",
    "description": "Updated description",
    "isActive": true
  }'
```

## 🏛️ Project Structure Details

### Domain Layer

The Domain layer contains the core business logic and entities:

- **BaseEntity**: Base class for all entities with ID and domain events
- **Template Entity**: Core business entity with properties and business rules
- **Domain Events**: TemplateCreatedEvent, TemplateUpdatedEvent, TemplateDeletedEvent
- **Exceptions**: Custom domain exceptions like TemplateNotFoundException

### Application Layer

The Application layer orchestrates the application logic:

- **Commands**: CreateTemplateCommand, UpdateTemplateCommand, DeleteTemplateCommand
- **Queries**: GetTemplateQuery, GetTemplatesQuery
- **Handlers**: MediatR request handlers for each command/query
- **Validators**: FluentValidation rules for input validation
- **DTOs**: Data transfer objects for API responses

### Infrastructure Layer

The Infrastructure layer handles external concerns:

- **ApplicationDbContext**: EF Core database context
- **Repositories**: Implementation of domain repository interfaces
- **Configurations**: Entity configurations for EF Core
- **Migrations**: Database schema migrations

### Web Layer

The Presentation layer exposes the API:

- **Controllers**: RESTful API controllers
- **Middleware**: Error handling and request pipeline
- **Swagger**: API documentation configuration

## 🔒 Validation Rules

### Create/Update Template

- **Name**: Required, max 100 characters
- **Description**: Optional, max 500 characters
- **IsActive**: Boolean (default: true)

## 🎯 Design Patterns

- **Clean Architecture**: Dependency inversion and separation of concerns
- **CQRS**: Command Query Responsibility Segregation
- **Repository Pattern**: Data access abstraction
- **Mediator Pattern**: Decoupled request handling
- **Domain Events**: Event-driven architecture
- **Unit of Work**: Transaction management via EF Core

## 📦 NuGet Packages

### Core Dependencies
- Microsoft.EntityFrameworkCore (9.0.10)
- Microsoft.EntityFrameworkCore.SqlServer (9.0.10)
- MediatR (13.1.0)
- FluentValidation (11.11.0)
- AutoMapper (15.1.0)
- Swashbuckle.AspNetCore (9.0.6)

### Testing Dependencies
- xUnit (2.9.4)
- Moq (4.20.72)
- FluentAssertions (8.8.0)
- Microsoft.AspNetCore.Mvc.Testing (9.0.10)
- Microsoft.EntityFrameworkCore.InMemory (9.0.10)

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## 📄 License

This project is licensed under the MIT License.

## 👤 Author

**Maicon Cardozo**

- GitHub: [@maiconcardozo](https://github.com/maiconcardozo)

## 🙏 Acknowledgments

- Inspired by Clean Architecture principles by Robert C. Martin
- CQRS pattern implementation using MediatR
- Repository pattern for data access abstraction
