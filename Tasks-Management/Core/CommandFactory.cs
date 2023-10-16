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
            case CommandType.ShowTeams:
                return new ShowTeamsCommand(repository);
            case CommandType.ShowBoards:
                return new ShowBoardsCommand(commandParameters, repository);
            case CommandType.ShowActivityBoard:
                return new ShowActivityBoardCommand(commandParameters, repository);
            case CommandType.ListTasks:
                return new ListTasksCommand(commandParameters, repository);
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
