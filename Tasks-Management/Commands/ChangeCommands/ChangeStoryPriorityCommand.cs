using TasksManagement.Commands.Abstracts;
using TasksManagement.Commands.Enums;
using TasksManagement.Core.Contracts;
using TasksManagement.Exceptions;
using TasksManagement.Models;
using TasksManagement.Models.Contracts;

namespace TasksManagement.Commands.ChangeCommands;
public class ChangeStoryPriorityCommand : BaseCommand
{
    private const int ExpectedNumberOfArguments = 4;
    private const string ChangeStoryPriorityErrorMessage = "Story '{0}' priority is already {1}.";
    private const string ChangeStoryPriorityOutputMessage = "Story '{0}' priority changed to '{1}'.";
    private const string InvalidStoryPriorityErrorMessage = "{0} is not a valid priority.";

    public ChangeStoryPriorityCommand(IList<string> commandParameters, IRepository repository)
        : base(commandParameters, repository)
    {
    }

    public override string Execute()
    {
        if (CommandParameters.Count != ExpectedNumberOfArguments)
        {
            throw new InvalidUserInputException
            ($"Invalid number of arguments. Expected: {ExpectedNumberOfArguments}" +
             $" Received: {CommandParameters.Count}");
        }

        var team = Repository.FindTeamByName(CommandParameters[0]);
        var board = Repository.FindBoardByName(CommandParameters[1], team);
        var story = Repository.FindTaskByTitle<IStory>(CommandParameters[2], board);
        var newStoryPriority = Validator.ParseTEnum<Priority>
            (CommandParameters[3], InvalidStoryPriorityErrorMessage);

        if (newStoryPriority == story.Priority)
        {
            throw new ArgumentException
                (string.Format(ChangeStoryPriorityErrorMessage, story.Title, newStoryPriority));
        }

        story.Priority = newStoryPriority;

        return string.Format(ChangeStoryPriorityOutputMessage, story.Title, newStoryPriority);
    }
}
