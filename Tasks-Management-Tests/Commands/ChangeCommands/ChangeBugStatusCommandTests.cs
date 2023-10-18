using Microsoft.VisualStudio.TestTools.UnitTesting;
using TasksManagement.Core;
using TasksManagement.Exceptions;
using TasksManagement.Models.Enums;
using static TasksManagement_Tests.Helpers.TestData;
using TasksManagement.Models.Enums;

namespace TasksManagement.Commands.ChangeCommands.Tests;

[TestClass]
public class ChangeBugStatusCommandTests
{
    [TestMethod]
    public void Constructor_InitializesCommand()
    {
        var testRepo = new Repository();
        var testParams = new List<string>();

        var sut = new ChangeBugStatusCommand(testParams, testRepo);

        Assert.IsInstanceOfType(sut, typeof(ChangeBugStatusCommand));
    }

    [TestMethod]
    public void Execute_ChangesBugStatus()
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
            "Fixed"
        };
        
        var expectedOutput = $"Bug '{TaskData.ValidTitle}' status changed to '{StatusBug.Fixed}'.";

        //act
        var sut = new ChangeBugStatusCommand(testParams, testRepo);

        Assert.AreEqual(expectedOutput, sut.Execute());
    }

    [TestMethod]
    public void Execute_Throws_When_StatusIsTheSame()
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
        
        //act
        var sut = new ChangeBugStatusCommand(testParams, testRepo);

        Assert.ThrowsException<ArgumentException>(() => sut.Execute());
    }

    [TestMethod]
    public void Execute_Throws_When_StatusCannotBeParsed()
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
            "wrongStatus"
        };
        
        var expectedOutput = $"Bug '{TaskData.ValidTitle}' status changed to '{StatusBug.Active}'.";

        //act
        var sut = new ChangeBugStatusCommand(testParams, testRepo);

        Assert.ThrowsException<InvalidUserInputException>(() => sut.Execute());
    }

    [TestMethod]
    public void Execute_Throws_When_ParameterCountIncorrect()
    {
        var testRepo = new Repository();
        var testParams = new List<string> { "testparam", "testparam2", "testparam3" };

        var sut = new ChangeBugStatusCommand(testParams, testRepo);

        Assert.ThrowsException<InvalidUserInputException>(() => sut.Execute());
    }
}