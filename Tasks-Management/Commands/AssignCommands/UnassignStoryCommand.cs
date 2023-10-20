using TasksManagement.Commands.Abstracts;
using TasksManagement.Core.Contracts;
using TasksManagement.Exceptions;
using TasksManagement.Models.Contracts;

namespace TasksManagement.Commands.AssignCommands;
public class UnassignStoryCommand : BaseCommand
{
    private const int ExpectedNumberOfArguments = 2;
    private const string StoryNotAssignedErrorMessage = "Story {0} is not assigned to member {1}.";
    private const string StoryUnassignedMessage = "Story {0} unassigned from member {1}.";
    public UnassignStoryCommand(IList<string> commandParameters, IRepository repository)
        : base(commandParameters, repository)
    {
    }

    public override string Execute()
    {
        if (CommandParameters.Count != ExpectedNumberOfArguments)
        {
            throw new InvalidUserInputException($"Invalid number of arguments. " +
                $"Expected: {ExpectedNumberOfArguments}, Received: {CommandParameters.Count}");
        }

        var team = Repository.FindTeamByName(CommandParameters[0]);
        var board = Repository.FindBoardByName(CommandParameters[1], team);
        var story = Repository.FindTaskByTitle<IStory>(CommandParameters[2], board);
        var member = Repository.FindMemberByName(CommandParameters[3]);

        if (!member.Tasks.Contains(story)) 
        {
            throw new EntityNotFoundException(StoryNotAssignedErrorMessage);
        }

        member.RemoveTask(story);
        story.Assignee = null;
        return string.Format(StoryUnassignedMessage, story.Title, member.Name);   

    }
}
