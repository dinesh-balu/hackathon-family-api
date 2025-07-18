# Family App API - Setup Instructions

This document provides detailed setup instructions for the Family App API with .NET 8, Entity Framework, and SQL Server.

## Project Structure

```
FamilyApp/
├── src/
│   ├── FamilyApp.Api/           # Web API controllers and configuration
│   ├── FamilyApp.Services/      # Business logic layer
│   ├── FamilyApp.Repositories/  # Data access layer
│   ├── FamilyApp.Database/      # Entity Framework DbContext and models
│   ├── FamilyApp.Models/        # Entities and DTOs
│   └── FamilyApp.DbUp/          # Database setup and migration scripts
├── Dockerfile
├── docker-compose.yml
└── FamilyApp.sln
```

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/products/docker-desktop)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or use Docker container)

## Quick Start with Docker

1. **Clone and navigate to the repository:**
   ```bash
   git clone <repository-url>
   cd hackathon-family-api
   ```

2. **Set up environment variables:**
   ```bash
   cp .env.example .env
   # Edit .env file and set SA_PASSWORD to your desired SQL Server password
   ```

3. **Start all services with Docker Compose:**
   ```bash
   docker-compose up --build
   ```

4. **Access the API:**
   - API: http://localhost:5000
   - Swagger UI: http://localhost:5000/swagger
   - Health Check: http://localhost:5000/api/health

## Local Development Setup

### 1. Database Setup

**Option A: Using Docker SQL Server**
```bash
# Set your SQL Server password as environment variable
export SA_PASSWORD="YourSecurePassword123"
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=${SA_PASSWORD}" \
   -p 1433:1433 --name sql-server \
   -d mcr.microsoft.com/mssql/server:2022-latest
```

**Option B: Local SQL Server**
- Install SQL Server locally
- Update connection strings in `appsettings.json` files

### 2. Run Database Setup Scripts

```bash
cd src/FamilyApp.DbUp
dotnet run
```

This will:
- Create all database tables
- Add functions and stored procedures
- Insert sample data

### 3. Run Entity Framework Migrations (Alternative)

```bash
cd src/FamilyApp.Api
dotnet ef database update
```

### 4. Start the API

```bash
cd src/FamilyApp.Api
dotnet run
```

## API Endpoints

### Users
- `GET /api/v1/users` - Get all users
- `POST /api/v1/users` - Create a new user
- `GET /api/v1/users/{id}` - Get user by ID
- `PUT /api/v1/users/{id}` - Update user
- `DELETE /api/v1/users/{id}` - Delete user

### Children
- `GET /api/v1/children` - Get all children
- `POST /api/v1/children` - Create a new child
- `GET /api/v1/children/{id}` - Get child by ID
- `GET /api/v1/children/{id}/sessions` - Get sessions for a child
- `PUT /api/v1/children/{id}` - Update child
- `DELETE /api/v1/children/{id}` - Delete child

### Sessions
- `GET /api/v1/sessions` - Get all sessions
- `GET /api/v1/sessions/today` - Get today's sessions
- `POST /api/v1/sessions` - Create a new session
- `GET /api/v1/sessions/{id}` - Get session by ID
- `PUT /api/v1/sessions/{id}` - Update session
- `PUT /api/v1/sessions/{id}/progress` - Update session progress
- `DELETE /api/v1/sessions/{id}` - Delete session

### Care Team
- `GET /api/v1/care-team` - Get all care team members
- `POST /api/v1/care-team` - Create a new care team member
- `GET /api/v1/care-team/{id}` - Get care team member by ID
- `PUT /api/v1/care-team/{id}` - Update care team member
- `DELETE /api/v1/care-team/{id}` - Delete care team member

## Database Schema

### Tables
- **Users**: User accounts (parents, guardians)
- **Children**: Child profiles linked to users
- **TherapySessions**: Therapy session records
- **CareTeamMembers**: Healthcare providers and therapists
- **SessionProgresses**: Progress tracking for sessions

### Functions
- `GetChildAge(@ChildId)` - Calculate child's age
- `GetSessionCompletionRate(@ChildId)` - Get average completion rate
- `CountSessionsInDateRange(@ChildId, @StartDate, @EndDate)` - Count sessions in range

### Stored Procedures
- `GetChildSessionStatistics(@ChildId)` - Comprehensive session stats
- `GetTodaysSessionSchedule()` - Today's session schedule
- `UpdateSessionProgress(@SessionId, @ProgressNotes, @CompletionPercentage)` - Update progress
- `GetCareTeamByRole(@Role)` - Filter care team by role

## Configuration

### Connection Strings
Update `appsettings.json` in both `FamilyApp.Api` and `FamilyApp.DbUp` projects:

```json
{
  "ConnectionStrings": {
    "Default": "Server=localhost,1433;Database=FamilyAppDb;User Id=sa;Password=Your_password123;TrustServerCertificate=true;"
  }
}
```

### Environment Variables
You can use environment variables to configure the application:
- `SA_PASSWORD` - SQL Server SA password for Docker container
- `DB_PASSWORD` - Database password for connection string
- `ConnectionStrings__Default` - Complete connection string (overrides individual components)
- `ASPNETCORE_ENVIRONMENT` - ASP.NET Core environment (Development/Production)

**For local development:**
```bash
export SA_PASSWORD="YourSecurePassword123"
export DB_PASSWORD="YourSecurePassword123"
```

**For Docker Compose:**
Create a `.env` file in the root directory:
```
SA_PASSWORD=YourSecurePassword123
DB_PASSWORD=YourSecurePassword123
```

## Testing

### Run Unit Tests
```bash
dotnet test
```

### Test API Endpoints
Use the Swagger UI at http://localhost:5000/swagger or tools like Postman/curl.

### Sample API Calls

**Create a User:**
```bash
curl -X POST "http://localhost:5000/api/v1/users" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Jane Doe",
    "email": "jane.doe@example.com",
    "role": "Parent"
  }'
```

**Get Today's Sessions:**
```bash
curl -X GET "http://localhost:5000/api/v1/sessions/today"
```

## Troubleshooting

### Common Issues

1. **Database Connection Failed**
   - Ensure SQL Server is running
   - Check connection string
   - Verify firewall settings

2. **Port Already in Use**
   - Change port in `docker-compose.yml` or `launchSettings.json`
   - Kill existing processes: `docker-compose down`

3. **Migration Errors**
   - Drop and recreate database: `dotnet ef database drop`
   - Run DbUp again: `cd src/FamilyApp.DbUp && dotnet run`

### Logs
- API logs are written to `logs/` directory
- Docker logs: `docker-compose logs api-service`

## React Native Integration

The API is configured with CORS to allow React Native app integration:
- All origins allowed in development
- JSON responses optimized for mobile consumption
- RESTful endpoints following mobile app conventions

## Production Deployment

1. Update connection strings for production database
2. Set `ASPNETCORE_ENVIRONMENT=Production`
3. Configure proper CORS policies
4. Set up SSL certificates
5. Use production-grade SQL Server instance

## Support

For issues or questions:
- Check the API logs
- Review Swagger documentation
- Verify database connectivity
- Ensure all services are running
