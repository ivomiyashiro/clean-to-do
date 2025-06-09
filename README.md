# Clean Architecture To-Do App

## Quick Start

### Prerequisites

- .NET 8.0 SDK
- Node.js 18+
- pnpm (`npm install -g pnpm`)
- SQL Server (or LocalDB)

### Run the App

```bash
# Install dependencies
pnpm install

# Setup database (update connection string in appsettings.json if needed)
dotnet ef database update --project To-Do.Infrastructure --startup-project To-Do.Presentation

# Option 1: Run both frontend and backend together
pnpm dev

# Option 2: Run separately (recommended for development)
# Terminal 1 - Backend API
pnpm dev:backend

# Terminal 2 - Frontend React app
pnpm dev:frontend
```

**Access:**

- Frontend: http://localhost:3000 (or check terminal output)
- Backend API: https://localhost:7000 (or check terminal output)

### Development Commands

```bash
# Build everything
pnpm build

# Run tests
pnpm test

# Format code
pnpm format

# Lint code
pnpm lint

# Add database migration
dotnet ef migrations add <MigrationName> --project To-Do.Infrastructure --startup-project To-Do.Presentation
```

### Project Structure

```
├── To-Do.Domain/              # Core business logic
├── To-Do.Application/         # Use cases & application services
├── To-Do.Infrastructure/      # Data access & external services
├── To-Do.Presentation/        # .NET Web API
│   └── ClientApp/            # React frontend
└── package.json              # Workspace root
```

Built with Clean Architecture, .NET 8, and React + TypeScript.
