using Microsoft.VisualStudio.TestTools.UnitTesting;
using TasksManagement.Core;
using TasksManagement.Exceptions;
using TasksManagement.Models.Enums;
using static TasksManagement_Tests.Helpers.TestData;

namespace TasksManagement.Commands.ChangeCommands.Tests;

[TestClass]
public class ChangeBugSeverityCommandTests
{
    [TestMethod]
    public void Constructor_InitializesCommand()
    {
        var testRepo = new Repository();
        var testParams = new List<string>();

        var sut = new ChangeBugSeverityCommand(testParams, testRepo);

        Assert.IsInstanceOfType(sut, typeof(ChangeBugSeverityCommand));
    }

    [TestMethod]
    public void Execute_ChangesBugSeverity()
    {
        // arrange
        var testRepo = new Repository();

        testRepo.CreateTeam(TeamData.ValidName);
        testRepo.CreateBoard(BoardData.ValidName, TeamData.ValidName);
        testRepo.CreateBug(
            TaskData.ValidTitle, TaskData.ValidDescription, TeamData.ValidName, BoardData.ValidName
            );
        
        var testParams = new List<string> {
            TeamData.ValidName,
            BoardData.ValidName,
            TaskData.ValidTitle,
            "Critical"
        };
        
        var expectedOutput = $"Bug '{TaskData.ValidTitle}' severity changed to '{Severity.Critical}'.";

        //act
        var sut = new ChangeBugSeverityCommand(testParams, testRepo);

        Assert.AreEqual(expectedOutput, sut.Execute());
    }

    [TestMethod]
    public void Execute_Throws_When_SeverityIsTheSame()
    {
        // arrange
        var testRepo = new Repository();

        testRepo.CreateTeam(TeamData.ValidName);
        testRepo.CreateBoard(BoardData.ValidName, TeamData.ValidName);
        testRepo.CreateBug(
            TaskData.ValidTitle, TaskData.ValidDescription, TeamData.ValidName, BoardData.ValidName
            );
        
        var testParams = new List<string> {
            TeamData.ValidName,
            BoardData.ValidName,
            TaskData.ValidTitle,
            "0"
        };
        
        var expectedOutput = $"Bug '{TaskData.ValidTitle}' severity changed to '{Severity.Minor}'.";

        //act
        var sut = new ChangeBugSeverityCommand(testParams, testRepo);

        Assert.ThrowsException<ArgumentException>(() => sut.Execute());
    }

    [TestMethod]
    public void Execute_Throws_When_SeverityCannotBeParsed()
    {
        // arrange
        var testRepo = new Repository();

        testRepo.CreateTeam(TeamData.ValidName);
        testRepo.CreateBoard(BoardData.ValidName, TeamData.ValidName);
        testRepo.CreateBug(
            TaskData.ValidTitle, TaskData.ValidDescription, TeamData.ValidName, BoardData.ValidName
            );
        
        var testParams = new List<string> {
            TeamData.ValidName,
            BoardData.ValidName,
            TaskData.ValidTitle,
            "wrongSeverity"
        };
        
        var expectedOutput = $"Bug '{TaskData.ValidTitle}' severity changed to '{Severity.Minor}'.";

        //act
        var sut = new ChangeBugSeverityCommand(testParams, testRepo);

        Assert.ThrowsException<InvalidUserInputException>(() => sut.Execute());
    }

    [TestMethod]
    public void Execute_Throws_When_ParameterCountIncorrect()
    {
        var testRepo = new Repository();
        var testParams = new List<string> { "testparam", "testparam2", "testparam3" };

        var sut = new ChangeBugSeverityCommand(testParams, testRepo);

        Assert.ThrowsException<InvalidUserInputException>(() => sut.Execute());
    }
}