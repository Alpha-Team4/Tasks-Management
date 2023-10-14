using TasksManagement.Commands.Contracts;
using TasksManagement.Models.Contracts;

namespace TasksManagement.Core.Contracts;

public interface IRepository
{
    public List<ITeam> Teams { get; }
    public List<IMember> Members { get; }

    IBug CreateBug(string title, string description, string team, string board);
    ITeam CreateTeam(string name);
    IBoard CreateBoard(string name, string teamName);
    IMember CreateMember(string memberName);
    IMember CreateMember(string memberName, string teamName);
    IMember FindMemberByName(string member);
    ITeam FindTeamByName(string team);
}
