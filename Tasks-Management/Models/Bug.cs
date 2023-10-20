using System.Runtime.CompilerServices;
using System.Text;
using TasksManagement.Commands.Enums;
using TasksManagement.Models.Contracts;
using TasksManagement.Models.Enums;

[assembly: InternalsVisibleTo("TasksManagement-Tests")]
namespace TasksManagement.Models;
internal class Bug : Task, IBug, IHasAssignee
{
    private const string PriorityChangedMessage = "Priority changed from '{0}' to '{1}'";
    private const string SeverityChangedMessage = "Severity changed from '{0}' to '{1}'";
    private const string StatusChangedMessage = "Status changed from '{0}' to '{1}'";
    private const string AssigneeChangedMessage = "Bug '{0}' assigned to '{1}'";

    private Priority priority;
    private Severity severity;
    private StatusBug status;
    private IMember assignee;
    private readonly IList<string> reproductionSteps = new List<string>();

    public Bug(string title, string description)
            : base(title, description)
    {
        priority = Priority.Low;
        severity = Severity.Minor;
        status = StatusBug.Active;
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

    public Severity Severity
    {
        get => severity;
        set
        {
            var message = string.Format(SeverityChangedMessage, severity, value);
            AddEvent(message);

            severity = value;
        }
    }

    public StatusBug Status
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

    public IList<string> ReproductionSteps => new List<string>(reproductionSteps);

    public void AddReproductionStep(string step)
    {
        reproductionSteps.Add(step);
    }

    public string PrintReproductionSteps()
    {
        StringBuilder sb = new();

        var stepIndex = 0;

        foreach (var step in ReproductionSteps)
        {
            sb.AppendLine($"{++stepIndex}. {step}");
        }

        return sb.ToString().TrimEnd();
    }

    public override string GetCurrentStatus()
    {
        return Status.ToString();
    }

    public override string ToString()
    {
        var assigneeName = Assignee != null ? Assignee.Name : string.Empty;

        return $"""
                Bug (ID: {Id})
                  Title: {Title}
                  Description: {Description}
                  Status: {status}
                  Reproduction Steps: {PrintReproductionSteps()}
                  Priority: {priority}
                  Severity: {severity}
                  Assignee: {assigneeName}
                    {ShowAllComments()}
                """;
    }
}
