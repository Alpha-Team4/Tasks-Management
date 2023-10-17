using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TasksManagement_Tests.Helpers;
using static TasksManagement_Tests.Helpers.TestData;
using static TasksManagement_Tests.Helpers.TestData.BoardData;
using static TasksManagement_Tests.Helpers.TestHelpers;
using TasksManagement.Models;

namespace TasksManagement.Models.Tests;

[TestClass]
public class BoardTests
{
    [TestMethod]
    public void Constructor_AssignsCorrectName()
    {
        var board = InitializeTestBoard();

        Assert.AreEqual(ValidName, board.Name);
    }

    [TestMethod]
    public void AddTask_AddsTaskToBoardTasksList()
    {
        var board = InitializeTestBoard();
        var task = InitializeTestBug();

        board.AddTask(task);

        Assert.AreEqual(task, board.Tasks.FirstOrDefault());
    }

    [TestMethod]
    public void AddEvent_AddsEventToBoardEventList()
    {
        var board = InitializeTestBoard();
        board.AddTask(InitializeTestBug());

        Assert.IsTrue(board.History.Any());
    }

    [TestMethod]
    public void PrintAllTasks_ReturnsContainingTasks()
    {
        var board = InitializeTestBoard();
        var task = InitializeTestBug();

        board.AddTask(task);

        Assert.AreEqual(task.ToString(), board.PrintAllTasks());
    }

    [TestMethod]
    public void PrintAllTasks_ReturnsNoTasksHeader()
    {
        var board = InitializeTestBoard();

        Assert.AreEqual("--NO TASKS--", board.PrintAllTasks());
    }

    [TestMethod]
    public void ToString_ReturnsCorrectOutput()
    {
        var board = InitializeTestBoard();

        var expectedOutput = $"""
                              Name: {board.Name}
                                 Tasks:
                                   --NO TASKS--
                              """;

        Assert.AreEqual(expectedOutput, board.ToString());
    }
}