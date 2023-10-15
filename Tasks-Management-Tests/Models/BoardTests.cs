using Microsoft.VisualStudio.TestTools.UnitTesting;
using TasksManagement_Tests.Helpers;
using static TasksManagement_Tests.Helpers.TestData;
using static TasksManagement_Tests.Helpers.TestData.BoardData;
using static TasksManagement_Tests.Helpers.TestHelpers;

namespace TasksManagement.Models.Tests;

[TestClass]
public class BoardTests
{
    [TestMethod]
    public void Constructor_AssignsCorrectName()
    {
        var board = new Board(ValidName);

        Assert.AreEqual(ValidName, board.Name);
    }

    [TestMethod]
    public void AddTask_AddsTaskToBoardTasksList()
    {
        var board = new Board(ValidName);
        var task = InitializeTestBug();

        board.AddTask(task);

        Assert.AreEqual(task, board.Tasks.FirstOrDefault());
    }

    [TestMethod]
    public void AddEvent_AddsEventToBoardEventList()
    {
        var board = new Board(ValidName);
        var testEvent = InitializeTestEvent();

        board.AddEvent(testEvent);

        Assert.AreEqual(testEvent, board.History.FirstOrDefault());
    }


}