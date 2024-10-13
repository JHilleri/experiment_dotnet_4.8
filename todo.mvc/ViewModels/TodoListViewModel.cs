using System.Collections.Generic;
using todo.application.UseCases;

namespace todo.mvc.ViewModels
{
    public class TodoListViewModel
    {
        public List<CollectionItemDto> Collections { get; set; } = [];
    }
}
