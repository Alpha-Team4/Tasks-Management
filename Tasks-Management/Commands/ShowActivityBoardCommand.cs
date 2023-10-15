using TasksManagement.Commands.Abstracts;
using TasksManagement.Core.Contracts;
using TasksManagement.Exceptions;

namespace TasksManagement.Commands;
public class ShowActivityBoardCommand : BaseCommand
{
    public const int ExpectedNumberOfArguments = 2;
    private const string NoActivityFoundMessage = "No activity found on board '{0}'.";

    public ShowActivityBoardCommand(IList<string> commandParameters, IRepository repository) 
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

        if (!board.History.Any())
        {
            var errorMessage = string.Format(NoActivityFoundMessage, board.Name);
            throw new EntityNotFoundException(errorMessage);
        }

        return string.Join(Environment.NewLine,
            board.History.Select(evt => evt.ToString()));
    }
}

