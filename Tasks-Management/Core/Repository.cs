using TasksManagement.Core.Contracts;
using TasksManagement.Exceptions;
using TasksManagement.Models;
using TasksManagement.Models.Contracts;

namespace TasksManagement.Core;

public class Repository : IRepository
{
    private readonly List<ITask> tasks = new();
    private readonly List<ITeam> teams = new();

    public List<ITask> Tasks => new(tasks);
    public List<ITeam> Teams => new(teams);

    public IBug CreateBug(string title, string description, IMember assignee)
    {
        var bug = new Bug(title, description, assignee);
        tasks.Add(bug);
        return bug;
    }

    public ITeam CreateTeam(string name)
    {
        var team = new Team(name);
        teams.Add(team);
        return team;
    }

    public IBoard CreateBoard(string name, ITeam team)
    {
        var board = new Board(name);
        team.AddBoard(board);
        return board;
    }

    public IMember CreateMember(string name, ITeam team)
    {
        var member = new Member(name);
        members.Add(member);
        return member;
    }

    public IMember FindMemberByName(string name)
    {
        IMember? member = members.SingleOrDefault(member => member.Name == name);

        if (member == null)
        {
            throw new EntityNotFoundException($"Member with name '{name}' was not found!");
        }

        return member;
    }

    public ITeam FindTeamByName(string name)
    {
        ITeam? team = teams.SingleOrDefault(team => team.Name == name);

        if (team == null)
        {
            throw new EntityNotFoundException($"Team with name '{name}' was not found!");
        }

        return team;
    }
}
