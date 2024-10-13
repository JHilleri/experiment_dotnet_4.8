using todo.application.Abstractions;
using todo.application.core;

namespace todo.application.UseCases;

public record GetCollectionsParam() : IUseCaseParam<IEnumerable<CollectionItemDto>>;

public record CollectionItemDto(string Id, string Title);

[Injectable]
public class GetCollectionsUseCase(ITaskCollectionRepository taskCollectionRepository)
    : IUseCase<GetCollectionsParam, IEnumerable<CollectionItemDto>>
{
    public async Task<IEnumerable<CollectionItemDto>> Execute(GetCollectionsParam request)
    {
        IEnumerable<domain.Aggregate.TaskCollectionAggregate> collections =
            await taskCollectionRepository.GetTaskCollections();
        return collections.Select(
            (collection) => new CollectionItemDto(Id: collection.Id, Title: collection.Title)
        );
    }
}
