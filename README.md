# Phone Number Validation Project

This is a small project that validates phone numbers given a number and a country. The project consists of a frontend and a backend component. The frontend runs in the browser, while the backend is an API developed exclusively in C#.

# Backend
The backend API is developed in C#, using the .NET and ASP.NET version 4.7.2 on Visual Studio 2019. The API provides the frontend with a list of supported countries for phone validation. There are 3 countries supported, Australia, USA, and Canada.

Phone validation is done in the backend, using the libphonenumber-csharp Nuget package. This package provides a robust and easy-to-use API for phone number validation.

# Frontend
The frontend is developed in .NET and ASP.NET. It communicates with the backend using AJAX calls. The frontend retrieves the list of countries allowed for phone validation.

The user can submit a phone number for validation. If the phone number is valid, the frontend allows the user to download a CSV file with the phone number information displayed on the screen. The CSV file has 4 columns and 2 rows. The first row contains the headers, which are IsValid, IsPossible, PhoneType, and InternationalFormat. The second row contains the phone number details accordingly.

# Detailed Summary

This program is a web application that allows users to input a phone number and country code and then validate the phone number using Google's libphonenumber library. The program uses jQuery to make an AJAX request to a validation API, passing the phone number and country code in the request body. The server-side code is written in C# and uses the libphonenumber library to validate the phone number.

The web page consists of a form that allows users to input a phone number and select a country code. When the "Validate" button is clicked, the JavaScript code in the page extracts the phone number and country code from the form, formats the phone number based on the country code, and then makes an AJAX request to the validation API.

The server-side code receives the request and uses the libphonenumber library to validate the phone number. The validation results are then returned to the client as a JSON object, which is processed by the JavaScript code in the web page. The validation results are displayed on the page, and a link to download a CSV file containing the validation results is also provided.

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
