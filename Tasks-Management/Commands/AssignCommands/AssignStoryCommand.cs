using TasksManagement.Commands.Abstracts;
using TasksManagement.Core.Contracts;
using TasksManagement.Exceptions;
using TasksManagement.Models.Contracts;

namespace TasksManagement.Commands.AssignCommands;
public class AssignStoryCommand : BaseCommand
{
    private const int ExpectedNumberOfArguments = 4;
    private const string StoryAlreadyAssignedErrorMessage = "Invalid input. Story {0} is already assigned to member {1}.";
    private const string StoryAddedToMemberMessage = "Story {0} assigned to member {1}.";
    public AssignStoryCommand(IList<string> commandParameters, IRepository repository) 
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
        
        if (story.Assignee != null)
        {
            throw new InvalidUserInputException(string.Format(StoryAlreadyAssignedErrorMessage, story.Title, member.Name));
        }

        member.AddTask(story);
        story.Assignee = member;
        return string.Format(StoryAddedToMemberMessage, story.Title, member.Name); 
    }
}
