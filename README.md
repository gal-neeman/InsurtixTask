# InsurTIX Bookstore API

A RESTful Web API built with **.NET 10** and **Clean Architecture** to manage a bookstore's inventory. The application interfaces with an XML-based data store and provides full CRUD capabilities along with custom HTML reporting.

Developed as a technical assignment for InsurTIX.

## 🏗 Architecture

This project strictly adheres to **Clean Architecture** principles, divided into four main layers:

* **Domain (`InsurtixTask.Domain`):** Contains enterprise logic, core entities (`Book`), and custom domain exceptions (e.g., `BookAlreadyExistsException`).
* **Application (`InsurtixTask.Application`):** Contains business use cases, DTOs, interfaces (`IBookDao`, `IReportService`), and explicit validation rules using **FluentValidation**.
* **Infrastructure (`InsurtixTask.Infrastructure`):** Contains external concerns, including the XML file data access implementation and the HTML report generator service.
* **API (`InsurtixTask.API`):** The presentation layer. Contains the Web API controllers, dependency injection setup, and global exception handling middleware.

## ✨ Features
* **Logging** With Serilog, configured for Errors only in Production, and Information+ in Test, Development
* **Mapping** With Automapper, mapping Request types, DTOs, and Entity types.
* **CRUD Operations:** Create, Read, Update, and Delete books.
* **Unique Identifier:** All operations target specific books using their unique `ISBN`.
* **XML Data Store:** Reads and writes to a persistent XML file.
* **HTML Reporting:** A dedicated endpoint that transforms the current XML inventory into a styled HTML table format. Multiple authors are handled dynamically as comma-separated values.
* **Environment Agnostic:** The XML file path is configurable via the `Options` pattern, allowing different paths for Development and Production environments.
* **Robust Validation:** Explicit request validation using `FluentValidation` to ensure data integrity before processing.
* **Standardized Errors:** Global exception handling middleware that returns standard HTTP status codes (e.g., `409 Conflict` for duplicate ISBNs, `400 Bad Request` for validation failures, `500` for missing files) formatted as RFC 7807 Problem Details.

## 🚀 Technologies

* C# / .NET 10.0
* ASP.NET Core Web API
* FluentValidation
* Dependency Injection / IOptions
* Serilog
* Automapper

## Endpoints
<img width="297" height="243" alt="image" src="https://github.com/user-attachments/assets/ae159187-a6d3-4d13-9157-e9100045dc4c" />

