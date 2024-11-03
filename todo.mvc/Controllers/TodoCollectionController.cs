using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using MediatR;
using Microsoft.Extensions.Logging;
using todo.application.TodoCollection;
using todo.application.TodoItem;
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

    [HttpGet]
    public async Task<ActionResult> GetCollection(string id) =>
        await sender
            .Send(new GetTodoCollection.Query(id))
            .Unwrap(
                collection =>
                    this.View(
                        "CollectionDetail",
                        new TodoCollectionDetailViewModel
                        {
                            Id = collection.Id,
                            Title = collection.Title,
                            Items = collection.Items.Select(item => new TodoItemViewModel
                            {
                                Id = item.Id,
                                Title = item.Title,
                                IsComplete = item.IsComplete,
                            }),
                        }
                    ),
                error =>
                    this.View(
                        "CollectionDetail",
                        new TodoCollectionDetailViewModel { ErrorMessage = error.Message }
                    )
            );

    [HttpPost]
    public async Task<ActionResult> AddTask(string id, TodoCollectionDetailViewModel model) =>
        await sender
            .Send(new CreateTodoItem.Command(Title: model.ItemCreation.Title, CollectionId: id))
            .Unwrap<string, ActionResult>(
                _ => this.RedirectToAction("GetCollection", new { id }),
                error =>
                {
                    logger.LogError("failed to add task: {Error}", error.Message);
                    return this.View(
                        "CollectionDetail",
                        new TodoCollectionDetailViewModel { ErrorMessage = error.Message }
                    );
                }
            );
}
