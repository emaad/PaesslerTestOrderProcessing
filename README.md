\## README.md

```markdown

\# Order Processing Microservice



\## Overview

This is a .NET 9 microservice that uses Domain-Driven Design (DDD) principles for order processing. The service provides REST endpoints to create and retrieve orders.



\### Architectural Decisions

\- \*\*DDD Layers\*\*: Separates Domain, Application, Infrastructure, and Presentation (API) layers for clear structure and single responsibility.

\- \*\*MediatR (Mediator Pattern)\*\*: Keeps controllers separate from business logic by sending commands and queries to handlers. This enables behaviors like validation and logging.

\- \*\*FluentValidation\*\*: Checks commands early to reject invalid requests before executing business logic.

\- \*\*Entity Framework Core\*\*: Offers ORM capabilities, using SQL Server in production and In-Memory for tests. It also uses owned entities to model value objects.

\- \*\*Serilog\*\*: Provides structured logging to files and Azure Application Insights for monitoring and troubleshooting.

\- \*\*Swagger/OpenAPI\*\*: Automatically generates interactive API documentation for easy exploration and testing of endpoints.



\## Getting Started

1\. \*\*Clone the repository\*\*

2\. \*\*Update `appsettings.json`\*\* with your SQL Server connection string.

3\. \*\*Run migrations\*\*:

&nbsp;  ```bash

&nbsp;  dotnet tool install --global dotnet-ef

&nbsp;  dotnet ef database update --project src/OrderProcessing.Infrastructure --startup-project src/OrderProcessing.Api

&nbsp;  ```

4\. \*\*Run the API\*\*:

&nbsp;  ```bash

&nbsp;  cd src/OrderProcessing.Api

&nbsp;  dotnet run

&nbsp;  ```

5\. \*\*Access Swagger UI\*\* at `http://localhost:5025/swagger`



\## Testing

\- \*\*Unit tests\*\*: `dotnet test src/OrderProcessing.Tests/Unit`  

\- \*\*Integration tests\*\*: `dotnet test src/OrderProcessing.Tests/Integration`

```

