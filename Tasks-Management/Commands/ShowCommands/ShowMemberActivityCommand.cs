using TasksManagement.Commands.Abstracts;
using TasksManagement.Core.Contracts;
using TasksManagement.Exceptions;
using TasksManagement.Models;
using TasksManagement.Models.Contracts;

namespace TasksManagement.Commands.ShowCommands;
public class ShowMemberActivityCommand : BaseCommand
{
    private const int ExpectedNumberOfArguments = 1;
    private const string NoMemberActivityFoundMessage = "The member {0} has no activity yet.";

    public ShowMemberActivityCommand(IList<string> commandParameters, IRepository repository)
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

        var member = Repository.FindMemberByName(CommandParameters[0]);
        var memberActivity = member.Tasks.SelectMany(task => task.History)
            .Concat(member.History)
            .OrderBy(evt => evt.Time)
            .ToList();

        if (!memberActivity.Any())
        {
            throw new ArgumentException
                (string.Format(NoMemberActivityFoundMessage, member.Name));
        }

        return string.Join(Environment.NewLine,
            memberActivity.Select(evt => evt.ToString()));
    }
}
