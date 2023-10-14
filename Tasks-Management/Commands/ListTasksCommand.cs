using System.Text;
using TasksManagement.Commands.Abstracts;
using TasksManagement.Core.Contracts;
using TasksManagement.Exceptions;
using TasksManagement.Models.Contracts;

namespace TasksManagement.Commands;
public class ListTasksCommand : BaseCommand
{
    public const int MinExpectedNumberOfArguments = 0;
    public const int MaxExpectedNumberOfArguments = 1;

    public const string NoTasksMessage = "No tasks found.";

    public ListTasksCommand(IList<string> commandParameters, IRepository repository)
        : base(commandParameters, repository)
    {
    }

    public override string Execute()
    {
        if (CommandParameters.Count < MinExpectedNumberOfArguments
            || CommandParameters.Count > MaxExpectedNumberOfArguments)
        {
            throw new InvalidUserInputException
                ($"Invalid number of arguments. Expected: {MinExpectedNumberOfArguments} - " +
                $"{MaxExpectedNumberOfArguments}, Received: {CommandParameters.Count}");
        }

        IList<ITask> foundTasks = new List<ITask>();

        foreach (var team in Repository.Teams)
        {
            foreach (var board in team.Boards)
            {
                foreach (var task in board.Tasks)
                {
                    foundTasks.Add(task);
                }
            }
        }

        if (!foundTasks.Any())
        {
            throw new EntityNotFoundException(NoTasksMessage);
        }

        StringBuilder sb = new StringBuilder();
        var taskIndex = 0;

        if (CommandParameters.Count == 0)
        {

            foreach (var task in foundTasks)
            {
                sb.Append($"{++taskIndex}. ");
                sb.AppendLine(task.ToString());
            }

            return sb.ToString().TrimEnd();
        }

        var titleFilter = CommandParameters[0];
        var filteredTasks = foundTasks.Where
            (task => task.Title.Contains(titleFilter, StringComparison.OrdinalIgnoreCase)).ToList();

        foreach (var task in filteredTasks)
        {
            sb.Append($"{++taskIndex}. ");
            sb.AppendLine(task.ToString());
        }

        return sb.ToString().TrimEnd();
    }
}
