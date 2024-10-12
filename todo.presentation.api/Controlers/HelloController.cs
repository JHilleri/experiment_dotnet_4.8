using System.Web.Http;

namespace todo.presentation.api.Controllers;

public class HelloController : ApiController
{
    [HttpGet]
    public string Get()
    {
        return "Hello, World!";
    }
}
