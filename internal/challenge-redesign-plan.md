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
- partial date validation for the incident form

The scaffold remains intentionally basic and keeps the current broad patterns in place.

## Intentional weak spots

### Seeded bug

The Angular asset/component flow uses naive request handling. If the selected asset changes quickly, an older response can still overwrite newer component options.

### Intentional anti-patterns

- controller logic remains close to the data store
- validation remains inline in controllers and the page component
- Angular orchestration remains in a single page component
- the implementation favors a basic first-pass style over a polished architecture

### Intentional validation gap

`plannedEndAt` validation is only partially implemented. Candidates should finish it consistently in frontend and backend.

## Candidate expectations

Ask candidates to:

1. review the current implementation
2. fix the critical asset/component correctness issue
3. keep the selected component valid for the chosen asset
4. complete the `plannedEndAt` business rules end to end
5. make one or two focused improvements
6. optionally add one focused test or leave testing notes

## Success criteria

A strong solution should show:

- correct diagnosis of the seeded bug
- focused changes on the critical path
- validation parity between frontend and backend
- clear but proportionate user feedback
- restraint around architecture and refactoring
