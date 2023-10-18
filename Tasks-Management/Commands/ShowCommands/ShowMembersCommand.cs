using TasksManagement.Core.Contracts;
using TasksManagement.Core;
using TasksManagement.Exceptions;
using TasksManagement.Commands.Abstracts;

namespace TasksManagement.Commands.ShowCommands;
internal class ShowMembersCommand : BaseCommand
{
    // TODO
    // Overload show all members (no params)
    // Overload show all members of a specific team (param: teamName)
    private const int MinParameters = 0;
    private const int MaxParameters = 1;    
    private const string NoMembersMessage = "No members found."; 

    public ShowMembersCommand(IList<string> commandParameters, IRepository repository)
        : base(commandParameters, repository)
    {
    }

    public override string Execute()
    {
        switch(CommandParameters.Count) 
        {
            case 0:
                return ShowMembers();
            case 1:
                return ShowMembers(CommandParameters[0]);
            default:
                throw new InvalidUserInputException($"Invalid number of arguments. Expected: {MinParameters} or {MaxParameters}" +
                $", Received: {CommandParameters.Count}");
        }
    }

    public string ShowMembers()
    {
        if (!Repository.Members.Any())
        {
            throw new EntityNotFoundException(NoMembersMessage);
        }

        return string.Join(Environment.NewLine,
            Repository.Members.Select(member => member.Name));
    }

    public string ShowMembers(string teamName)
    {
        var team = Repository.FindTeamByName(teamName);

        if (!team.Members.Any())
        {
            throw new EntityNotFoundException(NoMembersMessage);
        }

        return string.Join(Environment.NewLine,
            team.Members.Select(member => member.Name));
    }
}
