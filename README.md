
# Public Inquiries API

A .NET 6 Web API for managing public inquiries, including attachments, secure authentication, and rate-limiting.

## Features
- Manage public inquiries with CRUD operations.
- Upload and manage attachments for each inquiry.
- Secure authentication using JWT.
- Rate-limiting to prevent brute-force attacks.
- Global error handling middleware for consistent error responses.
- Configurable database connection with Entity Framework Core.

## Prerequisites
- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- SQL Server or other database provider.

## Project Structure
```
PublicInquiriesAPI/
├── Controllers/                 # API Controllers
│   ├── InquiryController.cs     # Controller for public inquiries
│   ├── AttachmentController.cs  # Controller for managing attachments
├── Data/                        # Database-related files
│   ├── AppDbContext.cs          # EF Core DbContext for the application
├── Middleware/                  # Custom Middleware
│   ├── ErrorHandlingMiddleware.cs # Middleware for global error handling
├── Models/                      # Domain Models
│   ├── Inquiry.cs               # Model for public inquiries
│   ├── Attachment.cs            # Model for attachments
├── Repositories/                # Data access logic
│   ├── InquiryRepository.cs     # Repository for inquiries
│   ├── AttachmentRepository.cs  # Repository for attachments
├── Repositories/Interfaces/     # Interfaces for repositories
│   ├── IInquiryRepository.cs    # Interface for inquiry repository
│   ├── IAttachmentRepository.cs # Interface for attachment repository
├── Services/                    # Business logic
│   ├── InquiryService.cs        # Service for inquiries
│   ├── AttachmentService.cs     # Service for attachments
├── Services/Interfaces/         # Interfaces for services
│   ├── IInquiryService.cs       # Interface for inquiry service
│   ├── IAttachmentService.cs    # Interface for attachment service
├── Utils/                       # Utility classes and exceptions
│   ├── Exceptions/              # Custom exception classes
│       ├── CustomException.cs
│       ├── NotFoundException.cs
│       ├── ValidationException.cs
│       ├── UnauthorizedException.cs
├── wwwroot/                     # Static files (e.g., uploaded attachments)
├── Program.cs                   # Entry point and middleware configuration
├── appsettings.json             # Application configuration
```

---

## Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/your-repo/public-inquiries-api.git
   cd public-inquiries-api
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Update the `appsettings.json` file with your database connection string:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Database=PublicInquiriesDB;Trusted_Connection=True;"
   }
   ```

4. Apply migrations:
   ```bash
   dotnet ef database update
   ```

## Running the Application
Run the application in development mode:
```bash
dotnet run
```

### Inquiries
- **GET /api/inquiry**: Get all inquiries.
- **GET /api/inquiry/{id}**: Get a specific inquiry by ID.
- **POST /api/inquiry**: Create a new inquiry (supports attachments).
- **DELETE /api/inquiry/{id}**: Delete an inquiry and its attachments.

### Attachments
- **GET /api/attachment/{inquiryId}**: Get attachments for a specific inquiry.
- **POST /api/attachment/{inquiryId}**: Upload an attachment to a specific inquiry.
- **DELETE /api/attachment/{id}**: Delete an attachment.

## Security
- JWT Authentication for secure access.
- Rate limiting to prevent abuse.
- CORS configuration for specific origins.

