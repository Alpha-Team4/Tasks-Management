using TasksManagement.Commands.Abstracts;
using TasksManagement.Core.Contracts;
using TasksManagement.Exceptions;
using TasksManagement.Models;
using TasksManagement.Models.Contracts;
using TasksManagement.Models.Enums;

namespace TasksManagement.Commands.ChangeCommands;
public class ChangeBugStatusCommand : BaseCommand
{
    private const int ExpectedNumberOfArguments = 2;
    private const string InvalidStatusError = "None of the enums in BugStatus match the value {0}.";

    public ChangeBugStatusCommand(IList<string> commandParameters, IRepository repository) 
        : base(commandParameters, repository)
    {
    }

    public override string Execute()
    {
        if (CommandParameters.Count != ExpectedNumberOfArguments)
        {
            throw new InvalidUserInputException
                ($"Invalid number of arguments. Expected: {ExpectedNumberOfArguments} Received: {CommandParameters.Count}");
        }

        IBug bug = Repository.FindTaskByTitle<IBug>(CommandParameters[0]);
        var newBugStatus = ParseStatus(CommandParameters[1]);

        if (newBugStatus == bug.Status)
        {
            return $"Bug {bug.Title} status already {newBugStatus}.";
        }

        bug.Status = newBugStatus;

        return $"Bug {bug.Title} status changed to {newBugStatus}.";
    }

    private StatusBug ParseStatus(string value)
    {
        if (Enum.TryParse(value, true, out StatusBug result))
        {
            return result;
        }
        throw new InvalidUserInputException(string.Format(InvalidStatusError, value));
    }
}
