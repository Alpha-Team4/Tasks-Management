using TasksManagement.Commands.Abstracts;
using TasksManagement.Core.Contracts;
using TasksManagement.Core;
using TasksManagement.Exceptions;
using TasksManagement.Models.Contracts;
using TasksManagement.Models.Enums;

namespace TasksManagement.Commands.ChangeCommands;
public class ChangeRatingCommand : BaseCommand
{
    public const int ExpectedNumberOfArguments = 4;

    public ChangeRatingCommand(IList<string> commandParameters, IRepository repository)
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
        var rating = ParseRating(CommandParameters[3]);

        if (feedback.Rating == rating)
        {
            throw new ArgumentException($"Rating is already set to {rating}");
        }

        feedback.Rating = rating;

        return $"Feedback {feedback.Title} rating changed to {feedback.Rating}.";

    }

    private Rating ParseRating(string rating)
    {
        if (Enum.TryParse(rating, true, out Rating result))
        {
            return result;
        }

        throw new ArgumentException($"{rating} is not a valid rating.");
    }
}
