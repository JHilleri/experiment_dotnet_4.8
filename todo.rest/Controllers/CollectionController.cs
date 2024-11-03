using MediatR;
using Microsoft.AspNetCore.Mvc;
using todo.application.TodoCollection;
using todo.domain.core;

namespace todo.rest.Controllers;

public record CollectionCreationInput(string Title);

public record CollectionCreationOutput(string Id);

[ApiController]
[Route("/collection")]
public class CollectionsController(ISender sender, ILogger<CollectionsController> logger)
    : ControllerBase
{
    [HttpGet(Name = "GetCollections")]
    [ProducesResponseType<IEnumerable<GetTodoCollections.ResponseItem>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get() =>
        await sender
            .Send(new GetTodoCollections.Query())
            .Unwrap(
                this.Ok,
                (error) =>
                {
                    logger.LogError("failed to get collections: {Error}", error.Message);
                    return this.Problem(error.Message, statusCode: 400);
                }
            );

    [HttpPost(Name = "CreateCollection")]
    [ProducesResponseType<CollectionCreationOutput>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCollection(
        [FromBody] CollectionCreationInput createCollectionDto
    )
    {
        var result = await sender.Send(new CreateTodoCollection.Command(createCollectionDto.Title));

        if (result.IsSuccess)
        {
            logger.LogInformation(
                "Created collection with title {Title}",
                createCollectionDto.Title
            );
            return this.Ok(result.GetValue());
        }

        logger.LogError("failed to create: {Error}", result.GetError().Message);
        return this.Problem(result.GetError().Message, statusCode: 400);
    }
}
