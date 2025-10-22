# PaintShop

Backend-first webshop for paints and supplies. ASP.NET Core + EF Core + MariaDB.

---

## Features
- .NET 8 Web API
- Entity Framework Core with code‑first migrations
- MariaDB 10.6+ (MySQL compatible)
- Clean, repository‑friendly structure
- Ready for Dockerized local DB

---

## Tech Stack
- **Language:** C#
- **Framework:** ASP.NET Core Web API (.NET 8)
- **ORM:** Entity Framework Core
- **Database:** MariaDB 10.6+
- **Testing:** xUnit (recommended)

---

## Getting Started

### 1) Prerequisites
- .NET SDK 8
- MariaDB 10.6+ (local install or Docker)
- Optional: VS Code + “C# Dev Kit”

### 2) Clone
```bash
git clone https://github.com/murczi/paintshop
cd paintshop
```

### 3) Start MariaDB locally (Docker)
```bash
docker run -d \
  --name paintshop-db \
  -e MARIADB_DATABASE=paintshop \
  -e MARIADB_USER=paintshop \
  -e MARIADB_PASSWORD=change_me \
  -e MARIADB_ROOT_PASSWORD=root_pw \
  -p 3306:3306 \
  mariadb:10.6
```

### 4) Configure the API
Create or update `appsettings.Development.json` inside your API project folder:
```json
{
  "ConnectionStrings": {
    "Default": "Server=127.0.0.1;Port=3306;Database=dbo;User=root;Password=mypass;TreatTinyAsBoolean=false"
  },
  "Logging": {
    "LogLevel": { "Default": "Information", "Microsoft": "Warning", "Microsoft.Hosting.Lifetime": "Information" }
  },
  "AllowedHosts": "*"
}
```


### 5) Restore, migrate, run
```bash
# from repo root
dotnet restore

# create the initial migration if not present
# replace PaintShop.Api with the actual project name
cd src/PaintShop.Api
dotnet tool restore || true

dotnet ef migrations add Initial

# apply schema to the database
dotnet ef database update

# run the API with hot reload
dotnet watch run
```

The API will print its listening URLs, typically `http://localhost:5000` and `https://localhost:5001`.

---

## Development

### Common EF Core commands
```bash
# add a new migration after model changes
dotnet ef migrations add <Name>

# update database to latest migration
dotnet ef database update

# revert the last migration (if needed)
dotnet ef migrations remove
```

### Repository-friendly setup
- Keep `AppDbContext` in `Infrastructure` or `Persistence`.
- Expose `DbSet<Product>` etc. for LINQ: `db.Products.Where(...)`.
- Keep a thin repository if needed, otherwise prefer direct LINQ with unit‑tested queries.

---

## API

### Health
```
GET /health
200 OK -> { "status": "ok" }
```

### Products (example)
```
GET    /api/products
GET    /api/products/{id}
POST   /api/products
PUT    /api/products/{id}
DELETE /api/products/{id}
```

Example request:
```http
POST /api/products
Content-Type: application/json

{
  "name": "Acrylic Paint",
  "sku": "ACR-001",
  "price": 1999,
  "color": "#ff0000"
}
```

---

## Testing (recommended)
```bash
# create a test project if not present
cd src
dotnet new xunit -n PaintShop.Tests

# reference the API project
cd PaintShop.Tests
dotnet add reference ../PaintShop.Api/PaintShop.Api.csproj

# run tests
dotnet test
```

---

## Dockerizing the API (optional)
**Dockerfile** (minimal):
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish src/PaintShop.Api/PaintShop.Api.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://0.0.0.0:8080
ENTRYPOINT ["dotnet", "PaintShop.Api.dll"]
```
Run:
```bash
docker build -t paintshop-api .
docker run --rm -p 8080:8080 \
  -e ConnectionStrings__Default="Server=host.docker.internal;Port=3306;Database=paintshop;User=paintshop;Password=change_me;TreatTinyAsBoolean=false" \
  paintshop-api
```

---

## Troubleshooting
- **`Access denied for user`**: Verify DB user, password, and host. In Docker, the host is usually `127.0.0.1` when the DB container publishes `-p 3306:3306`.
- **`Unable to connect to any of the specified MySQL hosts`**: DB not running or wrong port. Confirm `docker ps` and port mapping.
- **Migrations not applying**: Confirm the correct startup project and that the connection string points to the intended database.

---

## Roadmap
- Core entities: Product, Category, Inventory, Order
- Auth: JWT or cookie based
- Admin panel for catalog and stock
- Frontend: React/Next.js or minimal SPA
- CI: GitHub Actions for build, test, and migrations

---

## License
MIT

