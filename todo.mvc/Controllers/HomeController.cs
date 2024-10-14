using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.Extensions.Logging;
using todo.application.Collection;
using todo.application.core;
using todo.mvc.ViewModels;

namespace todo.mvc.Controllers;

public class HomeController(IUseCaseService useCase, ILogger<HomeController> logger) : Controller
{
    public async Task<ActionResult> Index()
    {
        var enumerableCollections = await useCase.Execute(new GetCollectionsParam());
        var collections = enumerableCollections.ToList();
        logger.LogInformation(
            $"Loaded collections {string.Join(", ", collections.Select(collection => collection.Title))}"
        );
        return this.View(new TodoListViewModel { Collections = collections });
    }

    [HttpPost]
    public ActionResult CreateCollection(string title)
    {
        useCase.Execute(new CreateTaskCollectionParam(title));
        logger.LogInformation($"Created collection with title {title}");
        return this.RedirectToAction("Index");
    }
}
