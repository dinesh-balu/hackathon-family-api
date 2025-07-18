# 🏡 Family App API Service (.NET 8 + SQL Server + Docker)

This is the REST API backend for the **Family Mobile App**, built using **.NET 8 (ASP.NET Core)** and **SQL Server**. It's designed with a clean **3-tier architecture** and a **microservice-ready structure**.

All services are containerized and run via **Docker Compose**:

*   API Service (.NET 8)
*   SQL Server (2022)

---

## 📐 Architecture Overview
Frontend (React Native)
|
v
.NET 8 REST API (api-service)
|
v
SQL Server 2022 (sql-server)

---

## 🧱 Project Structure
/api
/Controllers
/DTOs
/Models
/Services
/Repositories
/Infrastructure
Dockerfile
docker-compose.yml
.env (optional, for custom environment variables)
README.md

---

## ⚙️ Prerequisites

Before running the project, ensure you have the following installed:

*   [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
*   [Docker](https://www.docker.com/products/docker-desktop)

---

## 🚀 Running the Project (Docker 2-Container Setup)

Follow these steps to get the API service and SQL Server running locally using Docker Compose.

### 🔧 Step 1: Clone the repository

```bash
git clone [https://github.com/your-org/family-app-api.git](https://github.com/your-org/family-app-api.git)
cd family-app-api
🐳 Step 2: Build and Start Services

Navigate to the root of the cloned repository and run:
docker-compose up --build
This command will:
Build the `api-service` Docker image based on the `Dockerfile` in the `./api` directory.
Pull the `mcr.microsoft.com/mssql/server:2022-latest` image for `sql-server`.
Start both containers, ensuring the API service waits for the SQL Server to be ready.
Once started, the services will be accessible at:
Service
Port
Description
API Service
5000
.NET 8 REST API
SQL Server
1433
SQL Server 2022 DB

🌐 API Base URL

The API will be available at:
http://localhost:5000/api/v1/
📖 Swagger UI

Access the interactive API documentation (Swagger UI) at:
http://localhost:5000/swagger
-----📦 `docker-compose.yml`
version: '3.8'

services:
  api-service:
    build:
      context: ./api
    ports:
      - "5000:80"
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ConnectionStrings__Default=Server=sql-server;Database=FamilyAppDb;User Id=sa;Password=Your_password123;
    depends_on:
      - sql-server

  sql-server:
    image: [mcr.microsoft.com/mssql/server:2022-latest](https://mcr.microsoft.com/mssql/server:2022-latest)
    ports:
      - "1433:1433"
    environment:
      SA_PASSWORD: "Your_password123"
      ACCEPT_EULA: "Y"
    volumes:
      - sqlvolume:/var/opt/mssql

volumes:
  sqlvolume:
-----🛠️ API `Dockerfile`

File: `/api/Dockerfile`
FROM [mcr.microsoft.com/dotnet/aspnet:8.0](https://mcr.microsoft.com/dotnet/aspnet:8.0) AS base
WORKDIR /app
EXPOSE 80

FROM [mcr.microsoft.com/dotnet/sdk:8.0](https://mcr.microsoft.com/dotnet/sdk:8.0) AS build
WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "FamilyApp.Api.dll"]
-----🗃️ Database Configuration

The database connection string is configured within the `docker-compose.yml` for the API service.
"ConnectionStrings": {
  "Default": "Server=sql-server;Database=FamilyAppDb;User Id=sa;Password=Your_password123;"
}
-----📋 REST API Route Examples
Entity
Method
Route
Description
Users
`GET`
`/api/v1/users`
Get all users
Users
`POST`
`/api/v1/users`
Create a new user
Children
`GET`
`/api/v1/children`
Get all children
Children
`GET`
`/api/v1/children/{id}/sessions`
Get sessions for a child
Sessions
`GET`
`/api/v1/sessions/today`
Get today’s sessions
Sessions
`PUT`
`/api/v1/sessions/{id}/progress`
Update session progress
Care Team
`GET`
`/api/v1/care-team`
List care team members

-----📂 EF Core Migrations (Optional)

If you are using Entity Framework Core for database schema management, you can run migrations with the following commands (from the host, with .NET EF tools installed, or from within the API container):
# From inside the container or host with EF tools
dotnet ef migrations add InitialCreate -p ./api -s ./api
dotnet ef database update -p ./api -s ./api
-----✅ Health Check Endpoint

A simple health check endpoint is available to verify the API service is running:
GET /api/health
This returns a simple "Healthy" status.-----🔒 Optional Features (To Be Added)

The following features are planned for future implementation:
JWT Authentication or OAuth2
API Rate Limiting
Logging via Serilog
Unit Testing and Integration Testing
CI/CD (GitHub Actions or Azure DevOps)
-----🧠 Developer Notes
The project uses modular, microservice-style folders for a clear separation of concerns.
Designed to work seamlessly with a React Native frontend using HTTP fetch or Axios.
Easily extensible for additional services such as Billing, Notifications, Messaging, etc.
-----🛡 License

This project is licensed under the MIT License.-----✍️ Authors & Maintainers
Dinesh Balu (dinesh.balu@centriahealthcare.com)
[Add your team members here!]
<!-- end list -->


