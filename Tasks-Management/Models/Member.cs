using System.Collections.Generic;
using TasksManagement.Models.Contracts;

namespace TasksManagement.Models;
public class Member : IMember
{
    private const int MemberNameMinLength = 5;
    private const int MemberNameMaxLength = 15;
    private const string MemberNameErrorMessage = "Member name must be between {0} and {1} characters.";
    private const string NoTasksFoundMessage = "--NO TASKS--";
    private const string NoHistoryFoundMessage = "--NO HISTORY--";

    private const string TaskAddedMessage = "Task '{0}' assigned to member '{1}'.";

    private string name;
    private readonly IList<IEvent> history = new List<IEvent>();
    private readonly IList<ITask> tasks = new List<ITask>(); 

    public Member(string name)
    {
        Name = name;
    } 
    public string Name
    {
        get => name;
        private set
        {
            Validator.ValidateStringLength(value, MemberNameMinLength, MemberNameMaxLength,
                string.Format(MemberNameErrorMessage, MemberNameMinLength, MemberNameMaxLength));
            name = value;
        }
    }

    public IList<IEvent> History => new List<IEvent>(history);

    public IList<ITask> Tasks => new List<ITask>(tasks);

    private void AddEvent(string message)
    {
        history.Add(new Event(message));
    }

    public void AddTask(ITask task)
    {
        tasks.Add(task);
        var message = string.Format(TaskAddedMessage, task.Title, Name);
        AddEvent(message);
    }

    public void RemoveTask(ITask task)
    {
        tasks.Remove(task);    
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
