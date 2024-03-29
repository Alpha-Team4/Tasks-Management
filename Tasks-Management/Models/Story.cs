﻿using TasksManagement.Commands.Enums;
using TasksManagement.Models.Contracts;
using TasksManagement.Models.Enums;

namespace TasksManagement.Models;
public class Story : Task, IStory, IHasAssignee
{
    private const string PriorityChangedMessage = "Priority changed from '{0}' to '{1}'";
    private const string SizeChangedMessage = "Size changed to '{0}'";
    private const string StatusChangedMessage = "Status changed to '{0}'";
    private const string AssigneeChangedMessage = "Story '{0}' assigned to '{1}'";

    private Priority priority;
    private Size size;
    private StatusStory status;
    private IMember assignee;

    public Story(string title, string description)
        : base(title, description)
    {
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
            AddEvent(message);

            priority = value;
        }
    }

    public Size Size
    {
        get => size;
        set
        {
            var message = string.Format(SizeChangedMessage, size, value);
            AddEvent(message);

            size = value;
        }
    }

    public StatusStory Status
    {
        get => status;
        set
        {
            var message = string.Format(StatusChangedMessage, status, value);
            AddEvent(message);

            status = value;
        }
    }

    public IMember Assignee
    {
        get => assignee;
        set
        {
            var message = string.Format(AssigneeChangedMessage, Title, value.Name);

            assignee = value;
        }
    }

    public override string GetCurrentStatus()
    {
        return Status.ToString();
    }

    public override string ToString()
    {
        var assigneeName = Assignee != null ? Assignee.Name : string.Empty;

        return $"""
                Story (ID: {Id})
                  Title: {Title}
                  Description: {Description}
                  Status: {status}
                  Size: {size}
                  Priority: {priority}
                  Assignee: {assigneeName}
                    {ShowAllComments()}
                """;
    }
}
