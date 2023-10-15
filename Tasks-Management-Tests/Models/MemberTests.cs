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
        var member = new Member(ValidName);

        Assert.AreEqual(ValidName, member.Name);
    }

    [TestMethod]
    public void AddTask_AddsTaskToMemberTasksList()
    {
        var member = new Member(ValidName);
        var task = InitializeTestBug();

        member.AddTask(task);

        Assert.AreEqual(task, member.Tasks.FirstOrDefault());
    }

    [TestMethod]
    public void AddEvent_AddsEventToMemberEventList()
    {
        var member = new Member(ValidName);
        var testEvent = InitializeTestEvent();

        member.AddEvent(testEvent);

        Assert.AreEqual(testEvent, member.History.FirstOrDefault());
    }
}