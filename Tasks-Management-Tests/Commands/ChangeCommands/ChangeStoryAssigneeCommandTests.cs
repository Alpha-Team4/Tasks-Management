using Microsoft.VisualStudio.TestTools.UnitTesting;
using TasksManagement.Core;
using TasksManagement.Exceptions;
using static TasksManagement_Tests.Helpers.TestData;
using TasksManagement.Commands.ChangeCommands;

namespace TasksManagement_Tests.Commands.ChangeCommands;

[TestClass]
public class ChangeStoryAssigneeCommandTests
{
    [TestMethod]
    public void Constructor_InitializesCommand()
    {
        var repository = new Repository();
        var testParams = new List<string>
        {
            TeamData.ValidName,
            BoardData.ValidName,
            TaskData.ValidTitle,
            MemberData.ValidName
        };

        var command = new ChangeStoryAssigneeCommand(testParams, repository);

        Assert.IsInstanceOfType(command, typeof(ChangeStoryAssigneeCommand));
    }

    [TestMethod]
    public void Execute_ThrowsOn_InvalidNumberOfParameters()
    {
        var repository = new Repository();
        var testParams = new List<string>
        {
            TeamData.ValidName,
            BoardData.ValidName,
            TaskData.ValidTitle
        };

        var testParams2 = new List<string>
        {
            TeamData.ValidName,
            BoardData.ValidName,
            TaskData.ValidTitle,
            MemberData.ValidName,
            MemberData.ValidName
        };

        var command = new ChangeStoryAssigneeCommand(testParams, repository);
        var command2 = new ChangeStoryAssigneeCommand(testParams2, repository);

        Assert.ThrowsException<InvalidUserInputException>(() => command.Execute());
        Assert.ThrowsException<InvalidUserInputException>(() => command2.Execute());
    }

    [TestMethod]
    public void Execute_ValidArguments_ChangesStoryAssignee()
    {
        var repository = new Repository();

        repository.CreateTeam(TeamData.ValidName);
        repository.CreateBoard(BoardData.ValidName, TeamData.ValidName);
        repository.CreateStory
            (TaskData.ValidTitle, TaskData.ValidDescription, TeamData.ValidName, BoardData.ValidName);
        repository.CreateMember(MemberData.ValidName, TeamData.ValidName);

        var testParams = new List<string>
        {
            TeamData.ValidName,
            BoardData.ValidName,
            TaskData.ValidTitle,
            MemberData.ValidName
        };
        var command = new ChangeStoryAssigneeCommand(testParams, repository);

        var expectedOutput = $"Story '{TaskData.ValidTitle}' assigned to {MemberData.ValidName}.";
        var result = command.Execute();

        Assert.AreEqual(expectedOutput, result);
    }

    [TestMethod]
    public void Execute_Throw_WhenStoryHasSameAssignee()
    {
        var repository = new Repository();

        repository.CreateTeam(TeamData.ValidName);
        repository.CreateBoard(BoardData.ValidName, TeamData.ValidName);
        var story = repository.CreateStory
            (TaskData.ValidTitle, TaskData.ValidDescription, TeamData.ValidName, BoardData.ValidName);
        
        story.Assignee = repository.CreateMember(MemberData.ValidName, TeamData.ValidName);

        var testParams = new List<string>
        {
            TeamData.ValidName,
            BoardData.ValidName,
            TaskData.ValidTitle,
            MemberData.ValidName
        };

        var command = new ChangeStoryAssigneeCommand(testParams, repository);
        
        Assert.ThrowsException<ArgumentException>(() => command.Execute());
    }
}