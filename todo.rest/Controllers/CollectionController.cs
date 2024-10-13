using Microsoft.AspNetCore.Mvc;
using todo.application.core;
using todo.application.UseCases;

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
        await useCaseService.Execute(new CreateTaskCollectionParam(createCollectionDto.Title));
        logger.LogInformation($"Created collection with title {createCollectionDto.Title}");
        return this.RedirectToAction("Get");
    }
}
