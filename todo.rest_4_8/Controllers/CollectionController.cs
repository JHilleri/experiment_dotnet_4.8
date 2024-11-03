using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using MediatR;
using Microsoft.Extensions.Logging;
using todo.application.TodoCollection;
using todo.domain.core;

namespace todo.rest_4_8.Controllers;

public record CollectionCreationDto(string Title);

[RoutePrefix("api/controller")]
public class CollectionController(ISender sender, ILogger<CollectionController> logger)
    : ApiController
{
    [HttpGet]
    public async Task<IHttpActionResult> Get() => await sender
            .Send(new GetTodoCollections.Query())
            .Unwrap<IEnumerable<GetTodoCollections.ResponseItem>, IHttpActionResult>(
                this.Ok,
                error =>
                {
                    logger.LogError("failed to get collections: {Error}", error.Message);
                    return this.BadRequest(error.Message);
                }
            );

    [HttpPost]
    public async Task<IHttpActionResult> CreateCollection(
        [FromBody] CollectionCreationDto createCollectionDto
    )
    {
        await sender
            .Send(new CreateTodoCollection.Command(createCollectionDto.Title))
            .Tap(
                _ =>
                    logger.LogInformation(
                        "Created collection with title {Title}",
                        createCollectionDto.Title
                    ),
                error => logger.LogError("failed to create: {Error}", error.Message)
            );
        return this.Ok();
    }
}
