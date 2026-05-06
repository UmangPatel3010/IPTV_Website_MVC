# IPTV_Website_MVC

A .NET 8 ASP.NET Core MVC IPTV-style demo application with TV-style navigation, channel playback, search, favorites, and subscriber information screens.

## Features

- Home dashboard with channel browsing
- Player pages for live playback
- TV-style focus navigation with remote-friendly controls
- Channel overlay list and next/previous channel routing
- Favorites, search, and subscriber details pages
- API-backed data loading and token-based requests
- SignalR client wiring for app communication

## Requirements

- .NET 8 SDK
- Visual Studio 2026 or later
- A configured IPTV/API backend for live data

## Configuration

The application expects API settings in `appsettings.json` or environment-specific configuration.

Common values used by the app include:

- `ApiSettings:BaseUrl` - base URL for backend API requests
- `SignalR:HubUrl` - SignalR hub URL

The app also stores runtime values such as device number and token through helper classes in the project.

## Run the project

1. Open the solution in Visual Studio.
2. Restore NuGet packages.
3. Update configuration values if needed.
4. Start the application.

Or from the command line:

```powershell
dotnet restore
dotnet run --project IPTV_Website\IPTV_Website.csproj
```

## Main areas

- `Controllers/` - application routes and player actions
- `Models/` - view models and API models
- `Services/` - API communication layer
- `Views/` - Razor views for home, player, search, and account pages
- `wwwroot/` - static CSS, JavaScript, and media assets

## Player notes

The player views use channel navigation logic and custom focus handling intended for TV/remote input.

## License

No license file is currently included.
