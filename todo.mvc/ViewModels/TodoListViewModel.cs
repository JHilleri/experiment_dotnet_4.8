using System.Collections.Generic;
using todo.application.Collection;

namespace todo.mvc.ViewModels
{
    public class TodoListViewModel
    {
        public List<CollectionItemDto> Collections { get; set; } = [];
    }
}
