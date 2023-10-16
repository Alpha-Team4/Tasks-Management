using TasksManagement.Core.Contracts;
using TasksManagement.Core;
using TasksManagement.Exceptions;
using TasksManagement.Models.Contracts;
using TasksManagement.Commands.Abstracts;
using TasksManagement.Models.Enums;

namespace TasksManagement.Commands.ChangeCommands;
public class ChangeBugSeverityCommand : BaseCommand
{
    public const int ExpectedNumberOfArguments = 2;

    public ChangeBugSeverityCommand(IList<string> commandParameters, IRepository repository) : base(commandParameters, repository)
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


        bug.Severity = ParseSeverity(CommandParameters[1]);

        return $"Bug {bug.Title} severity changed to {bug.Severity}";

    }

    protected Severity ParseSeverity(string value)
    {
        if (Enum.TryParse(value, true, out Severity result))
        {
            return result;
        }
        throw new ArgumentException($"None of the enums in Severity match the value {value}.");
    }
}
