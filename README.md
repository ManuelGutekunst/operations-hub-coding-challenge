# Operations Hub Full-Stack Challenge

This repository targets a single **full-stack (.NET + Angular)** coding challenge. It stays intentionally small so candidates can understand the codebase quickly, then spend most of their time improving an existing implementation instead of building a feature from scratch.

## What this repo is for

The challenge centers on the incident creation flow in the Angular app and ASP.NET Core API. The scaffold is intentionally functional but basic. It includes a small correctness bug and a few weak spots so candidates have to analyze the current implementation, improve the critical path, and decide what is worth fixing inside a timebox.

It is designed to evaluate:

- understanding an existing codebase
- diagnosing and improving an intentionally naive implementation
- tracing an API/data contract across frontend and backend
- handling dependent async UI state in the frontend
- keeping validation rules consistent across frontend and backend
- making good scope tradeoffs under a timebox
- keeping user feedback consistent
- debugging, testability, and code clarity

## Structure

| Path                          | Purpose                                                             |
| ----------------------------- | ------------------------------------------------------------------- |
| `src/OperationsHub.Api`       | Small ASP.NET Core API with in-memory data for assets and incidents |
| `src/OperationsHub.Api.Tests` | Backend unit tests                                                  |
| `web`                         | Angular app for the incident creation flow                          |
| `candidate`                   | Candidate-facing challenge brief                                    |
| `internal`                    | Internal plan and reviewer notes                                    |

## Requirements

### Local setup

For a normal local setup outside a devcontainer, the candidate should have:

- **Git**
- **.NET 10 SDK**
- **Node 24** (recommended LTS version for this repo)
- **npm** (bundled with Node)

Optional:

- **Docker** if they want to run the backend via the provided Dockerfile instead of `dotnet run`

### Local devcontainer setup

For using the repository in a local VS Code devcontainer, the candidate should have:

- **Git**
- **Docker Desktop** or another local Docker engine compatible with Dev Containers
- **Visual Studio Code**
- the **Dev Containers** VS Code extension

The devcontainer already provides:

- **.NET 10 SDK**
- **Node 24**
- forwarded ports for the API and Angular app

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

## Challenge files

- Candidate brief: `candidate/fullstack.md`
- Internal plan: `internal/challenge-redesign-plan.md`
- Internal review notes: `internal/fullstack-review.md`

## Sharing with candidates

When using this repo for a real assessment:

1. Copy the repository or create a dedicated branch.
2. Share `candidate/fullstack.md`.
3. Remove the `internal/` directory before handing it to the candidate.

## Notes

- The codebase is intentionally small.
- The task is intentionally scoped to be solvable in roughly **60 minutes**.
- The scaffold is intentionally functional but not solid; some anti-patterns are left in place on purpose.
- Incident validation must be reusable, independently testable, and covered by focused automated tests; other tests are optional.
- The goal is not production completeness; it is to surface engineering judgement.
