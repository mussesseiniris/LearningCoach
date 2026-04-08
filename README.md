# LearningCoach

An AI-powered learning assistant that helps you plan, track, and reflect on your studies. Built with ASP.NET Core and React.

## What It Does

LearningCoach lets you manage subjects and learning sessions, then chat with an AI coach (powered by Claude) that understands your study context — what you're learning, your goals, and your progress. Conversations are persisted so you can pick up where you left off.

## Tech Stack

**Backend:** ASP.NET Core Web API, Entity Framework Core, PostgreSQL  
**Frontend:** React (Vite), react-markdown  
**AI:** Anthropic Claude API (official C# SDK)  
**Database:** PostgreSQL (Docker)

## Prerequisites

- .NET 8 SDK
- Node.js
- Docker

## Getting Started

### 1. Database

```bash
docker run --name learningcoach-db -e POSTGRES_PASSWORD=yourpassword -p 5433:5432 -d postgres
```

### 2. Backend

```bash
cd LearningCoachAPI
dotnet ef database update
dotnet run
```

The API runs at `http://localhost:5138`.

### 3. Frontend

```bash
cd frontend
npm install
npm run dev
```

### 4. Configuration

Add your Anthropic API key to `appsettings.json` (not committed to Git):

```json
{
  "Anthropic": {
    "ApiKey": "your-api-key"
  }
}
```

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET/POST | `/api/Subject` | CRUD operations for subjects |
| GET/POST | `/api/LearningSession` | CRUD operations for learning sessions |
| POST | `/api/AI/ask` | Chat with the AI coach |

## Features

- **Subject Management** — Create and track subjects with goals
- **Learning Sessions** — Log study sessions with notes and duration
- **AI Chat** — Context-aware conversations about your learning
- **Chat History** — Multi-turn conversations persisted in the database
- **Markdown Rendering** — AI responses rendered with full markdown support

## License

This project is for educational purposes.
