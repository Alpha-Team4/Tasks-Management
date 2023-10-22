using TasksManagement.Commands.Abstracts;
using TasksManagement.Core.Contracts;
using TasksManagement.Exceptions;

namespace TasksManagement.Commands.CreateCommands;
public class CreateMemberCommand : BaseCommand
{
    public const int MinExpectedNumberOfArguments = 1;
    public const int MaxExpectedNumberOfArguments = 2;

    public CreateMemberCommand(IList<string> commandParameters, IRepository repository)
        : base(commandParameters, repository)
    {
    }

    public override string Execute()
    {
        if (CommandParameters.Count < MinExpectedNumberOfArguments 
            || CommandParameters.Count > MaxExpectedNumberOfArguments)
        {
            throw new InvalidUserInputException
                ($"Invalid number of arguments. Expected: {MinExpectedNumberOfArguments} - {MaxExpectedNumberOfArguments}" +
                $", Received: {CommandParameters.Count}");
        }

        var memberName = CommandParameters[0];

        if (CommandParameters.Count == MinExpectedNumberOfArguments)
        {
            Repository.CreateMember(memberName);
            return $"Member '{memberName}' was created.";
        }

        var teamName = CommandParameters[1];
        Repository.CreateMember(memberName, teamName);

        return $"Member '{memberName}' was created in team '{teamName}'.";
    }
}
