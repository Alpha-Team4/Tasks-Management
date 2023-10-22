using TasksManagement.Commands.Abstracts;
using TasksManagement.Core.Contracts;
using TasksManagement.Exceptions;
using TasksManagement.Models.Contracts;
using TasksManagement.Models.Enums;

namespace TasksManagement.Commands.ListCommands;
public class ListFeedbackCommand : BaseCommand
{
    private const int MinExpectedNumberOfArguments = 0;
    private const int MaxExpectedNumberOfArguments = 1;
    private const string InvalidFeedbackStatusErrorMessage = "None of the enums in 'StatusFeedback' match the value {0}.";
    private const string NoFeedbackErrorMessage = "No feedback yet.";
    private const string NoFeedbackWithStatusErrorMessage = "There are no feedback tasks with the '{0}' status.";
    public ListFeedbackCommand(IList<string> commandParameters, IRepository repository) 
        : base(commandParameters, repository)
    {

    }

    public override string Execute()
    {
        var feedback = Repository.FindAllTasks().OfType<IFeedback>().ToList();
        if (!feedback.Any()) 
        {
            throw new EntityNotFoundException(NoFeedbackErrorMessage);
        }
        switch(CommandParameters.Count) 
        {
            case 0:
                var orderedFeedback = feedback
                    .OrderBy(feedback => feedback.Title)
                    .ThenBy(feedback => feedback.Rating)
                    .ToList();

                return string.Join(Environment.NewLine, orderedFeedback);
            case 1:
                var statusFilter = Validator.ParseTEnum<StatusFeedback>
                    (CommandParameters[0], InvalidFeedbackStatusErrorMessage);

                if (!feedback.Any(f => f.Status == statusFilter))
                {
                    throw new EntityNotFoundException(NoFeedbackWithStatusErrorMessage);
                }
                var filteredFeedback = feedback
                    .Where(feedback => feedback.Status == statusFilter)
                    .OrderBy(feedback => feedback.Title)
                    .ThenBy(feedback => feedback.Rating)
                    .ToList();

                return string.Join(Environment.NewLine, filteredFeedback);
            default:
                throw new InvalidUserInputException
                        ($"Invalid number of arguments. Expected: {MinExpectedNumberOfArguments} - " +
                         $"{MaxExpectedNumberOfArguments}, Received: {CommandParameters.Count}");

        }

    }
}
