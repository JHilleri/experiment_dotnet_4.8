using todo.domain.core;

namespace todo.application.TodoCollection.Abstractions;

public record TodoCollectionNotFoundError(string RequestedId)
    : Error($"Todo collection (Id=\"{RequestedId}\") not found");

public record InvalidCollectionNameError(string Name)
    : Error($"Invalid todo collection name: {Name}");
