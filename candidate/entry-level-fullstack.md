# Full-Stack Challenge

## Context

The application already supports creating incidents in the Angular UI and the .NET API.

The incident form already loads assets, and the API now exposes asset-specific subsystem/component options.

## Task

Complete both parts of the incident form challenge.

### 1. Add a dependent dropdown to the Angular incident form

The new dropdown should:

- load subsystem/component options for the currently selected asset
- reload when the asset selection changes
- keep the selected component in sync with the latest asset-specific options

The backend endpoint for this data is already available. Focus on wiring the existing flow in the frontend.

### 2. Implement end-to-end validation for `plannedEndAt`

The rule should be:

- `plannedEndAt` must not be earlier than `startsAt`
- if `endsAt` is set, `plannedEndAt` must not be later than `endsAt`

Apply that validation in:

1. the Angular form, with visible feedback
2. the API, so invalid requests are still rejected server-side

## Requirements

Update the Angular form so that:

1. a second dropdown appears under the existing asset dropdown
2. it queries the provided API when the page loads and whenever the asset changes
3. it resets or clears the child selection if the previous value is no longer valid
4. it shows understandable loading, empty, and error states
5. `plannedEndAt` validation is enforced in both frontend and backend
6. the existing incident creation flow keeps working

## Scope

- Do **not** add the new component value to `POST /api/incidents`.
- Keep the implementation small and consistent with the existing Angular patterns in the repo.

## What we care about

- understanding an existing codebase
- managing dependent async UI state
- consistent validation across frontend and backend
- making a focused change without unnecessary rewrites
- clear user feedback
- basic test thinking

## Timebox

Aim for about **60 minutes**. If you do not finish everything, leave short notes about tradeoffs or next steps.
