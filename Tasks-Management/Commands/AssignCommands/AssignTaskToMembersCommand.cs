using TasksManagement.Commands.Abstracts;
using TasksManagement.Core.Contracts;
using TasksManagement.Exceptions;
using TasksManagement.Models.Contracts;

namespace TasksManagement.Commands.AssignCommands;
public class AssignTaskToMembersCommand : BaseCommand
{
    private const int ExpectedNumberOfArguments = 4;
    private const string TaskAlreadyAssignedErrorMessage = "Invalid input. Task {0} is already assigned to member {1}.";
    private const string TaskAddedToMemberMessage = "Task {0} assigned to member {1}.";
    public AssignTaskToMembersCommand(IList<string> commandParameters, IRepository repository) 
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

        var team = Repository.FindTeamByName(CommandParameters[0]);
        var board = Repository.FindBoardByName(CommandParameters[1], team);
        var task = Repository.FindTaskByTitle<ITask>(CommandParameters[2], board);
        var member = Repository.FindMemberByName(CommandParameters[3]);
        
        if (member.Tasks.Contains(task))
        {
            throw new InvalidUserInputException(string.Format(TaskAlreadyAssignedErrorMessage, task.Title, member.Name));
        }

        member.AddTask(task);

        return string.Format(TaskAddedToMemberMessage, task.Title, member.Name); 
    }
}
