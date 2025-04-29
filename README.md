# NGO Tracker API

The **NGO Tracker API** is a backend service designed to help organizations track grants, funding, and project activities. 
It enables NGOs, donors, and stakeholders to manage and monitor financial support, project implementation, and reporting in a centralized system.

## Table of Contents

- [Features](#features)
- [Tech Stack](#tech-stack)
- [Getting Started](#getting-started)
- [Environment Variables](#environment-variables)
- [Database Migrations](#database-migrations)
- [API Endpoints](#api-endpoints)
- [Authentication](#authentication)
- [Deployment](#deployment)
- [Contributing](#contributing)
- [License](#license)

---

## Features

- ✅ User Registration & Authentication (JWT-based)
- ✅ NGO Profile Management
- ✅ Grant & Funding Tracking
- ✅ Project Management & Reporting
- ✅ Role-based Access Control (Admin, Donor, NGO)
- ✅ API-first design (for easy frontend integration)
- ✅ Swagger API Documentation

---

## Tech Stack

- **Backend**: ASP.NET Core 8 / 7
- **Database**: PostgreSQL / SQL Server
- **ORM**: Entity Framework Core
- **Authentication**: JWT Bearer Tokens
- **Containerization**: Docker
- **CI/CD**: GitHub Actions / Azure Pipelines
- **Hosting**: Azure App Service / Kubernetes

---

## Getting Started

### Prerequisites

- [.NET SDK 8](https://dotnet.microsoft.com/en-us/download)
- [Docker](https://www.docker.com/)
- [PostgreSQL](https://www.postgresql.org/) or [SQL Server](https://www.microsoft.com/en-us/sql-server/)
- [Visual Studio Code](https://code.visualstudio.com/) or Visual Studio

### Clone the Repo

```bash
git clone https://github.com/your-org/ngotracker-api.git
cd ngotracker-api

#### Run the Application
 - dotnet build 
 - dotnet run

````Via Docker
 - docker build -t ngotracker-api .
 - docker run -p 5000:80 ngotracker-api

### Database Migrations
  
 - dotnet ef migrations add InitialCreate
 - dotnet ef database update


