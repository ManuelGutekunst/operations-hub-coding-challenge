# Senior .NET Backend Challenge

## Context

The API already supports batch status updates for assets.

You will extend that flow so that users can **preview** a batch update before applying it.

## Task

Add a **dry-run / preview** capability for asset batch status updates.

The preview should:

- run the same validation as the real update
- return which updates would succeed or fail
- **not** persist any change

You may choose the API shape yourself, for example:

- a dedicated preview endpoint, or
- a flag on the existing endpoint

## What we care about

- good API design choices
- clear separation of responsibilities
- safe handling of validation and edge cases
- readable, maintainable code
- pragmatic test coverage

## Timebox

Aim for about **one hour**. If you do not finish everything, leave short notes about tradeoffs or next steps.
