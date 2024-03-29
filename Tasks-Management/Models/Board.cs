﻿using TasksManagement.Models.Contracts;

namespace TasksManagement.Models;
public class Board : IBoard
{
    private const int BoardNameMinLength = 5;
    private const int BoardNameMaxLength = 10;
    private const string BoardNameErrorMessage = "Board name must be between {0} and {1} characters.";
    private const string NoTasksFoundMessage = "--NO TASKS--";

    private const string TaskAddedMessage = "Task '{0}' added to board '{1}'.";

    private string name;
    private readonly IList<ITask> tasks = new List<ITask>();
    private readonly IList<IEvent> history = new List<IEvent>();

    public Board(string name) 
    {
        Name = name;
    }  
    public string Name
    {
        get => name;
        private set
        {
            Validator.ValidateStringLength(value, BoardNameMinLength, BoardNameMaxLength,
                string.Format(BoardNameErrorMessage, BoardNameMinLength, BoardNameMaxLength));
            name = value;
        }
    }

    public IList<IEvent> History => new List<IEvent>(history);

    public IList<ITask> Tasks => new List<ITask>(tasks);

    public void AddEvent(string message)
    {
        history.Add(new Event(message));
    }

    public void AddTask(ITask taskToAdd)
    {
        tasks.Add(taskToAdd);

        var message = string.Format(TaskAddedMessage, taskToAdd.Title, Name);
        AddEvent(message);
    }

    public string PrintAllTasks()
    {
        if (!tasks.Any())
        {
            return NoTasksFoundMessage;
        }

        return string.Join("\r\n",
                tasks.Select(task => task.ToString())
                );
    }

    public override string ToString()
    {
        return $"""
                Name: {Name}
                  Tasks:
                    {PrintAllTasks()}
                """;
    }
}
