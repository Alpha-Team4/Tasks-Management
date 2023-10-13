using TasksManagement.Commands.Contracts;
using TasksManagement.Models.Contracts;

namespace TasksManagement.Core.Contracts;

public interface IRepository
{
    public List<ITask> Tasks { get; }
    public List<ITeam> Teams { get; }

    IBug CreateBug(string title, string description, IMember assignee);
    ITeam CreateTeam(string name);
    IBoard CreateBoard(string name, ITeam team);
    IMember CreateMember(string name, ITeam team);
    IMember FindMemberByName(string member);
    ITeam FindTeamByName(string team);
}
