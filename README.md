# Bank Application

This project is a demonstration of a **Bank Application** developed using **Clean Architecture** principles. The application is designed with a focus on **Test-Driven Development (TDD)** and includes multiple test projects to ensure high code quality and reliability. The project also utilizes modern development practices such as **Dependency Injection**, **Entity Framework Core**, and **Middleware** for error handling.

## Table of Contents
- [Overview](#overview)
- [Technologies](#technologies)
- [Project Structure](#project-structure)
- [How to Run](#how-to-run)
- [Testing](#testing)
- [Future Enhancements](#future-enhancements)

## Overview
The Bank Application allows users to:
- Create bank accounts.
- Deposit money into accounts.
- Withdraw money from accounts.

This application demonstrates a scalable architecture and best practices for building robust APIs.

## Technologies
- **.NET 8**: Backend framework.
- **Entity Framework Core**: Database ORM.
- **xUnit**: Unit testing framework.
- **FluentAssertions**: For more readable test assertions.
- **SQLite**: Lightweight database for development and testing.
- **GitHub Actions**: Continuous Integration and Deployment (CI/CD).

## Project Structure
```
└── imansafari1991-bank/
    ├── Bank.sln                 # Solution file
    └── src/
        ├── code/
        │   ├── Bank.API/        # API project
        │   ├── Bank.Business/   # Business logic layer
        │   ├── Bank.Domain/     # Domain layer (Entities and Constants)
        │   └── Bank.Persistence/# Persistence layer (EF Core)
        └── test/
            ├── Bank.API.Tests/               # API Integration Tests
            ├── Bank.Business.Tests.Unit/    # Business Logic Unit Tests
            ├── Bank.Domain.Tests.Unit/      # Domain Logic Unit Tests
            ├── Bank.Integration.Tests/      # Full Integration Tests
            └── Bank.Persistence.Tests.Integration/ # Database Tests
```

## How to Run

### Prerequisites
Ensure you have the following installed:
- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQLite](https://sqlite.org/download.html)

### Steps to Run Locally
1. Clone the repository:
   ```bash
   git clone https://github.com/imansafari1991/imansafari1991-bank.git
   cd imansafari1991-bank
   ```
2. Navigate to the API project:
   ```bash
   cd src/code/Bank.API
   ```
3. Restore dependencies:
   ```bash
   dotnet restore
   ```
4. Run the application:
   ```bash
   dotnet run
   ```
5. The API will be available at `http://localhost:5000` (or as defined in the `launchSettings.json`).

### HTTP Requests
Use the provided `Bank.API.http` file to test API endpoints via tools like **Visual Studio Code REST Client** or **Postman**.

## Testing
This project follows **Test-Driven Development (TDD)** principles. The tests are divided into:
- **Unit Tests**: Focus on isolated components (e.g., `BankAccountServiceTests`).
- **Integration Tests**: Verify interactions between multiple components (e.g., `BankAccountControllerTests`).
- **API Tests**: Test the API endpoints.

### Run All Tests
From the root of the solution, run:
```bash
dotnet test
```

### Run Specific Tests
Navigate to the desired test project and execute:
```bash
cd test/Bank.Business.Tests.Unit

dotnet test
```

## Future Enhancements
- **Implement CI/CD Pipeline**: Automate build, test, and deployment using **GitHub Actions**.
- **Add Swagger**: Provide an interactive API documentation.
- **Containerization**: Use **Docker** to containerize the application for easier deployment.
- **Enhanced Error Handling**: Improve the middleware to support custom error codes and logging.

---
If you have any questions or suggestions, feel free to open an issue or create a pull request!

---
**Author**: [Iman Safari](https://github.com/imansafari1991)
