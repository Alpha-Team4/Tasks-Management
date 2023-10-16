using TasksManagement.Commands.Abstracts;
using TasksManagement.Core.Contracts;
using TasksManagement.Exceptions;
using TasksManagement.Models.Contracts;
using TasksManagement.Models.Enums;

namespace TasksManagement.Commands.ChangeCommands;
public class ChangeStorySizeCommand : BaseCommand
{
    public const int ExpectedNumberOfArguments = 2;

    public ChangeStorySizeCommand(IList<string> commandParameters, IRepository repository) : base(commandParameters, repository)
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

        story.Size = ParseSize(CommandParameters[1]);

        return $"Story {story.Title} size changed to {story.Size}";

    }

    protected Size ParseSize(string value)
    {
        if (Enum.TryParse(value, true, out Size result))
        {
            return result;
        }
        throw new ArgumentException($"None of the enums in Size match the value {value}.");
    }
}
