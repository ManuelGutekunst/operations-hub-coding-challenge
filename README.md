# Operations Hub Coding Challenge

This repository is a **neutral coding challenge codebase** for assessment exercises. It is inspired by the kinds of qualities that matter in our production systems, but candidates should **not** work in any internal repository directly.

## What this repo is for

The repo contains one small domain and three role-specific challenge tracks:

1. **Senior .NET Backend**
2. **Senior Angular**
3. **Entry-Level Full-Stack (.NET + Angular)**

The shared domain keeps the setup lightweight while still letting us evaluate:

- API design and change safety
- state handling and UX under failure
- validation across frontend and backend
- debugging, testability, and clarity

## Structure

| Path | Purpose |
| --- | --- |
| `src/OperationsHub.Api` | Small ASP.NET Core API with in-memory data |
| `src/OperationsHub.Api.Tests` | Backend unit tests |
| `web` | Angular app with dashboard and incident flow |
| `candidate` | Candidate-facing challenge briefs |
| `internal` | Internal review notes and scoring guidance |

## Setup

### API

```bash
dotnet run --project src/OperationsHub.Api
```

The API uses `http://localhost:5146` via launch settings.

### API with Docker

```bash
docker build -f src/OperationsHub.Api/Dockerfile -t operations-hub-api .
docker run --rm -p 5146:8080 operations-hub-api
```

The container exposes the API on port `8080`, so the example above maps it to `http://localhost:5146`.

### Web

```bash
cd web
npm install
npm start
```

The Angular app proxies `/api/*` calls to the local API.

### Devcontainer (optional)

If you use VS Code Dev Containers or GitHub Codespaces, the repository includes a minimal optional setup in `.devcontainer/`.

It provides:

- .NET 10 SDK
- Node 24
- forwarded ports for the API and Angular app
- automatic `dotnet restore` and `npm ci` on first container creation

After opening the repo in the devcontainer, use:

```bash
dotnet run --project src/OperationsHub.Api
cd web
npm run start:container
```

## Sharing with candidates

When using this repo for a real assessment:

1. Copy the repository or create a dedicated branch.
2. Share **only the relevant file from `candidate/`**.
3. Remove the `internal/` directory before handing it to the candidate.

## Notes

- The codebase is intentionally small.
- The tasks are intentionally scoped to be solvable in roughly **one hour**.
- The goal is not production completeness; it is to surface engineering judgement.
