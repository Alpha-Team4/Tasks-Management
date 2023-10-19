using TasksManagement.Commands.Abstracts;
using TasksManagement.Core.Contracts;
using TasksManagement.Exceptions;
using TasksManagement.Models.Contracts;
using TasksManagement.Models.Enums;

namespace TasksManagement.Commands.ChangeCommands;
public class ChangeStorySizeCommand : BaseCommand
{
    private const int ExpectedNumberOfArguments = 4;
    private const string ChangeStorySizeErrorMessage = "Story '{0}' size already {1}.";
    private const string ChangeStorySizeOutputMessage = "Story '{0}' size changed to {1}.";
    private const string InvalidStorySizeErrorMessage = "{0} is not a valid size.";

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
        var story = Repository.FindTaskByTitle<IStory>(CommandParameters[2], board);
        var newStorySize = Validator.ParseTEnum<Size>
            (CommandParameters[3], InvalidStorySizeErrorMessage);

        if (newStorySize == story.Size)
        {
            throw new ArgumentException
                (string.Format(ChangeStorySizeErrorMessage, story.Title, newStorySize));
        }

        story.Size = newStorySize;

        return string.Format(ChangeStorySizeOutputMessage, story.Title, newStorySize);
    }
}
