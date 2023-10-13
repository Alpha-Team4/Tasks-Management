using TasksManagement.Commands.Enums;
using TasksManagement.Models.Enums;

namespace TasksManagement.Models.Contracts;
public interface IBug : ITask
{
    public Priority Priority { get; set; }

    public Severity Severity { get; set; }

    public StatusBug Status { get; set; }

    public IMember Assignee { get; set; }

    public IList<string> ReproductionSteps { get; }
}
