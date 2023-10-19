using TasksManagement.Commands.Abstracts;
using TasksManagement.Core.Contracts;
using TasksManagement.Core;
using TasksManagement.Exceptions;
using TasksManagement.Models.Contracts;
using TasksManagement.Models.Enums;
using TasksManagement.Models;

namespace TasksManagement.Commands.ChangeCommands;
internal class ChangeStoryAssigneeCommand : BaseCommand
{
    public const int ExpectedNumberOfArguments = 4;
    private const string ChangeStoryAssigneeErrorMessage = "Story '{0}' is already assigned to {1}.";
    private const string ChangeStoryAssigneeOutputMessage = "Story '{0}' assigned to {1}.";

    public ChangeStoryAssigneeCommand(IList<string> commandParameters, IRepository repository)
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
        var story = Repository.FindTaskByTitle<IStory>(CommandParameters[2], board);
        var assignee = CommandParameters[3];

        if (story.Assignee is not null && story.Assignee.Name == assignee)
        {
            throw new ArgumentException(string.Format(ChangeStoryAssigneeErrorMessage, story.Title, assignee));
        }

        story.Assignee = Repository.FindMemberByName(assignee);

        return string.Format(ChangeStoryAssigneeOutputMessage, story.Title, assignee);
    }
}
