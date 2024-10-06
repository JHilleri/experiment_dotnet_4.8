#nullable enable

using System;
using System.Collections.Generic;
using todo.application.Abstractions;
using todo.application.Contracts;
using todo.domain.Aggregate;
using todo.domain.Entities;

namespace todo.application.UseCases;

public class CreateTaskCollectionUseCase(ITaskCollectionRepository taskCollectionRepository) : ICreateTaskCollectionUseCase
{
    public void CreateTaskCollection(string title)
    {
        var taskCollection = new TaskCollectionAggregate(
            Id: Guid.NewGuid().ToString(),
            Title: title,
            Tasks: new List<TaskEntity>().AsReadOnly(),
            Message: null
        );
        taskCollectionRepository.SaveTaskCollection(taskCollection);
    }
}
