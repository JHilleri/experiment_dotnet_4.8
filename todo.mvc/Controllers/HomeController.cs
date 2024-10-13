using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.Extensions.Logging;
using todo.application.core;
using todo.application.UseCases;
using todo.mvc.ViewModels;

namespace todo.mvc.Controllers;

public class HomeController(
    //IUseCase<GetCollectionsParam, IEnumerable<CollectionItemDto>> getCollectionsUseCase,
    //IUseCase<CreateTaskCollectionParam> createTaskCollectionUseCase,
    IUseCaseService useCase,
    ILogger<HomeController> logger
) : Controller
{
    public async Task<ActionResult> Index()
    {
        //IEnumerable<CollectionItemDto> enumerableCollections = await getCollectionsUseCase.Execute(
        //    new GetCollectionsParam()
        //);
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
        //createTaskCollectionUseCase.Execute(new CreateTaskCollectionParam(title));
        useCase.Execute(new CreateTaskCollectionParam(title));
        logger.LogInformation($"Created collection with title {title}");
        return this.RedirectToAction("Index");
    }
}
