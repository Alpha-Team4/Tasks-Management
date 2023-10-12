﻿using TasksManagement.Commands.Enums;
using TasksManagement.Models.Contracts;
using TasksManagement.Models.Enums;

namespace TasksManagement.Models;
public class Bug : Task, IBug
{
    private const string PriorityChangedMessage = "Priority changed from '{0}' to '{1}'";
    private const string SeverityChangedMessage = "Severity changed from '{0}' to '{1}'";
    private const string StatusChangedMessage = "Status changed from '{0}' to '{1}'";
    private const string AssigneeChangedMessage = "Assignee changed from '{0}' to '{1}'";

    private Priority priority;
    private BugSeverity severity;
    private BugStatus status;
    private IMember assignee;
    private readonly IList<string> reproductionSteps = new List<string>();

    public Bug(string title, string description, IMember assignee)
            : base(title, description)
    {
        this.assignee = assignee;

        priority = Priority.Low;
        severity = BugSeverity.Minor;
        status = BugStatus.Active;
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

    public BugSeverity Severity
    {
        get => severity;
        set
        {
            var message = string.Format(SeverityChangedMessage, severity, value);
            eventsList.Add(new Event(message));

            severity = value;
        }
    }

    public BugStatus Status
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

    public IList<string> ReproductionSteps => reproductionSteps;
}