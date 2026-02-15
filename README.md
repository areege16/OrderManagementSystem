# Order Management System

An Order Management System built with **ASP.NET Core Web API (.NET 9)** following **Clean Architecture** and **CQRS** principles. Supports order processing, inventory validation, tiered discounts, role-based access control (RBAC), and automated email notifications.

## ✨ Key Features

- **Order Management**: Place orders with real-time stock validation  
- **Tiered Discounts**: 5% off for orders > $100, 10% off for orders > $200  
- **Inventory Tracking**: Automatic stock deduction on successful orders  
- **Role-Based Access Control (RBAC)**: Separate permissions for `Customer` and `Admin`  
- **JWT Authentication**: Secure token-based auth with configurable expiry  
- **Email Notifications**: Sent via MailKit when order status changes  
- **Auto Invoice Generation**: Created upon order placement (viewable by admin)  

### Layer Dependencies
* **Domain**: No external dependencies
* **Application**: References Domain layer
* **Infrastructure**: References Application and Domain layers
* **Api**: References Application and Infrastructure layers

>💡 API documentation is available via Swagger UI:  
> 🖥️ **Local**: [http://localhost:7176/swagger](http://localhost:7176/swagger)


## 🛠️ Tech Stack

| Category       | Technologies                          |
|----------------|---------------------------------------|
| Framework      | .NET 9 Web API                        |
| Architecture   | Clean Architecture + CQRS (MediatR)  |
| Data           | Entity Framework Core 9 + SQL Server |
| Validation     | FluentValidation                      |
| Mapping        | AutoMapper                            |
| Auth           | JWT Bearer + RBAC                     |
| Email          | MailKit                               |
| Logging        | Serilog (Console + SQL Server)        |
| Docs           | Swagger/OpenAPI                       |

### Setup
1. Clone the repository
2. Update the connection string in `Api/appsettings.json` (all other settings are pre-configured)
3. Apply database migrations:
   ```bash
   dotnet ef database update --project src/Infrastructure --startup-project src/Api

   
## 📡 API Endpoints

### 🔑 Auth
| Endpoint                          | Method | Description         |
|----------------------------------|--------|---------------------|
| `/api/auth/Account/Register`     | POST   | Register new user   |
| `/api/auth/Account/login`        | POST   | Login & get JWT     |

### 🛍️ Customer (Authenticated)
| Endpoint                     | Method | Description               |
|------------------------------|--------|---------------------------|
| `/api/customer/Orders`       | POST   | Create new order          |
| `/api/customer/Orders`       | GET    | Get customer’s orders     |
| `/api/customer/Orders/{id}`  | GET    | Get specific order        |
| `/api/customer/Products`     | GET    | List all products         |
| `/api/customer/Products/{id}`| GET    | Get product details       |

### 👨‍💼 Admin (Authenticated + Role: Admin)
| Endpoint                                | Method | Description                   |
|-----------------------------------------|--------|-------------------------------|
| `/api/admin/Orders`                     | GET    | List all orders               |
| `/api/admin/Orders/{id}/status`         | PATCH  | Update order status           |
| `/api/admin/Products`                   | POST   | Create new product            |
| `/api/admin/Products/{id}`              | PUT    | Update product                |
| `/api/admin/Products/{id}/stock`        | PATCH  | Adjust product stock          |
| `/api/admin/Invoices`                   | GET    | List all invoices             |
| `/api/admin/Invoices/{id}`              | GET    | Get specific invoice          |
