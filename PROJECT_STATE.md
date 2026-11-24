# PROJECT_STATE.md

**1. Current Tech Stack & Packages**
*   **Framework:** C# .NET 10.0 (Windows Forms)
*   **Database:** PostgreSQL 16 (via Docker)
*   **Data Access & Migrations:** Dapper (Micro-ORM), DbUp-PostgreSQL (Automated Schema Migrations)
*   **Security:** BCrypt.Net-Next (Password Hashing)
*   **Architecture & Logging:** Microsoft.Extensions.DependencyInjection (IoC Container), Microsoft.Extensions.Configuration (Settings), Serilog (Logging), Polly (Resilience)

**2. Folder Layout**
*   `ATMS.slnx` (Solution File)
*   `/Core/Models/` (Domain Entities: `Student`, `AcademicYear`, `FeeHead`, `FeeLedger`, `UserSession`)
*   `/Data/Repositories/` (`AuthService`, `StudentRepository`, `FeeRepository`)
*   `/Data/DataSeeder.cs` (Initial Admin Seeding)
*   `/Migrations/` (`001` through `006` SQL scripts for Schema and RBAC)
*   `/` (Root UI Components: `BaseDialogForm`, `LoginForm`, `MainShellForm`, `UserManagementControl`, `StudentManagementControl`, `FeeCollectionControl`)

**3. Docker & PostgreSQL Connection Details**
*   **Host:** `localhost` (or `127.0.0.1` via Docker port mapping)
*   **Port:** `5432`
*   **Configuration Source:** Driven via `appsettings.json` connection strings.

**4. Implementation Checklist**
1.  **Environment & DB Infrastructure:** Verify Docker PostgreSQL container state, configure `appsettings.json`, and run DbUp migrations to generate tables.
2.  **Core Application Startup:** Establish the Dependency Injection (DI) container and global configuration in `Program.cs`.
3.  **Authentication & Security:** Implement the custom `BaseDialogForm`, build the `LoginForm`, and wire up `AuthService` with `BCrypt` for initial login.
4.  **Application Shell:** Build `MainShellForm` navigation and load the `UserManagementControl` for RBAC assignments.
5.  **[BLOCKED: Master Data] Student Repository & Interface:** Create interface stubs for student data access pending finalized school data parameters.
6.  **[BLOCKED: Master Data] Fee Ledger Interface:** Create interface stubs for fee collections pending finalized master data.

**5. Strict Operating Rules**
*   **One Step at a Time:** I will propose only ONE numbered step or file change at a time.
*   **Explicit Approval:** I will stop and wait for your exact "Yes" or "No" before generating any code.
*   **Complete Files:** When writing code, I will provide the entire, fully copy-pasteable file. No partial snippets, no "insert here" comments.
*   **Verification:** Every step will conclude with exact instructions on how to build, run, or test the change to ensure it works before moving forward.