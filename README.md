## README.md
```markdown
# Order Processing Microservice

## Overview
This is a .NET 9 microservice implementing Domain-Driven Design (DDD) principles for order processing. The service exposes REST endpoints for creating and retrieving orders.

### Architectural Decisions
- **DDD Layers**: Separates Domain, Application, Infrastructure, and Presentation (API) layers to maintain a clear structure and single responsibility.
- **MediatR (Mediator Pattern)**: Decouples controllers from business logic by sending commands/queries to handlers, enabling pipeline behaviors (validation, logging).
- **FluentValidation**: Validates commands early, ensuring invalid requests are rejected before business logic executes.
- **Entity Framework Core**: Provides ORM capabilities; uses SQL Server in production and In-Memory for tests, with owned entities to model value objects.
- **Serilog**: Offers structured logging to files/Azure Application Insights for monitoring and troubleshooting.
- **Swagger/OpenAPI**: Auto-generates interactive API docs for easy exploration and testing of endpoints.

## Getting Started
1. **Clone the repository**
2. **Update `appsettings.json`** with your SQL Server connection string
3. **Run migrations**:
   ```bash
   dotnet tool install --global dotnet-ef
   dotnet ef database update --project src/OrderProcessing.Infrastructure --startup-project src/OrderProcessing.Api
   ```
4. **Run the API**:
   ```bash
   cd src/OrderProcessing.Api
   dotnet run
   ```
5. **Access Swagger UI** at `http://localhost:5025/swagger`

## Testing
- **Unit tests**: `dotnet test src/OrderProcessing.Tests/Unit`  
- **Integration tests**: `dotnet test src/OrderProcessing.Tests/Integration`
```