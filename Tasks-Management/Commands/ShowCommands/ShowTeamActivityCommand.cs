using TasksManagement.Core.Contracts;
using TasksManagement.Core;
using TasksManagement.Exceptions;
using TasksManagement.Commands.Abstracts;

namespace TasksManagement.Commands.ShowCommands;
public class ShowTeamActivityCommand : BaseCommand
{
    private const int ExpectedNumberOfArguments = 1;
    private const string NoTeamActivityFoundMessage = "The team {0} has no activity yet.";

    public ShowTeamActivityCommand(IList<string> commandParameters, IRepository repository)
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

        if (!team.History.Any())
        {
            throw new ArgumentException
                (string.Format(NoTeamActivityFoundMessage, team.Name));
        }

        return string.Join(Environment.NewLine,
            team.History.OrderBy(evt => evt.Time).Select(evt => evt.ToString()));
    }
}
