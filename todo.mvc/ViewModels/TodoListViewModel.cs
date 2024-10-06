#nullable enable

using System.Collections.Generic;
using todo.application.Dto;

namespace todo.mvc.ViewModels
{
    public class TodoListViewModel
    {
        public List<CollectionItemDto> Collections { get; set; } = [];
    }
}
