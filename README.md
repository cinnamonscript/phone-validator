# Phone Number Validation Project

This is a small project that validates phone numbers given a number and a country. The project consists of a frontend and a backend component. The frontend runs in the browser, while the backend is an API developed exclusively in C#.

# Backend
The backend API is developed in C#, using the .NET and ASP.NET version 4.7.2 on Visual Studio 2019. The API provides the frontend with a list of supported countries for phone validation. There are 3 countries supported, Australia, USA, and Canada.

Phone validation is done in the backend, using the libphonenumber-csharp Nuget package. This package provides a robust and easy-to-use API for phone number validation.

# Frontend
The frontend is developed in .NET and ASP.NET. It communicates with the backend using AJAX calls. The frontend retrieves the list of countries allowed for phone validation.

The user can submit a phone number for validation. If the phone number is valid, the frontend allows the user to download a CSV file with the phone number information displayed on the screen. The CSV file has 4 columns and 2 rows. The first row contains the headers, which are IsValid, IsPossible, PhoneType, and InternationalFormat. The second row contains the phone number details accordingly.

# Getting Started
To get started with the project, clone the repository and follow the instructions below.

# Dependencies

* .NET Framework 4.5 or higher
* [PhoneNumbers library](https://github.com/google/libphonenumber) for phone number validation and formatting
* [Newtonsoft.Json library](https://www.newtonsoft.com/json) for JSON serialization and deserialization

To install the PhoneNumbers and Newtonsoft.Json libraries, you can use NuGet Package Manager in Visual Studio or run the following commands in the Package Manager Console:
`Install-Package PhoneNumbers`
`Install-Package Newtonsoft.Json`

# Prerequisites
* .NET and ASP.NET environment
* Nuget package manager
