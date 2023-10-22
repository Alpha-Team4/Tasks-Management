using TasksManagement.Core.Contracts;
using TasksManagement.Core;
using TasksManagement.Exceptions;
using TasksManagement.Commands.Abstracts;

namespace TasksManagement.Commands.CreateCommands;
public class CreateFeedbackCommand : BaseCommand
{
    public const int ExpectedNumberOfArguments = 4;

    public CreateFeedbackCommand(IList<string> commandParameters, IRepository repository)
        : base(commandParameters, repository)
    {
    }

    public override string Execute()
    {
        if (CommandParameters.Count != ExpectedNumberOfArguments)
        {
            throw new InvalidUserInputException($"Invalid number of arguments. Expected: {ExpectedNumberOfArguments}, Received: {CommandParameters.Count}");
        }

        var title = CommandParameters[0];
        var description = CommandParameters[1];
        var teamName = CommandParameters[2];
        var boardName = CommandParameters[3];

        var feedback = Repository.CreateFeedback(title, description, teamName, boardName);
        return $"Feedback with ID '{feedback.Id}' was created.";
    }
}
