Talabaat Solution is a comprehensive software project designed to manage and streamline product-related operations. It is built using modern technologies and adheres to best practices in software development. The solution includes APIs, core business logic, and unit tests to ensure reliability and maintainability.

Features

- Product Management: Manage products with attributes like name, description, price, brand, and category.

- API Integration: RESTful APIs for seamless interaction with the system.

- Automated Unit Testing: Comprehensive test coverage using NUnit and Moq for mocking dependencies.

- Entity Relationships: Support for complex relationships between entities such as Product, ProductBrand, and ProductCategory.

- Specification Pattern: Implements the specification pattern for flexible and reusable query logic.

- AutoMapper Integration: Simplifies object-to-object mapping for DTOs and entities.

Technologies Used

- Frameworks: .NET 6.0, .NET 9.0

- Languages: C# 10.0, C# 13.0

- Architecture: ASP.NET Core MVC

- Testing: NUnit 3.x-4.x, Moq

- Mapping: AutoMapper

- Dependency Injection: Built-in DI container in ASP.NET Core

Project Structure

1. Talabaat.Core: Contains core business logic, entities, and repository interfaces.

2. Talabaat.APIs: Provides RESTful APIs for interacting with the system.

3. Talabaat.Tests: Includes unit tests to ensure the correctness of the application.

Key Components

- Entities:

  - Product: Represents a product with attributes like name, description, price, brand, and category.

  - ProductBrand and ProductCategory: Represent the brand and category of a product, respectively.

- DTOs:

  - ProductToReturnDto: Used to return product data in a structured format.

- Controllers:

  - ProductsController: Handles API requests related to products.

- Repositories:

  - IGenericRepository<T>: A generic repository interface for data access.

- Specifications:

  - Implements reusable query logic using the specification pattern.

