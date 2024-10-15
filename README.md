# Ag#### Assessment

This repository contains a suite of automated tests for Ag### assessment validating both API endpoints and UI elements. The tests are built using the **NUnit** framework, **RestSharp** for API testing, and **Selenium WebDriver** for UI testing.

## Table of Contents
- [Prerequisites](#prerequisites)
- [Project Structure](#project-structure)
- [Runsettings File Configuration](#runsettings-file-configuration)
- [Setting Up and Running Tests](#setting-up-and-running-tests)
- [API Tests](#api-tests)
- [UI Tests](#ui-tests)
- [Loggin](#nlogc-onfiguration)

---

## Prerequisites

Before you can run the tests, ensure that you have the following software installed on your machine:

1. **.NET 6.0 SDK** or later
2. **NUnit** (4.x or later)
3. **Selenium WebDriver**
4. **RestSharp** (for API tests)
5. **WebDriverManager** (for Chromedrver running Selenium tests on Chrome)
6. **Visual Studio 2022** (recommended for IDE)

You can install the necessary NuGet packages from VS or console by running:

bash
dotnet add package NUnit
dotnet add package RestSharp
dotnet add package Selenium.WebDriver
dotnet add package Selenium.Chrome.WebDriver


## Project Structure

![image](https://github.com/user-attachments/assets/57b7ac48-68d0-4d73-8590-924c45a350bd)


## Runsettings File Configuration

The env.runsettings file is used to configure runtime settings for tests, including the environment URL for API and UI tests. Ensure you have the env.runsettings file set up in your project under the Ui/ directory:

xml
<?xml version="1.0" encoding="utf-8"?>
<RunSettings>
  <TestRunParameters>
		<Parameter name="baseUrlUi" value="#####" />
		<Parameter name="baseUrlApi" value="#####" />
  </TestRunParameters>
  <NUnit>
    <NumberOfTestWorkers>#</NumberOfTestWorkers> <!-- Ensure this is set for parallelism -->
    <DefaultTestExecutionMode>Parallel</DefaultTestExecutionMode> <!-- Set parallel execution -->
  </NUnit>
</RunSettings>


This file defines:
- **baseUrlUi**: The base URL for UI tests.
- **baseUrlApi**: The base URL for API tests.
- **NumberOfTestWorkers**: This specifies the number of workers/treads for running the tests in parallel by test-level.
- **DefaultTestExecutionMode**: This enables by default parallel execution.

Make sure you select the env.runsettings file in Visual Studio:
- **Test** > **Configure Run Settings** > **Select Solution Wide Run Settings File** > Choose your env.runsettings file.

## Setting Up and Running Tests

### Running Tests from Visual Studio
1. Open **Visual Studio**.
2. Ensure that the env.runsettings file is selected as the active run settings.
3. In the **Test Explorer**, you can find all available tests.
4. Click **Run All** or select specific tests to execute them.

### Running Tests from Command Line
You can run tests using the .NET CLI as follows:

bash
dotnet test --settings env.runsettings -m


This command ensures that the tests use the settings from the env.runsettings file.


## API Tests

The API tests are located in the JsonPlaceholder.cs file and cover the following endpoints:

- **GET /posts**
- **POST /posts**
- **PUT /posts/{postId}**
- **DELETE /posts/{postId}**
- **POST /posts/{postId}/comments**
- **GET /comments?postId={postId}**

### API Test Structure
The API tests are designed to:
1. **Create a post** using a POST request.
2. **Edit a post** using a PUT request.
3. **Add a comment** to the post using a POST request.
4. **Delete a post** and verify its deletion with a GET request.
5. Invalid PostID handled

Use assertions to ensure that the correct HTTP status codes are returned.
For negative testing, validatting missing fields, invalid fields .. etc were attempted but the endpoints don't support this so only invalid postid was added for negative testing

Note that initially, in a real environment where the data is retained, test data would be created/modified and then validated. However, this environment doesn't retain data so only the response codes where validated. Also, the created data in the setup would have allowed for more control of the data and so it is not conflicting with the data currently in the environment

**Key Utility**: 
- BaseUtils.GetParameter("apiUrl"): Fetches the API URL from the .runsettings file.

### UI Tests
- **Page Object Model (POM)** is used to separate page-related logic into HomePage, MarketIntelligencePage, and ContactPage classes located in the PageObjects directory.
- Tests make use of WebDriverUtils.InitializeDriver() to initialize the Selenium WebDriver with settings from the .runsettings file.

**Key Utility**: 
- BaseUtils.GetParameter("baseUrl"): Fetches the base URL for UI tests from the .runsettings file.

## Logging

Log4Net has been integrated into this solution to capture logs for both API and UI tests. You can configure logging to output to both a file and the console.

### Log4Net Configuration

The `Log4Net.config` file is located in the root directory (`AgData/`) and is shared by both projects. Logs will be stored in the project directory under `logs/`

---

Thank you for your time looking at my solution. I appreciate the oppurtunity and i hope it satisfies!
Ehab
