using TasksManagement.Commands.Abstracts;
using TasksManagement.Core.Contracts;
using TasksManagement.Exceptions;
using TasksManagement.Models.Contracts;
using TasksManagement.Models.Enums;

namespace TasksManagement.Commands.ChangeCommands;
public class ChangeFeedbackStatusCommand : BaseCommand
{
    private const int ExpectedNumberOfArguments = 4;
    private const string ChangeFeedbackStatusErrorMessage = "Feedback {0} status already {1}.";
    private const string ChangeFeedbackStatusOutputMessage = "Feedback {0} status changed to {1}.";
    private const string InvalidFeedbackStatusErrorMessage = "None of the enums in FeedbackStatus match the value {0}.";

    public ChangeFeedbackStatusCommand(IList<string> commandParameters, IRepository repository)
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
        var newFeedbackStatus = ParseStatus(CommandParameters[3]);

        IFeedback feedback = Repository.FindTaskByTitle<IFeedback>(title, board);

        if (newFeedbackStatus == feedback.Status)
        {
            throw new ArgumentException
                (string.Format(ChangeFeedbackStatusErrorMessage, title, newFeedbackStatus));
        }

        feedback.Status = newFeedbackStatus;

        return string.Format(ChangeFeedbackStatusOutputMessage, title, newFeedbackStatus);
    }

    private StatusFeedback ParseStatus(string value)
    {
        if (Enum.TryParse(value, true, out StatusFeedback result))
        {
            return result;
        }
        throw new InvalidUserInputException(string.Format(InvalidFeedbackStatusErrorMessage, value));
    }
}
