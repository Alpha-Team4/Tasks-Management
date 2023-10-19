using TasksManagement.Commands.Abstracts;
using TasksManagement.Core.Contracts;
using TasksManagement.Exceptions;
using TasksManagement.Models.Contracts;

namespace TasksManagement.Commands.ListCommands;
public class ListAssignedTasks : BaseCommand
{
    private const int MinExpectedNumberOfArguments = 0;
    private const int MaxExpectedNumberOfArguments = 2;
    private const string InvalidStatusErrorMessage = "None of the enums in 'StatusFeedback' match the value {0}.";
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

        var stories = assignedTasks.OfType<IStory>();
        var bugs = assignedTasks.OfType<IBug>();

        throw new NotImplementedException();    
        

    }
}

