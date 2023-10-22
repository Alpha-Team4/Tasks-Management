using Microsoft.VisualStudio.TestTools.UnitTesting;
using TasksManagement;
using TasksManagement.Core;
using TasksManagement.Exceptions;
using TasksManagement.Models.Enums;
using static TasksManagement_Tests.Helpers.TestData;
using static TasksManagement_Tests.Helpers.TestHelpers;
using TasksManagement.Models.Enums;
using TasksManagement.Commands.ChangeCommands;

namespace TasksManagement_Tests.Commands.ChangeCommands;

[TestClass]
public class ChangeStorySizeCommandTests
{
    [TestMethod]
    public void Constructor_InitializesCommand()
    {
        var repository = new Repository();
        var testParams = new List<string>
        {
            TeamData.ValidName,
            BoardData.ValidName,
            TaskData.ValidTitle,
            "Small"
        };

        var command = new ChangeStorySizeCommand(testParams, repository);

        Assert.IsInstanceOfType(command, typeof(ChangeStorySizeCommand));
    }

    [TestMethod]
    public void Execute_ThrowsOn_InvalidNumberOfParameters()
    {
        var repository = new Repository();
        var testParams = new List<string>
        {
            TeamData.ValidName,
            BoardData.ValidName,
            TaskData.ValidTitle
        };

        var testParams2 = new List<string>
        {
            TeamData.ValidName,
            BoardData.ValidName,
            TaskData.ValidTitle,
            "Small",
            "Medium"
        };

        var command = new ChangeStorySizeCommand(testParams, repository);
        var command2 = new ChangeStorySizeCommand(testParams2, repository);

        Assert.ThrowsException<InvalidUserInputException>(() => command.Execute());
        Assert.ThrowsException<InvalidUserInputException>(() => command2.Execute());
    }

    [TestMethod]
    public void Execute_ValidArguments_ChangesStorySize()
    {
        var repository = new Repository();

        repository.CreateTeam(TeamData.ValidName);
        repository.CreateBoard(BoardData.ValidName, TeamData.ValidName);
        repository.CreateStory
            (TaskData.ValidTitle, TaskData.ValidDescription, TeamData.ValidName, BoardData.ValidName);

        var testParams = new List<string>
        {
            TeamData.ValidName,
            BoardData.ValidName,
            TaskData.ValidTitle,
            "Medium"
        };
        var command = new ChangeStorySizeCommand(testParams, repository);

        var expectedOutput = $"Story '{TaskData.ValidTitle}' size changed to {Size.Medium}.";
        var result = command.Execute();

        Assert.AreEqual(expectedOutput, result);
    }

    [TestMethod]
    public void Execute_Throws_When_SizeIsTheSame()
    {
        var repository = new Repository();

        repository.CreateTeam(TeamData.ValidName);
        repository.CreateBoard(BoardData.ValidName, TeamData.ValidName);
        repository.CreateStory(
            TaskData.ValidTitle, TaskData.ValidDescription, TeamData.ValidName, BoardData.ValidName);

        var testParams = new List<string>
        {
            TeamData.ValidName,
            BoardData.ValidName,
            TaskData.ValidTitle,
            "Small"
        };

        var command = new ChangeStorySizeCommand(testParams, repository);

        Assert.ThrowsException<ArgumentException>(() => command.Execute());
    }
}
