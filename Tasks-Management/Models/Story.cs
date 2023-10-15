using TasksManagement.Commands.Enums;
using TasksManagement.Models.Contracts;
using TasksManagement.Models.Enums;

namespace TasksManagement.Models;
internal class Story : Task, IStory
{
    private const string PriorityChangedMessage = "Priority changed from '{0}' to '{1}'";
    private const string SizeChangedMessage = "Size changed from '{0}' to '{1}'";
    private const string StatusChangedMessage = "Status changed from '{0}' to '{1}'";
    private const string AssigneeChangedMessage = "Assignee changed from '{0}' to '{1}'";

    private Priority priority;
    private Size size;
    private StatusStory status;
    private IMember assignee;

    public Story(string title, string description, IBoard board)
        : base(title, description)
    {
        this.assignee = assignee;

        priority = Priority.Low;
        size = Size.Small;
        status = StatusStory.NotDone;
    }

    public Priority Priority
    {
        get => priority;
        set
        {
            var message = string.Format(PriorityChangedMessage, priority, value);
            eventsList.Add(new Event(message));

            priority = value;
        }
    }

    public Size Size
    {
        get => size;
        set
        {
            var message = string.Format(SizeChangedMessage, size, value);
            eventsList.Add(new Event(message));

            size = value;
        }
    }

    public StatusStory Status
    {
        get => status;
        set
        {
            var message = string.Format(StatusChangedMessage, status, value);
            eventsList.Add(new Event(message));

            status = value;
        }
    }

    public IMember Assignee
    {
        get => assignee;
        set
        {
            var message = string.Format(AssigneeChangedMessage, assignee, value);
            eventsList.Add(new Event(message));

            assignee = value;
        }
    }

    public override string GetCurrentStatus()
    {
        return Status.ToString();
    }

    public override string ToString()
    {
        return $"""
                Story (ID: {Id})
                   Title: {Title}
                   Description: {Description}
                   Status: {status}
                   Size: {size}
                   Priority: {priority}
                   Assignee: {assignee}
                     Comments:
                       {ShowAllComments}
                """;
    }
}
