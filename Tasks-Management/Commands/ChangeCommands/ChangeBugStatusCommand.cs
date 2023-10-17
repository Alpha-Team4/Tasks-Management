using TasksManagement.Commands.Abstracts;
using TasksManagement.Core.Contracts;
using TasksManagement.Exceptions;
using TasksManagement.Models;
using TasksManagement.Models.Contracts;
using TasksManagement.Models.Enums;

namespace TasksManagement.Commands.ChangeCommands;
public class ChangeBugStatusCommand : BaseCommand
{
    private const int ExpectedNumberOfArguments = 4;
    private const string ChangeBugStatusErrorMessage = "Bug {0} status already {1}.";
    private const string ChangeBugStatusOutputMessage = "Bug {0} status changed to {1}.";
    private const string InvalidBugStatusErrorMessage = "None of the enums in BugStatus match the value {0}.";

    public ChangeBugStatusCommand(IList<string> commandParameters, IRepository repository) 
        : base(commandParameters, repository)
    {
    }

    public override string Execute()
    {
        if (CommandParameters.Count != ExpectedNumberOfArguments)
        {
            throw new InvalidUserInputException
                ($"Invalid number of arguments. Expected: {ExpectedNumberOfArguments}" +
                 $" Received: {CommandParameters.Count}");
        }

        var team = Repository.FindTeamByName(CommandParameters[0]);
        var board = Repository.FindBoardByName(CommandParameters[1], team);
        var title = CommandParameters[2];
        var newBugStatus = ParseStatus(CommandParameters[3]);

        IBug bug = Repository.FindTaskByTitle<IBug>(title, board);

        if (newBugStatus == bug.Status)
        {
            throw new ArgumentException
                (string.Format(ChangeBugStatusErrorMessage, title, newBugStatus));
        }

        bug.Status = newBugStatus;

        return string.Format(ChangeBugStatusOutputMessage, title, newBugStatus);
    }

    private StatusBug ParseStatus(string value)
    {
        if (Enum.TryParse(value, true, out StatusBug result))
        {
            return result;
        }
        throw new InvalidUserInputException(string.Format(InvalidBugStatusErrorMessage, value));
    }
}
