using TasksManagement.Commands.Abstracts;
using TasksManagement.Core.Contracts;
using TasksManagement.Exceptions;
using TasksManagement.Models.Contracts;
using TasksManagement.Models.Enums;

namespace TasksManagement.Commands.ChangeCommands;
public class ChangeStorySizeCommand : BaseCommand
{
    private const int ExpectedNumberOfArguments = 4;
    private const string ChangeStorySizeErrorMessage = "Story {0} size already {1}.";
    private const string ChangeStorySizeOutputMessage = "Story {0} size changed to {1}.";
    private const string InvalidStorySizeErrorMessage = "None of the enums in Size match the value {0}.";

    public ChangeStorySizeCommand(IList<string> commandParameters, IRepository repository)
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
        var newStorySize = ParseSize(CommandParameters[3]);

        IStory story = Repository.FindTaskByTitle<IStory>(title, board);

        if (newStorySize == story.Size)
        {
            throw new ArgumentException
                (string.Format(ChangeStorySizeErrorMessage, title, newStorySize));
        }

        story.Size = newStorySize;

        return string.Format(ChangeStorySizeOutputMessage, title, newStorySize);
    }

    private Size ParseSize(string value)
    {
        if (Enum.TryParse(value, true, out Size result))
        {
            return result;
        }
        throw new InvalidUserInputException
            (string.Format(InvalidStorySizeErrorMessage, value));
    }
}
