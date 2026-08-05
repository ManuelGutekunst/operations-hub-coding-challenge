# Full-Stack Challenge

## Context

The application already supports creating incidents in the Angular UI and the .NET API.

A first pass of the asset-specific component flow is already scaffolded in both layers. It works partially, but the implementation is intentionally basic and contains at least one correctness issue.

The Angular form has partial validation. The API endpoint deliberately performs no request validation.

## Task

Improve the current incident form implementation.

### 1. Review and improve the asset/component flow

The form already includes an asset-specific component dropdown backed by the API.

Improve that flow so it behaves correctly and predictably.

That should include:

- reviewing the current implementation and identifying the critical issue in the asset/component interaction
- ensuring the component options stay in sync with the currently selected asset
- ensuring the selected component is still valid after the asset changes
- improving the loading, empty, and error handling only as much as needed to make the flow understandable

### 2. Implement reusable incident validation

The Angular form applies some validation, but the API endpoint deliberately applies none. Implement the incident business rules in both layers. The API must not rely on the UI for validation.

#### Business rules

- `assetCode`, `title`, `description`, `severity`, and `startsAt` are required.
- `assetCode` must identify an existing asset.
- `severity` must be `Low`, `Medium`, or `High`.
- `endsAt`, when set, must not be earlier than `startsAt`.
- `plannedEndAt`, when set, must not be earlier than `startsAt`.
- When both are set, `plannedEndAt` must not be later than `endsAt`.

Show understandable validation feedback in the Angular form and reject invalid API requests server-side. Keep the validation reusable and independently testable; do not leave the rules embedded only in a page component or controller.

### 3. Make one or two focused improvements

After fixing the critical path, make one or two pragmatic improvements you think are worth doing inside the timebox.

Examples could include:

- tightening duplicated or unclear logic
- improving naming or state handling
- adding an additional focused test
- leaving short notes about what you would improve next

## Functional constraints

- The component-options flow works on initial page load and after asset changes; stale or invalid component state is not left behind.
- The UI communicates loading, empty, error, and validation states clearly.
- The Angular form and API enforce every incident business rule above.
- Invalid API requests receive a client-error response; valid incident creation continues to work.
- Do **not** add the component value to `POST /api/incidents`.

## Non-functional constraints

- Keep validation in reusable units that can be tested without exercising a page component or HTTP controller.
- Add focused automated tests covering every invalid business rule and valid equality boundaries for the date rules.
- Do **not** add persistence, authentication, dependencies, or unrelated UI changes.
- Do **not** rewrite the architecture. Keep the existing broad patterns unless a small change is clearly justified.
- Favor focused, readable fixes over big refactors.

## What we care about

- understanding an existing codebase
- diagnosing an intentionally weak implementation
- fixing the critical path before polishing secondary concerns
- managing dependent async UI state
- consistent, reusable validation across frontend and backend
- separating business rules from transport and UI code
- making focused changes without unnecessary rewrites
- clear user feedback
- pragmatic test thinking and engineering judgment

## Testing

Add focused automated tests for the reusable validation. Cover every invalid business rule and valid equality boundaries for the date rules.

Additional tests are optional. If you do not add them, leave short notes describing what you would test next and which existing test harness you would extend.

## Timebox

Aim for about **60 minutes**. If you do not finish everything, leave short notes about tradeoffs or next steps.
