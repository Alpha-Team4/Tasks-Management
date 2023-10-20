using System;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TasksManagement.Models.Contracts;
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
    public void RemoveTask_RemovesTaskFromMemberTasksList()
    {
        var member = InitializeTestMember();
        var task = InitializeTestBug();

        member.AddTask(task);
        member.RemoveTask(task);

        Assert.IsTrue(!member.Tasks.Any());
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
    public void ToString_PrintsMemberInfo()
    {
        var member = InitializeTestMember();

        var expectedOutput = $"""
                              Name: {member.Name}
                                Tasks:
                                  --NO TASKS--
                              """;

        Assert.AreEqual(expectedOutput, member.ToString());
    }

    [TestMethod]
    public void ToString_PrintsMemberInfo_And_Tasks()
    {
        var member = InitializeTestMember();
        var task = InitializeTestBug();

        member.AddTask(task);

        var expectedOutput = $"""
                              Name: {member.Name}
                                Tasks:
                                  {member.PrintAllTasks()}
                              """;

        Assert.AreEqual(expectedOutput, member.ToString());
    }

}