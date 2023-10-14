using TasksManagement.Core.Contracts;
using TasksManagement.Exceptions;
using TasksManagement.Models;
using TasksManagement.Models.Contracts;

namespace TasksManagement.Core;

public class Repository : IRepository
{
    private readonly List<ITeam> teams = new();
    private readonly List<IMember> members = new();

    public List<ITeam> Teams => new(teams);
    public List<IMember> Members => new(members);

    public ITeam CreateTeam(string teamName)
    {
        var team = new Team(teamName);
        teams.Add(team);
        return team;
    }

    public IMember CreateMember(string memberName)
    {
        var member = new Member(memberName);
        members.Add(member);

        return member;
    }

    public IMember CreateMember(string memberName, string teamName)
    {
        var member = new Member(memberName);
        members.Add(member);

        var team =  FindTeamByName(teamName);
        team.AddMember(member);
        
        return member;
    }

    public IBoard CreateBoard(string boardName, string teamName)
    {
        var board = new Board(boardName);

        var team = FindTeamByName(teamName);
        team.AddBoard(board);

        return board;
    }

    public IBug CreateBug(string title, string description, string teamName, string boardName)
    {
        var team = FindTeamByName(teamName);
        var board = FindBoardByName(boardName, team);

        var bug = new Bug(title, description, board);
        return bug;
    }

    public IMember FindMemberByName(string memberName)
    {
        IMember? member = members.SingleOrDefault(member => member.Name == memberName);

        if (member == null)
        {
            throw new EntityNotFoundException($"Member with memberName '{memberName}' was not found!");
        }

        return member;
    }

    public ITeam FindTeamByName(string teamName)
    {
        ITeam? team = teams.SingleOrDefault(team => team.Name == teamName);

        if (team == null)
        {
            throw new EntityNotFoundException($"Team with name '{teamName}' was not found!");
        }

        return team;
    }

    public IBoard FindBoardByName(string boardName, ITeam team)
    {
        IBoard? board = team.Boards.SingleOrDefault(team => team.Name == boardName);

        if (board == null)
        {
            throw new EntityNotFoundException($"Team with name '{boardName}' was not found!");
        }

        return board;
    }
}
