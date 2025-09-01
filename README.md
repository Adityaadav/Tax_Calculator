# 💰 Tax Calculator (ASP.NET Core + SQL Server)

![.NET](https://img.shields.io/badge/.NET-6.0-blue)
![Database](https://img.shields.io/badge/SQL%20Server-Enabled-red)
![License](https://img.shields.io/badge/License-MIT-green)
![Status](https://img.shields.io/badge/Project-Active-success)

A C# ASP.NET Core project to calculate income tax under **Old** and **New** regimes (FY 2024–25), using **SQL Server** for taxpayer details, deductions, and exemptions. Includes ready-to-use database schema and seed scripts.

---

## ✨ Features

* Income tax calculation for both regimes
* SQL Server database integration (ADO.NET)
* Schema + seed scripts for easy setup
* REST API for taxpayers & tax calculations

---

## ⚙️ Setup

### 1️⃣ Clone the Repository

```bash
git clone https://github.com/Adityaadav/Tax_Calculator.git
cd Tax_Calculator
```

### 2️⃣ Database Setup

1. Open **SQL Server Management Studio (SSMS)**.
2. Run the script in `/Database/01_Create_TaxCal_Schema.sql` to create tables.
3. Run `/Database/02_Seed_Data.sql` to insert base values (tax regimes, deductions).
4. Update the connection string in `appsettings.json` with your SQL Server credentials.

### 3️⃣ Configure Connection

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=TaxCal;Trusted_Connection=True;"
}
```

### 4️⃣ Run the Project

```bash
dotnet run
```

API will be available at `https://localhost:5001` (or `http://localhost:5000`).

---

## 🙌 Author

**Aditya Yadav** – [GitHub](https://github.com/Adityaadav)
