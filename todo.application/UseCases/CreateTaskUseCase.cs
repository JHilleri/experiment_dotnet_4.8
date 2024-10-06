#nullable enable
using System;
using System.Collections.Generic;
using todo.application.Abstractions;
using todo.domain.Entities;

namespace todo.application.UseCases;

public class CreateTaskUseCase(
    IDateProvider dateProvider,
    ITaskRepository taskRepository,
    ITaskCollectionRepository taskCollectionRepository
)
{
    public string CreateTask(string title, string? message, DateTime? deadline, string collectionId)
    {
        string createdId = Guid.NewGuid().ToString();

        var task = new TaskEntity(
            Id: createdId,
            Title: title,
            Message: message,
            IsCompleted: false,
            CreatedAt: dateProvider.Now,
            Deadline: deadline,
            SubTasks: new List<TaskEntity>().AsReadOnly()
        );

        var taskCollection =
            taskCollectionRepository.GetTaskCollection(collectionId)
            ?? throw new Exception("Task collection not found");

        var updatedTaskCollection = taskCollection.AddTask(task);

        taskRepository.SaveTask(task);
        taskCollectionRepository.SaveTaskCollection(updatedTaskCollection);

        return createdId;
    }
}
