using Microsoft.VisualStudio.TestTools.UnitTesting;
using TasksManagement.Core;
using TasksManagement.Exceptions;
using static TasksManagement_Tests.Helpers.TestData;
using static TasksManagement_Tests.Helpers.TestHelpers;

namespace TasksManagement.Commands.CreateCommands.Tests;

[TestClass]
public class CreateBoardCommandTests
{
    [TestMethod]
    public void Constructor_InitializesCommand()
    {
        var testRepo = new Repository();
        var testParams = new List<string>();

        var sut = new CreateBoardCommand(testParams, testRepo);

        Assert.IsInstanceOfType(sut, typeof(CreateBoardCommand));
    }

    [TestMethod]
    public void Execute_CreatesBoard_WithValidParameters()
    {
        var testRepo = new Repository();
        testRepo.CreateTeam(TeamData.ValidName);
        var testParams = new List<string> { BoardData.ValidName, TeamData.ValidName };
        var expectedOutput = $"Board '{BoardData.ValidName}' was created for team '{TeamData.ValidName}'.";

        var sut = new CreateBoardCommand(testParams, testRepo);

        Assert.AreEqual(expectedOutput, sut.Execute());
    }

    [TestMethod]
    public void Execute_Throws_When_ParameterCountIncorrect()
    {
        var testRepo = new Repository();
        var testParams = new List<string> { "testparam", "testparam2", "testparam3" };

        var sut = new CreateBoardCommand(testParams, testRepo);

        Assert.ThrowsException<InvalidUserInputException>(() => sut.Execute());
    }
}