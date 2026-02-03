# 🚀 BilTech Service — Microservices Ecosystem For Business

**BilTech Service** is a modern retail management ecosystem. Built on a high-performance microservices architecture, it
provides a scalable solution for businesses managing multiple shop branches and centralized warehouses. The project
handles user management, inventory tracking, and retail operations for
various shop branches.

##

# 🏗 System Architecture

* The project is divided into independent services communicating through an API Gateway and an event-driven model (
  Kafka):
* API Gateway (YARP): The single entry point providing routing and primary JWT token validation.
* BUser Service: Manages authentication, worker registration, and Role-Based Access Control (RBAC).
* BShop Service: Handles retail sales, receipt management, and local shop inventory.
* BWarehouse Service: Manages the global product catalog and logistics transfer requests.

##

# 🛠 Tech Stack

**Backend:**

* .NET 8, ASP.NET Core API, Entity Framework Core
* API Interface: GraphQL (HotChocolate) for flexible data querying.
* Database: PostgreSQL
* Security: JWT Bearer Authentication with specific roles (Owner, ShopWorker, Accountant).
* Messaging: Apache Kafka for asynchronous inter-service communication.
* DevOps: Docker & Docker Compose for containerization.