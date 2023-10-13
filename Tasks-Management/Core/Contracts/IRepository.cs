using TasksManagement.Commands.Contracts;
using TasksManagement.Models.Contracts;

namespace TasksManagement.Core.Contracts;

public interface IRepository
{
    public List<IMember> Members { get; }
    public List<ITask> Tasks { get; }
    public List<ITeam> Teams { get; }
    public List<IBoard> Boards { get; }

    IBug CreateBug(string title, string description, IMember assignee);
    IMember FindMemberByName(string member);
}
