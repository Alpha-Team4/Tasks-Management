using TasksManagement.Core.Contracts;
using TasksManagement.Exceptions;
using TasksManagement.Models;
using TasksManagement.Models.Contracts;

namespace TasksManagement.Core;

public class Repository : IRepository
{
    public const string NoTasksMessage = "No tasks found.";

    private readonly List<ITeam> teams = new();
    private readonly List<IMember> members = new();

    public List<ITeam> Teams => new(teams);
    public List<IMember> Members => new(members);

    public ITeam CreateTeam(string teamName)
    {
        var teamNameExists = teams.Any(t => t.Name == teamName);

        if (teamNameExists)
        {
            throw new NameAlreadyExistsException($"Team '{teamName} already exists.'");
        }

        var team = new Team(teamName);
        teams.Add(team);
        return team;
    }

    public IBoard CreateBoard(string boardName, string teamName)
    {
        var team = FindTeamByName(teamName);

        var boardNameExists = team.Boards.Any(b => b.Name == boardName);

        if (boardNameExists)
        {
            throw new NameAlreadyExistsException($"Board '{boardName}' already exists in '{teamName}'.");
        }

        var board = new Board(boardName);
        team.AddBoard(board);

        return board;
    }

    public IMember CreateMember(string memberName)
    {
        var memberNameExists = members.Any(m => m.Name == memberName);

        if (memberNameExists)
        {
            throw new NameAlreadyExistsException($"Member '{memberName}' already exists.");
        }

        var member = new Member(memberName);
        members.Add(member);

        return member;
    }

    public IMember CreateMember(string memberName, string teamName)
    {
        var memberNameExists = members.Any(m => m.Name == memberName);

        if (memberNameExists)
        {
            throw new NameAlreadyExistsException($"Member '{memberName}' already exists.");
        }

        var member = new Member(memberName);
        members.Add(member);

        var team =  FindTeamByName(teamName);
        team.AddMember(member);
        
        return member;
    }

    public IBug CreateBug(string title, string description, string teamName, string boardName)
    {
        var team = FindTeamByName(teamName);
        var board = FindBoardByName(boardName, team);

        var bug = new Bug(title, description);
        board.AddTask(bug);
        return bug;
    }

    public IStory CreateStory(string title, string description, string teamName, string boardName)
    {
        var team = FindTeamByName(teamName);
        var board = FindBoardByName(boardName, team);

        var story = new Story(title, description);
        board.AddTask(story);
        return story;
    }

    public IFeedback CreateFeedback(string title, string description, string teamName, string boardName)
    {
        var team = FindTeamByName(teamName);
        var board = FindBoardByName(boardName, team);

        var feedback = new Feedback(title, description);
        board.AddTask(feedback);
        return feedback;
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
        IBoard? board = team.Boards.SingleOrDefault(board => board.Name == boardName);

        if (board == null)
        {
            throw new EntityNotFoundException($"Board with name '{boardName}' was not found!");
        }

        return board;
    }

    public IList<ITask> FindAllTasks()
    {
        var foundTasks = Teams
            .SelectMany(team => team.Boards)
            .SelectMany(board => board.Tasks)
            .ToList();

        if (foundTasks == null)
        {
            throw new EntityNotFoundException(NoTasksMessage);
        }

        return foundTasks;
    }

    public T FindTaskByTitle<T>(string taskTitle, IBoard board) where T : ITask
    {
        ITask? foundTask = board.Tasks
            .OfType<T>()
            .FirstOrDefault(task => task.Title == taskTitle);

        if (foundTask is T typedTask)
        {
            return typedTask;
        }

        throw new EntityNotFoundException($"Task with name '{taskTitle}' was not found!");
    }
}
