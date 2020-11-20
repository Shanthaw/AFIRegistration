
This is a .Net Core 3.1 Web Api project.
When the application runs swagger api documentation will load.
Database Connection string should be updated 
[field "ConnectionString" in "appsettings.Development.json" file 
if it runs in development mode. Otherwise update/Add same entry in appsettings.json file]

Developer note:

* There are a few ways to do validation. 
	- controller level ex: using FluentValidation package
	- domain level validation
	- controller level and domain level both validation
 In this project domain level (Entity) validation is implemented as validation on entity level seems to be more
 appropriate rather validating DTOs. (choise made)

 * Repository pattern impelemented with a generic repository keeping extendability in mind.
 * SOLID, DRY, KISS principles were applied wherever possible.
 * Logs are written to the console. It can be configured sending logs to Azure application insights telemerty, grey logs 
 or elasticsearch etc.. can use serilog if needed.
 * In the Unit test project, AutoFixture package and MOQ package are used.
 