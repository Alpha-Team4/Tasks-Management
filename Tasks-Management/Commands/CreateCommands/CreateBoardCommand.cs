using System.Windows.Input;
using TasksManagement.Commands.Abstracts;
using TasksManagement.Core.Contracts;
using TasksManagement.Exceptions;

namespace TasksManagement.Commands.CreateCommands;
public class CreateBoardCommand : BaseCommand
{
    public const int ExpectedNumberOfArguments = 2;

    public CreateBoardCommand(IList<string> commandParameters, IRepository repository)
        : base(commandParameters, repository)
    {
    }

    public override string Execute()
    {
        if (CommandParameters.Count < ExpectedNumberOfArguments)
        {
            throw new InvalidUserInputException($"Invalid number of arguments. Expected: {ExpectedNumberOfArguments}, Received: {CommandParameters.Count}");
        }

        var boardName = CommandParameters[0];
        var teamName = CommandParameters[1];

        Repository.CreateBoard(boardName, teamName);

        return $"Board '{boardName}' was created for team '{teamName}'.";
    }
}
