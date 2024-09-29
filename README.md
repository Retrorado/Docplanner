# Docplanner API

## Overview
This project is a Docplanner that provides endpoints for managing doctor availability and appointments. It uses ASP.NET Core, MediatR, FluentValidation, and Swagger for API documentation.

## Prerequisites
- .NET 8 SDK or later

## Getting started

### Clone the repository
```sh
git clone https://github.com/Retrorado/Docplanner.git
cd docplanner
```

## Build and run the project
```
dotnet build
dotnet run --project Docplanner.Api
```
## Using the API

### Swagger UI
1. Open your web browser and navigate to `https://localhost:5235/swagger`.
2. You will see the Swagger UI with all available endpoints.
3. Click on an endpoint to expand it and see the details.
4. Use the "Try it out" button to test the endpoints directly from the Swagger UI.

### Endpoints
- **GET /api/v1/doctors/availability/weekly**: Retrieve doctor availability.
- **POST /api/v1/doctors/availability/takeSlot**: Book a doctor slot (based on response from /api/v1/doctors/availability/weekly).


