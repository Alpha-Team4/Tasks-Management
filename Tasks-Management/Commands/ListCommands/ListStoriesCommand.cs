using System.Security.Cryptography.X509Certificates;
using TasksManagement.Commands.Abstracts;
using TasksManagement.Core.Contracts;
using TasksManagement.Exceptions;
using TasksManagement.Models.Contracts;
using TasksManagement.Models.Enums;

namespace TasksManagement.Commands.ListCommands;
public class ListStoriesCommand : BaseCommand
{
    private const int MinExpectedNumberOfArguments = 0;
    private const int MaxExpectedNumberOfArguments = 2;
    private const string InvalidStoryStatusErrorMessage = "None of the enums in 'StoryStatus' match the value {0}.";
    private const string NoStoriesErrorMessage = "No stories yet.";
    private const string NoStoriesWithStatusErrorMessage = "There are no stories with the '{0}' status.";
    private const string NoStoriesWithAssigneeErrorMessage = "There are no stories assigned to {0}.";

    public ListStoriesCommand(IList<string> commandParameters, IRepository repository)
        : base(commandParameters, repository)
    {
    }

    public override string Execute()
    {
        var stories = Repository.FindAllTasks().OfType<IStory>().ToList();
        string assigneeName;
        StatusStory statusFilter;
        if (!stories.Any())
        {
            throw new EntityNotFoundException(NoStoriesErrorMessage);
        }

        switch(CommandParameters.Count) 
        {
            case 0:
                var orderedStories = stories.OrderBy(story => story.Title)
                .ThenBy(story => story.Priority)
                .ThenBy(story => story.Size)
                .ToList();

                return string.Join(Environment.NewLine, orderedStories);
            case 1:
                assigneeName = CommandParameters[0];
                if (Repository.Members.Exists(member => member.Name == assigneeName))
                {
                    var filteredStories = FilterStoriesByAssignee(stories, assigneeName);
                    return string.Join(Environment.NewLine, filteredStories);
                }
                else
                {
                    statusFilter = ParseStatus(CommandParameters[0]);
                    var filteredStories = FilterStoriesByStatus(stories, statusFilter);
                    return string.Join(Environment.NewLine, filteredStories);
                }
            case 2:
                statusFilter = ParseStatus(CommandParameters[0]);
                assigneeName = CommandParameters[1];
                var filteredStoriesByStatus = FilterStoriesByStatus(stories, statusFilter);
                var filteredStoriesByAssignee = FilterStoriesByAssignee(filteredStoriesByStatus, assigneeName);
                return string.Join (Environment.NewLine, filteredStoriesByAssignee);

            default:
                throw new InvalidUserInputException
                        ($"Invalid number of arguments. Expected: {MinExpectedNumberOfArguments} - " +
                         $"{MaxExpectedNumberOfArguments}, Received: {CommandParameters.Count}");
        }


    }

    private StatusStory ParseStatus(string value)
    {
        if (Enum.TryParse(value, true, out StatusStory result))
        {
            return result;
        }

        throw new InvalidUserInputException
            (string.Format(InvalidStoryStatusErrorMessage, value));
    }

    private List<IStory> FilterStoriesByStatus(List<IStory> stories, StatusStory statusFilter)
    {
        if (!stories.Select(story => story.Status == statusFilter).Any())
        {
            throw new EntityNotFoundException(string.Format(NoStoriesWithStatusErrorMessage, statusFilter));  
        }
        return stories.Where(story => story.Status == statusFilter)
                .OrderBy(story => story.Title)
                .ThenBy(story => story.Priority)
                .ThenBy(story => story.Size)
                .ToList();
    }

    private List<IStory> FilterStoriesByAssignee(List<IStory> stories, string assigneeName)
    {
        var assignee = Repository.FindMemberByName(assigneeName);
        if (!assignee.Tasks.OfType<IStory>().Any())
        {
            throw new EntityNotFoundException(string.Format(NoStoriesWithAssigneeErrorMessage, assigneeName));
        }    
        return stories.Where(story => story.Assignee.Name == assigneeName)
                .OrderBy(story => story.Title)
                .ThenBy(story => story.Priority)
                .ThenBy(story => story.Size)
                .ToList();
    }



    //public override string Execute()
    //{
    //    if (CommandParameters.Count < MinExpectedNumberOfArguments
    //        || CommandParameters.Count > MaxExpectedNumberOfArguments)
    //    {
    //        throw new InvalidUserInputException
    //        ($"Invalid number of arguments. Expected: {MinExpectedNumberOfArguments} - " +
    //         $"{MaxExpectedNumberOfArguments}, Received: {CommandParameters.Count}");
    //    }

    //    var stories = Repository.FindAllTasks().OfType<IStory>().ToList();
    //    var statusFilter = ParseStatus(CommandParameters[0]);

    //    if (!stories.Any())
    //    {
    //        throw new EntityNotFoundException(NoStoriesErrorMessage);
    //    }

    //    if (CommandParameters.Count == 1)
    //    {
    //        var filteredStoriesByStatus = stories
    //            .Where(story => story.Status == statusFilter)
    //            .OrderBy(story => story.Title)
    //            .ThenBy(story => story.Priority)
    //            .ThenBy(story => story.Size)
    //            .ToList();

    //        if (filteredStoriesByStatus.Any())
    //        {
    //            return string.Join(Environment.NewLine, filteredStoriesByStatus);
    //        }

    //        throw new EntityNotFoundException
    //            (string.Format(NoStoriesWithStatusErrorMessage, statusFilter));

    //    }

    //    var assignee = Repository.FindMemberByName(CommandParameters[1]);

    //    var filteredStoriesByAssignee = stories
    //        .Where(story => story.Assignee == assignee && story.Status == statusFilter)
    //        .OrderBy(story => story.Title)
    //        .ThenBy(story => story.Priority)
    //        .ThenBy(story => story.Size)
    //        .ToList();

    //    if (filteredStoriesByAssignee.Any())
    //    {
    //        return string.Join(Environment.NewLine, filteredStoriesByAssignee);
    //    }

    //    throw new EntityNotFoundException
    //        (string.Format(NoStoriesWithAssigneeErrorMessage, assignee.Name));
    //}

    //private StatusStory ParseStatus(string value)
    //{
    //    if (Enum.TryParse(value, true, out StatusStory result))
    //    {
    //        return result;
    //    }

    //    throw new InvalidUserInputException
    //        (string.Format(InvalidStoryStatusErrorMessage, value));
    //}

}
