using TasksManagement.Commands.Abstracts;
using TasksManagement.Commands.Enums;
using TasksManagement.Core.Contracts;
using TasksManagement.Exceptions;
using TasksManagement.Models.Contracts;
using TasksManagement.Models.Enums;

namespace TasksManagement.Commands.ChangeCommands;
public class ChangeBugSeverityCommand : BaseCommand
{
    private const int ExpectedNumberOfArguments = 4;
    private const string ChangeBugSeverityErrorMessage = "Bug '{0}' severity already '{1}'.";
    private const string ChangeBugSeverityOutputMessage = "Bug '{0}' severity changed to '{1}'.";
    private const string InvalidBugSeverityErrorMessage = "None of the enums in Severity match the value {0}.";

    public ChangeBugSeverityCommand(IList<string> commandParameters, IRepository repository) 
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
        var newBugSeverity = Validator.ParseTEnum<Severity>(CommandParameters[3], InvalidBugSeverityErrorMessage);

        IBug bug = Repository.FindTaskByTitle<IBug>(title, board);

        if (newBugSeverity == bug.Severity)
        {
            throw new ArgumentException
                (string.Format(ChangeBugSeverityErrorMessage, title, newBugSeverity));
        }

        bug.Severity = newBugSeverity;

        return string.Format(ChangeBugSeverityOutputMessage, title, newBugSeverity);
    }
}
