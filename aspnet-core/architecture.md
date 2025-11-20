# WebCore Project Architecture

## Overview

WebCore is a full-stack web application built using the **ASP.NET Boilerplate (ABP)** framework with a **Clean Architecture** approach. The project consists of two main parts:
- **Backend**: ASP.NET Core Web API
- **Frontend**: Angular SPA (Single Page Application)

## Architecture Pattern

The project follows **Domain-Driven Design (DDD)** principles with **Clean Architecture** layers:

```
┌─────────────────────────────────────────────────────────────┐
│                    Presentation Layer                       │
│  ┌─────────────────┐    ┌─────────────────────────────────┐ │
│  │   Angular SPA   │    │    ASP.NET Core Web API         │ │
│  └─────────────────┘    └─────────────────────────────────┘ │
└─────────────────────────────────────────────────────────────┘
┌─────────────────────────────────────────────────────────────┐
│                    Application Layer                        │
│                 (WebCore.Application)                       │
└─────────────────────────────────────────────────────────────┘
┌─────────────────────────────────────────────────────────────┐
│                     Domain Layer                            │
│                   (WebCore.Core)                            │
└─────────────────────────────────────────────────────────────┘
┌─────────────────────────────────────────────────────────────┐
│                 Infrastructure Layer                        │
│             (WebCore.EntityFrameworkCore)                   │
└─────────────────────────────────────────────────────────────┘
```

## Project Structure

### Backend (.NET Core)

#### 1. **WebCore.Core** (Domain Layer)
The core domain layer containing:
- **Entities**: User, Role, Tenant, Restaurant, Table, Booking
- **Domain Services**: Business logic and rules
- **Interfaces**: Repository contracts and domain services
- **Value Objects**: Domain-specific value types
- **Constants**: Application constants and configuration

**Key Components:**
- `User`: Extends ABP's `AbpUser<User>` for user management
- `Role`: User roles and permissions system
- `Tenant`: Multi-tenancy support
- `Restaurant`: Restaurant entity with details like name, address, capacity
- `Table`: Restaurant table entity with capacity and availability
- `Booking`: Restaurant booking entity with customer details and booking status
- `WebCoreConsts`: Application constants including localization and security settings

#### 2. **WebCore.Application** (Application Layer)
Application services and business workflows:
- **Application Services**: `UserAppService`, `RoleAppService`, `SessionAppService`, `RestaurantAppService`, `BookingAppService`
- **DTOs**: Data Transfer Objects for API communication
- **AutoMapper Profiles**: Object mapping configurations
- **Authorization**: Permission-based access control

**Key Features:**
- CRUD operations for entities
- Authorization and permission management
- Restaurant booking management with complete workflow
- Booking conflict validation and table availability checking
- Customer booking lifecycle (Pending → Confirmed → Completed/Cancelled/NoShow)
- Multi-tenancy handling
- Localization support

#### 3. **WebCore.EntityFrameworkCore** (Infrastructure/Data Layer)
Data access and persistence:
- **DbContext**: `WebCoreDbContext` extending ABP's Zero DbContext
- **Repositories**: Data access implementations
- **Migrations**: Database schema management
- **Configuration**: Entity configurations and relationships

#### 4. **WebCore.Web.Core** (Web Infrastructure)
Shared web infrastructure:
- **Controllers**: Base controller classes
- **Authentication**: JWT token authentication
- **Models**: Web-specific models and DTOs
- **Configuration**: Web-related configurations

#### 5. **WebCore.Web.Host** (Presentation/API Layer)
The Web API host application:
- **Controllers**: API endpoints (`HomeController`, `TokenAuthController`, `RestaurantsController`, `BookingsController`)
- **Restaurant API**: Full CRUD operations for restaurant management
- **Booking API**: Complete booking lifecycle with status management
- **Startup**: Application configuration and dependency injection
- **Authentication**: JWT Bearer token authentication
- **CORS**: Cross-Origin Resource Sharing configuration
- **Swagger**: API documentation with restaurant booking endpoints

#### 6. **WebCore.Migrator** (Database Migration Tool)
Console application for database migrations:
- **Multi-tenant Migration**: Handles database updates for all tenants
- **Seeding**: Initial data population

### Frontend (Angular)

#### Angular Project Structure
- **Angular 19**: Latest Angular framework
- **TypeScript**: Strongly typed JavaScript
- **PrimeNG**: UI component library
- **Bootstrap**: CSS framework (Admin LTE theme)
- **SignalR**: Real-time communication

**Key Components:**
- **Modules**: Feature-based module organization
- **Services**: HTTP clients and business logic
- **Components**: UI components and pages
- **Guards**: Route protection and authentication
- **Interceptors**: HTTP request/response handling

## Key Technologies and Patterns

### Backend Technologies
- **ASP.NET Core 9.0**: Web framework
- **Entity Framework Core**: ORM for data access
- **ABP Framework**: Application framework with built-in features
- **AutoMapper**: Object-object mapping
- **JWT Authentication**: Stateless authentication
- **Swagger/OpenAPI**: API documentation
- **Log4Net**: Logging framework
- **SignalR**: Real-time communication

### Frontend Technologies
- **Angular 19**: SPA framework
- **TypeScript**: Programming language
- **RxJS**: Reactive programming
- **PrimeNG**: UI components
- **NgBootstrap**: Bootstrap components for Angular
- **Moment.js**: Date/time manipulation
- **SweetAlert2**: Beautiful alert dialogs

### Cross-Cutting Concerns

#### 1. **Authentication & Authorization**
- **JWT Tokens**: Stateless authentication
- **Role-based Authorization**: Permission system
- **Multi-tenancy**: Tenant isolation
- **Claims-based Security**: Fine-grained permissions

#### 2. **Multi-Tenancy**
- **Tenant per Database**: Data isolation strategy
- **Tenant Resolution**: Host and subdomain-based tenant identification
- **Feature Management**: Tenant-specific feature enabling/disabling

#### 3. **Localization**
- **Multiple Languages**: Support for multiple UI languages
- **Resource Files**: Localized text management
- **RTL Support**: Right-to-left language support

#### 4. **Validation**
- **Client-side Validation**: Angular reactive forms
- **Server-side Validation**: Data annotations and custom validators
- **Fluent Validation**: Complex validation rules

#### 5. **Caching**
- **Memory Caching**: Application-level caching
- **Distributed Caching**: Redis support for scaled deployments

#### 6. **Logging & Auditing**
- **Structured Logging**: Log4Net configuration
- **Audit Logging**: Entity change tracking
- **Error Handling**: Global exception handling

## Data Flow

### API Request Flow
```
Angular App → HTTP Client → ASP.NET Core API → 
Application Service → Domain Service → Repository → 
Entity Framework Core → SQL Server Database
```

### Authentication Flow
```
Login Request → TokenAuthController → UserManager → 
JWT Token Generation → Angular Storage → 
Subsequent Requests with Bearer Token
```

## Restaurant Booking Feature Architecture

### Domain Model
The restaurant booking system implements a comprehensive domain model with the following entities:

#### **Restaurant Entity**
- **Properties**: Name, Description, Address, Phone, Email, Capacity, IsActive, Rating
- **Relationships**: One-to-Many with Tables and Bookings
- **Business Rules**: Capacity validation, active status management
- **Audit Trail**: Full ABP audit logging (creation, modification, deletion tracking)

#### **Table Entity**
- **Properties**: Name, Capacity, IsAvailable, RestaurantId
- **Relationships**: Many-to-One with Restaurant, One-to-Many with Bookings
- **Business Rules**: Availability management, capacity constraints
- **Audit Trail**: Complete audit logging for table management

#### **Booking Entity**
- **Properties**: CustomerName, CustomerPhone, CustomerEmail, BookingDate, BookingTime, NumberOfGuests, Status, SpecialRequests
- **Relationships**: Many-to-One with Restaurant, Table, and User
- **Status Workflow**: Pending → Confirmed → Completed/Cancelled/NoShow
- **Business Rules**: Booking conflict prevention, table capacity validation, date/time constraints

### Booking Workflow

```
Customer Request → Availability Check → Table Assignment → 
Booking Creation (Pending) → Manager Confirmation → 
Status Update (Confirmed) → Service Completion → 
Final Status (Completed/Cancelled/NoShow)
```

### Authorization & Permissions
- **Restaurant Management**: `Pages.Restaurants` permission for CRUD operations
- **Booking Management**: `Pages.Bookings` permission for booking operations
- **Customer Bookings**: Users can view and manage their own bookings
- **Manager Functions**: Booking confirmation, cancellation, and status updates

### API Endpoints

#### Restaurant Endpoints
- `GET /api/services/app/Restaurant/GetAll` - Get all restaurants with filtering
- `GET /api/services/app/Restaurant/Get?Id={id}` - Get specific restaurant
- `GET /api/services/app/Restaurant/GetAllActiveRestaurants` - Get active restaurants only
- `POST /api/services/app/Restaurant/Create` - Create new restaurant
- `PUT /api/services/app/Restaurant/Update` - Update restaurant
- `DELETE /api/services/app/Restaurant/Delete?Id={id}` - Delete restaurant

#### Booking Endpoints
- `GET /api/services/app/Booking/GetAll` - Get all bookings with filtering
- `GET /api/services/app/Booking/Get?Id={id}` - Get specific booking
- `GET /api/services/app/Booking/GetMyBookings` - Get current user's bookings
- `POST /api/services/app/Booking/Create` - Create new booking
- `PUT /api/services/app/Booking/Update` - Update booking
- `POST /api/services/app/Booking/ConfirmBooking` - Confirm pending booking
- `POST /api/services/app/Booking/CancelBooking` - Cancel booking
- `DELETE /api/services/app/Booking/Delete?Id={id}` - Delete booking

### Database Schema
```sql
-- Restaurants table with audit columns
CREATE TABLE Restaurants (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(200) NOT NULL,
    Description NVARCHAR(1000),
    Address NVARCHAR(500) NOT NULL,
    Phone NVARCHAR(20),
    Email NVARCHAR(100),
    Capacity INT NOT NULL,
    IsActive BIT NOT NULL,
    Rating DECIMAL(18,2) NOT NULL,
    -- ABP Audit columns
    CreationTime DATETIME2 NOT NULL,
    CreatorUserId BIGINT,
    LastModificationTime DATETIME2,
    LastModifierUserId BIGINT,
    IsDeleted BIT NOT NULL,
    DeleterUserId BIGINT,
    DeletionTime DATETIME2
);

-- Tables table with restaurant relationship
CREATE TABLE Tables (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL,
    Capacity INT NOT NULL,
    IsAvailable BIT NOT NULL,
    RestaurantId INT NOT NULL,
    -- Foreign key and audit columns
    FOREIGN KEY (RestaurantId) REFERENCES Restaurants(Id)
);

-- Bookings table with complex relationships
CREATE TABLE Bookings (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CustomerName NVARCHAR(100) NOT NULL,
    CustomerPhone NVARCHAR(20),
    CustomerEmail NVARCHAR(100),
    BookingDate DATETIME2 NOT NULL,
    BookingTime TIME NOT NULL,
    NumberOfGuests INT NOT NULL,
    Status INT NOT NULL, -- Enum: Pending=0, Confirmed=1, Cancelled=2, Completed=3, NoShow=4
    SpecialRequests NVARCHAR(500),
    RestaurantId INT NOT NULL,
    TableId INT,
    UserId BIGINT,
    -- Foreign keys and audit columns
    FOREIGN KEY (RestaurantId) REFERENCES Restaurants(Id),
    FOREIGN KEY (TableId) REFERENCES Tables(Id),
    FOREIGN KEY (UserId) REFERENCES AbpUsers(Id)
);
```

## Development Patterns

### 1. **Repository Pattern**
- Generic repositories for common CRUD operations
- Custom repositories for complex queries
- Unit of Work pattern for transaction management

### 2. **Dependency Injection**
- Constructor injection throughout the application
- Service registration in ABP modules
- Interface-based programming

### 3. **CQRS Concepts**
- Application services act as command handlers
- Separate DTOs for input and output operations
- Query optimization through specialized methods

### 4. **Domain Events**
- Event-driven architecture for cross-cutting concerns
- Audit logging through domain events
- Notification system integration

## Security Features

### 1. **Input Validation**
- Data annotation validation
- Anti-forgery token protection
- SQL injection prevention through EF Core

### 2. **Authentication**
- JWT token-based authentication
- Refresh token support
- Password hashing and salting

### 3. **Authorization**
- Permission-based access control
- Role management system
- Multi-tenant security isolation

### 4. **HTTPS**
- SSL/TLS encryption
- Secure cookie configuration
- HSTS headers

## Deployment Architecture

### Development Environment
- **Backend**: IIS Express or Kestrel (localhost:44311)
- **Frontend**: Angular CLI Dev Server (localhost:4200)
- **Database**: SQL Server LocalDB

### Production Deployment Options
- **Containerization**: Docker support with Dockerfile
- **Cloud Deployment**: Azure, AWS, or other cloud providers
- **Database**: SQL Server, PostgreSQL, or MySQL
- **Reverse Proxy**: Nginx or IIS for production hosting

## Testing Strategy

### Backend Testing
- **Unit Tests**: `WebCore.Tests` project
- **Integration Tests**: `WebCore.Web.Tests` project
- **Test Framework**: xUnit with ABP test infrastructure

### Frontend Testing
- **Unit Tests**: Jasmine and Karma
- **E2E Tests**: Protractor configuration
- **Component Testing**: Angular Testing Utilities

## Configuration Management

### Backend Configuration
- **appsettings.json**: Environment-specific settings
- **Connection Strings**: Database configuration
- **JWT Settings**: Authentication configuration
- **CORS Origins**: Cross-origin request settings

### Frontend Configuration
- **Environment Files**: Development and production configs
- **AppConsts**: Application constants
- **API Base URLs**: Backend service endpoints

## Build and Deployment

### Backend Build
```powershell
dotnet build WebCore.sln
dotnet publish src/WebCore.Web.Host -c Release
```

### Frontend Build
```bash
ng build --configuration production
```

### Docker Support
- Dockerfile for containerized deployment
- Multi-stage build for optimized images
- Docker Compose for development environment

This architecture provides a scalable, maintainable, and secure foundation for enterprise web applications with modern development practices and proven architectural patterns.