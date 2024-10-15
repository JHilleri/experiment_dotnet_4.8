using Microsoft.AspNetCore.Mvc;
using todo.application.Collection;
using todo.application.core;
using todo.domain.core;

namespace todo.rest.Controllers;

public record CollectionCreationDto(string Title);

[ApiController]
[Route("/collection")]
public class CollectionsController(
    IUseCaseService useCaseService,
    ILogger<CollectionsController> logger
) : ControllerBase
{
    [HttpGet(Name = "GetCollections")]
    public async Task<IEnumerable<CollectionItemDto>> Get()
    {
        return await useCaseService.Execute(new GetCollectionsParam());
    }

    [HttpPost(Name = "CreateCollection")]
    public async Task<IActionResult> CreateCollection(
        [FromBody] CollectionCreationDto createCollectionDto
    )
    {
        await useCaseService
            .Execute(new CreateCollection(createCollectionDto.Title))
            .Tap(
                _ =>
                    logger.LogInformation(
                        "Created collection with title {title}",
                        createCollectionDto.Title
                    ),
                error => logger.LogError("failed to create: {error}", error.Message)
            );
        return this.Ok();
    }
}
