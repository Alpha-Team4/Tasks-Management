using Microsoft.VisualStudio.TestTools.UnitTesting;
using TasksManagement.Commands.ListCommands;
using TasksManagement.Core;
using TasksManagement.Exceptions;
using static TasksManagement_Tests.Helpers.TestData;
using static TasksManagement_Tests.Helpers.TestHelpers;

namespace TasksManagement_Tests.Commands.ListCommands;

[TestClass]
public class ListBugsCommandTests
{
    [TestMethod]
    public void Constructor_InitializesCommandInstance()
    {
        var testRepo = new Repository();

        var sut = new ListBugsCommand(
                new List<string> { "filter", "assignee" },
                testRepo
            );

        Assert.IsInstanceOfType(sut, typeof(ListBugsCommand));
    }

    [TestMethod]
    public void Execute_ThrowsOn_TooFewParameters()
    {
        var testRepo = new Repository();

        var sut = new ListBugsCommand(
                new List<string> { },
                testRepo
            );

        Assert.ThrowsException<InvalidUserInputException>(
                () => sut.Execute()
            ); ;
    }

    [TestMethod]
    public void Execute_ThrowsOn_TooManyParameters()
    {
        var testRepo = new Repository();

        var sut = new ListBugsCommand(
                new List<string> { "1", "2", "3" },
                testRepo
            );

        Assert.ThrowsException<InvalidUserInputException>(
                () => sut.Execute()
            ); ;
    }

    [TestMethod]
    public void Execute_ReturnsListOfAllBugs()
    {
        ResetTaskLastIssuedIdState();
        var testRepo = new Repository();
        testRepo.CreateTeam(TeamData.ValidName);
        testRepo.CreateBoard(BoardData.ValidName, TeamData.ValidName);
        testRepo.CreateBug(
            TaskData.ValidTitle, TaskData.ValidDescription, TeamData.ValidName, BoardData.ValidName
            );
        
        ResetTaskLastIssuedIdState();
        var testBug = InitializeTestBug();

        var sut = new ListBugsCommand(
                new List<string> { "Active" },
                testRepo
            );

        Assert.AreEqual(testBug.ToString(), sut.Execute());
    }

    [TestMethod]
    public void Execute_ThrowsOn_NoBugsFound()
    {
        var testRepo = new Repository();
        var testBug = InitializeTestBug();
        testRepo.CreateTeam(TeamData.ValidName);
        testRepo.CreateBoard(BoardData.ValidName, TeamData.ValidName);

        var sut = new ListBugsCommand(
                new List<string> { "Active" },
                testRepo
            );

        Assert.ThrowsException<EntityNotFoundException>(() => sut.Execute());
    }

    [TestMethod]
    public void Execute_Returns_ListFilteredBy_Status()
    {
        ResetTaskLastIssuedIdState();
        var testRepo = new Repository();
        testRepo.CreateTeam(TeamData.ValidName);
        testRepo.CreateBoard(BoardData.ValidName, TeamData.ValidName);
        testRepo.CreateBug(TaskData.ValidTitle, TaskData.ValidDescription, TeamData.ValidName, BoardData.ValidName);

        ResetTaskLastIssuedIdState();
        var testBug = InitializeTestBug();

        var sut = new ListBugsCommand(
                new List<string> { "Active" },
                testRepo
            );

        Assert.AreEqual(testBug.ToString(), sut.Execute());
    }

    [TestMethod]
    public void Execute_ThrowsOn_NoFilteredBugsFound()
    {
        var testRepo = new Repository();
        testRepo.CreateTeam(TeamData.ValidName);
        testRepo.CreateBoard(BoardData.ValidName, TeamData.ValidName);
        testRepo.CreateBug(TaskData.ValidTitle, TaskData.ValidDescription, TeamData.ValidName, BoardData.ValidName);

        var sut = new ListBugsCommand(
                new List<string> { "Fixed" },
                testRepo
            );

        Assert.ThrowsException<EntityNotFoundException>(() => sut.Execute());
    }

    //[TestMethod]
    //public void Execute_Returns_ListFilteredBy_StatusAndAssignee()
    //{
    //    ResetTaskLastIssuedIdState();
    //    var testRepo = new Repository();
    //    testRepo.CreateTeam(TeamData.ValidName);
    //    testRepo.CreateBoard(BoardData.ValidName, TeamData.ValidName);
    //    testRepo.CreateBug(TaskData.ValidTitle, TaskData.ValidDescription, TeamData.ValidName, BoardData.ValidName);
    //    testRepo.CreateMember(MemberData.ValidName);

    //    ResetTaskLastIssuedIdState();
    //    var testBug = InitializeTestBug();

    //    var assign = new AssignTaskCommand(
    //            new List<string> { TeamData.ValidName, BoardData.ValidName, TaskData.ValidTitle, MemberData.ValidName },
    //            testRepo
    //        );

    //    assign.Execute();

    //    var sut = new ListBugsCommand(
    //        new List<string> { "Active", MemberData.ValidName },
    //        testRepo
    //    );

    //    Assert.AreEqual(testBug.ToString(), sut.Execute());
    //}
}
