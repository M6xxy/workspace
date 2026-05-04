# Project Overview

StarterApp is a .NET MAUI rental marketplace application that allows users to create listings, browse items, and manage rental requests.  
The application follows the MVVM architectural pattern and communicates with a  API backend for authentication, item management, and rental operations.

## Features

- User authentication and registration
- Item listing creation and editing
- Item browsing 
- Rental request system
- Incoming and outgoing rental management
- Repository pattern abstraction layer
- Unit testing with xUnit
- CI/CD pipeline using GitHub Actions

---

# Setup Instructions

## Requirements

Install the following dependencies:

- .NET 10 SDK
- Visual Studio 2022/2026 with:
  - .NET MAUI workload
  - Android SDK
- Docker Desktop

---

# Docker Setup

The application uses Docker containers for backend services.

The following containers are used:

- Application API container
- PostgreSQL database container
- pgAdmin container

Start Docker Desktop before running the project.

Run the containers using:

```bash
docker compose up --build
```

This will:

- Build the API container
- Start the PostgreSQL database
- Start pgAdmin
- Configure networking between containers

## Default Ports

| Service | Port |
|---|---|
| API | 8080 |
| PostgreSQL | 5432 |
| pgAdmin | 5050 |

---

# Database Setup

The application uses PostgreSQL running inside Docker.

Apply migrations:

```bash
dotnet ef database update
```

Example connection string:

```bash
Host=localhost;Port=5432;Database=starterapp;Username=postgres;Password=password
```

---

# How To Run The Application

## Backend API

Start Docker Compose:

```bash
docker compose up --build
```

## MAUI Application

Open the solution in Visual Studio.

Set:

```text
StarterApp
```

as the startup project.

Run the application using:

```text
F5
```

## Android

- Start Android Emulator
- Select emulator device
- Run the MAUI project

---

# How To Run Tests

Open terminal in the solution directory.

## Run All Tests

```bash
dotnet test
```

## Generate Code Coverage

```bash
dotnet test --collect:"XPlat Code Coverage"
```

## Generate HTML Coverage Report

```bash
reportgenerator -reports:"**/coverage.cobertura.xml" -targetdir:"coveragereport" -reporttypes:Html -classfilters:"-StarterApp.Database.Migrations.*"
```

Open:

```text
coveragereport/index.html
```

to view coverage results.

---

# API Endpoint Documentation

## API Base URL

```text
https://set09102-api.b-davison.workers.dev
```

---

# Architecture Overview

The application follows a layered MVVM architecture.

## Architecture Layers

```text
Views
↓
ViewModels
↓
Repositories / Services
↓
API / Database
```

---

## MVVM Pattern

- Views handle UI rendering
- ViewModels manage presentation logic
- Repositories abstract API access
- Models represent application data

---

## Repository Pattern

Repositories provide an abstraction layer between ViewModels and API calls.

### Repositories Implemented

- ItemRepository
- RentalRepository

### Interfaces Implemented

- IItemRepository
- IRentalRepository

---

## Testing

Testing is implemented using:

- xUnit
- Moq
- GitHub Actions CI pipeline

### Current Testing Includes

- Repository testing
- Model testing
- Database context testing
- API response testing
