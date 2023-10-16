using TasksManagement.Commands.Abstracts;
using TasksManagement.Core.Contracts;
using TasksManagement.Exceptions;
using TasksManagement.Models.Contracts;

namespace TasksManagement.Commands.ShowCommands;
public class ShowBoardActivityCommand : BaseCommand
{
    public const int ExpectedNumberOfArguments = 2;
    private const string NoActivityFoundMessage = "No activity found on board '{0}'.";

    public ShowBoardActivityCommand(IList<string> commandParameters, IRepository repository)
        : base(commandParameters, repository)
    {
    }

    public override string Execute()
    {
        if (CommandParameters.Count != 2)
        {
            throw new InvalidUserInputException
                ($"Invalid number of arguments. Expected: {ExpectedNumberOfArguments}" +
                $", Received: {CommandParameters.Count}");
        }

        var team = Repository.FindTeamByName(CommandParameters[0]);
        var board = Repository.FindBoardByName(CommandParameters[1], team);

        var boardAllActivity = board.Tasks.SelectMany(task => task.History)
            .Concat(board.History)
            .OrderBy(evt => evt.Time)
            .ToList();

        if (!boardAllActivity.Any())
        {
            var errorMessage = string.Format(NoActivityFoundMessage, board.Name);
            throw new EntityNotFoundException(errorMessage);
        }

        return string.Join(Environment.NewLine,
            boardAllActivity.Select(evt => evt.ToString()));
    }
}

