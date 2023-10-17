using TasksManagement.Commands.Abstracts;
using TasksManagement.Core.Contracts;
using TasksManagement.Core;
using TasksManagement.Exceptions;
using TasksManagement.Models.Contracts;
using TasksManagement.Models.Enums;
using TasksManagement.Models;

namespace TasksManagement.Commands.ChangeCommands;
public class ChangeStoryStatusCommand : BaseCommand
{
    private const int ExpectedNumberOfArguments = 4;
    private const string ChangeStoryStatusErrorMessage = "Story {0} status already {1}.";
    private const string ChangeStoryStatusOutputMessage = "Story {0} status changed to {1}.";
    private const string InvalidStoryStatusErrorMessage = "None of the enums in StoryStatus match the value {0}.";

    public ChangeStoryStatusCommand(IList<string> commandParameters, IRepository repository)
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
        var newStoryStatus = ParseStatus(CommandParameters[3]);

        IStory story = Repository.FindTaskByTitle<IStory>(title, board);

        if (newStoryStatus == story.Status)
        {
            throw new ArgumentException
                (string.Format(ChangeStoryStatusErrorMessage, title, newStoryStatus));
        }

        story.Status = newStoryStatus;

        return string.Format(ChangeStoryStatusOutputMessage, title, newStoryStatus);
    }

    private StatusStory ParseStatus(string value)
    {
        if (Enum.TryParse(value, true, out StatusStory result))
        {
            return result;
        }
        throw new InvalidUserInputException
            (string.Format(InvalidStoryStatusErrorMessage, value));
    }
}
