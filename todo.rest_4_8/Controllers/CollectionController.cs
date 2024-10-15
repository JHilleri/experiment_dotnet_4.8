using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Extensions.Logging;
using todo.application.Collection;
using todo.application.core;
using todo.domain.core;

namespace todo.rest_4_8.Controllers;

public record CollectionCreationDto(string Title);

[RoutePrefix("api/controller")]
public class CollectionController(
    IUseCaseService useCaseService,
    ILogger<CollectionController> logger
) : ApiController
{
    [HttpGet]
    public async Task<IEnumerable<CollectionItemDto>> Get()
    {
        return await useCaseService.Execute(new GetCollectionsParam());
    }

    [HttpPost]
    public async Task<IHttpActionResult> CreateCollection(
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
