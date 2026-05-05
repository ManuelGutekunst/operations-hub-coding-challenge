# Entry-Level Full-Stack Challenge

## Context

The application already supports creating incidents in the Angular UI and the .NET API.

The current flow is missing a business rule around dates.

## Task

Implement end-to-end validation for `plannedEndAt`.

The rule should be:

- `plannedEndAt` must not be earlier than `startsAt`
- if `endsAt` is set, `plannedEndAt` must not be later than `endsAt`

Apply the rule in:

1. the Angular form, with visible feedback
2. the API, so invalid requests are still rejected server-side

## What we care about

- understanding an existing codebase
- making a small change across frontend and backend
- consistent validation
- clear user feedback
- basic test thinking

## Timebox

Aim for about **one hour**. If you do not finish everything, leave short notes about tradeoffs or next steps.
