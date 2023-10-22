using Microsoft.VisualStudio.TestTools.UnitTesting;
using TasksManagement.Commands.ListCommands;
using TasksManagement.Core;
using TasksManagement.Exceptions;
using TasksManagement.Models.Contracts;
using TasksManagement.Models.Enums;
using static TasksManagement_Tests.Helpers.TestData;

namespace TasksManagement_Tests.Commands.ListCommands;

[TestClass]
public class ListFeedbackCommandTests
{
    [TestMethod]
    public void Constructor_InitializesCommandInstance()
    {
        var repository = new Repository();

        var command = new ListFeedbackCommand(
            new List<string> { "status" },
            repository);

        var command2 = new ListFeedbackCommand(
            new List<string> { },
            repository);

        Assert.IsInstanceOfType(command, typeof(ListFeedbackCommand));
        Assert.IsInstanceOfType(command2, typeof(ListFeedbackCommand));
    }

    [TestMethod]
    public void Execute_Throws_WhenTooManyParameters()
    {
        var repository = new Repository();
        repository.CreateTeam(TeamData.ValidName);
        repository.CreateBoard(BoardData.ValidName, TeamData.ValidName);
        repository.CreateFeedback(
            TaskData.ValidTitle, TaskData.ValidDescription, TeamData.ValidName, BoardData.ValidName);

        var command = new ListFeedbackCommand(
            new List<string> { "New", "New", "Unscheduled"},
            repository
        );

        Assert.ThrowsException<InvalidUserInputException>(
            () => command.Execute());
    }

    [TestMethod]
    public void Execute_ReturnsListOfAllFeedbacksWhenNoParams()
    {
        var repository = new Repository();
        repository.CreateTeam(TeamData.ValidName);
        repository.CreateBoard(BoardData.ValidName, TeamData.ValidName);
        repository.CreateFeedback(
            TaskData.ValidTitle, TaskData.ValidDescription, TeamData.ValidName, BoardData.ValidName);
        var feedbackList = repository.FindAllTasks().OfType<IFeedback>().ToList();

        var command = new ListFeedbackCommand(
            new List<string> { },
            repository);
        var result = command.Execute();

        var filteredFeedback = feedbackList
            .OrderBy(f => f.Title)
            .ThenBy(f => f.Rating);
        var expectedOutput = string.Join(Environment.NewLine, filteredFeedback);

        Assert.AreEqual(expectedOutput, result);
    }

    [TestMethod]
    public void Execute_OneArgument_ReturnsFilteredFeedback()
    {
        var repository = new Repository();
        repository.CreateTeam(TeamData.ValidName);
        repository.CreateBoard(BoardData.ValidName, TeamData.ValidName);
        repository.CreateFeedback(
            TaskData.ValidTitle, TaskData.ValidDescription, TeamData.ValidName, BoardData.ValidName);
        var feedbackList = repository.FindAllTasks().OfType<IFeedback>().ToList();

        var command = new ListFeedbackCommand(
            new List<string> { "New" },
            repository);

        var result = command.Execute();

        var filteredFeedback = feedbackList
            .Where(f => f.Status == StatusFeedback.New)
            .OrderBy(f => f.Title)
            .ThenBy(f => f.Rating);
        var expectedOutput = string.Join(Environment.NewLine, filteredFeedback);

        Assert.AreEqual(expectedOutput, result);
    }

    [TestMethod]
    public void Execute_ThrowsOn_NoFeedbackFound()
    {
        var repository = new Repository();
        repository.CreateTeam(TeamData.ValidName);
        repository.CreateBoard(BoardData.ValidName, TeamData.ValidName);

        var command = new ListFeedbackCommand(
            new List<string> { "New" },
            repository);

        Assert.ThrowsException<EntityNotFoundException>(() => command.Execute());
    }

    [TestMethod]
    public void Execute_OneArgument_Throw_WhenNoFeedbackFound()
    {
        var repository = new Repository();
        repository.CreateTeam(TeamData.ValidName);
        repository.CreateBoard(BoardData.ValidName, TeamData.ValidName);
        repository.CreateFeedback(
            TaskData.ValidTitle, TaskData.ValidDescription, TeamData.ValidName, BoardData.ValidName);

        var command = new ListFeedbackCommand(
            new List<string> { "Unscheduled" },
            repository);

        Assert.ThrowsException<EntityNotFoundException>(() => command.Execute());
    }
}
