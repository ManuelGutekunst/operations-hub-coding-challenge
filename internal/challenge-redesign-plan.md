# Challenge Redesign Plan

## Goal

Shift the exercise away from blank-slate feature implementation and toward improving a deliberately basic implementation.

## Why

We want stronger signal on:

- understanding an existing codebase
- identifying critical issues quickly
- making pragmatic tradeoffs under a timebox
- improving correctness without unnecessary rewrites

## Challenge shape

Candidates receive a scaffold that already includes:

- the asset-specific component endpoint in the API
- the dependent component dropdown in Angular
- partial client-side validation in the Angular form, with no request validation in the API

The scaffold remains intentionally basic and keeps the current broad patterns in place.

## Intentional weak spots

### Seeded bug

The Angular asset/component flow uses naive request handling. If the selected asset changes quickly, an older response can still overwrite newer component options.

### Intentional anti-patterns

- controller logic remains close to the data store
- the Angular form's existing validation remains inline in the page component
- Angular orchestration remains in a single page component
- the implementation favors a basic first-pass style over a polished architecture

### Intentional validation gap

The API deliberately has no request validation. The Angular form has required-field and partial date validation. Candidates must implement all incident business rules in both layers through reusable, independently testable validation units: required asset code, title, description, severity, and start time; an existing asset; a supported severity; `endsAt >= startsAt`; `plannedEndAt >= startsAt`; and `plannedEndAt <= endsAt` when both values are set.

## Candidate expectations

Ask candidates to:

1. review the current implementation
2. fix the critical asset/component correctness issue
3. keep the selected component valid for the chosen asset
4. implement every incident validation business rule end to end
5. make the validation reusable and independently testable
6. add focused tests for the validation
7. make one or two focused improvements

## Success criteria

A strong solution should show:

- correct diagnosis of the seeded bug
- focused changes on the critical path
- reusable, tested validation parity between frontend and backend
- clear separation between validation, UI, and HTTP transport
- clear but proportionate user feedback
- restraint around architecture and refactoring
