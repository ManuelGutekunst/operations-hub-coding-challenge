# Mid-Level Full-Stack Review Notes

## What this challenge is testing

This version of the challenge is meant to evaluate how a candidate improves an existing implementation rather than how much they can build from scratch.

The seeded flow should already:

- load assets
- load asset-specific component options
- show a dependent component dropdown
- create incidents through the existing API
- apply partial validation in the Angular form; the API endpoint has no request validation

## Intentional weak spots in the scaffold

### 1. Seeded correctness bug

The component loading flow is intentionally naive. If the user changes assets quickly, an older response can overwrite the latest component options.

Example sequence:

1. the user selects asset A and request A starts
2. before request A finishes, the user selects asset B and request B starts
3. request B finishes first and updates the UI correctly
4. request A finishes later and overwrites the UI with stale component options for asset A

That leaves the form in an inconsistent state where the selected asset and the displayed component options no longer match.

A second side effect of the same naive approach is that loading and error state can also become stale. An older request can stop the loading indicator or surface an error after a newer request has already started or succeeded.

There is also a related selection reset problem. When the component options are refreshed, the current component value should only be preserved if it still exists in the latest option set for the latest asset. A stale response can incorrectly clear a valid selection or keep an invalid one.

We expect candidates to notice and fix this with a proportionate solution.

#### Preferred solution direction

The most Angular-idiomatic fix is to drive component loading from the asset control with RxJS and `switchMap`.

That gives the right behavior because:

- a new asset selection unsubscribes the previous component request
- only the latest asset request is allowed to update UI state
- loading and error handling can be tied to the latest request rather than whichever response finishes last
- the component selection reset logic can be applied only against the latest option set

A good implementation usually:

- subscribes to `assetCode.valueChanges`
- uses `startWith` for initial load
- uses `distinctUntilChanged` to avoid unnecessary reloads
- uses `switchMap` to request the component options for the selected asset
- handles loading and error state around that stream
- clears `componentValue` only when the latest returned options no longer contain the selected value

Example shape:

```ts
import { catchError, distinctUntilChanged, of, startWith, switchMap, tap } from 'rxjs';

ngOnInit(): void {
  this.assetsApi.getAssets$().subscribe(assets => {
    this.assets.set(assets);

    if (assets.length > 0 && !this.form.controls.assetCode.value) {
      this.form.controls.assetCode.setValue(assets[0].assetCode);
    }

    this.loadingAssets.set(false);
  });

  this.form.controls.assetCode.valueChanges.pipe(
    startWith(this.form.controls.assetCode.value),
    distinctUntilChanged(),
    tap(() => {
      this.loadingComponents.set(true);
      this.componentsError.set(null);
    }),
    switchMap(assetCode =>
      this.assetsApi.getAssetComponents$(assetCode).pipe(
        catchError(() => {
          this.componentsError.set('Could not load component options.');
          return of([]);
        })
      )
    )
  ).subscribe(components => {
    this.componentOptions.set(components);

    if (!components.some(component => component.value === this.form.controls.componentValue.value)) {
      this.form.controls.componentValue.setValue('');
    }

    this.loadingComponents.set(false);
  });
}
```

A smaller imperative freshness guard is also acceptable, but `switchMap` is the preferred solution for review discussions because it matches the problem shape directly.

### 2. Validation gap

The API deliberately has no request validation. The Angular form has required-field validation and these partial date rules:

- `endsAt`, when set, is not earlier than `startsAt`
- `plannedEndAt`, when set, is not earlier than `startsAt`

Candidates must implement the following rules in both layers:

- asset code, title, description, severity, and start time are required
- the asset exists and severity is `Low`, `Medium`, or `High`
- `endsAt >= startsAt`
- `plannedEndAt >= startsAt`
- `plannedEndAt <= endsAt` when both values are set

Validation must be extracted into reusable, independently testable units rather than left only in the page component or controller. Focused automated tests should cover every invalid rule and date equality boundaries.

### 3. Intentional anti-patterns

These are left in place on purpose and should not automatically count against the candidate:

- controllers use the data store directly
- the Angular page component owns most orchestration
- the implementation is basic and not heavily abstracted

Do not expect a full architectural rewrite.

## What good solutions usually do

- fix the stale async state issue without rewriting the entire form
- keep the component selection valid when the asset changes
- complete every incident validation rule in both layers through reusable validation
- preserve or slightly improve loading, empty, error, and validation messaging
- add focused automated validation tests
- explain tradeoffs or next steps briefly

## What strong judgment looks like

- prioritizes correctness over polish
- improves the critical path first
- makes small, readable changes
- avoids unnecessary layers and abstractions
- knows when to stop refactoring

## What weak solutions often do

- spend too long restructuring the project
- ignore the race/stale-state issue
- only fix validation in one layer
- add complexity without improving correctness
- leave the child selection invalid after asset changes

## Suggested scoring

### 1. Diagnosis and prioritization

- Did they identify the critical issue quickly?
- Did they focus on the highest-value fixes first?

### 2. Correctness

- Did they fix the stale component-loading behavior?
- Did they keep the selected component aligned with the latest asset options?
- Did they implement every incident validation rule consistently?

### 3. Improvement quality

- Were changes proportionate to the task?
- Did the code become clearer or more reliable?

### 4. Engineering judgment

- Did they avoid unnecessary rewrites?
- Did they explain tradeoffs sensibly?

### 5. Testing

- Is validation reusable and covered by focused automated tests for all rules?

## Interview prompts

If you review the submission live, useful follow-ups are:

- What issue did you choose to fix first, and why?
- What other improvements did you deliberately leave out?
- How would you test the asset/component behavior more thoroughly?
- If you had another hour, what would you change next?
