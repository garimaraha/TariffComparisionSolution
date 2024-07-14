# Tariff Comparison Project

This project builds an electricity tariff comparison model for two given products. The goal is to compare different electricity products based on their annual costs.
## Products

1. **Basic Electricity Tariff**
   - Base costs per month: 5€
   - Consumption costs: 22 cent/kWh
   - Calculation model: base costs per month 5€ + consumption costs 22cent/kWh

2. **Packaged Tariff**
   - Fixed costs for up to 4000 kWh/year: 800€
   - Additional costs above 4000 kWh/year: 30 cent/kWh
   - Calculation model: 800€ for up to  4000kWh/year and above 4000kWh/year additionally 30 cent/kWh.
### Prerequisites
- .NET 8 SDK installed on machine(https://dotnet.microsoft.com/en-us/download/dotnet/8.0).
### Installation
- Clone the repository:
- `git clone https://github.com/garimaraha/TariffComparisionSolution.git`
- Navigate to the solutoin directory:
- `cd TariffComparisionSolution`
- Restore the dependencies:
  ```bash
  dotnet restore
  
### Running the Application
- Navigate to the project directory:
- `cd TariffComparisionSolution\TariffComparisionModel.API`
- Build the project:
  ```bash
  dotnet build
- Run the project:
  ```bash
  dotnet run
- The application should now be running at **https://localhost:7286**.
## Using Postman to Call API

If you encounter an SSL Error: "Unable to verify the first certificate" while using Postman, follow these steps to disable SSL verification in Postman:

1. Open Postman.
2. Click on the **File** menu.
3. Select **Settings**.
4. In the **Settings** dialog, go to the **General** tab.
5. Turn off the **SSL certificate verification** setting.
Now you can make API calls without encountering SSL certificate errors.

### Example Postman Call
1. Open Postman.
2. Create a new GET request.
3. Enter the following URL:**https://localhost:7286/api/TariffComparision/compareCosts?Consumption=4500**
4. Send Request.

### Example CURL Command Usage
```bash
curl https://localhost:7286/api/TariffComparision/compareCosts?Consumption=450
```
### Unit Tests
- Navigate to the Test project directory:
- `TariffComparisionSolution\TariffComparisionModel.Test`
   ```bash
  dotnet test

### Project consists of 
- Tariff Comparison Model
- Traiff Comparsion API
- Traiff Comparsion Unit and Integration Test cases

### Project Structure

The project is organized into the following structure:

```
TariffComparisonSolution.sln                    // Solution file for the entire application
│
├───TariffComparisonModel                       // Core project: Contains business logic and models
│   │   TariffComparisonModel.csproj            // Project file for the core model project
│   │   TariffConfig.json                        // Configuration file for tariff settings
│   │
│   ├───Factories                                // Factory layer: Responsible for creating instances
│   │       ITariffComparisonFactory.cs          // Interface for getting all tariffs 
│   │       TariffComparisonFactory.cs           // Implementation of the factoryto retrieve all tariffs 
│   │
│   ├───Models                                   // Domain Entities: Contains domain entities
│   │       TariffCost.cs                        // Domain model representing tariff costs
│   │
│   ├───Products                                 // Product layer: Defines different tariff products  
│   │       BasicElectricityTariffProduct.cs     // Basic electricity tariff product annual cost calculation 
│   │       ITariffProduct.cs                     // Interface for all tariff products
│   │       PackagedTariffProduct.cs             // Packaged tariff product annual cost calculation
│   │
│   └───Services                                 // Service layer: Contains business logic services
│           TariffComparisonService.cs           //  Service for executing tariff comparison logic
│
├───TariffComparisonModel.API                    // API project: Exposes HTTP endpoints
│   │   appsettings.Development.json             // Development configuration settings
│   │   appsettings.json                          // General application configuration settings
│   │   Program.cs                               // Main entry point for the API
│   │   TariffComparisonModel.API.csproj         // Project file for the API project
│   │
│   ├───Controllers                              // Presentation layer: Handles incoming requests
│   │       TariffComparisonController.cs        // Controller for tariff comparison API endpoints
│   │
│   ├───DTOs                                     // Data Transfer Objects: Defines request/response shapes
│   │       ConsumptionRequestDTO.cs             // DTO for incoming consumption requests
│   │       ResponseTariffDTO.cs                 // DTO for outgoing tariff comparison responses
│   │
│   ├───ExceptionHandler                          // Manages global exceptions
│   │       GlobalExceptionHandler.cs            // Global exception handler for the API
│   │
│   ├───Extensions                                // Extension methods: Adds functionalities to existing classes
│   │       ModelExtension.cs                     // Extensions for model-related functionalities
│   │
│   └───Properties                                // Project settings: Configurations related to the API
│           launchSettings.json                   // Launch settings for the API project
│
└───TariffComparisonModel.Test                    // Test project: Contains unit and integration tests
    │   TariffComparisonModel.Test.csproj        // Project file for the test project
    │
    ├───Controller                                // Tests for the presentation layer
    │       TariffComparisonControllerTest.cs     // Tests for the tariff comparison controller
    │
    ├───IntegrationTest                           // Tests for the integration layer
    │       TariffComparisonIntegrationTest.cs     // Integration tests for the network request validation
    │
    ├───Products                                   // Tests for product-related functionalities
    │       BasicElectricityTariffProductTest.cs   // Tests for basic electricity tariff product
    │       PackagedTariffProductTest.cs          // Tests for packaged tariff product
    │       TariffComparisonModelTest.cs          // General tests for tariff comparison model
    │
    └───SampleData                                // Test data: Contains sample data for testing purposes
            SampleTestData.cs                      // Sample test data used in various tests


```
## Architecture of the Project

### Core (Domain)
- **Product Layer**: Contains essential business logic and domain models, ensuring a strong foundation for the application through well-defined tariff products.
### Services
- **Business Logic Handling**: Orchestrates operations and business logic, facilitating seamless interactions between Tariif Comaprision models and Tariff Comparision API controllers for a cohesive workflow.
### Factories
- **Loose Coupling**: Promotes loose coupling by abstracting diffrent product object creation, allowing for flexible and maintainable code architecture.
### API (Presentation)
- **HTTP Management**: Efficiently manages HTTP requests and responses, exposing application functionality through a structured interface.

### Tests
- **Quality Assurance**: Includes unit and integration tests to ensure high code quality and functionality, providing confidence in the robustness of the application.

## API Endpoint

- `GET /api/TariffComparison/compareCosts?Consumption={kWh/year}`
  - Accepts a query parameter `Consumption` representing annual kWh consumption.
  - Returns a list of tariffs with columns **Tariff name** and **Annual costs (Euro/year)**, sorted by annual cost in ascending order.
  - Example response:[
    {
        "tariffName": "Basic Electricity Tariff",
        "annualCosts": 1380.22
    },
    {
        "tariffName": "Packaged Tariff",
        "annualCosts": 1400.30
    }
]

    
## ASP.NET Core Developer Certificate Issue
If you see the error "The ASP.NET Core developer certificate is in an invalid state," it may be due to an expired or untrusted certificate. 
### To Fix It
Run the following command to trust the developer certificate:

```bash
dotnet dev-certs https --trust

