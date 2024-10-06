#nullable enable

namespace todo.application.Contracts;

public interface ICreateTaskCollectionUseCase
{
    void CreateTaskCollection(string title);
}
