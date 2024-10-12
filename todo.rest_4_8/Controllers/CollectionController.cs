using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Microsoft.Extensions.Logging;
using todo.application.Contracts;

namespace todo.rest_4_8.Controllers;

public record CollectionCreationDto(string Title);
public record CollectionDto(string Id, string Title);

[RoutePrefix("api/controller")]
public class CollectionController(
    IGetCollectionsUseCase getCollections,
    ICreateTaskCollectionUseCase createTaskCollection,
    ILogger<CollectionController> logger
) : ApiController
{
    [HttpGet]
    public List<CollectionDto> Get()
    {
        return getCollections
            .GetCollections()
            .Select(item => new CollectionDto(Id: item.Id, Title: item.Title))
            .ToList();
    }

    [HttpPost]
    public IHttpActionResult Post([FromBody] CollectionCreationDto createCollectionDto)
    {
        createTaskCollection.CreateTaskCollection(createCollectionDto.Title);
        logger.LogInformation($"Created collection with title {createCollectionDto.Title}");
        return this.Ok();
    }
}
