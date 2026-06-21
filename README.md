# 🎵 Playlist API

A RESTful API for managing music playlists, built with **ASP.NET Core 9** following **Clean Architecture** principles.

---

## 📋 Table of Contents

- [Tech Stack](#tech-stack)
- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
  - [Option 1: Run with Docker (Recommended)](#option-1-run-with-docker-recommended)
  - [Option 2: Run Locally (Manual Setup)](#option-2-run-locally-manual-setup)
- [Database Setup](#database-setup)
- [Environment Variables](#environment-variables)
- [API Endpoints](#api-endpoints)
- [Running Tests](#running-tests)
- [Project Structure](#project-structure)
- [Database Design](#database-design)

---

## Tech Stack

| Layer          | Technology                        |
|----------------|-----------------------------------|
| Framework      | ASP.NET Core 10                    |
| Language       | C# 14                             |
| Database       | Microsoft SQL Server 2022         |
| ORM            | Entity Framework Core 10           |
| Containerization | Docker + Docker Compose         |

### Why SQL Server?

The data is inherently relational — users own playlists, playlists link to songs via a many-to-many relationship. SQL Server provides ACID transactions, strong referential integrity, and excellent EF Core support, making it the right tool for this domain.

---

## Prerequisites

### Option 1 — Docker (no installs required except Docker)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) (Windows / macOS / Linux)

### Option 2 — Local Setup
- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [SQL Server 2022](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (Developer edition is free)
- [SQL Server Management Studio](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms) (optional, for browsing the DB)

---

## Getting Started

### Option 1: Run with Docker (Recommended)

This is the easiest way to run the project on any machine. Docker will automatically spin up both the API and a SQL Server instance — no manual database setup needed.

**Step 1 — Clone the repository**

```bash
git clone https://github.com/your-username/playlist-api.git
cd playlist-api
```

**Step 2 — Start everything with one command**

```bash
docker-compose up --build
```

That's it. Docker will:
- Pull the official SQL Server 2022 image
- Build the API image
- Apply all database migrations automatically
- Start the API on `http://localhost:8001`



**To stop the application:**

```bash
docker-compose down
```

**To stop and delete all data (fresh start):**

```bash
docker-compose down -v
```

---

### Option 2: Run Locally (Manual Setup)

Use this if you already have SQL Server installed on your machine.

**Step 1 — Clone the repository**

```bash
git clone https://github.com/your-username/playlist-api.git
cd playlist-api
```

**Step 2 — Configure your connection string**

Open `PlaylistAPI.API/appsettings.Development.json` and update the connection string to match your SQL Server instance:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=MusicDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```


**Step 3 — Install the EF Core CLI tool** (skip if already installed)

```bash
dotnet tool install --global dotnet-ef
```

**Step 4 — Apply database migrations**

This creates the database and all tables automatically:

```bash
cd PlaylistAPI.API
dotnet ef database update
```

Expected output:
```
Build started...
Build succeeded.
Applying migration '20240101000000_InitialCreate'...
Done.
```

**Step 5 — Run the API**

```bash
dotnet run
```


## Database Setup

The database schema is managed entirely through **EF Core Migrations** — you never need to run SQL scripts manually.

### What gets created automatically

```
PlaylistDb
├── users              (id, username, email, created_at)
├── playlists          (id, name, description, user_id, created_at, updated_at)
├── songs              (id, title, artist, duration_seconds, added_by)
└── playlist_songs     (playlist_id, song_id)  ← junction table
```

### Creating a new migration (for developers)

If you modify any entity and need to update the schema:

```bash
cd PlaylistAPI.API
dotnet ef migrations add YourMigrationName --project ../PlaylistAPI.Infrastructure
dotnet ef database update
```

### Resetting the database

```bash
dotnet ef database drop
dotnet ef database update
```

---

## Environment Variables

| Variable | Description | Default |
|---|---|---|
| `ConnectionStrings__DefaultConnection` | SQL Server connection string | See appsettings.json |
| `ASPNETCORE_ENVIRONMENT` | Environment name | `Development` |

For Docker, these are set inside `docker-compose.yml`. For local dev, set them in `appsettings.Development.json` or as system environment variables.

---

## API Endpoints

Base URL: `http://localhost:5000/api`

### Users

| Method | Endpoint | Description |
|--------|----------|-------------|
| `POST` | `/users` | Create a new user |
| `GET` | `/users/{id}` | Get user by ID |

### Songs (Global Catalog)

| Method | Endpoint | Description |
|--------|----------|-------------|
| `POST` | `/songs` | Add a song to the catalog |
| `GET` | `/songs` | Browse all songs (supports `?search=query`) |
| `GET` | `/songs/{id}` | Get song by ID |

### Playlists

| Method | Endpoint | Description |
|--------|----------|-------------|
| `POST` | `/playlists` | Create a new playlist |
| `GET` | `/playlists/{id}` | Get a playlist by ID |
| `GET` | `/playlists/user/{userId}` | Get all playlists for a user |
| `PUT` | `/playlists/{id}` | Update playlist name/description |
| `DELETE` | `/playlists/{id}` | Delete a playlist |
| `POST` | `/playlists/{id}/songs` | Add an existing song to a playlist |
| `DELETE` | `/playlists/{id}/songs/{songId}` | Remove a song from a playlist |

### Example Request Flow

```bash
# 1. Create a user
POST /api/users
{
  "username": "ahmed",
  "email": "ahmed@example.com"
}
# → returns { "id": "user-guid", ... }

# 2. Add a song to the global catalog
POST /api/songs
{
  "title": "Bohemian Rhapsody",
  "artist": "Queen",
  "durationSeconds": 354
}
# → returns { "id": "song-guid", ... }

# 3. Create a playlist
POST /api/playlists
{
  "name": "My Favorites",
  "userId": "user-guid"
}
# → returns { "id": "playlist-guid", ... }

# 4. Add the song to the playlist (no duplication — just a link)
POST /api/playlists/playlist-guid/songs
{
  "songId": "song-guid"
}
```


## Database Design

```
users
  id           UNIQUEIDENTIFIER  PK
  name     NVARCHAR(50)      NOT NULL
  email        NVARCHAR(200)     NOT NULL UNIQUE
  created_at   DATETIME2         NOT NULL

playlists
  id           UNIQUEIDENTIFIER  PK
  name         NVARCHAR(100)     NOT NULL
  user_id      UNIQUEIDENTIFIER  FK → users.id  (CASCADE DELETE)
  created_at   DATETIME2         NOT NULL
  updated_at   DATETIME2         NOT NULL

songs
  id               UNIQUEIDENTIFIER  PK
  title            NVARCHAR(200)     NOT NULL
  artist           NVARCHAR(100)     NOT NULL

playlist_songs                        ← Many-to-many junction table
  playlist_id  UNIQUEIDENTIFIER  FK → playlists.id  (CASCADE DELETE)
  song_id      UNIQUEIDENTIFIER  FK → songs.id      (CASCADE DELETE)
  PRIMARY KEY (playlist_id, song_id)
```

**Why a junction table?** One song (e.g. "Bohemian Rhapsody") can exist in thousands of playlists. A junction table stores only the relationship — not a copy of the song — so the database never has duplicate song data regardless of how many playlists reference it.


**AI Usage**
I used AI for :
- suggesting code architecture
- understanding the requirements
- fixing bugs
- Click the link to view conversation with Kimi AI Assistant https://www.kimi.com/share/19eebf87-a942-8cb2-8000-00008c17ee1c
- https://gemini.google.com/share/d/1JMVcHSX4XchXi-24OabRfxQMmVyiiwg2?usp=sharing

