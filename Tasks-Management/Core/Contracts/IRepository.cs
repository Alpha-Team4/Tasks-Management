using TasksManagement.Commands.Contracts;
using TasksManagement.Models.Contracts;
using TasksManagement.Models.Enums;

namespace TasksManagement.Core.Contracts;

public interface IRepository
{
    public List<ITeam> Teams { get; }
    public List<IMember> Members { get; }

    ITeam CreateTeam(string name);
    IBoard CreateBoard(string name, string teamName);
    IMember CreateMember(string memberName);
    IMember CreateMember(string memberName, string teamName);

    IFeedback CreateFeedback(string title, string description, int rating, string team, string board);    
    IBug CreateBug(string title, string description, string team, string board);
    IStory CreateStory(string title, string description, string team, string board);
    ITeam FindTeamByName(string team);
    IMember FindMemberByName(string member);
    IBoard FindBoardByName(string boardName, ITeam team);
    IList<ITask> FindAllTasks();
    ITask FindTaskByTitle(string taskTitle);
}
