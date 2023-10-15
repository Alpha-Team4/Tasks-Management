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

        if (CommandParameters.Count == 0)
        {
            return PrintTaskList(Repository.FindAllTasks());
        }
        else
        {
            var titleFilter = CommandParameters[0];
            var filteredTasks = Repository.FindAllTasks()
                .Where(task => task.Title.Contains(titleFilter, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return PrintTaskList(filteredTasks);
        }
    }

    public string PrintTaskList(IList<ITask> tasks)
    {
        StringBuilder sb = new StringBuilder();
        var taskIndex = 0;

        foreach (var task in tasks)
        {
            sb.Append($"{++taskIndex}. ");
            sb.AppendLine(task.ToString());
        }

        return sb.ToString().TrimEnd();
    }
}
