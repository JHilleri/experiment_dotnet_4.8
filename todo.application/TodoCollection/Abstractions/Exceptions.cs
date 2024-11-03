namespace todo.application.TodoCollection.Abstractions;

public class InvalidCollectionNameException(string name)
    : Exception($"Invalid collection name: {name}");
