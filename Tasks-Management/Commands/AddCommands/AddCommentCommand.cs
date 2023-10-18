using TasksManagement.Commands.Abstracts;
using TasksManagement.Core.Contracts;
using TasksManagement.Exceptions;
using TasksManagement.Models;
using TasksManagement.Models.Contracts;

namespace TasksManagement.Commands.AddCommands;
public class AddCommentCommand : BaseCommand
{
    private const int ExpectedNumberOfArguments = 5;
    private const string AddCommentOutputMessage = "The comment has been successfully added to the task: '{0}'";

    public AddCommentCommand(IList<string> commandParameters, IRepository repository)
        : base(commandParameters, repository)
    {
    }

    public override string Execute()
    {

        if (CommandParameters.Count != ExpectedNumberOfArguments)
        {
            throw new InvalidUserInputException
            ($"Invalid number of arguments. Expected: {ExpectedNumberOfArguments} " +
             $"Received: {CommandParameters.Count}");
        }

        var teamName = CommandParameters[0];
        var boardName = CommandParameters[1];
        var taskTitle = CommandParameters[2];
        var content = CommandParameters[3];
        var author = CommandParameters[4];

        var team = Repository.FindTeamByName(teamName);
        var board = Repository.FindBoardByName(boardName, team);
        var task = Repository.FindTaskByTitle<ITask>(taskTitle, board);

        task.AddComment(new Comment(content, author));

        return string.Format(AddCommentOutputMessage, taskTitle);
    }
}
