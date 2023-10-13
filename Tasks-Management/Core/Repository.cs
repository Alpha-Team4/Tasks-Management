using System.Linq;
using TasksManagement.Commands.Contracts;
using TasksManagement.Core.Contracts;
using TasksManagement.Exceptions;
using TasksManagement.Models;
using TasksManagement.Models.Contracts;

namespace TasksManagement.Core;

public class Repository : IRepository
{
    private readonly List<IMember> members = new();
    private readonly List<ITask> tasks = new();
    private readonly List<ITeam> teams = new();
    private readonly List<IBoard> boards = new();

    public List<IMember> Members => new(members);
    public List<ITask> Tasks => new(tasks);
    public List<ITeam> Teams => new(teams);
    public List<IBoard> Boards => new(boards);

    public IBug CreateBug(string title, string description, IMember assignee)
    {
        var bug = new Bug(title, description, assignee);
        tasks.Add(bug);
        return bug;
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
}
