# Banes.Todo
Small ToDo API created to demonstrate some C# concepts

This is meant to be API only. Design choices as follows:

* I left all the MVC boilerplate code VS adds when creating a new WebAPI project
* If desired, the built-in authentication and authorization could be used by simply uncommenting the [Authorize] attribute in the ToDoTasksController
* SSL could be enforced by flipping the AllowInsecureHttp to false in Startup.Auth.cs
* Logging for the controller is kept in a separate class for better separation of concerns and to keep the code uncluttered. I chose to implement it as a collection of extension methods so that they can simply be chained onto the existing code in the controller. The location of the file is configurable via the settings file
* I used LiteDB as the facility for storing the todo tasks. It's a lightweight NoSQL database for .net that simply adds a dll via NuGet and creates a database file on disc. The only caveat is that if the API is being tested using IISExpress, VS must be run in administrator mode. In normal mode it does not have sufficient permissions to create the database file in location where IIS express executes the code from
* To access LiteDB I added a repository to abstract the data layer from the controller and to allow me to inject a mock repository into the unit tests
* The unit tests are trivial but they demonstrate testing each HTTP action specified by the controller

Planned enhancements:
* Add UI piece to the project that will allow for easy ToDo manipulation
* Move the code to .net core to make it platform-agnostic
* Move unit tests to Nunit as MSTest is not compatible with non-Windows OSs