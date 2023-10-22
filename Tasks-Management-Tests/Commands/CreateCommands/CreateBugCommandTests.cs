using Microsoft.VisualStudio.TestTools.UnitTesting;
using TasksManagement.Commands.CreateCommands;
using TasksManagement.Core;
using TasksManagement.Core.Contracts;
using TasksManagement.Exceptions;
using TasksManagement_Tests.Helpers;
using static TasksManagement_Tests.Helpers.TestData;
using static TasksManagement_Tests.Helpers.TestHelpers;

namespace TasksManagement_Tests.Commands.CreateCommands;

[TestClass]
public class CreateBugCommandTests
{
    [TestMethod]
    public void Constructor_InitializesCommandInstance()
    {
        var repository = new Repository();

        var command = new CreateBugCommand(
            new List<string> { "title", "description", "team", "board" },
            repository);

        Assert.IsInstanceOfType(command, typeof(CreateBugCommand));
    }

    [TestMethod]
    public void Execute_Throws_WhenTooManyParameters()
    {
        var repository = new Repository();

        var command = new CreateBugCommand(
            new List<string> { "title", "description", "team", "board", "other" },
            repository);

        Assert.ThrowsException<InvalidUserInputException>(
            () => command.Execute());
    }

    [TestMethod]
    public void Execute_ThrowsOn_TooFewParameters()
    {
        var repository = new Repository();

        var command = new CreateBugCommand(
            new List<string> { "title", "description" },
            repository);

        Assert.ThrowsException<InvalidUserInputException>(
            () => command.Execute());
    }

    [TestMethod]
    public void Execute_WithValidParameters_CreatesBug()
    {
        ResetTaskLastIssuedIdState();
        var repository = new Repository();
        var command = new CreateBugCommand
            (new List<string> { TaskData.ValidTitle, TaskData.ValidDescription, TeamData.ValidName, BoardData.ValidName }, repository);
        repository.CreateTeam(TeamData.ValidName);
        repository.CreateBoard(BoardData.ValidName, TeamData.ValidName);

      
        var result = command.Execute();
        var expected = "Bug with ID '1' was created.";
        var createdBug = repository.CreateBug
            (TaskData.ValidTitle, TaskData.ValidDescription, TeamData.ValidName, BoardData.ValidName);

        Assert.AreEqual(expected, result);
        Assert.IsNotNull(createdBug);
    }
}
