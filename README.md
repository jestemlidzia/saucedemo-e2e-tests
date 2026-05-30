# SauceDemo E2E Tests

Automated end-to-end tests for SauceDemo written in C# using Playwright and NUnit.

## Requirements

- .NET 8 SDK
- PowerShell
- Playwright browsers

## Setup & Run tests

```bash
git clone <repository-url>
cd saucedemo-e2e-tests/SauceDemo.Tests
dotnet restore
dotnet build
powershell -ExecutionPolicy Bypass -File bin/Debug/net8.0/playwright.ps1 install
dotnet test
```

## Project structure

- `Tests` - test scenarios
- `Pages` - Page Object classes
- `Fixtures` - shared setup, teardown, screenshots, traces and authenticated session setup
- `Config` - test configuration
- `TestData` - test data such as users, products and expected messages

## Test artifacts

Screenshots and Playwright traces are generated automatically when a test fails.

- `Artifacts/Screenshots`
- `Artifacts/Traces`
