# Vehicle Bidding Application - Backbone API

A comprehensive ASP.NET Core backend API for a real-time vehicle bidding auction platform built with clean architecture principles, Entity Framework Core, JWT authentication, and SignalR for real-time updates.

## Project demo

[Demo_Video_Link](https://drive.google.com/file/d/1ABw3LMAHKHIbqwpjAj-g2-5lP9ouV7zA/view?usp=sharing)

## Table of Contents

- [Project Overview](#project-overview)
- [Architecture](#architecture)
- [Technologies Stack](#technologies-stack)
- [Project Structure](#project-structure)
- [Database Schema](#database-schema)
- [API Endpoints](#api-endpoints)
- [Getting Started](#getting-started)
- [Configuration](#configuration)
- [Authentication](#authentication)
- [Real-Time Features](#real-time-features)
- [Testing](#testing)

## Project Overview

The Vehicle Bidding Application is a REST API platform that enables users to participate in real-time vehicle auctions. It features:

- **User Management**: Admin and Dealer roles with secure authentication
- **Vehicle Catalog**: Browse and manage vehicles with detailed specifications
- **Real-Time Bidding**: Live auction sessions with real-time bid updates via SignalR
- **Bidding Sessions**: Create and manage auction sessions with customizable parameters
- **User Reports**: Track bidding history and user activity

### Key Features

- JWT-based authentication and authorization
- Real-time bidding updates using SignalR
- Role-based access control (Admin, Dealer)
- User budget tracking
- Vehicle status management
- Bidding session lifecycle management
- Comprehensive error handling and validation
- Pagination support for queries

## Architecture

This project follows the **Clean Architecture** pattern organized in 5 logical layers:

```
1. Domain Layer (BiddingApp.Domain)
   ├── Entities
   ├── Enums
   ├── Models
   └── EF Configurations

2. Infrastructure Layer (BiddingApp.Infrastructure + BiddingApp.BuildingBlock)
   ├── Repositories
   ├── DTOs
   ├── Pagination
   ├── Extensions
   └── Exceptions

3. Application Layer (BiddingApp.Application)
   ├── Services
   ├── SignalR Hubs
   └── Business Logic

4. API Layer (BiddingApp.Api)
   ├── Controllers
   ├── Middleware
   └── Configuration

5. Tests (BiddingApp.ApplicationTest)
   ├── Unit Tests
   └── Service Tests
```

## Technologies Stack

### Core Framework

- **ASP.NET Core 8.0**: Latest LTS framework
- **Entity Framework Core 8.0**: ORM for data access
- **SQL Server**: Database engine
- **SignalR**: Real-time communication

### Authentication & Authorization

- **JWT (JSON Web Tokens)**: Token-based authentication
- **Microsoft.AspNetCore.Authentication.JwtBearer**: JWT bearer authentication

### API & Documentation

- **Swagger/Swashbuckle 6.4.0**: API documentation and exploration

### Testing

- Unit testing framework for service layer validation

### Other

- **Docker**: Containerization support
- **Dependency Injection**: Built-in DI container
- **CORS**: Cross-origin request handling

## Project Structure

```
Project_VehicleBiddingApplication_Backbone/
├── src/
│   ├── 1.Domain/
│   │   └── BiddingApp.Domain/
│   │       ├── Entities/
│   │       │   ├── Bidding.cs
│   │       │   ├── BiddingSession.cs
│   │       │   ├── User.cs
│   │       │   └── Vehicle.cs
│   │       ├── Enums/
│   │       │   ├── Brand.cs
│   │       │   ├── UserRole.cs
│   │       │   └── VehicleStatus.cs
│   │       ├── Models/
│   │       │   ├── ApiResponse.cs
│   │       │   ├── ApiSetting.cs
│   │       │   └── EF/
│   │       │       └── ApplicationDbContext.cs
│   │       ├── FluentAPIs/
│   │       │   ├── BiddingConfiguration.cs
│   │       │   ├── BiddingSessionConfiguration.cs
│   │       │   ├── UserConfiguration.cs
│   │       │   └── VehicleConfiguration.cs
│   │       └── Migrations/
│   │
│   ├── 2.Infrastructure/
│   │   ├── BiddingApp.Infrastructure/
│   │   │   ├── Repositories/
│   │   │   ├── Dtos/
│   │   │   ├── MapperConfigs/
│   │   │   ├── Paginations/
│   │   │   ├── IUnitOfWork.cs
│   │   │   └── UnitOfWork.cs
│   │   └── BiddingApp.BuildingBlock/
│   │       ├── Exceptions/
│   │       │   └── Handler/
│   │       │       └── CustomExceptionHandler.cs
│   │       ├── Extentions/
│   │       └── Utilities/
│   │
│   ├── 3.Application/
│   │   └── BiddingApp.Application/
│   │       ├── Services/
│   │       │   ├── AuthenticateServices/
│   │       │   ├── BiddingServices/
│   │       │   ├── BiddingSessionServices/
│   │       │   ├── UserServices/
│   │       │   └── VehicleSevices/
│   │       ├── SignalRServices/
│   │       │   └── BiddingNotificationService.cs
│   │       └── Hubs/
│   │           └── BiddingHub.cs
│   │
│   ├── 4.Apis/
│   │   └── BiddingApp.Api/
│   │       ├── Controllers/
│   │       │   ├── AuthenticateController.cs
│   │       │   ├── BiddingsController.cs
│   │       │   ├── BiddingSessionsController.cs
│   │       │   ├── UsersController.cs
│   │       │   └── VehiclesController.cs
│   │       ├── Program.cs
│   │       ├── appsettings.json
│   │       ├── Dockerfile
│   │       └── BiddingApp.Api.csproj
│   │
│   └── 5.Tests/
│       └── BiddingApp.ApplicationTest/
│           ├── AuthenticateService/
│           ├── BiddingService/
│           ├── BiddingSessionService/
│           ├── UserService/
│           └── VehicleService/
│
├── BiddingDb2_v2.0.sql
├── ProjectSignalR_BiddingApp.sln
└── README.md
```

## Technical Architecture & Design Patterns

This project implements industry-leading architectural patterns and best practices for building scalable, maintainable, and testable applications.

### Core Architectural Patterns

#### 1. **Clean Architecture**

The application strictly adheres to Robert C. Martin's Clean Architecture principles with a clear separation of concerns across 5 distinct layers:

- **Domain Layer**: Contains pure business logic, entity models, and domain rules. Has no external dependencies.
- **Infrastructure Layer**: Implements technical concerns like data access, external services, and persistence mechanisms.
- **Application Layer**: Orchestrates business logic, implements use cases, and acts as a bridge between presentation and infrastructure.
- **API Layer**: Handles HTTP requests/responses and acts as the entry point to the system.
- **Test Layer**: Comprehensive unit and integration tests ensuring code quality and reliability.

#### 2. **Domain-Driven Design (DDD)**

The project implements DDD concepts to model complex business domains:

- **Entities**: Core business objects (User, Vehicle, BiddingSession, Bidding) with unique identities and lifecycle
- **Value Objects**: Immutable objects representing values (enums for UserRole, Brand, VehicleStatus)
- **Aggregates**: BiddingSession acts as an aggregate root managing related biddings
- **Bounded Contexts**: Segregation between authentication, bidding, vehicles, and users domains
- **Ubiquitous Language**: Domain models and services use business terminology (BiddingSession, HighestBidding, etc.)

#### 3. **Repository Pattern**

Abstracts data access logic and provides a collection-like interface:

- Repositories encapsulate database queries
- Unit of Work pattern manages multiple repositories
- Allows easy switching between SQL Server and other data stores
- Facilitates unit testing with mock repositories

#### 4. **Unit of Work Pattern**

Coordinates multiple repositories within a single business transaction:

- Ensures atomic operations across multiple entities
- Manages transaction lifecycle
- Provides consistency guarantees

#### 5. **Dependency Injection (DI)**

ASP.NET Core's built-in DI container manages service lifetimes:

- **Scoped**: Services tied to HTTP request lifetime
- **Transient**: New instances created per request
- **Singleton**: Single instance throughout application lifetime

### Database Technologies

#### 1. **Entity Framework Core 8.0**

Modern ORM providing:

- LINQ-based query language
- Change tracking and automatic updates
- Lazy loading and eager loading
- Query optimization
- Database migrations

#### 2. **Fluent API Configuration**

Type-safe entity mapping without attributes

**Benefits**:

- Centralized configuration in dedicated classes
- Strongly-typed and compile-time safe
- Clear relationship definitions
- Cascade delete behavior specification

#### 3. **SQL Server**

Enterprise-grade relational database with:

- ACID compliance for data integrity
- Full-Text Search capabilities
- Advanced indexing strategies
- Query optimization with execution plans
- Read Committed Snapshot Isolation (RCSI) enabled

#### 4. **SQL Stored Procedures**

The database includes 20+ optimized stored procedures:

**Authentication & User Management**:

- `AuthenticateUser` - Validate user credentials
- `GetUserById` - Retrieve user profile
- `GetUserReportByUserIdWithPaging` - User bidding history with pagination

**Bidding Operations**:

- `CreateBidding` - Insert new bid with validation
- `FetchBiddingValue` - Retrieve current bid amount
- `GetBiddingListBySessionId` - Get all bids in a session
- `CheckUserBiddingStatus` - Verify user participation
- `GetWinnerUser` - Determine session winner
- `GetTop10Bidding` - Leaderboard data

**Session Management**:

- `CreateBiddingSession` - Initialize new auction
- `GetBiddingSessionById` - Retrieve session details
- `GetBiddingSessionsByUserIdWithPaging` - User's sessions
- `GetBiddingSessionsWithPaging` - All sessions with filtering
- `CloseBiddingSession` - End auction and determine winner
- `DisableBiddingSession` - Deactivate session
- `AutoCloseExpiredSessions` - Scheduled job for expired sessions

**Vehicle Management**:

- `CreateVehicle` - Add new vehicle to catalog
- `GetVehicleById` - Retrieve vehicle details
- `GetVehicleByVIN` - Search by VIN
- `GetVehiclesWithPaging` - Browse catalog with pagination
- `DeleteVehicle` - Remove vehicle from system

#### 5. **Entity Framework Migrations**

Version-controlled database schema changes:

- `20250107093100_updateCreatedDateSession` - Session creation date tracking
- `20250107085327_updateBiddingDate` - Bidding timestamp handling
- `20250102014319_updateDbV3` - Schema optimization
- `20241229111829_updateDb3` - Relationship refinements
- `20241229092819_updateDb2` - Initial data model

### Data Access Patterns

#### 1. **Generic Repository Pattern**

Implements CRUD operations for data persistence and retrieval

#### 2. **Specification Pattern**

Encapsulates query logic:

- Filters, sorting, and pagination criteria
- Reusable query specifications
- Cleaner service layer code
- Type-safe query construction

#### 3. **Pagination**

Efficient data retrieval for large datasets with page indexing and result counting

### Authentication & Security

#### 1. **JWT (JSON Web Tokens)**

Stateless authentication mechanism:

- **Token Structure**: Header.Payload.Signature
- **Signing Algorithm**: HS256 (HMAC with SHA-256)
- **Secret Key**: Securely stored in `appsettings.json`
- **Validation**: Issuer, audience, and signature verification
- **Claims**: User ID, role, and custom claims

#### 2. **Role-Based Access Control (RBAC)**

Implements authorization based on user roles:

- **Admin**: Full system access
- **Dealer**: Bidding and user-specific operations

#### 3. **Password Security**

- Passwords encrypted before storage
- Validation during authentication
- Secure comparison to prevent timing attacks

### Real-Time Communication

#### 1. **SignalR**

WebSocket-based real-time framework providing:

- **Bi-directional Communication**: Server to client and vice versa
- **Hub Pattern**: `BiddingHub` manages session connections
- **Group Management**: Users grouped by bidding session
- **Automatic Fallback**: Graceful degradation to polling if WebSocket unavailable

#### 2. **Real-Time Events**

- `UserJoined` - Notifies participants of new bidder
- `UserLeft` - Alerts when bidder exits
- `BidPlaced` - Broadcasts new bids in real-time
- `BiddingSessionUpdated` - Live session state updates

### API Design Patterns

#### 1. **RESTful Principles**

- Resource-oriented endpoints
- Standard HTTP methods (GET, POST, PUT, DELETE)
- Proper HTTP status codes
- Consistent naming conventions (lowercase URLs)

#### 2. **DTO (Data Transfer Object) Pattern**

Separates internal domain models from API contracts

- **Benefits**: Versioning flexibility, security (hide sensitive data), performance optimization

#### 3. **Response Envelope Pattern**

Consistent response format across all endpoints with success status, HTTP code, message, and data payload

#### 4. **CORS (Cross-Origin Resource Sharing)**

Enables secure cross-origin requests from specified origins with customizable policies

### Exception Handling

#### 1. **Custom Exception Handler**

Global exception handling middleware

- Catches unhandled exceptions
- Logs errors for debugging
- Returns appropriate HTTP status codes
- Prevents internal error details from leaking

#### 2. **Exception Types**

- **ValidationException**: 400 Bad Request
- **UnauthorizedException**: 401 Unauthorized
- **ForbiddenException**: 403 Forbidden
- **NotFoundException**: 404 Not Found
- **InternalServerException**: 500 Internal Server Error

### Testing Infrastructure

#### 1. **Unit Testing Framework**

Organized test projects by domain:

- `AuthenticateService/` - Authentication logic tests
- `BiddingService/` - Bidding operation tests
- `BiddingSessionService/` - Session management tests
- `UserService/` - User profile and reporting tests
- `VehicleService/` - Vehicle catalog tests

#### 2. **Testing Patterns**

- **Arrange-Act-Assert (AAA)**: Clear test structure
- **Mock Objects**: Isolated service testing
- **Test Fixtures**: Reusable test data
- **Edge Case Coverage**: Boundary and error conditions

### Cross-Cutting Concerns

#### 1. **Logging**

- Integrated with ASP.NET Core logging
- Error tracking and diagnostics
- Performance monitoring

#### 2. **Validation**

- Model state validation at API layer
- Business rule validation in services
- Database constraints enforced

#### 3. **Mappers & AutoMapper**

Configuration-based object mapping:

- Domain models ↔ DTOs
- Reduces boilerplate code
- Centralized transformation logic

### Technology Stack Summary

| Category          | Technology            | Version  |
| ----------------- | --------------------- | -------- |
| Framework         | ASP.NET Core          | 8.0      |
| ORM               | Entity Framework Core | 8.0.1    |
| Database          | SQL Server            | 2019+    |
| Authentication    | JWT Bearer            | 8.0.1    |
| Real-Time         | SignalR               | Built-in |
| API Documentation | Swagger/Swashbuckle   | 6.4.0    |
| Serialization     | System.Text.Json      | Built-in |
| Containerization  | Docker                | -        |
| Language          | C#                    | 12.0     |
| Testing           | xUnit (compatible)    | -        |

### Design Principles Applied

1. **SOLID Principles**
   - **S**ingle Responsibility: Each class has one reason to change
   - **O**pen/Closed: Open for extension, closed for modification
   - **L**iskov Substitution: Derived classes can substitute base classes
   - **I**nterface Segregation: Clients depend on specific interfaces
   - **D**ependency Inversion: Depend on abstractions, not concretions

2. **DRY (Don't Repeat Yourself)**
   - Shared logic extracted to reusable services
   - Common configurations in extensions
   - Centralized exception handling

3. **YAGNI (You Aren't Gonna Need It)**
   - Minimal overhead, focused on required features
   - No premature optimizations

4. **Composition Over Inheritance**
   - Service composition through DI
   - Flexible and maintainable code structure

## API Endpoints

List account test:

```
"admin",
"password",
"admin123",
"Test1234!",
"P@ssw0rd",
"123456",
"password123",
"test123",
"user123",
"test",
"test1",
"test2",
"test3",
"test4",
"tina",
"admin@admin.com",
"test@gmail.com",
"tina@gmail.com",
"user@example.com"
"user123@example.com"
```

Password default: `123123`

### Authentication Controller

| Method | Endpoint                     | Description               |
| ------ | ---------------------------- | ------------------------- |
| POST   | `/api/authenticate/login`    | User login with JWT token |
| POST   | `/api/authenticate/register` | Register new user         |

### Users Controller

| Method | Endpoint                 | Description             |
| ------ | ------------------------ | ----------------------- |
| GET    | `/api/users/{id}`        | Get user by ID          |
| GET    | `/api/users/{id}/report` | Get user bidding report |

### Vehicles Controller

| Method | Endpoint             | Description                 |
| ------ | -------------------- | --------------------------- |
| GET    | `/api/vehicles`      | Get all vehicles            |
| GET    | `/api/vehicles/{id}` | Get vehicle by ID           |
| POST   | `/api/vehicles`      | Create vehicle (Admin only) |
| PUT    | `/api/vehicles/{id}` | Update vehicle (Admin only) |
| DELETE | `/api/vehicles/{id}` | Delete vehicle (Admin only) |

### Bidding Sessions Controller

| Method | Endpoint                    | Description            |
| ------ | --------------------------- | ---------------------- |
| GET    | `/api/biddingsessions`      | Get all sessions       |
| GET    | `/api/biddingsessions/{id}` | Get session by ID      |
| POST   | `/api/biddingsessions`      | Create bidding session |
| PUT    | `/api/biddingsessions/{id}` | Update session         |
| DELETE | `/api/biddingsessions/{id}` | End session            |

### Biddings Controller

| Method | Endpoint                            | Description          |
| ------ | ----------------------------------- | -------------------- |
| POST   | `/api/biddings`                     | Place a bid          |
| GET    | `/api/biddings/session/{sessionId}` | Get bids for session |

## Getting Started

### Prerequisites

- .NET 8.0 SDK or later
- Docker Desktop with WSL2 support (Windows) or Docker Engine on Linux/macOS
- Optional: Visual Studio 2022 or VS Code with C# extensions

### Installation (Local)

1. Clone the repository
2. Open the solution in Visual Studio or VS Code
3. Restore NuGet packages
4. Apply migrations or run database setup scripts
5. Build the solution
6. Run the `BiddingApp.Api` project

The API will be available at the configured endpoint with Swagger documentation.

### Running with Docker

This project includes a `docker-compose.yml` file for running the backend API and SQL Server together.

1. Open a bash shell in `Project_VehicleBiddingApplication_Backbone`
2. Start the services:

```bash
cd /mnt/d/srcode/Project_VehicleBiddingApplication_Backbone
docker compose up --build
```

3. The compose setup includes:
   - `sqlserver`: SQL Server container
   - `db-init`: temporary container to initialize `BiddingDb2` using `BiddingDb2_v2.0.sql`
   - `api`: ASP.NET Core backend API

4. After startup, access the API at:

```text
http://localhost:8080
```

5. To stop and remove the containers:

```bash
docker compose down
```

### Notes

- The database script `BiddingDb2_v2.0.sql` is configured to use container-friendly paths.
- The API container is configured to connect to SQL Server using the service name `sqlserver`.
- If you need to rebuild the API image after changes, run:

```bash
docker compose up --build api
```

## Configuration

### appsettings.json

The application configuration includes:

- **ConnectionStrings**: SQL Server database connection settings
- **AppSettings**: JWT secret key, issuer, audience for token validation
- **Logging**: Configurable log levels for diagnostics
- **AllowedHosts**: CORS and host configuration

### CORS Configuration

The API is configured to accept requests from specified origins. Modify configuration to adjust CORS settings for your environment.

## Authentication

### JWT Token

The application uses JWT (JSON Web Tokens) for stateless authentication.

#### Token Generation Flow

1. User calls `/api/authenticate/login` with credentials
2. API validates credentials against the database
3. JWT token is generated with:
   - **Secret Key**: `TFB7q6pXxPjDckZdJGVbfeKyEhsrH24n`
   - **Algorithm**: HS256
   - **Expiration**: Configurable (default in `Program.cs`)

#### Using the Token

Include JWT tokens in the Authorization header for protected endpoints

#### Token Validation Parameters

- `ValidateIssuer`: false
- `ValidateAudience`: false
- `ValidateIssuerSigningKey`: true
- `ClockSkew`: 0 seconds

### Authentication Service

The `IAuthenticateService` interface provides:

- `Authenticate(LoginVm request)` - Login user and return JWT token
- `Register(RegisterVm request)` - Register new user account

## Real-Time Features

### SignalR Hub: BiddingHub

The application uses SignalR for real-time bidding updates.

#### Hub Methods (Client can invoke)

- `JoinBiddingSession(sessionId)` - Join a bidding session
- `LeaveBiddingSession(sessionId)` - Leave a bidding session

#### Hub Events (Server sends to clients)

- `UserJoined` - Notifies when user joins session
- `UserLeft` - Notifies when user leaves session
- `BidPlaced` - Notifies when new bid is placed
- `BiddingSessionUpdated` - Notifies session state changes

### BiddingNotificationService

Manages real-time notifications for bidding activities:

- Broadcasts bid updates to all session participants
- Updates highest bidding amount
- Notifies when session ends
- Tracks active bidders

### SignalR Endpoint

WebSocket connection for real-time updates is available at the bidding hub

## Services

### Authentication Service (`IAuthenticateService`)

- User registration with role assignment
- User login with credential validation
- JWT token generation

### User Service (`IUserService`)

- Retrieve user profile by ID
- Generate user bidding reports with pagination
- Track user bidding history

### Vehicle Service (`IVehicleService`)

- Manage vehicle catalog
- Vehicle search and filtering
- Vehicle status updates
- Pagination support

### Bidding Session Service (`IBiddingSessionService`)

- Create new auction sessions
- Manage session lifecycle (active, closed)
- Track highest bidding and bid count
- Calculate minimum jumping values

### Bidding Service (`IBiddingService`)

- Place new bids
- Validate bid amounts
- Track winner determination
- Update user budget

## Testing

The project includes comprehensive unit tests in the `BiddingApp.ApplicationTest` project.

### Test Categories

- **AuthenticateService Tests**: Login and registration validation
- **BiddingService Tests**: Bid placement and validation
- **BiddingSessionService Tests**: Session management
- **UserService Tests**: User profile and reporting
- **VehicleService Tests**: Vehicle management

### Running Tests

Run tests using `dotnet test` command for all tests or specify a specific test project

## Error Handling

The application implements custom exception handling:

- **Validation Errors**: 400 Bad Request
- **Unauthorized Access**: 401 Unauthorized
- **Forbidden Access**: 403 Forbidden
- **Not Found**: 404 Not Found
- **Internal Server Error**: 500 Internal Server Error

All error responses follow a standardized format with success status, HTTP status code, message, and optional data payload

## Migrations

The project uses Entity Framework Core migrations for database versioning.

### Recent Migrations

- `20250107093100_updateCreatedDateSession` - Latest update
- `20250107085327_updateBiddingDate` - Bidding date updates
- `20250102014319_updateDbV3` - Database version 3 updates

### Create New Migration

Use Entity Framework Core CLI to add new migrations for schema changes

### Update Database

Apply migrations using Entity Framework Core update command

## Development Guidelines

### Project Structure Best Practices

1. **Domain Layer**: Pure business logic, no external dependencies
2. **Infrastructure Layer**: Database access, external integrations
3. **Application Layer**: Use cases, business services
4. **API Layer**: HTTP contracts, controllers
5. **Tests**: Comprehensive unit tests for services

### Adding New Features

1. Create entity in Domain layer (`Models/Entities`)
2. Add repository in Infrastructure layer
3. Create service interface and implementation in Application layer
4. Add controller in API layer
5. Update Entity Framework configurations in Domain layer
6. Create migration and update database
7. Add unit tests in Test project

## Contributing

1. Create a feature branch from `main`
2. Make your changes with proper commit messages
3. Write or update unit tests
4. Submit a pull request with detailed description

## License

See LICENSE file for details.

## Support

For issues, questions, or suggestions, please open an issue in the repository.
