using TasksManagement.Commands.Abstracts;
using TasksManagement.Core.Contracts;
using TasksManagement.Exceptions;
using TasksManagement.Models.Contracts;

namespace TasksManagement.Commands;
public class CreateBugCommand : BaseCommand
{
    public const int ExpectedNumberOfArguments = 3;

    public CreateBugCommand(IList<string> commandParameters, IRepository repository)
        : base(commandParameters, repository)
    {
    }

    public override string Execute()
    {
        if (this.CommandParameters.Count < ExpectedNumberOfArguments)
        {
            throw new InvalidUserInputException($"Invalid number of arguments. Expected: {ExpectedNumberOfArguments}, Received: {this.CommandParameters.Count}");
        }

        var title = CommandParameters[0];
        var description = CommandParameters[1];
        IMember assignee = Repository.FindMemberByName(CommandParameters[2]);

        var bug = Repository.CreateBug(title, description, assignee);
        return $"Bug with ID '{bug.Id}' was created.";
    }
}

