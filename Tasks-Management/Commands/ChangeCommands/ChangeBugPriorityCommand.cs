using TasksManagement.Commands.Abstracts;
using TasksManagement.Commands.Enums;
using TasksManagement.Core.Contracts;
using TasksManagement.Exceptions;
using TasksManagement.Models.Contracts;
using TasksManagement.Models.Enums;

namespace TasksManagement.Commands.ChangeCommands;
public class ChangeBugPriorityCommand : BaseCommand
{
    private const int ExpectedNumberOfArguments = 4;
    private const string ChangeBugPriorityErrorMessage = "Bug '{0}' priority is already {1}.";
    private const string ChangeBugPriorityOutputMessage = "Bug '{0}' priority changed to {1}.";
    private const string InvalidBugPriorityErrorMessage = "{0} is not a valid priority.";

    public ChangeBugPriorityCommand(IList<string> commandParameters, IRepository repository)
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
        var bug = Repository.FindTaskByTitle<IBug>(CommandParameters[2], board);
        var newBugPriority = Validator.ParseTEnum<Priority>
            (CommandParameters[3], InvalidBugPriorityErrorMessage);

        if (newBugPriority == bug.Priority)
        {
            throw new ArgumentException
                (string.Format(ChangeBugPriorityErrorMessage, bug.Title, newBugPriority));
        }

        bug.Priority = newBugPriority;

        return string.Format(ChangeBugPriorityOutputMessage, bug.Title, newBugPriority);
    }
}
