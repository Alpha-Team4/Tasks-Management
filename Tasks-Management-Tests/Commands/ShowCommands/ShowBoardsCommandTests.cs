using Microsoft.VisualStudio.TestTools.UnitTesting;
using TasksManagement.Core;
using TasksManagement.Exceptions;
using static TasksManagement_Tests.Helpers.TestData;
using TasksManagement.Commands.ShowCommands;

namespace TasksManagement_Tests.Commands.ShowCommands;

[TestClass]
public class ShowBoardsCommandTests
{
    [TestMethod]
    public void Constructor_InitializesCommandInstance()
    {
        var repository = new Repository();

        var command = new ShowBoardsCommand(
            new List<string> { TeamData.ValidName },
            repository);

        Assert.IsInstanceOfType(command, typeof(ShowBoardsCommand));
    }

    [TestMethod]
    public void Execute_ThrowsOn_InvalidNumberOfParameters()
    {
        var repository = new Repository();

        var command = new ShowBoardsCommand(
            new List<string> { },
            repository);

        var command2 = new ShowBoardsCommand(
            new List<string> { TeamData.ValidName , TeamData.ValidName },
            repository);

        Assert.ThrowsException<InvalidUserInputException>(() => command.Execute());
        Assert.ThrowsException<InvalidUserInputException>(() => command2.Execute());
    }

    [TestMethod]
    public void Execute_WithValidParameters_ReturnsBoards()
    {
        var repository = new Repository();
        var command = new ShowBoardsCommand(
            new List<string> { TeamData.ValidName },
            repository);

        var team = repository.CreateTeam(TeamData.ValidName);
        repository.CreateBoard(BoardData.ValidName, TeamData.ValidName);
        var result = command.Execute();

        Assert.IsFalse(string.IsNullOrEmpty(result));
    }

    [TestMethod]
    public void Execute_Throw_WhenNoBoard()
    {
        var repository = new Repository();
        var command = new ShowBoardsCommand(
            new List<string> { TeamData.ValidName },
            repository);
        var team = repository.CreateTeam(TeamData.ValidName);

        Assert.ThrowsException<EntityNotFoundException>(() => command.Execute());
    }
}