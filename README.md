# Pokedex Version 1.0
This application has been written in ASP.NET Core Web API, targeting .NET 5.0

It should run on a non-Windows environment, although I have not been able to test this.
## How to run
### Running in Visual Studio
This can be run by opening the solution file in Visual Studio, and opening the solution file (Pokedex.sln), and running it.
### Running in a Docker Container
Ensure that Docker is installed on your machine.

In the command prompt, type the following to run the application : 
     
	 docker build -t aspnetapp .
     docker run -d -p 8080:80 --name myapp aspnetapp

Documentation for the endpoints can be obtained by accessing the /swagger URL in your browser - e.g http://localhost:8080/swagger

## Production considerations
If this was a production application the following things could be added : 
* Exception reporting using a tool such as RayGun
* Adding restrictions to accessing the API via an API key (if we don't want it public)
* Adding rate limiting on the server, to prevent the API being hammered