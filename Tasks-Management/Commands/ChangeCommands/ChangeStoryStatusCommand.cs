using TasksManagement.Commands.Abstracts;
using TasksManagement.Core.Contracts;
using TasksManagement.Core;
using TasksManagement.Exceptions;
using TasksManagement.Models.Contracts;
using TasksManagement.Models.Enums;

namespace TasksManagement.Commands.ChangeCommands;
public class ChangeStoryStatusCommand : BaseCommand
{
    private const int ExpectedNumberOfArguments = 2;
    private const string InvalidStatusError = "None of the enums in StoryStatus match the value {0}.";

    public ChangeStoryStatusCommand(IList<string> commandParameters, IRepository repository)
        : base(commandParameters, repository)
    {
    }

    public override string Execute()
    {
        if (CommandParameters.Count != ExpectedNumberOfArguments)
        {
            throw new InvalidUserInputException
                ($"Invalid number of arguments. Expected: {ExpectedNumberOfArguments} Received: {CommandParameters.Count}");
        }

        IStory story = Repository.FindTaskByTitle<IStory>(CommandParameters[0]);

        story.Status = ParseStatus(CommandParameters[1]);

        return $"Story {story.Title} status changed to {story.Status}.";

    }

    private StatusStory ParseStatus(string value)
    {
        if (Enum.TryParse(value, true, out StatusStory result))
        {
            return result;
        }
        throw new InvalidUserInputException(string.Format(InvalidStatusError, value));
    }
}
