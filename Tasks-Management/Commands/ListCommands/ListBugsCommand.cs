using Microsoft.VisualBasic;
using TasksManagement.Commands.Abstracts;
using TasksManagement.Core.Contracts;
using TasksManagement.Exceptions;
using TasksManagement.Models;
using TasksManagement.Models.Contracts;
using TasksManagement.Models.Enums;

namespace TasksManagement.Commands.ListCommands;

public class ListBugsCommand : BaseCommand
{
    private const int MinExpectedNumberOfArguments = 1;
    private const int MaxExpectedNumberOfArguments = 2;
    private const string InvalidBugStatusErrorMessage = "None of the enums in 'BugStatus' match the value {0}.";
    private const string NoBugsErrorMessage = "No bugs yet.";
    private const string NoBugsWithStatusErrorMessage = "There are no bugs with the '{0}' status.";
    private const string NoBugsWithAssigneeErrorMessage = "There are no bugs assigned to {0}.";

    public ListBugsCommand(IList<string> commandParameters, IRepository repository)
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

        var bugs = Repository.FindAllTasks().OfType<IBug>().ToList();
        var statusFilter = ParseStatus(CommandParameters[0]);

        if (bugs.Any())
        {
            throw new EntityNotFoundException(NoBugsErrorMessage);
        }

        if (CommandParameters.Count == 1)
        {
            var filteredBugsByStatus = bugs
                .Where(bug => bug.Status == statusFilter)
                .OrderBy(bug => bug.Title)
                .ThenBy(bug => bug.Priority)
                .ThenBy(bug => bug.Severity)
                .ToList();

            if (filteredBugsByStatus.Any())
            {
                return string.Join(Environment.NewLine, filteredBugsByStatus);
            }

            throw new EntityNotFoundException
                (string.Format(NoBugsWithStatusErrorMessage, statusFilter));

        }

        var assignee = Repository.FindMemberByName(CommandParameters[1]);

        var filteredBugsByAssignee = bugs
            .Where(bug => bug.Assignee == assignee && bug.Status == statusFilter)
            .OrderBy(bug => bug.Title)
            .ThenBy(bug => bug.Priority)
            .ThenBy(bug => bug.Severity)
            .ToList();

        if (filteredBugsByAssignee.Any())
        {
            return string.Join(Environment.NewLine, filteredBugsByAssignee);
        }

        throw new EntityNotFoundException
            (string.Format(NoBugsWithAssigneeErrorMessage, assignee.Name));
    }

    private StatusBug ParseStatus(string value)
    {
        if (Enum.TryParse(value, true, out StatusBug result))
        {
            return result;
        }

        throw new InvalidUserInputException
            (string.Format(InvalidBugStatusErrorMessage, value));
    }
}
