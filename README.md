# Task Management API

A simple task management system built with ASP.NET Core (.NET 8), Entity Framework Core, and JWT-based authentication.

## Features

- User authentication with JWT tokens
- Role-based access control (Admin, User)
- Task CRUD operations
- Comments on tasks
- Swagger UI for testing API endpoints

## Technologies Used

- ASP.NET Core Web API (.NET 8)
- Entity Framework Core with SQL Server
- JWT (JSON Web Tokens) for authentication
- Swagger (Swashbuckle) for API documentation

---

## Project Structure

├── Controllers # API endpoints
├── DataBase # Database context
├── Migrations # EF Core migration files
├── Models # Data models (User, Task, Comment, etc.)
├── Properties # launchSettings.json and related configs
├── Services # Business logic (e.g., AuthService)
│ └── AuthService.cs
├── obj # Build artifacts (auto-generated)
├── TaskManagement.csproj


---

## Getting Started

### Prerequisites

- .NET 8 SDK
- SQL Server (LocalDB or full SQL Server)
- Visual Studio / VS Code

### Setup

1. Clone the repository:
   ```bash
   https://github.com/Geman99/Task_Management.git
   cd taskmanagement-api
Run the following commands:
dotnet restore
dotnet ef database update
dotnet run
