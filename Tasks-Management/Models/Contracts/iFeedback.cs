using TasksManagement.Models.Enums;

namespace TasksManagement.Models.Contracts;
public interface IFeedback
{
    public int Rating { get; set; }

    public StatusFeedback Status { get; set; }
}
