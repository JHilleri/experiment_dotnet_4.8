using System.Collections.Generic;

namespace todo.mvc.ViewModels;

public class TodoItemViewModel
{
    public string Id { get; set; } = "";
    public string Title { get; set; } = "";
    public bool IsComplete { get; set; } = false;
}

public class TodoItemCreationViewModel
{
    public string Title { get; set; } = "";
}

public class TodoCollectionDetailViewModel
{
    public string Id { get; set; } = "";
    public string Title { get; set; } = "";
    public IEnumerable<TodoItemViewModel> Items { get; set; } = [];
    public string ErrorMessage { get; set; } = "";
    public TodoItemCreationViewModel ItemCreation { get; set; } = new();
}
