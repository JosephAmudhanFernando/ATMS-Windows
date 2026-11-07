# ATMS - Annai Theresa Matriculation School Management System

A production-grade, standalone Windows desktop application designed to manage student enrollments, master data, and fee collections for Annai Theresa Matriculation School. Built with a focus on Rapid Application Development (RAD), performance, and a modern user experience.

## 🏗 Architecture & UI
The application moves away from dated MDI (Multiple Document Interface) designs, utilizing a **Modern Shell Architecture**. It features a centralized `MainShellForm` with a persistent dark sidebar navigation and a dynamic view-swapping content panel that loads lightweight `UserControl` screens (Single-Page Application style).

## 💻 Tech Stack
*   **Framework:** C# .NET 
*   **UI Presentation:** Windows Forms (WinForms) with custom flat styling
*   **Database:** PostgreSQL 16 (Hosted locally via Docker)
*   **Data Access (Micro-ORM):** Dapper (for high-performance, raw SQL mapping)
*   **Database Migrations:** DbUp (Embedded resource append-only SQL scripts)
*   **Logging & Diagnostics:** Serilog (Rolling file sinks)
*   **IoC/Dependency Injection:** Microsoft.Extensions.DependencyInjection

## ✨ Core Modules (Current Status)
*   **Shell Navigation:** Fully implemented modern routing shell.
*   **Student Master:** Complete CRUD operations for 15+ demographic and academic fields with real-time grid filtering.
*   **Academic History Engine:** Decoupled academic year tracking (`student_enrollments` mapping) to preserve historical integrity upon student promotions.
*   **Fee Ledger & Collection:** Transactional UI for viewing allocated dues and processing fee payments against specific academic years.

## 🚀 Getting Started

### Prerequisites
*   [Docker Desktop](https://www.docker.com/products/docker-desktop/) (Must be running for the database)
*   Visual Studio 2022+ with the .NET Desktop Development workload

### 1. Database Setup
The database is containerized. Navigate to the directory containing the `docker-compose.yml` file and start the PostgreSQL instance:
```bash
docker compose up -d