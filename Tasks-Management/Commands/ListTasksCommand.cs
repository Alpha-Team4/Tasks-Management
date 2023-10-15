using System.Text;
using TasksManagement.Commands.Abstracts;
using TasksManagement.Core.Contracts;
using TasksManagement.Exceptions;
using TasksManagement.Models;
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

        var foundTasks = Repository.Teams
            .SelectMany(team => team.Boards)
            .SelectMany(board => board.Tasks);

        if (!foundTasks.Any())
        {
            throw new EntityNotFoundException(NoTasksMessage);
        }

        StringBuilder sb = new StringBuilder();
        var taskIndex = 0;

        if (CommandParameters.Count == 0)
        {

            foreach (var task in Repository.Tasks)
            {
                sb.Append($"{++taskIndex}. ");
                sb.AppendLine(task.ToString());
            }

            return sb.ToString().TrimEnd();
        }

        var titleFilter = CommandParameters[0];
        var filteredTasks = Repository.Tasks.Where
            (task => task.Title.Contains(titleFilter, StringComparison.OrdinalIgnoreCase)).ToList();

        foreach (var task in filteredTasks)
        {
            sb.Append($"{++taskIndex}. ");
            sb.AppendLine(task.ToString());
        }

        return sb.ToString().TrimEnd();
    }
}
