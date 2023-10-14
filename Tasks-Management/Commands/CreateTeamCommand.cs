using TasksManagement.Commands.Abstracts;
using TasksManagement.Core.Contracts;
using TasksManagement.Exceptions;

namespace TasksManagement.Commands;
public class CreateTeamCommand : BaseCommand
{
    public const int ExpectedNumberOfArguments = 1;

    public CreateTeamCommand(IList<string> commandParameters, IRepository repository)
        : base(commandParameters, repository)
    {
    }

    public override string Execute()
    {
        if (this.CommandParameters.Count < ExpectedNumberOfArguments)
        {
            throw new InvalidUserInputException($"Invalid number of arguments. Expected: {ExpectedNumberOfArguments}, Received: {this.CommandParameters.Count}");
        }

        var teamName = CommandParameters[0];

        Repository.CreateTeam(teamName);

        return $"Team with name '{teamName}' was created.";
    }
}
