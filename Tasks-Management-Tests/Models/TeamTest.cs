using Microsoft.VisualStudio.TestTools.UnitTesting;
using TasksManagement.Exceptions;
using static TasksManagement_Tests.Helpers.TestData;
using static TasksManagement_Tests.Helpers.TestHelpers;

namespace TasksManagement_Tests.Models;

[TestClass]
public class TeamTest
{
    [TestMethod]
    public void Constructor_AssignsCorrectName()
    {
        var team = InitializeTestTeam();

        Assert.AreEqual(team.Name, TeamData.ValidName);
    }

    [TestMethod]
    public void Constructor_ThrowsOn_InvalidNameLength()
    {
        Assert.ThrowsException<InvalidUserInputException>(
            () => InitializeTestTeam(TeamData.TooShortName));

        Assert.ThrowsException<InvalidUserInputException>(
            () => InitializeTestTeam(TeamData.TooLongName));
    }

    [TestMethod]
    public void Comments_Should_ReturnCopyOfTheCollection()
    {
        var team = InitializeTestTeam();

        var member = InitializeTestMember();
        team.Members.Add(member);

        Assert.AreEqual(0, team.Members.Count);
    }

    [TestMethod]
    public void History_Should_ReturnCopyOfTheCollection()
    {
        var team = InitializeTestTeam();

        var evt = InitializeTestEvent();
        team.History.Add(evt);

        Assert.AreEqual(0, team.History.Count);
    }

    [TestMethod]
    public void Boards_Should_ReturnCopyOfTheCollection()
    {
        var team = InitializeTestTeam();

        var board = InitializeTestBoard();
        team.Boards.Add(board);

        Assert.AreEqual(0, team.Boards.Count);
    }

    [TestMethod]
    public void AddEvent_Should_AddsEventToHistory()
    {
        var team = InitializeTestTeam();

        team.AddEvent("test description");

        Assert.AreEqual(1, team.History.Count);
    }

    [TestMethod]
    public void PrintAllMembers_ReturnsCorrectValues()
    {
        var team = InitializeTestTeam();
        var member = InitializeTestMember();
        team.AddMember(member);
        var expected = "Valid Name";

        Assert.AreEqual(expected, team.PrintAllMembers());
    }

    [TestMethod]
    public void PrintAllMembers_ReturnsCorrectValuesWhenNoMembersInTheCollection()
    {
        var team = InitializeTestTeam();

        var expected = "--NO MEMBERS--";

        Assert.AreEqual(expected, team.PrintAllMembers());
    }

    [TestMethod]
    public void ToString_ReturnsCorrectValues()
    {
        var team = InitializeTestTeam();

        var expected = """
                         Name: Valid Name
                           Members:
                             --NO MEMBERS--
                         """;

        Assert.AreEqual(expected, team.ToString());
    }
}
