# Full-Stack CRUD Application (.NET + React)

This repository contains a full-stack CRUD application demonstrating modern **ASP.NET Core**, **EF Core**, and **React (Vite)** development practices.

The solution intentionally supports **multiple user interfaces** (Razor MVC and React) consuming the same REST API to showcase clean architecture, separation of concerns, and dependency injection.

---

## Tech Stack

**Backend**
- ASP.NET Core Web API
- ASP.NET Core MVC (Razor Views)
- Entity Framework Core
- SQLite
- Dependency Injection
- Structured Logging

**Frontend**
- React (Vite)
- JavaScript (ES6+)
- Fetch API
- Environment-based configuration

**Tooling**
- .NET CLI
- npm / Vite
- Git

---
Backend Run (API and Razor)
dotnet run
===========
React
npm install
npm run dev
============

## Application Entry Points

### Razor (MVC) UI
Server-rendered HTML interface using ASP.NET Core MVC.

http://localhost:5192/ProductsCrud

| Action | URL |
|------|----|
| List | `/ProductsCrud` |
| Create | `/ProductsCrud/Create` |
| Edit | `/ProductsCrud/Edit/{id}` |
| Delete | `/ProductsCrud/Delete/{id}` |

---

### REST API (JSON)

Stable RESTful API consumed by Razor, React, and external tools.

http://localhost:5192/api/products

| Method | Endpoint |
|------|----------|
| GET | `/api/products` |
| GET | `/api/products/{id}` |
| POST | `/api/products` |
| PUT | `/api/products/{id}` |
| DELETE | `/api/products/{id}` |

**Request Body (POST / PUT):**
```json
{
  "name": "Mango"
}

REACT
http://localhost:5173
VITE_API_BASE_URL=http://localhost:5192
/backend
  └── CRUD_API
/React
  └── ProductsReactUI
/docs
  └── architecture.md




