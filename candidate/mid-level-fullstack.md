# Mid-Level Full-Stack Challenge

## Context

The application already supports creating incidents in the Angular UI and the .NET API.

The incident form already loads assets, and the backend already contains seeded asset-specific subsystem/component data.

The end-to-end flow for exposing and consuming those component options is not complete yet.

## Task

Complete both parts of the incident form challenge.

### 1. Implement asset-specific component options end to end

Add the missing asset-component flow across the API and Angular app.

That should include:

- exposing asset-specific subsystem/component options from the API in a way that is consistent with the existing codebase
- adding a dependent dropdown under the existing asset dropdown in the Angular form
- loading options for the currently selected asset on initial page load and whenever the asset changes
- keeping the selected component in sync with the latest asset-specific options
- avoiding stale UI state if the selected asset changes while an earlier request is still in flight

### 2. Implement end-to-end validation for `plannedEndAt`

The rule should be:

- `plannedEndAt` must not be earlier than `startsAt`
- if `endsAt` is set, `plannedEndAt` must not be later than `endsAt`

Apply that validation in:

1. the Angular form, with visible feedback
2. the API, so invalid requests are still rejected server-side

## Requirements

Update the API and Angular form so that:

1. the component options endpoint is implemented and wired through the frontend
2. a second dropdown appears under the existing asset dropdown
3. the dropdown reloads on page load and whenever the asset changes
4. it resets or clears the child selection if the previous value is no longer valid
5. it shows understandable loading, empty, and error states
6. `plannedEndAt` validation is enforced in both frontend and backend
7. the existing incident creation flow keeps working

## Scope

- Do **not** add the new component value to `POST /api/incidents`.
- Do **not** add persistence, authentication, or unrelated UI changes.
- Keep the implementation small and consistent with the existing Angular patterns in the repo.
- Reuse the existing backend patterns instead of introducing a new architecture.

## What we care about

- understanding an existing codebase
- tracing seeded data through the backend and into the UI
- managing dependent async UI state
- avoiding stale async results and invalid child selections
- consistent validation across frontend and backend
- making a focused change without unnecessary rewrites
- clear user feedback
- pragmatic test thinking and engineering judgment

## Testing

Tests are a **nice to have**, not a hard requirement for finishing the challenge.

If you have time, add one or two focused tests in the existing backend or Angular test setup.

If you do not add tests, leave short notes describing:

- what you would test first
- which existing test harness you would extend
- any gaps in the current test setup you would address next

## Timebox

Aim for about **60 minutes**. If you do not finish everything, leave short notes about tradeoffs or next steps.
