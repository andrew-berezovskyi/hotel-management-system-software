# The Hotel Kyiv Management System

A C# Windows Forms application for hotel operations with SQLite storage. It has guest and administrator interfaces for rooms, users, staff, bookings, payments, and reports. On first launch, the application initializes its database and opens the initial administrator setup when no administrator exists.

## Tech stack

.NET 8 for Windows, Windows Forms, SQLite, ReaLTaiizor, and the NuGet packages listed in the project files.

## Run on Windows

Install the .NET 8 SDK and Visual Studio with the .NET desktop development workload. Open `hotel management system/HotelManagementSystem.csproj`, restore packages, and run the project. Alternatively, from the repository root:

```powershell
dotnet restore "hotel management system/HotelManagementSystem.csproj"
dotnet run --project "hotel management system/HotelManagementSystem.csproj"
```

The root `hotel management system.sln` also contains an absolute path to a separate local web project. If loading the solution reports a missing project, open the main `.csproj` directly; its relative project references point to modules in this repository.

Configuration is in `hotel management system/Resources/appsettings.json`. It contains the SQLite path and example SMTP settings. Configure your own mail service before using email features, and keep real credentials out of Git.

## Repository layout

- `hotel management system/` — Windows Forms UI and resources.
- `HotelManagementSystem.Services/` — application logic.
- `HotelManagementSystem.Data/` — SQLite access and queries.
- `HotelManagementSystem.Models/` — data models.
- `HotelManagementSystem.Helpers/` — shared helpers.
- `HotelManagementSystem.Voice/` — voice command module.
- `HotelManagementSystem.Mobile/` and `HotelManagementSystem.Controls/` — additional project folders.

For a more detailed module map, see the [application README](hotel%20management%20system/README.md). The application requires Windows to run.
