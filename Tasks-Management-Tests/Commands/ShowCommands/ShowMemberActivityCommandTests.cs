using Microsoft.VisualStudio.TestTools.UnitTesting;
using TasksManagement.Commands.ShowCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksManagement.Commands.ListCommands;
using TasksManagement.Core;
using TasksManagement.Exceptions;
using static TasksManagement_Tests.Helpers.TestData;
using static TasksManagement_Tests.Helpers.TestHelpers;
using TasksManagement.Commands.AssignCommands;

namespace TasksManagement.Commands.ShowCommands.Tests;

[TestClass]
public class ShowMemberActivityCommandTests
{
    [TestMethod]
    public void Constructor_InitializesCommandInstance()
    {
        var testRepo = new Repository();

        var sut = new ShowMemberActivityCommand(
                new List<string> { "filter", "assignee" },
                testRepo
            );

        Assert.IsInstanceOfType(sut, typeof(ShowMemberActivityCommand));
    }


    [TestMethod]
    public void Execute_ThrowsOn_InvalidNumberOfParameters()
    {
        var testRepo = new Repository();

        var sut = new ShowMemberActivityCommand(
                new List<string> { },
                testRepo
            );

        Assert.ThrowsException<InvalidUserInputException>(() => sut.Execute());
    }

    [TestMethod]
    public void Execute_Returns_MemberActivity()
    {
        var testRepo = new Repository();
        testRepo.CreateTeam(TeamData.ValidName);
        testRepo.CreateBoard(BoardData.ValidName, TeamData.ValidName);
        testRepo.CreateMember(
                MemberData.ValidName, TeamData.ValidName);
        testRepo.CreateBug(
                TaskData.ValidTitle, TaskData.ValidDescription, TeamData.ValidName, BoardData.ValidName);
        var assignCommand = new AssignBugCommand(
                new List<string> { TeamData.ValidName, BoardData.ValidName, TaskData.ValidTitle, MemberData.ValidName},
                testRepo
            );
        assignCommand.Execute();

        var command = new ShowMemberActivityCommand(
                new List<string> { MemberData.ValidName },
                testRepo
            );

        var testMember = InitializeTestMember();
        testMember.AddTask(InitializeTestBug());

        var expectedOutput = testMember.History.FirstOrDefault();

        Assert.AreEqual(expectedOutput.ToString(), command.Execute());
    }

    [TestMethod]
    public void Execute_ThrowsOn_NoMemberActivity()
    {
        var testRepo = new Repository();
        testRepo.CreateTeam(TeamData.ValidName);
        testRepo.CreateBoard(BoardData.ValidName, TeamData.ValidName);
        testRepo.CreateMember(
                MemberData.ValidName, TeamData.ValidName);

        var sut = new ShowMemberActivityCommand(
                new List<string> { MemberData.ValidName },
                testRepo
            );

        Assert.ThrowsException<ArgumentException>(() => sut.Execute());
    }
}