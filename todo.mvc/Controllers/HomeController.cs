using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.Extensions.Logging;
using todo.application.Collection;
using todo.application.core;
using todo.domain.core;
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
    public async Task<ActionResult> CreateCollection(string title)
    {
        Result<string> result = await useCase.Execute(new CreateCollection(title));
        result.Tap(
            _ => logger.LogInformation("Created collection with title {title}", title),
            error => logger.LogError("failed to create: {error}", error.Message)
        );
        return this.RedirectToAction("Index");
    }
}
