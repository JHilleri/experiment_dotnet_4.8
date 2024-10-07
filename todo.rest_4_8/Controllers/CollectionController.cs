using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web.Http;
using Microsoft.Extensions.Logging;
using todo.application.Contracts;

namespace todo.rest_4_8.Controllers;

public record CollectionCreationDto(string Title);

[DataContract]
public class CollectionDto
{
    [DataMember]
    public string Title { get; set; }

    [DataMember]
    public string Id { get; set; }
}

public class CollectionController(
    IGetCollectionsUseCase getCollections,
    ICreateTaskCollectionUseCase createTaskCollection,
    ILogger<CollectionController> logger
) : ApiController
{
    public List<CollectionDto> Get()
    {
        return getCollections
            .GetCollections()
            .Select(item => new CollectionDto { Id = item.Id, Title = item.Title })
            .ToList();
    }

    public void Post([FromBody] CollectionCreationDto createCollectionDto)
    {
        createTaskCollection.CreateTaskCollection(createCollectionDto.Title);
        logger.LogInformation($"Created collection with title {createCollectionDto.Title}");
    }
}
