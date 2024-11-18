
# **Daily Transactions Management System**

A project to manage daily financial transactions and generate consolidated summaries using **.NET 8**, following **Clean Architecture**, **SOLID Principles**, and leveraging **Entity Framework Core** with a PostgreSQL database.

---

## **Table of Contents**

1. [Features](#features)
2. [Technologies Used](#technologies-used)
3. [Project Structure](#project-structure)
4. [Setup Instructions](#setup-instructions)
5. [Running the Application](#running-the-application)
6. [Testing the Application](#testing-the-application)
7. [API Endpoints](#api-endpoints)
8. [Contributing](#contributing)

---

## **Features**

- Manage daily financial transactions:
  - Add credits and debits.
  - Query transactions.
- Generate daily consolidated summaries of transactions.
- Handle high-volume processing using **RabbitMQ** for queue-based messaging.
- Designed for performance and scalability.

---

## **Technologies Used**

- **.NET 8** (Minimal API)
- **Entity Framework Core** (PostgreSQL)
- **RabbitMQ** (Message Queue)
- **xUnit/NUnit** (Unit Testing)
- **Docker** and **Docker Compose**

---

## **Project Structure**

```plaintext
├── Core
│   ├── Domain
│   │   ├── Entities
│   │   └── Interfaces
│   └── Application
│       ├── Services
│       ├── Commands
│       └── Queries
├── Infrastructure
│   ├── Persistence
│   ├── Repositories
│   ├── MessageQueue
│   └── DependencyInjection
├── API
│   ├── Controllers
│   ├── Middlewares
│   └── Program.cs
├── Tests
│   ├── UnitTests
│   └── IntegrationTests
└── scripts
    └── init-db.sql
```

---

## **Setup Instructions**

### Prerequisites

1. **Docker** and **Docker Compose** installed.
2. **.NET 8 SDK** installed.

---

### Setting Up the Environment

1. Clone the repository:
   ```bash
   git clone https://github.com/fuccela/Teste-Carrefour-Lancamento.git
   cd Teste-Carrefour-Lancamento/Teste-Carrefour-Lancamento
   ```

2. Build the Docker containers:
   ```bash
   docker-compose up -d --build
   ```

   This will start:
   - **PostgreSQL** on port `5432`.
   - **RabbitMQ** with Management UI on `http://localhost:15672` (user: `teste`, password: `teste123`).

3. Verify the database is ready:
   - Access PostgreSQL on `localhost:5432`.
   - Use the credentials defined in `docker-compose.yml` (`admin:admin`).

4. Apply the database schema:
   ```bash
   dotnet ef database update
   ```

---

## **Running the Application**

1. Restore and build the solution:
   ```bash
   dotnet restore
   dotnet build
   ```

2. Run the application:
   ```bash
   dotnet run --project API
   ```

3. Access the API:
   - Swagger UI: `http://localhost:5000/swagger`

---

## **Testing the Application**

### Unit Tests

1. Run the tests:
   ```bash
   dotnet test
   ```

2. Ensure all tests pass successfully.

### Integration Tests

1. Configure the integration test environment to point to the Dockerized PostgreSQL.
2. Run the tests:
   ```bash
   dotnet test --filter Category=Integration
   ```

---

## **API Endpoints**

### **Transaction Endpoints**

- **Add a Transaction**  
  `POST /api/transactions`  
  **Request Body**:
  ```json
  {
    "amount": 100.0,
    "type": "Credit"
  }
  ```

- **Get Transactions by Date**  
  `GET /api/transactions/{date}`  

### **Daily Summary Endpoints**

- **Get Daily Summary**  
  `GET /api/daily-summary/{date}`  

---