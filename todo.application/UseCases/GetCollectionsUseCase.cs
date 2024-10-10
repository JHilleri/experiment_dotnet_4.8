using todo.application.Abstractions;
using todo.application.Contracts;
using todo.application.Dto;

namespace todo.application.UseCases;

public class GetCollectionsUseCase(ITaskCollectionRepository taskCollectionRepository)
    : IGetCollectionsUseCase
{
    public IEnumerable<CollectionItemDto> GetCollections()
    {
        return taskCollectionRepository
            .GetTaskCollections()
            .Select(
                (collection) => new CollectionItemDto(Id: collection.Id, Title: collection.Title)
            );
    }
}
