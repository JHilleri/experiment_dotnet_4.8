using System.Web.Mvc;

namespace todo.mvc.Controllers;

public class HomeController : Controller
{
    public ActionResult Index() => this.RedirectToAction("Index", "TodoCollection");
}
