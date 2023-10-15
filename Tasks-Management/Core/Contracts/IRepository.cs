using TasksManagement.Commands.Contracts;
using TasksManagement.Models.Contracts;

namespace TasksManagement.Core.Contracts;

public interface IRepository
{
    public List<ITeam> Teams { get; }
    public List<IMember> Members { get; }
    public List<ITask> Tasks { get; }

    ITeam CreateTeam(string name);
    IBoard CreateBoard(string name, string teamName);
    IMember CreateMember(string memberName);
    IMember CreateMember(string memberName, string teamName);
    IBug CreateBug(string title, string description, string team, string board);
    IStory CreateStory(string title, string description, string team, string board);
    ITeam FindTeamByName(string team);
    IMember FindMemberByName(string member);
}
