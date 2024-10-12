using System.Linq;
using System.Web.Mvc;
using Microsoft.Extensions.Logging;
using todo.application.Contracts;
using todo.mvc.ViewModels;

namespace todo.mvc.Controllers;

public class HomeController(
    IGetCollectionsUseCase getCollectionsUseCase,
    ICreateTaskCollectionUseCase createTaskCollectionUseCase,
    ILogger<HomeController> logger
) : Controller
{
    public ActionResult Index()
    {
        var collections = getCollectionsUseCase.GetCollections().ToList();
        logger.LogInformation($"Loaded collections {string.Join(", ", collections.Select(collection => collection.Title))}");
        return this.View(
            new TodoListViewModel
            {
                Collections = collections,
            }
        );
    }

    [HttpPost]
    public ActionResult CreateCollection(string title)
    {
        createTaskCollectionUseCase.CreateTaskCollection(title);
        logger.LogInformation($"Created collection with title {title}");
        return this.RedirectToAction("Index");
    }
}
