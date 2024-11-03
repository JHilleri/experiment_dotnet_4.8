using System.Collections.Generic;
using todo.application.TodoCollection;

namespace todo.mvc.ViewModels;

public class TodoListViewModel
{
    public IEnumerable<GetTodoCollections.ResponseItem> Collections { get; set; } = [];
    public CollectionCreation CollectionCreation { get; set; } = new();
}

public class CollectionCreation
{
    public string Title { get; set; } = "";
}
