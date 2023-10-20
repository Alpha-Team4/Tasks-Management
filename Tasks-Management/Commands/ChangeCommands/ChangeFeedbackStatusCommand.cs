using TasksManagement.Commands.Abstracts;
using TasksManagement.Core.Contracts;
using TasksManagement.Exceptions;
using TasksManagement.Models.Contracts;
using TasksManagement.Models.Enums;

namespace TasksManagement.Commands.ChangeCommands;
public class ChangeFeedbackStatusCommand : BaseCommand
{
    private const int ExpectedNumberOfArguments = 4;
    private const string ChangeFeedbackStatusErrorMessage = "Feedback '{0}' status is already '{1}'.";
    private const string ChangeFeedbackStatusOutputMessage = "Feedback '{0}' status changed to '{1}'.";
    private const string InvalidFeedbackStatusErrorMessage = "{0} is not a valid status.";

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
        var feedback = Repository.FindTaskByTitle<IFeedback>(CommandParameters[2], board);
        var newFeedbackStatus = Validator.ParseTEnum<StatusFeedback>
            (CommandParameters[3], InvalidFeedbackStatusErrorMessage);

        if (newFeedbackStatus == feedback.Status)
        {
            throw new ArgumentException
                (string.Format(ChangeFeedbackStatusErrorMessage, feedback.Title, newFeedbackStatus));
        }

        feedback.Status = newFeedbackStatus;

        return string.Format(ChangeFeedbackStatusOutputMessage, feedback.Title, newFeedbackStatus);
    }
}
