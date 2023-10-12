using TasksManagement.Commands.Enums;
using TasksManagement.Models.Enums;

namespace TasksManagement.Models.Contracts;
public interface IStory : ITask
{
    public Priority Priority { get; set; }

    public Size Size { get; set; }

    public StoryStatus Status { get; set; }

    public IMember Assignee { get; set; }
}
