# Northwind Web API example

My goal was to implement the simplest possible example of an ASP.NET Web API. The solution contains four projects, each project represents a layer. The idea is to achieve a separation of concerns. Each layer has a narrow scope of responsibility and is only aware of the layer below it. It is typically a good idea to avoid calls betweeen components within the same layer as this results in dependencies between those components. 

The Models layer is the exception, see description below for details. 

![Spring Web Application Architecture](https://www.petrikainulainen.net/wp-content/uploads/spring-web-app-architecture.png "Web Application Architecture (approximate representation)")

## Web API Layers
The Visual Studio Solution includes the following major components
### Models
This layer contains objects that map on a one-to-one basis with tables in the Northwind database. I used an 'ADO.NET Entity Model' to generate the classes rather than type them by hand. Some artifacts from the template remain in the code. The .EDMX file while physically present was excluded from the project once the desired classes were generated.
The objects in this layer are known across all layers and used to pass messages between the Web API Controllers, Services and Repositories.

### Data
This project contains the repository layer. This layer is responsible for all interaction with the database and is unaware of the Services & Controllers. Its job is to simply store and/or retrieve information from the database.

### Services
This layer contains the all business logic for the application. It interacts with the Data layer.

### Controllers
The Controllers are part of the main MVC project which was generated from the standard ASP.NET MVC template. The controllers act as the go between the UI (views) and the services layer. Their task is to simply to respond to application events by gathering data received from the UI, and to call the relevant Services that can process the event data.


## Acknowledgements
1. [Pragim Technologies - ASP.NET Web API tutorial for beginners](https://www.youtube.com/playlist?list=PL6n9fhu94yhW7yoUOGNOfHurUE6bpOO2b)

2. [Roger Harford - How to structure a .net project ](https://www.youtube.com/watch?v=KM5o9M4cuQA)

3. [Petri Kainulainen - Understanding Spring Web Application Architecture: The Classic Way](https://www.petrikainulainen.net/software-development/design/understanding-spring-web-application-architecture-the-classic-way/)

4. [Jonathan Danylko - Creating a Repository Pattern without an ORM](https://www.danylkoweb.com/Blog/creating-a-repository-pattern-without-an-orm-A9)
