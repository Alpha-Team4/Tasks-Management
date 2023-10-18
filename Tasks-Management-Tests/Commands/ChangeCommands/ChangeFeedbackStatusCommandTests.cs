using Microsoft.VisualStudio.TestTools.UnitTesting;
using TasksManagement.Core;
using TasksManagement.Exceptions;
using TasksManagement.Models.Enums;
using static TasksManagement_Tests.Helpers.TestData;

namespace TasksManagement.Commands.ChangeCommands.Tests;

[TestClass]
public class ChangeFeedbackStatusCommandTests
{
    [TestMethod]
    public void Constructor_InitializesCommand()
    {
        var testRepo = new Repository();
        var testParams = new List<string>();

        var sut = new ChangeFeedbackStatusCommand(testParams, testRepo);

        Assert.IsInstanceOfType(sut, typeof(ChangeFeedbackStatusCommand));
    }

    [TestMethod]
    public void Execute_ChangesFeedbackStatus()
    {
        // arrange
        var testRepo = new Repository();

        testRepo.CreateTeam(TeamData.ValidName);
        testRepo.CreateBoard(BoardData.ValidName, TeamData.ValidName);
        testRepo.CreateFeedback(
            TaskData.ValidTitle, TaskData.ValidDescription, TeamData.ValidName, BoardData.ValidName
            );

        var testParams = new List<string> {
            TeamData.ValidName,
            BoardData.ValidName,
            TaskData.ValidTitle,
            "Scheduled"
        };

        var expectedOutput = $"Feedback '{TaskData.ValidTitle}' status changed to '{StatusFeedback.Scheduled}'.";

        //act
        var sut = new ChangeFeedbackStatusCommand(testParams, testRepo);

        Assert.AreEqual(expectedOutput, sut.Execute());
    }

    [TestMethod]
    public void Execute_Throws_When_StatusIsTheSame()
    {
        // arrange
        var testRepo = new Repository();

        testRepo.CreateTeam(TeamData.ValidName);
        testRepo.CreateBoard(BoardData.ValidName, TeamData.ValidName);
        testRepo.CreateFeedback(
            TaskData.ValidTitle, TaskData.ValidDescription, TeamData.ValidName, BoardData.ValidName
            );

        var testParams = new List<string> {
            TeamData.ValidName,
            BoardData.ValidName,
            TaskData.ValidTitle,
            "0"
        };

        //act
        var sut = new ChangeFeedbackStatusCommand(testParams, testRepo);

        Assert.ThrowsException<ArgumentException>(() => sut.Execute());
    }

    [TestMethod]
    public void Execute_Throws_When_StatusCannotBeParsed()
    {
        // arrange
        var testRepo = new Repository();

        testRepo.CreateTeam(TeamData.ValidName);
        testRepo.CreateBoard(BoardData.ValidName, TeamData.ValidName);
        testRepo.CreateFeedback(
            TaskData.ValidTitle, TaskData.ValidDescription, TeamData.ValidName, BoardData.ValidName
            );

        var testParams = new List<string> {
            TeamData.ValidName,
            BoardData.ValidName,
            TaskData.ValidTitle,
            "wrongStatus"
        };

        //act
        var sut = new ChangeFeedbackStatusCommand(testParams, testRepo);

        Assert.ThrowsException<InvalidUserInputException>(() => sut.Execute());
    }

    [TestMethod]
    public void Execute_Throws_When_ParameterCountIncorrect()
    {
        var testRepo = new Repository();
        var testParams = new List<string> { "testparam", "testparam2", "testparam3" };

        var sut = new ChangeFeedbackStatusCommand(testParams, testRepo);

        Assert.ThrowsException<InvalidUserInputException>(() => sut.Execute());
    }
}