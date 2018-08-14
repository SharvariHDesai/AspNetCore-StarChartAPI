# Build a Starchart Web API using ASP.NET Core

The Build a Starchart Web API using ASP.NET Core Application is designed to allow users to submit and retrieve data about celestial objects. This will cover using EntityFramework to retrieve, add, update, and remove data from an in memory database and making it accessible via a web service.

# Setup the Application

## If you want to use Visual Studio
If you want to use Visual Studio (highly recommended) follow the following steps:
-   If you already have Visual Studio installed make sure you have .Net Core installed by running the "Visual Studio Installer" and making sure ".NET Core cross-platform development" is checked
-   If you need to install visual studio download it at https://www.microsoft.com/net/download/ (If you're using Windows you'll want to check "ASP.NET" and ".NET Core cross-platform development" on the workloads screen during installation.)
-   Open the .sln file in visual studio
-   To run the application simply press the Start Debug button (green arrow) or press F5
-   If you're using Visual Studio on Windows, to run tests open the Test menu, click Run, then click on Run all tests (results will show up in the Test Explorer)
-   If you're using Visual Studio on macOS, to run tests, select the GradeBookTests Project, then go to the Run menu, then click on Run Unit Tests (results will show up in the Unit Tests panel)

(Note: All tests should fail at this point, this is by design. As you progress through the projects more and more tests will pass. All tests should pass upon completion of the project.)

## If you don't plan to use Visual studio
If you would rather use something other than Visual Studio
-   Install the .Net Core SDK from https://www.microsoft.com/net/download/core once that installation completes you're ready to roll!
-   To run the application go into the StarChart project folder and type `dotnet run`
-   To run the tests go into the StarChartTests project folder and type `dotnet test`

# Features you will implement

- Configuring MVC and EntityFramework
- Retrieving data from the database
- Submitting data to the database
- Updating existing data
- Remove data from the database

## Tasks necessary to complete implementation:

__Note:__ this isn't the only way to accomplish this, however; this is what the project's tests are expecting. Implementing this in a different way will likely result in being marked as incomplete / incorrect.

- [ ] Adding Middleware/Configuration to `Startup.cs`
  - [ ] In the `ConfigureServices` method call `AddMvc` to add support for MVC middleware.
  - [ ] In the `Configure` method remove the `app.Run` entirely and replace it with a call to `UseMvc` on `app`.
  - [ ] In the `ConfigureServices` method call `AddDbContext<ApplicationDbContext>` on `services` with the argument `options => options.UseInMemoryDatabase("StarChart")` to point `EntityFramework` to the application's `DbContext`. (Note: You will need to add a `using` directive for `StartChart.Data`)
- [ ] Create `CelestialObject` Model
  - [ ] Create a new class `CelestialObject` in the `Models` directory
  - [ ] Create a new property of type `int` named `Id`.
  - [ ] Create a new property of type `string` named `Name`. This property should have the `Required` attribute. (Note: you will need to add a `using` directive for `Systems.ComponentModel.DataAnnotations`)
  - [ ] Create a new property of type `CelestialObject` named `OrbitedObject`.
  - [ ] Create a new property of type `List<CelestialObject>` named `Satellites`. This property should have the `NotMapped` attribute. (Note: you will need to add using directives for `System.Collections.Generic` and `System.ComponentModels.DataAnnotations.Schema`)
  - [ ] Create a new property of type `TimeSpan` named `OrbitalPeriod`.
- [ ] Create `CelestialObjectController` class
  - [ ] Create a new class `CelestialObjectController` that inherits the `ControllerBase` class (Note: you will need to add a `using` directive for `Microsoft.AspNetCore.Mvc`)
  - [ ] Add a `route` attribute with a value of `string.empty` and `ApiController` attribute to the `CelestialObjectController`.
  - [ ] Create a new private field of type `ApplicationDbContext` named `_context`.
  - [ ] Create a constructor that accepts a parameter of type `ApplicationDbContext` and sets the `_context` field using the provided parameter.
- [ ] Create all `Get` methods
  - [ ] Create a new method `GetById` that accepts a parameter of type `int` named `id`. This method should have an `HttpGet` attribute with a value of `"{id}"`. This method should return an `Ok` with a value of the `CelestialObject` who's `Id` property matches the `id` parameter.
  - [ ] `GetById` should return `NotFound` there is no `CelestialObject` with an `Id` property that matches the parameter.
  - [ ] `GetById` should also set the `Satellites` property to any `CelestialObjects` who's `OrbitedObject` is the current `CelestialObject`.
  - [ ] Create a new method `GetByName` that accepts a parameter of type `string` named `name`. This method should have an `HttpGet` attribute with a value of `"{name}"`. This method should return an `Ok` with a value of the `CelestialObject` who's `Name` property matches the `name` parameter.
  - [ ] `GetByName` should return `NotFound` there is no `CelestialObject` with an `Name` property that matches the `name` parameter.
  - [ ] `GetByName` should also set the `Satellites` property to any `CelestialObjects` who's `OrbitedObject` is the current `CelestialObject`.
  - [ ] Create a new method `GetAll` with no parameters. This method should have an `HttpGet` attribute. This method should return `Ok` with a value of all `CelestialObjects`s.
  - [ ] `GetAll` should also set the `Satellites` property for each `CelestialObject` returned (as we've done in the last two `Get` methods).
- [ ] Create all remaining methods
  - [ ] Create a new method `Create` that accepts a parameter of type `[FromBody]CelestialObject`. This method should have an `HttpPost` attribute. This method should return a `CreatedAtRoute` with the arguments `"GetById"`, a new `object` with an `id` of the `CelestialObject`'s `Id` , and the newly created `CelestialObject`.
  - [ ] `Create` should check if the `ModelState` is valid, if not it should return a `BadRequest`.
  - [ ] If the `ModelState` is valid, `Create` should add the provided `CelestialObject` to the `CelestialObjects` `DbSet` then `SaveChanges`.
  - [ ] Create a new method `Update` that accepts a parameter of type `int` named `id` and a parameter of type `CelestialObject`. This method should have the `HttpPut` attribute with a value of `"{id}"`. This method should return `NoContent`.
  - [ ] `Update` should locate the `CelestialObject` with an `Id` that matches the provided `int` parameter. If no match is found return `NotFound`.
  - [ ] If a match is found set it's `Name`, `OrbitalPeriod`, `OrbitedObject`, and `Satellites` properties based on the provided `CelestialObject` parameter. Call `Update` on the `CelestialObjects` `DbSet` with an argument of the updated `CelestialObject`, and then call `SaveChanges`.
  - [ ] Create a new method `UpdateName` that accepts a parameter of type `int` named `id` and a parameter of type `string`. This method should have the `HttpPatch` attribute with an argument of `"{id}"`. This method should return `NoContent`.
  - [ ] `UpdateName` should locate the `CelestialObject` with an `Id` that matches the provided `int` parameter. If no match is found return `NotFound`.
  - [ ] If a match is found set it's `Name` property to the provided `string` parameter. Then call `Update` on the `CelestialObjects` `DbSet` with an argument of the updated `CelestialObject`, and then call `SaveChanges`.
  - [ ] Create a new method `Delete` that accepts a parameter of type `int` named `id`. This method should have the `HttpDelete` attribute with an argument of `"{id}"`. This method should return `NoContent`.
  - [ ] `Delete` should get a `List` of all `CelestialObject`s who either have an `Id` or `OrbitedObject` with an `Id` that matches the provided parameter. If there are no matches it should return `NotFound`.
  - [ ] If there are matching `CelestialObject`s call `RemoveRange` on the `CelestialObjects` `DbSet` with an argument of the list of matching `CelestialObject`s. Then call `SaveChanges`.
	
## What Now?

You've completed the tasks of this project, if you want to continue working on this project there will be additional projects added to the ASP.NET Core path that continue where this project left off adding more advanced views and models, as well as providing and consuming data as a web service.

Otherwise now is a good time to continue on the ASP.NET Core path to expand your understanding of the ASP.NET Core framework or take a look at the Microsoft Azure for Developers path as Azure is a common choice for hosting, scaling, and expanding the functionality of ASP.NET Core applications.