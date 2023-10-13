using TasksManagement.Commands.Abstracts;
using TasksManagement.Core.Contracts;
using TasksManagement.Exceptions;

namespace TasksManagement.Commands;
public class CreateMemberCommand : BaseCommand
{
    public const int ExpectedNumberOfArguments = 2;

    public CreateMemberCommand(IList<string> commandParameters, IRepository repository)
        : base(commandParameters, repository)
    {
    }

    public override string Execute()
    {
        if (this.CommandParameters.Count < ExpectedNumberOfArguments)
        {
            throw new InvalidUserInputException($"Invalid number of arguments. Expected: {ExpectedNumberOfArguments}, Received: {this.CommandParameters.Count}");
        }

        var memberName = CommandParameters[0];
        var teamName = CommandParameters[1];
        var team = Repository.FindTeamByName(teamName);

        var member = Repository.CreateMember(memberName, team);
        team.AddMember(member);
        return $"Member '{memberName}' was created in team '{team.Name}'.";
    }
}
