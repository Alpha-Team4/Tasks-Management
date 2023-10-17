using System;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static TasksManagement_Tests.Helpers.TestData.MemberData;
using static TasksManagement_Tests.Helpers.TestHelpers;

namespace TasksManagement.Models.Tests;

[TestClass]
public class MemberTests
{
    [TestMethod]
    public void Constructor_AssignsCorrectTitle()
    {
        var member = InitializeTestMember();

        Assert.AreEqual(ValidName, member.Name);
    }

    [TestMethod]
    public void AddTask_AddsTaskToMemberTasksList()
    {
        var member = InitializeTestMember();
        var task = InitializeTestBug();

        member.AddTask(task);

        Assert.AreEqual(task, member.Tasks.FirstOrDefault());
    }

    [TestMethod]
    public void PrintAllTasks_PrintsSingleTask()
    {
        var member = InitializeTestMember();
        var task = InitializeTestBug();

        member.AddTask(task);

        Assert.AreEqual(task.ToString(), member.PrintAllTasks());
    }

    [TestMethod]
    public void PrintAllTasks_PrintsNoTasksMessage()
    {
        var member = InitializeTestMember();

        var expectedOutput = "--NO TASKS--";

        Assert.AreEqual(expectedOutput, member.PrintAllTasks());
    }

    [TestMethod]
    public void AddEvent_AddsEventToMemberEventList()
    {
        var member = InitializeTestMember();
        member.AddTask(InitializeTestBug());

        Assert.IsTrue(member.History.Any());
    }

    [TestMethod]
    public void ToString_PrintsCorrectOutput()
    {
        var member = InitializeTestMember();
        var task = InitializeTestBug();

        member.AddTask(task);

        var expectedOutput = "Name: Valid Name\n   Tasks:\n     Bug (ID: 1)\n   Title: This is a valid title\n   Description: This is a valid description.\n   Status: Active\n   Reproduction Steps: \n   Priority: Low\n   Severity: Minor\n   Assignee: \n     --NO COMMENTS--";

        var output = member.ToString();

        Assert.AreEqual(expectedOutput, member.ToString());
    }

}