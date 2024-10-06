using System.Collections.Generic;
using todo.application.Dto;

namespace todo.application.Contracts
{
    public interface IGetCollectionsUseCase
    {
        IEnumerable<CollectionItemDto> GetCollections();
    }
}