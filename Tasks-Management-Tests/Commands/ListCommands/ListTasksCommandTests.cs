using Microsoft.VisualStudio.TestTools.UnitTesting;
using TasksManagement.Commands.ListCommands;
using TasksManagement.Commands.ListCommands;
using TasksManagement.Core;
using TasksManagement.Exceptions;
using TasksManagement.Models.Contracts;
using TasksManagement.Models.Enums;
using static TasksManagement_Tests.Helpers.TestData;

namespace TasksManagement_Tests.Commands.ListCommands;

[TestClass]
public class ListTasksCommandTests
{
    [TestMethod]
    public void Constructor_InitializesCommandInstance()
    {
        var repository = new Repository();

        var command = new ListTasksCommand(
            new List<string> { TaskData.ValidTitle },
            repository);

        var command2 = new ListTasksCommand(
            new List<string> { },
            repository);

        Assert.IsInstanceOfType(command, typeof(ListTasksCommand));
        Assert.IsInstanceOfType(command2, typeof(ListTasksCommand));
    }

    [TestMethod]
    public void Execute_Throws_WhenTooManyParameters()
    {
        var repository = new Repository();

        var command = new ListTasksCommand(
            new List<string> { TaskData.ValidTitle, TaskData.ValidTitle, TaskData.ValidTitle },
            repository);

        Assert.ThrowsException<InvalidUserInputException>(() => command.Execute());
    }

    [TestMethod]
    public void Execute_ReturnsListAllTasksWhenNoParameters()
    {
        var repository = new Repository();
        var command = new ListTasksCommand(new List<string> { }, repository);

        repository.CreateTeam(TeamData.ValidName);
        repository.CreateBoard(BoardData.ValidName, TeamData.ValidName);
        repository.CreateBug(TaskData.ValidTitle, TaskData.ValidDescription, TeamData.ValidName, BoardData.ValidName);
        repository.CreateStory(TaskData.ValidTitle, TaskData.ValidDescription, TeamData.ValidName, BoardData.ValidName);
        repository.CreateFeedback(TaskData.ValidTitle, TaskData.ValidDescription, TeamData.ValidName, BoardData.ValidName);

        var tasks = repository.FindAllTasks();
        var expected = command.PrintTaskList(tasks);

        Assert.AreEqual(expected, command.Execute());
    }

    [TestMethod]
    public void Execute_OneArgument_ReturnsFilteredTasks()
    {
        var repository = new Repository();

        var command = new ListTasksCommand(
            new List<string> { TaskData.ValidTitle }, repository);

        repository.CreateTeam(TeamData.ValidName);
        repository.CreateBoard(BoardData.ValidName, TeamData.ValidName);
        repository.CreateBug(TaskData.ValidTitle, TaskData.ValidDescription, TeamData.ValidName, BoardData.ValidName);
        repository.CreateStory(TaskData.ValidTitle, TaskData.ValidDescription, TeamData.ValidName, BoardData.ValidName);
        repository.CreateFeedback(TaskData.ValidTitle, TaskData.ValidDescription, TeamData.ValidName, BoardData.ValidName);

        var tasks = repository.FindAllTasks();
        var filteredTasks = tasks
            .Where(task => task.Title.Contains(TaskData.ValidTitle, StringComparison.OrdinalIgnoreCase))
            .ToList();
        var expected = command.PrintTaskList(filteredTasks);

        Assert.AreEqual(expected, command.Execute());
    }
}
