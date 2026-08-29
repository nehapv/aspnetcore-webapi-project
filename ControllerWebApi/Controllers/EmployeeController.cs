using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ControllerWebApi.Controllers
{
    //case -3
    //how to solve ambiguity: add action with route attribute to the action method to specify a unique route for that method. This will ensure that each action method has a distinct URL and will eliminate any ambiguity in routing.

    //GET //api/employee

    [Route("api/[controller]/[action]")]
    [ApiController]

    public class EmployeeController : ControllerBase
    {



        //GET //api/employee/GET

        
        [HttpGet]

        public string Get()
        {
            return "Returning from EmployeeController Get Method";
        }



        //GET //api/employee/GetEmployee


        [HttpGet]
        public string GetEmployee()
        {
            return "Returning from EmployeeController GetEmployee Method";
        }

    }
}













//case -1 single get method with no ambiguity
//Normal explanation of the code:

////route attribute specifies the base URL for all the actions in the controller. In this case, it is set to "api/[controller]", where [controller] is a placeholder that will be replaced with the name of the controller (in this case, "Employee"). This means that the base URL for this controller will be "api/employee".
//[Route("api/[controller]")]
//[ApiController]

////controlbase is a base class fo web api controllers that provides common functionality for handling HTTP requests and responses. It is part of the Microsoft.AspNetCore.Mvc namespace and is used to create RESTful APIs in ASP.NET Core applications.
//public class EmployeeController : ControllerBase
//{
//    //httpget attribute indicates that this action method will handle HTTP GET requests. When a GET request is made to the URL "api/employee", this method will be invoked.
//    [HttpGet]

//    //the get method returns a string message indicating that it is returning from the EmployeeController's Get method. This is a simple example of an action method that can be expanded to return actual employee data in a real-world application.
//    public string Get()
//    {
//        return "Returning from EmployeeController Get Method";
//    }





/////////////////////////

//case -2 2 get methods with ambiguity
//ambiguity example: In this example, we have two action methods (Get() and GetEmployee()) that are both decorated with the [HttpGet] attribute and have the same route. This can lead to ambiguity in routing, as the framework won't know which method to invoke when a GET request is made to the "api/employee" URL.



//api/employee
//is the base URL for this controller. The [controller] placeholder will be replaced with the name of the controller, which is "Employee".
//This means that any action methods defined in this controller will be accessible via URLs that start with "api/employee".
//[Route("api/[controller]")]
//[ApiController]

//public class EmployeeController : ControllerBase
//{

//    //GET //api/employee
//    //is the URL that will trigger this action method.
//    //When a GET request is made to this URL, the Get() method will be invoked.

//    [HttpGet]

//    public string Get()
//    {
//        return "Returning from EmployeeController Get Method";
//    }



//    //GET //api/employee
//    //ambiguity arises because both Get() and GetEmployee() methods are decorated with the [HttpGet] attribute and have the same route. In ASP.NET Core, when multiple action methods match the same HTTP verb and route, it can lead to ambiguity in routing, resulting in a runtime error.
//    //ambuguity request having multple endpoints with the same HTTP verb and route can cause confusion for the routing system, as it won't know which action method to invoke when a request is made to that route. This can lead to unexpected behavior or errors in your application.

//    [HttpGet]
//    public string GetEmployee()
//    {
//        return "Returning from EmployeeController GetEmployee Method";
//    }

//}