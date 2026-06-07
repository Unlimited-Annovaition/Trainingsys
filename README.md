# 🎓 Training Management System API

A RESTful API built with **ASP.NET Core 8** following **Clean Architecture** principles, designed to manage training courses, trainees, trainers, enrollments, attendance, and grades.

---

## 🏗️ Architecture

The project follows Clean Architecture with 4 layers:

```
TrainnigSystem/
├── Domain/          # Entities, Interfaces, Enums
├── Application/     # Services, DTOs, Interfaces, Mappings
├── Infrastructure/  # EF Core DbContext, Repositories, Seeders
└── API/             # Controllers, Middlewares, Program.cs
```

---

## ⚙️ Tech Stack

| Technology | Usage |
|---|---|
| ASP.NET Core 8 | Web API framework |
| Entity Framework Core | ORM + Migrations |
| Dapper | Raw SQL queries |
| SQL Server | Database |
| JWT Bearer | Authentication |
| AutoMapper | Object mapping |
| BCrypt.Net | Password hashing |
| Swagger / Swashbuckle | API documentation |

---

## 📦 Domain Entities

- **User** — base account with role (Trainee / Trainer)
- **Trainee** — linked to User, can enroll in courses
- **Trainer** — linked to User, assigned to courses
- **Course** — has title, description, capacity, start/end dates
- **Enrollment** — maps trainees to courses
- **Attendance** — tracks attendance per enrollment
- **Grade** — stores grades per enrollment
- **Role** — user roles enum

---

## 🔌 API Endpoints

### Auth
| Method | Endpoint | Description |
|---|---|---|
| POST | `/api/auth/login` | Login and receive JWT token |

### Trainees
| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/trainee` | Get all trainees |
| POST | `/api/trainee` | Register new trainee |
| PUT | `/api/trainee/{id}` | Update trainee |
| DELETE | `/api/trainee/{id}` | Delete trainee |

### Trainers
| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/trainers` | Get all trainers |
| POST | `/api/trainers` | Register new trainer |
| PUT | `/api/trainers/{id}` | Update trainer |
| DELETE | `/api/trainers/{id}` | Delete trainer |

### Courses
| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/course` | Get all courses |
| POST | `/api/course` | Create course |
| PUT | `/api/course/{id}` | Update course |
| DELETE | `/api/course/{id}` | Delete course |

### Enrollments
| Method | Endpoint | Description |
|---|---|---|
| POST | `/api/enrollment` | Enroll trainee in course |
| DELETE | `/api/enrollment/{id}` | Remove enrollment |

### Attendance
| Method | Endpoint | Description |
|---|---|---|
| POST | `/api/attendance` | Record attendance |
| GET | `/api/attendance/{enrollmentId}` | Get attendance percentage |

### Grades
| Method | Endpoint | Description |
|---|---|---|
| POST | `/api/grade` | Assign grade |
| GET | `/api/grade/{enrollmentId}` | Get grades |

---

## 🚀 Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server (local or Docker)

### 1. Clone the repo
```bash
git clone https://github.com/Unlimited-Annovaition/Trainingsys.git
cd Trainingsys
```

### 2. Configure the database

Edit `API/appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=TrainingDB;User Id=sa;Password=YOUR_PASSWORD;TrustServerCertificate=True;"
  },
  "Jwt": {
    "Key": "YOUR_SECRET_KEY_MIN_16_CHARS",
    "Issuer": "TrainingSystemAPI",
    "Audience": "TrainingSystemUsers"
  }
}
```

> ⚠️ **Never commit real credentials to GitHub.** Use environment variables or `appsettings.Development.json` (git-ignored) for local development.

### 3. Run the API
```bash
cd API
dotnet run
```

The app will automatically run migrations and seed initial data on startup.

### 4. Access Swagger UI
```
https://localhost:{port}/swagger
```

---

## 🔐 Authentication

The API uses JWT Bearer tokens. After login, include the token in requests:

```
Authorization: Bearer {your_token}
```

In Swagger, click **Authorize** and enter: `Bearer {your_token}`

---

## 🌱 Database Seeding

On first run, `DbSeeder` automatically seeds initial data (roles, default users, etc.) into the database.

---

## 📁 Project Structure

```
Domain/
├── Entities/         # Course, User, Trainee, Trainer, Enrollment, Attendance, Grade
├── Interfaces/       # ITraineeRepo, ITrainerRepo, ICourseRepo, IUnitOfWork...
└── Enums/            # UserRoles

Application/
├── DTOS/             # Request/Response DTOs
├── Interfaces/       # IAuthService, ICourseService, ITraineeServices...
├── Services/         # Business logic implementations
├── Mappings/         # AutoMapper profiles
└── Wrappers/         # ApiResponse<T> wrapper

Infrastructure/
├── Data/             # TrainingDbContext, DateOnlyHandler
├── Repositories/     # EF Core + Dapper implementations
└── Seeders/          # DbSeeder

API/
├── Controllers/      # Auth, Trainee, Trainer, Course, Enrollment, Attendance, Grade
├── Middlewares/      # GlobalExceptionMiddleware
└── Program.cs        # DI registration, middleware pipeline
```