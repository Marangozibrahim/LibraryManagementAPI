# Library Management API

A clean, scalable, and testable ASP.NET Core Web API for managing a library system. Built with Onion Architecture, Entity Framework Core, Redis caching, JWT authentication, FluentValidation, MediatR, and Serilog logging. Fully containerized with Docker and tested with xUnit and Moq.

---

## Features

- **Complete CRUD Operations** - Books, Authors, Categories and Borrows management
- **JWT Authentication** - Secure token-based authorization
- **Redis Caching** - High-performance distributed caching with cache-aside pattern
- **CQRS Pattern** - Command and Query separation using MediatR
- **Result Pattern** - Type-safe error handling without exceptions
- **Specification Pattern** - Reusable, composable query logic
- **Request Validation** - Automated validation with FluentValidation
- **Structured Logging** - Comprehensive logging with Serilog
- **Fully Tested** - Unit and integration tests with xUnit, Moq, and FluentAssertions
- **Docker Ready** - Complete containerization with Docker Compose
- **Clean Architecture** - Maintainable and scalable onion architecture

---

## Tech Stack

| Layer                | Technologies                   |
| -------------------- | ------------------------------ |
| **Framework**        | ASP.NET Core 9.0               |
| **Architecture**     | Onion Architecture, CQRS       |
| **Design Patterns**  | Result & Specification Pattern |
| **ORM**              | Entity Framework Core          |
| **Database**         | MS SQL Server                  |
| **Caching**          | Redis                          |
| **Authentication**   | JWT Bearer Tokens              |
| **Validation**       | FluentValidation               |
| **Mediator**         | MediatR                        |
| **Logging**          | Serilog                        |
| **Testing**          | xUnit, Moq, FluentAssertions   |
| **Containerization** | Docker, Docker Compose         |

---

## Quick Start

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop) (for containerized setup)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (if running locally without Docker)

### Option 1: Run with Docker (Recommended)

```bash
# Clone the repository
git clone https://github.com/Marangozibrahim/LibraryManagementAPI.git
cd LibraryManagementAPI

# Start all services
docker-compose up --build
```

**Services will be available at:**

- API: `http://localhost:8080`
- Swagger: `http://localhost:8080/swagger`
- SQL Server: `localhost:1433`
- Redis: `localhost:6379`
- Redis Commander: `http://localhost:8181`

### Option 2: Run Locally

```bash
# Restore dependencies
dotnet restore

# Update database
cd Library.API
dotnet ef database update

# Run the application
dotnet run
```

**Access Swagger UI:** `http://localhost:8080/swagger`

---

## Configuration

Create `appsettings.Development.json` in the `Library.API` project:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=LibraryDb;User Id=sa;Password=YourStrongPassword123;TrustServerCertificate=True"
  },
  "Jwt": {
    "Key": "YourSuperSecretKeyAtLeast32CharactersLong",
    "Issuer": "LibraryAPI",
    "Audience": "LibraryUsers",
    "ExpirationMinutes": 60
  },
  "Redis": {
    "ConnectionString": "localhost:6379",
    "InstanceName": "LibraryCache:"
  },
  "Serilog": {
    "MinimumLevel": "Information"
  }
}
```

---

## API Endpoints

### Authentication

```
POST   /api/auth/register       Register new user
POST   /api/auth/login          Login and get JWT token
POST   /api/Auth/refresh        Exchange a valid refresh token for a new access token
```

### Books

```
GET    /api/books               Get books (with optional filtering/sorting/pagination)
GET    /api/books/{id}          Get book by ID
POST   /api/Books               Create new book
PUT    /api/Books/{id}          Update book
DELETE /api/Books/{id}          Delete book
```

### Borrows

```
GET    /api/borrows             Get all borrows (admin) or current user's borrows
GET    /api/borrows/{id}        Get specific borrow record
POST   /api/borrows             Create new borrow (borrow a book)
PUT    /api/borrows/return/{id} Return book
DELETE /api/borrows/{id}        Delete borrow record
```

### Authors

```
GET    /api/authors             Get all authors
GET    /api/authors/{id}        Get author by ID
POST   /api/authors             Create new author
PUT    /api/authors/{id}        Update author
DELETE /api/authors/{id}        Delete author
```

### Categories

```
GET    /api/categories          Get all categories
GET    /api/categories/{id}     Get category by ID
POST   /api/categories          Create new category
PUT    /api/categories/{id}     Update category
DELETE /api/categories/{id}     Delete category
```

---

## Testing

Run all tests:

```bash
dotnet test
```

---

## Caching Strategy

**Cache-Aside Pattern Implementation:**

1. **Read Operation:**

   - Check Redis cache first
   - If miss → fetch from database → store in cache
   - Return data

2. **Write Operations:**

   - Invalidate related cache entries
   - Ensure cache consistency

---

## Logging

**Serilog Configuration:**

- Console sink for development
- File sink for production (`logs/log-.txt`)
- Structured logging with request/response details
- Performance metrics tracking

**Example Log Output:**

```
[2025-01-15 10:23:45 INF] HTTP GET /api/books completed with 200 in 42.5ms
[2025-01-15 10:23:46 INF] Cache hit for key: books:all
```

---

## Docker Services

The `docker-compose.yml` includes:

- **API Container** - The ASP.NET Core application
- **SQL Server Container** - Database with persistent volume
- **Redis Container** - Caching layer
- **Redis Commander Container** - Redis management tool

```bash
# Start services
docker-compose up -d

# View logs
docker-compose logs -f api

# Stop services
docker-compose down
```

---

## Author

**İbrahim Marangoz**

- GitHub: [@Marangozibrahim](https://github.com/Marangozibrahim)
- Email: marangozibrahim49@gmail.com
