using TasksManagement.Commands.Abstracts;
using TasksManagement.Core.Contracts;
using TasksManagement.Exceptions;
using TasksManagement.Models.Contracts;
using TasksManagement.Models.Enums;

namespace TasksManagement.Commands.ListCommands;
public class ListAssignedTasks : BaseCommand
{
    private const int MinExpectedNumberOfArguments = 0;
    private const int MaxExpectedNumberOfArguments = 2;
    private const string InvalidStatusErrorMessage = "None of the enums in 'StatusFeedback' or 'StatusStory' match the value {0}.";
    private const string NoAssignedTasksMessage = "No assigned tasks yet.";
    private const string NoAssignedTaskWithStatusErrorMessage = "There are no assigned tasks with the '{0}' status.";
    private const string NoTasksWithAssigneeErrorMessage = "There are no tasks assigned to {0}.";

    public ListAssignedTasks(IList<string> commandParameters, IRepository repository) : base(commandParameters, repository)
    {

    }

    public override string Execute()
    {
        var assignedTasks = Repository.FindAllTasks().OfType<IHasAssignee>().Where(tsk => tsk.Assignee != null).ToList();

        if (!assignedTasks.Any())
        {
            throw new EntityNotFoundException(NoAssignedTasksMessage);
        }

        var assignedStories = assignedTasks.OfType<IStory>().ToList();
        var assignedBugs = assignedTasks.OfType<IBug>().ToList();
        string assigneeFilter;


        switch (CommandParameters.Count)
        {
            case 0:
                var sortedStories = assignedStories.OrderBy(story => story.Title).ToList();
                var sortedBugs = assignedBugs.OrderBy(bug => bug.Title).ToList();
                return string.Join(Environment.NewLine, sortedStories, sortedBugs);
            case 1:
                assigneeFilter = CommandParameters[0];
                if (Repository.Members.Exists(member => member.Name == assigneeFilter))
                {
                    return ListByAssignee(assigneeFilter);
                }
                else
                {
                    var filter = CommandParameters[0];
                    return ParseStatusFilter(filter, assignedBugs, assignedStories);
                }

            case 2:
                var assigneeName = CommandParameters[0];
                var assignee = Repository.FindMemberByName(assigneeName);
                var filteredBugs = FilterBugsByAssignee(assigneeName);
                var filteredStories = FilterStoriesByAssignee(assigneeName);
                var statusFilter = CommandParameters[1];
                return ParseStatusFilter(statusFilter, filteredBugs, filteredStories);
            default:
                throw new InvalidUserInputException
                ($"Invalid number of arguments. Expected: {MinExpectedNumberOfArguments} - " +
                $"{MaxExpectedNumberOfArguments}, Received: {CommandParameters.Count}");

        }

    }
    private List<IStory> FilterStoriesByAssignee(string assigneeName)
    {
        var assignee = Repository.FindMemberByName(assigneeName);
        return assignee.Tasks.OfType<IStory>()
                .OrderBy(story => story.Title)
                .ThenBy(story => story.Priority)
                .ThenBy(story => story.Size)
                .ToList();
    }

    private List<IBug> FilterBugsByAssignee(string assigneeName)
    {
        var assignee = Repository.FindMemberByName(assigneeName);
        return assignee.Tasks.OfType<IBug>()
            .OrderBy(bug => bug.Title)
            .ThenBy(bug => bug.Priority)
            .ThenBy(bug => bug.Severity)
            .ToList();
    }

    private List<IStory> FilterStoriesByStatus(List<IStory> stories, StatusStory statusFilter)
    {
        return stories.Where(story => story.Status == statusFilter)
                .OrderBy(story => story.Title)
                .ThenBy(story => story.Priority)
                .ThenBy(story => story.Size)
                .ToList();
    }

    private List<IBug> FilterBugsByStatus(List<IBug> bugs, StatusBug statusFilter)
    {
        return bugs.Where(bug => bug.Status == statusFilter)
                .OrderBy(bug => bug.Title)
                .ThenBy(bug => bug.Priority)
                .ThenBy(bug => bug.Severity)
                .ToList();
    }

    private string ListByAssignee(string assigneeFilter)
    {
        var filteredStories = FilterStoriesByAssignee(assigneeFilter);
        var filteredBugs = FilterBugsByAssignee(assigneeFilter);
        if (filteredStories.Any() || filteredBugs.Any())
        {
            return string.Join(Environment.NewLine, filteredStories, filteredBugs);
        }
        else
        {
            throw new EntityNotFoundException(NoTasksWithAssigneeErrorMessage);
        }
    }

    private string ParseStatusFilter(string filter, List<IBug> assignedBugs, List<IStory> assignedStories)
    {
        if (Enum.TryParse(filter, true, out StatusStory storyResult))
        {
            var storyFilter = storyResult;
            var filteredStories = FilterStoriesByStatus(assignedStories, storyFilter);
            if (!filteredStories.Any())
            {
                throw new EntityNotFoundException(NoAssignedTaskWithStatusErrorMessage);
            }
            return string.Join(Environment.NewLine, filteredStories);
        }
        else if (Enum.TryParse(filter, true, out StatusBug bugResult))
        {
            var bugFilter = bugResult;
            var filteredBugs = FilterBugsByStatus(assignedBugs, bugFilter);
            if (!filteredBugs.Any())
            {
                throw new EntityNotFoundException(NoAssignedTaskWithStatusErrorMessage);
            }
            return string.Join(Environment.NewLine, filteredBugs);
        }
        else
        {
            throw new InvalidUserInputException(InvalidStatusErrorMessage);
        }
    }


}

