using TasksManagement.Commands.Abstracts;
using TasksManagement.Core.Contracts;
using TasksManagement.Exceptions;

namespace TasksManagement.Commands.AssignCommands;
public class UnassignTaskCommand : BaseCommand
{
    private const int ExpectedNumberOfArguments = 2;
    private const string TaskNotAssignedErrorMessage = "Invalid input. Task {0} is not assigned to member {1}.";
    private const string TaskUnassignedMessage = "Task {0} unassigned from member {1}.";
    public UnassignTaskCommand(IList<string> commandParameters, IRepository repository)
        : base(commandParameters, repository)
    {
    }

    public override string Execute()
    {
        if (CommandParameters.Count != ExpectedNumberOfArguments)
        {
            throw new InvalidUserInputException($"Invalid number of arguments. " +
                $"Expected: {ExpectedNumberOfArguments}, Received: {CommandParameters.Count}");
        }

        var member = Repository.FindMemberByName(CommandParameters[0]);
        string taskTitle = CommandParameters[1];
        var task = member.Tasks.FirstOrDefault(t => t.Title == taskTitle);

        if (task == null) 
        {
            throw new InvalidUserInputException(string.Format(TaskNotAssignedErrorMessage, taskTitle, member.Name));  
        }

        member.RemoveTask(task);
        return string.Format(TaskUnassignedMessage, taskTitle, member.Name);   

    }
}
