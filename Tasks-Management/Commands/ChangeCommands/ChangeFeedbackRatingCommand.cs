using TasksManagement.Commands.Abstracts;
using TasksManagement.Core.Contracts;
using TasksManagement.Core;
using TasksManagement.Exceptions;
using TasksManagement.Models.Contracts;
using TasksManagement.Models.Enums;

namespace TasksManagement.Commands.ChangeCommands;
public class ChangeFeedbackRatingCommand : BaseCommand
{
    private const int ExpectedNumberOfArguments = 4;
    private const string ChangeBugStatusErrorMessage = "Feedback {0} rating is already {1}.";
    private const string ChangeBugStatusOutputMessage = "Feedback {0} rating changed to {1}.";
    private const string InvalidFeedbackRatingErrorMessage = "{0} is not a valid rating.";

    public ChangeFeedbackRatingCommand(IList<string> commandParameters, IRepository repository)
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
        var rating = Validator.ParseTEnum<Rating>
            (CommandParameters[3], InvalidFeedbackRatingErrorMessage);

        if (feedback.Rating == rating)
        {
            throw new ArgumentException(string.Format(ChangeBugStatusErrorMessage, feedback.Title, rating));
        }

        feedback.Rating = rating;

        return string.Format(ChangeBugStatusOutputMessage, feedback.Title, rating);
    }
}
