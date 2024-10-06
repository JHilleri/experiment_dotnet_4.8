using System.Linq;
using System.Web.Mvc;
using todo.application.Contracts;
using todo.mvc.ViewModels;

namespace todo.mvc.Controllers;

public class HomeController : Controller
{
    private readonly Dependencies dependencies = new();

    public ActionResult Index()
    {
        var getCollectionsUseCase = this.dependencies.Resolve<IGetCollectionsUseCase>();
        return View(
            new TodoListViewModel
            {
                Collections = getCollectionsUseCase.GetCollections().ToList(),
            }
        );
    }

    [HttpPost]
    public ActionResult CreateCollection(string title)
    {
        var createTaskCollectionUseCase =
            this.dependencies.Resolve<ICreateTaskCollectionUseCase>();
        createTaskCollectionUseCase.CreateTaskCollection(title);
        return RedirectToAction("Index");
    }
}
