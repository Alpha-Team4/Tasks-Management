using TasksManagement.Models.Contracts;

namespace TasksManagement.Models;
public class Team : ITeam
{
    private const int TeamNameMinLength = 5;
    private const int TeamNameMaxLength = 15;
    private const string TeamNameErrorMessage = "Team name must be between {0} and {1} characters.";
    private const string NoMembersMessage = "--NO MEMBERS--";

    private string name;
    private readonly IList<IBoard> boards = new List<IBoard>();
    private readonly IList<IMember> members = new List<IMember>();
    private readonly IList<IEvent> history = new List<IEvent>();

    public Team(string name)
    {
        this.Name = name;
    }

    public string Name
    {
        get => this.name;
        private set
        {
            Validator.ValidateStringLength(value,TeamNameMinLength, TeamNameMaxLength, 
                string.Format(TeamNameErrorMessage, TeamNameMinLength, TeamNameMaxLength));

            name = value;
        }

    }

    public IList<IMember> Members => new List<IMember>(members);

    public IList<IBoard> Boards => new List<IBoard>(boards);

    public IList<IEvent> History => new List<IEvent>(history);

    public void AddMember(IMember member)
    {
        this.members.Add(member);
    }

    public void AddBoard(IBoard board)
    {
        this.boards.Add(board);
    }

    public void AddEvent(string message)
    {
        history.Add(new Event(message));
    }

    public string PrintAllMembers()
    {
        if (!members.Any())
        {
            return NoMembersMessage;
        }

        return string.Join(Environment.NewLine, 
                members.Select(member => member.Name)
            );
    }

    public override string ToString()
    {
        return $"""
                Name: {Name}
                  Members:
                    {PrintAllMembers()}
                """;
    }
}
