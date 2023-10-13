using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksManagement.Models.Contracts;

namespace TasksManagement.Models;
public class Team : ITeam
{
    private const int TeamNameMinLength = 5;
    private const int TeamNameMaxLength = 15;
    private const string TeamNameErrorMessage = "Team name must be between {0} and {1} characters.";

    private string name;
    private readonly IList<IMember> members = new List<IMember>();
    private readonly IList<IBoard> boards = new List<IBoard>();

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

    public IList<IMember> Members
    {
        get => new List<IMember>(members);

    }

    public IList<IBoard> Boards
    {
        get => new List<IBoard>(boards);
    }

    public void AddMember(IMember member)
    {
        this.members.Add(member);
    }

    public void AddBoard(IBoard board)
    {
        this.boards.Add(board);
    }
}
