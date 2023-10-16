using TasksManagement.Commands.Abstracts;
using TasksManagement.Core.Contracts;
using TasksManagement.Exceptions;
using TasksManagement.Models.Contracts;

namespace TasksManagement.Commands.CreateCommands;
public class CreateBugCommand : BaseCommand
{
    public const int ExpectedNumberOfArguments = 4;

    public CreateBugCommand(IList<string> commandParameters, IRepository repository)
        : base(commandParameters, repository)
    {
    }

    public override string Execute()
    {
        if (CommandParameters.Count < ExpectedNumberOfArguments)
        {
            throw new InvalidUserInputException($"Invalid number of arguments. Expected: {ExpectedNumberOfArguments}, Received: {CommandParameters.Count}");
        }

        var title = CommandParameters[0];
        var description = CommandParameters[1];
        var teamName = CommandParameters[2];
        var boardName = CommandParameters[3];

        var bug = Repository.CreateBug(title, description, teamName, boardName);
        return $"Bug with ID '{bug.Id}' was created.";
    }
}

