# Senior Angular Challenge

## Context

The dashboard loads overview metrics from the API on page load.

Right now, the loading flow is intentionally a bit thin: error handling is basic, and the state model is limited.

## Task

Improve the dashboard loading flow so that:

1. users can **manually refresh** the overview
2. the UI keeps showing the **last valid data** if a refresh fails
3. the state clearly distinguishes between loading, success, error, and stale data

Add or adjust tests where they help you show the behaviour.

## What we care about

- state modelling
- UX under failure conditions
- side-effect handling
- code clarity and maintainability
- sensible tests

## Timebox

Aim for about **one hour**. If you do not finish everything, leave short notes about tradeoffs or next steps.
