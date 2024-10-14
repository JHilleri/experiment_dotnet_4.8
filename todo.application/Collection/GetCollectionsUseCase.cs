using todo.application.core;
using todo.domain.Collection;

namespace todo.application.Collection;

public record GetCollectionsParam() : IUseCase<IEnumerable<CollectionItemDto>>;

public record CollectionItemDto(string Id, string Title);

[Injectable]
public class GetCollectionsUseCase(ITaskCollectionRepository taskCollectionRepository)
    : IUseCaseImplementation<GetCollectionsParam, IEnumerable<CollectionItemDto>>
{
    public async Task<IEnumerable<CollectionItemDto>> Execute(GetCollectionsParam request)
    {
        IEnumerable<TaskCollectionAggregate> collections =
            await taskCollectionRepository.GetTaskCollections();
        return collections.Select(
            (collection) => new CollectionItemDto(Id: collection.Id, Title: collection.Title)
        );
    }
}
