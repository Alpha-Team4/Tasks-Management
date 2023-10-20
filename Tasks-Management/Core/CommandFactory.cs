using TasksManagement.Commands;
using TasksManagement.Commands.Contracts;
using TasksManagement.Commands.Enums;
using TasksManagement.Core.Contracts;
using TasksManagement.Commands.AddCommands;
using TasksManagement.Commands.AssignCommands;
using TasksManagement.Commands.ChangeCommands;
using TasksManagement.Commands.CreateCommands;
using TasksManagement.Commands.ListCommands;
using TasksManagement.Commands.ShowCommands;

namespace TasksManagement.Core;

public class CommandFactory : ICommandFactory
{
    private readonly IRepository repository;

    public CommandFactory(IRepository repository)
    {
        this.repository = repository;
    }

    public ICommand Create(string commandLine)
    {
        // RemoveEmptyEntries makes sure no empty strings are added to the result of the split operation.
        var arguments = commandLine.Split(" ", StringSplitOptions.RemoveEmptyEntries);

        CommandType commandType = ParseCommandType(arguments[0]);
        List<string> commandParameters = ExtractCommandParameters(arguments);

        switch (commandType)
        {
            // Add Commands
            case CommandType.AddMember:
                return new AddMemberCommand(commandParameters, repository);
            case CommandType.AddComment:
                return new AddCommentCommand(commandParameters, repository);

            // Assign Tasks
            case CommandType.AssignStory:
                return new AssignStoryCommand(commandParameters, repository);
            case CommandType.UnassignStory:
                return new UnassignStoryCommand(commandParameters, repository);
            case CommandType.AssignBug:
                return new AssignBugCommand(commandParameters, repository);
            case CommandType.UnassignBug:
                return new UnassignBugCommand(commandParameters, repository);

            // Create Commands
            case CommandType.CreateTeam:
                return new CreateTeamCommand(commandParameters, repository);

            case CommandType.CreateBoard:
                return new CreateBoardCommand(commandParameters, repository);

            case CommandType.CreateMember:
                return new CreateMemberCommand(commandParameters, repository);

            case CommandType.CreateBug:
                return new CreateBugCommand(commandParameters, repository);

            case CommandType.CreateStory:
                return new CreateStoryCommand(commandParameters, repository);

            case CommandType.CreateFeedback:
                return new CreateFeedbackCommand(commandParameters, repository);

            // Change Commands
            case CommandType.ChangeStoryStatus:
                return new ChangeStoryStatusCommand(commandParameters, repository);

            case CommandType.ChangeBugStatus:
                return new ChangeBugStatusCommand(commandParameters, repository);

            case CommandType.ChangeFeedbackStatus:
                return new ChangeFeedbackStatusCommand(commandParameters, repository);

            case CommandType.ChangeFeedbackRating:
                return new ChangeFeedbackRatingCommand(commandParameters, repository);

            case CommandType.ChangeBugAssignee:
                return new ChangeBugAssigneeCommand(commandParameters, repository);

            case CommandType.ChangeStoryAssignee:
                return new ChangeStoryAssigneeCommand(commandParameters, repository);

            case CommandType.ChangeBugPriority:
                return new ChangeBugPriorityCommand(commandParameters, repository);

            case CommandType.ChangeStoryPriority:
                return new ChangeStoryPriorityCommand(commandParameters, repository);

            case CommandType.ChangeStorySize:
                return new ChangeStorySizeCommand(commandParameters, repository);

            case CommandType.ChangeBugSeverity:
                return new ChangeBugSeverityCommand(commandParameters, repository);

            // Show Commands
            case CommandType.ShowTeams:
                return new ShowTeamsCommand(repository);

            case CommandType.ShowBoards:
                return new ShowBoardsCommand(commandParameters, repository);

            case CommandType.ShowMembers:
                return new ShowMembersCommand(commandParameters, repository);

            case CommandType.ShowBoardActivity:
                return new ShowBoardActivityCommand(commandParameters, repository);

            case CommandType.ShowMemberActivity:
                return new ShowMemberActivityCommand(commandParameters, repository);

            case CommandType.ShowTeamActivity:
                return new ShowTeamActivityCommand(commandParameters, repository);

            // List Commands
            case CommandType.ListTasks:
                return new ListTasksCommand(commandParameters, repository);

            case CommandType.ListBugs:
                return new ListBugsCommand(commandParameters, repository);

            case CommandType.ListStories:
                return new ListStoriesCommand(commandParameters, repository);

            default:
                throw new InvalidOperationException($"Command with name '{commandType}' doesn't exist!");
        }
    }

    // Attempts to parse CommandType from a given string value.
    // If successful, returns the command enum value, otherwise returns null
    private CommandType ParseCommandType(string value)
    {
        var parseSuccess = Enum.TryParse(value, true, out CommandType result);

        if (parseSuccess)
        {
            return result;
        }

        throw new InvalidOperationException($"Command with name '{value}' doesn't exist!");
    }

    // Receives a full line and extracts the parameters that are needed for the command to execute.
    // For example, if the input line is "FilterBy Assignee John",
    // the method will return a list of ["Assignee", "John"].
    private List<String> ExtractCommandParameters(string[] arguments)
    {
        List<string> commandParameters = new List<string>();

        for (int i = 1; i < arguments.Length; i++)
        {
            commandParameters.Add(arguments[i]);
        }

        return commandParameters;
    }
}
