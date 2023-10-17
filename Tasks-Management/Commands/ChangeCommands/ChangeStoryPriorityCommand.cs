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
    private const string ChangeStoryPriorityErrorMessage = "Story {0} priority already {1}.";
    private const string ChangeStoryPriorityOutputMessage = "Story {0} priority changed to {1}.";
    private const string InvalidStoryPriorityErrorMessage = "None of the enums in Priority match the value {0}.";

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
        var title = CommandParameters[2];
        var newStoryPriority = ParsePriority(CommandParameters[3]);

        IStory story = Repository.FindTaskByTitle<IStory>(title, board);

        if (newStoryPriority == story.Priority)
        {
            throw new ArgumentException
                (string.Format(ChangeStoryPriorityErrorMessage, title, newStoryPriority));
        }

        story.Priority = newStoryPriority;

        return string.Format(ChangeStoryPriorityOutputMessage, title, newStoryPriority);
    }

    private Priority ParsePriority(string value)
    {
        if (Enum.TryParse(value, true, out Priority result))
        {
            return result;
        }
        throw new InvalidUserInputException
            (string.Format(InvalidStoryPriorityErrorMessage, value));
    }
}
