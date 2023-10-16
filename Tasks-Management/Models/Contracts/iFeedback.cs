using TasksManagement.Models.Enums;

namespace TasksManagement.Models.Contracts;
public interface IFeedback : ITask
{
    public Rating Rating { get; set; }

    public StatusFeedback Status { get; set; }
}
