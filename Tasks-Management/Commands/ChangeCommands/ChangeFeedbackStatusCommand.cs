using TasksManagement.Commands.Abstracts;
using TasksManagement.Core.Contracts;
using TasksManagement.Exceptions;
using TasksManagement.Models.Contracts;
using TasksManagement.Models.Enums;

namespace TasksManagement.Commands.ChangeCommands;
public class ChangeFeedbackStatusCommand : BaseCommand
{
    private const int ExpectedNumberOfArguments = 2;
    private const string InvalidStatusError = "None of the enums in FeedbackStatus match the value {0}.";

    public ChangeFeedbackStatusCommand(IList<string> commandParameters, IRepository repository)
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

        IFeedback feedback = Repository.FindTaskByTitle<IFeedback>(CommandParameters[0]);

        var newFeedbackStatus = ParseStatus(CommandParameters[1]);

        if (newFeedbackStatus == feedback.Status)
        {
            return $"Feedback {feedback.Title} status already {feedback.Status}.";
        }

        feedback.Status = newFeedbackStatus;

        return $"Feedback {feedback.Title} status changed to {newFeedbackStatus}.";
    }

    private StatusFeedback ParseStatus(string value)
    {
        if (Enum.TryParse(value, true, out StatusFeedback result))
        {
            return result;
        }
        throw new InvalidUserInputException(string.Format(InvalidStatusError, value));
    }
}
