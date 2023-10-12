using TasksManagement.Commands.Enums;
using TasksManagement.Models.Enums;

namespace TasksManagement.Models.Contracts;
public interface IBug : ITask
{
    public Priority Priority { get; set; }

    public BugSeverity Severity { get; set; }

    public BugStatus Status { get; set; }

    public IMember Assignee { get; set; }

    public IList<string> ReproductionSteps { get; }
}
