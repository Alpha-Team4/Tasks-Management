using TasksManagement.Commands.Abstracts;
using TasksManagement.Core.Contracts;
using TasksManagement.Exceptions;

namespace TasksManagement.Commands;
public class CreateBoardCommand : BaseCommand
{
    public const int ExpectedNumberOfArguments = 2;

    public CreateBoardCommand(IList<string> commandParameters, IRepository repository)
        : base(commandParameters, repository)
    {
    }

    public override string Execute()
    {
        if (this.CommandParameters.Count < ExpectedNumberOfArguments)
        {
            throw new InvalidUserInputException($"Invalid number of arguments. Expected: {ExpectedNumberOfArguments}, Received: {this.CommandParameters.Count}");
        }

        var boardName = CommandParameters[0];
        var teamName = CommandParameters[1];
        var team = Repository.FindTeamByName(teamName);

        var board = Repository.CreateBoard(boardName, team);
        team.AddBoard(board);
        return $"Board '{boardName}' was created for team '{team.Name}'.";
    }
}
