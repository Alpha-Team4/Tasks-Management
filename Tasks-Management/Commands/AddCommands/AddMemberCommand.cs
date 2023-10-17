using TasksManagement.Commands.Abstracts;
using TasksManagement.Core.Contracts;
using TasksManagement.Exceptions;

namespace TasksManagement.Commands.AddCommands;
public class AddMemberCommand : BaseCommand
{
    private const int ExpectedNumberOfArguments = 2;
    private const string AddMemberOutputMessage = "Member {0} was added to a team {1}";
    private const string AddMemberErrorMessage = "The member {0} is already in this team.";

    public AddMemberCommand(IList<string> commandParameters, IRepository repository)
        : base(commandParameters, repository)
    {
    }

    public override string Execute()
    {
        if (CommandParameters.Count != ExpectedNumberOfArguments)
        {
            throw new InvalidUserInputException
            ($"Invalid number of arguments. Expected: {ExpectedNumberOfArguments}" +
             $", Received: {CommandParameters.Count}");
        }

        var team = Repository.FindTeamByName(CommandParameters[0]);
        var member = Repository.FindMemberByName(CommandParameters[1]);
        var memberExist = team.Members.Any(m => m.Name == member.Name);
      
        if(!memberExist)
        {
            throw new NameAlreadyExistsException
                (string.Format(AddMemberErrorMessage, member.Name));
        }

        team.Members.Add(member);

        return string.Format(AddMemberOutputMessage, member, team);
    }
}
