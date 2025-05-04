
# Partner Management System

This is a full-stack solution for managing partners and their policies using ASP.NET Core, Dapper, FastEndpoints, Angular, and Transloco for multilingual UI support.

---

## 📦 Backend Setup (.NET 9 with Dapper + FastEndpoints)

### 1. Clone the Repository

```bash
gh repo clone petravitez/partner-management
cd partner-management
cd PartnerManagement
```

### 2. Configure the Database

- Use **SQL Server** or **LocalDB**
- Create the schema and seed data:

```bash
Run the file: partner_management_schema_and_seed.sql
```

Or use a SQL tool like SSMS or Azure Data Studio to run the script manually.

### 3. Set Connection String

Update `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=PartnerManagement;Trusted_Connection=True;"
}
```

Or use secrets manager or environment variables for security.

### 4. Run the API

```bash
dotnet run
```

The API runs on `http://localhost:5111`.

---

## 🌐 Frontend Setup (Angular + Bootstrap 4 + Transloco)

### 1. Navigate to Frontend

```bash
cd client
```

### 2. Install Dependencies

```bash
npm install
```

### 3. Run the App

```bash
npm start
```

The app runs at `http://localhost:4200`.

---

## ✨ Features

- ✅ Paginated partners list view
- ✅ Partner creation with real-time validation (async uniqueness check)
- ✅ Partner policy creation with real-time validation (async uniqueness check)
- ✅ Partner details view
- ✅ Updates when partner importance changes
- ✅ Transloco-powered language switching (EN / HR)


---

## ⚙️ Technologies Used

- ASP.NET Core + FastEndpoints
- Dapper + SQL Server
- Angular 17 (Standalone APIs)
- Transloco (i18n)
- NG Bootstrap

