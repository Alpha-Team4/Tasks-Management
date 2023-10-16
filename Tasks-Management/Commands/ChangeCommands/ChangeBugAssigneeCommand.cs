using TasksManagement.Commands.Abstracts;
using TasksManagement.Core.Contracts;
using TasksManagement.Core;
using TasksManagement.Exceptions;
using TasksManagement.Models.Contracts;
using TasksManagement.Models.Enums;

namespace TasksManagement.Commands.ChangeCommands;
internal class ChangeBugAssigneeCommand : BaseCommand
{
    public const int ExpectedNumberOfArguments = 4;

    public ChangeBugAssigneeCommand(IList<string> commandParameters, IRepository repository)
        : base(commandParameters, repository)
    {
    }

    public override string Execute()
    {
        if (CommandParameters.Count != ExpectedNumberOfArguments)
        {
            throw new InvalidUserInputException
               ($"Invalid number of arguments. Expected: {ExpectedNumberOfArguments} " +
               $"Received: {CommandParameters.Count}");
        }

        var team = Repository.FindTeamByName(CommandParameters[0]);
        var board = Repository.FindBoardByName(CommandParameters[1], team);
        var bug = Repository.FindTaskByTitle<IBug>(CommandParameters[2], board);
        var assignee = CommandParameters[3];

        if (bug.Assignee is not null && bug.Assignee.Name == assignee)
        {
            throw new ArgumentException($"Bug '{bug.Title}' is already assigned to {assignee}.");
        }

        bug.Assignee = Repository.FindMemberByName(assignee);

        return $"Bug '{bug.Title}' assigned to {assignee}.";
    }
}
