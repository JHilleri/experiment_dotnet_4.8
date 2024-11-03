using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using MediatR;
using Microsoft.Extensions.Logging;
using todo.application.TodoCollection;
using todo.domain.core;
using todo.mvc.ViewModels;

namespace todo.mvc.Controllers;

public class TodoCollectionController(ISender sender, ILogger<HomeController> logger) : Controller
{
    [HttpGet]
    public async Task<ActionResult> Index() =>
        await sender
            .Send(new GetTodoCollections.Query())
            .Tap(collections =>
                logger.LogInformation(
                    "Loaded collections {Collection}",
                    string.Join(", ", collections.Select(collection => collection.Title))
                )
            )
            .Unwrap(
                collections => this.View(new TodoListViewModel { Collections = collections }),
                error => this.View()
            );

    [HttpPost]
    public async Task<ActionResult> CreateCollection(TodoListViewModel model) =>
        await sender
            .Send(new CreateTodoCollection.Command(model.CollectionCreation.Title))
            .CatchException(
                (Exception ex) =>
                {
                    logger.LogError(
                        ex,
                        "Failed to create collection with title {Title}",
                        model.CollectionCreation.Title
                    );
                    return new Error(ex.Message);
                }
            )
            .Unwrap(
                _ => this.RedirectToAction("Index"),
                error =>
                {
                    logger.LogError("failed to create: {Error}", error.Message);
                    return this.RedirectToAction("Index");
                }
            );
}
