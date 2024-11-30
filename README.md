## About the project

This is a **Task Management API** developed following the principles of **Domain-Driven Design** as part of a **backend study** in **C#** using **.NET 8**. The project aims to provide a simple and efficient application for task management, allowing users to perform **CRUD (Create, Read, Update, Delete)** operations on their tasks.

The **API** architecture is based on **REST**, using standard **HTTP** methods for efficient and simplified communication. Additionally, it is complemented by **Swagger documentation**, providing an interactive graphical interface for developers to easily explore and test the endpoints.
Among the NuGet packages used, **AutoMapper** is responsible for mapping between domain objects and request/response models, reducing the need for repetitive and manual code. **FluentAssertions** is used in unit tests to make assertions more readable, helping to write clear and understandable tests. For validation, **FluentValidation** is used to implement validation rules in a simple and intuitive way within request classes, keeping the code clean and easy to maintain. Finally, **EntityFramework** acts as an **ORM (Object-Relational Mapper)** that simplifies interactions with the database, allowing the use of .NET objects to manipulate data directly, without the need to work with SQL queries.

### Features

- **Domain Driven Design (DDD)**: A modular structure that helps to understand and maintain the application's domain.
- **Unit tests**: Comprehensive tests with FluentAssertions to ensure functionality and quality.
- **RESTful API with Swagger Documentation**: A documented interface that makes integration and testing easier for developers.
- **Pending Task Limitation**: To ensure efficient task management, each user can have a maximum of 10 pending tasks at a time. If a new task is attempted to be created when the limit is reached, the API returns an error message informing that the maximum number of pending tasks has been reached. This helps to avoid overload and encourages completing tasks before adding new ones.

### Built with

![.NET Badge](https://img.shields.io/badge/.NET-512BD4?logo=dotnet&logoColor=fff&style=for-the-badge)
![badge-windows](https://img.shields.io/badge/Windows-0078D6?style=for-the-badge&logo=windows&logoColor=white)
![visual-studio](https://img.shields.io/badge/Visual_Studio-5C2D91?style=for-the-badge&logo=visual%20studio&logoColor=white)
![badge-mysql](https://img.shields.io/badge/MySQL-005C84?style=for-the-badge&logo=mysql&logoColor=white)
![badge-swagger](http://img.shields.io/badge/Swagger-85EA2D?logo=swagger&logoColor=000&style=for-the-badge)

## Getting Started

To get a local copy up and running, follow these simple steps.

### Requirements

- Visual Studio version 2022+ or Visual Studio Code
- Windows 10+ or Linux/MacOS with .NET SDK installed
- MySQL Server

### Installation

1. Clone the repository:

    ```sh
    git clone git@github.com:benicio227/taskmanagement.git
    ```

2. Fill in the information in the `appsettings.Development.json` file.
3. Run the API
