using TasksManagement.Models.Enums;

namespace TasksManagement.Models.Contracts;
public interface IFeedback : ITask
{
    public int Rating { get; set; }

    public StatusFeedback Status { get; set; }
}
