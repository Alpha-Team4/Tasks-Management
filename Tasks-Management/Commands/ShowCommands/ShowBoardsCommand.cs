using System.Text;
using TasksManagement.Commands.Abstracts;
using TasksManagement.Core.Contracts;
using TasksManagement.Exceptions;
using TasksManagement.Models.Contracts;

namespace TasksManagement.Commands.ShowCommands;
public class ShowBoardsCommand : BaseCommand
{
    public const int ExpectedNumberOfArguments = 1;
    private const string NoBoardsMessage = "No boards found.";

    public ShowBoardsCommand(IList<string> commandParameters, IRepository repository)
        : base(commandParameters, repository)
    {
    }

    public override string Execute()
    {
        if (CommandParameters.Count != 1)
        {
            throw new InvalidUserInputException
                ($"Invalid number of arguments. Expected: {ExpectedNumberOfArguments}" +
                $", Received: {CommandParameters.Count}");
        }

        var team = Repository.FindTeamByName(CommandParameters[0]);

        if (!team.Boards.Any())
        {
            throw new EntityNotFoundException(NoBoardsMessage);
        }

        return string.Join(Environment.NewLine,
            team.Boards.Select(board => board.ToString()));
    }
}

