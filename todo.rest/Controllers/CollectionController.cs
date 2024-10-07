using Microsoft.AspNetCore.Mvc;
using todo.application.Contracts;
using todo.application.Dto;

namespace todo.rest.Controllers;

public record CollectionCreationDto(string Title);

[ApiController]
[Route("/collection")]
public class CollectionsController(
    IGetCollectionsUseCase getCollections,
    ICreateTaskCollectionUseCase createTaskCollection,
    ILogger<CollectionsController> logger
) : ControllerBase
{
    [HttpGet(Name = "GetCollections")]
    public IEnumerable<CollectionItemDto> Get()
    {
        return getCollections.GetCollections();
    }

    [HttpPost(Name = "CreateCollection")]
    public IActionResult CreateCollection([FromBody] CollectionCreationDto createCollectionDto)
    {
        createTaskCollection.CreateTaskCollection(createCollectionDto.Title);
        logger.LogInformation($"Created collection with title {createCollectionDto.Title}");
        return this.RedirectToAction("Get");
    }
}
