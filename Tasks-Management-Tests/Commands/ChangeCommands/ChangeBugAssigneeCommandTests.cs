using Microsoft.VisualStudio.TestTools.UnitTesting;
using TasksManagement.Core;
using TasksManagement.Exceptions;
using static TasksManagement_Tests.Helpers.TestData;
using TasksManagement.Commands.ChangeCommands;

namespace TasksManagement_Tests.Commands.ChangeCommands;

[TestClass]
public class ChangeBugAssigneeCommandTests
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

        var command = new ChangeBugAssigneeCommand(testParams, repository);

        Assert.IsInstanceOfType(command, typeof(ChangeBugAssigneeCommand));
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

        var command = new ChangeBugAssigneeCommand(testParams, repository);
        var command2 = new ChangeBugAssigneeCommand(testParams2, repository);

        Assert.ThrowsException<InvalidUserInputException>(() => command.Execute());
        Assert.ThrowsException<InvalidUserInputException>(() => command2.Execute());
    }

    [TestMethod]
    public void Execute_ValidArguments_ChangesBugAssignee()
    {
        var repository = new Repository();

        repository.CreateTeam(TeamData.ValidName);
        repository.CreateBoard(BoardData.ValidName, TeamData.ValidName);
        repository.CreateBug
            (TaskData.ValidTitle, TaskData.ValidDescription, TeamData.ValidName, BoardData.ValidName);
        repository.CreateMember(MemberData.ValidName, TeamData.ValidName);

        var testParams = new List<string>
        {
            TeamData.ValidName,
            BoardData.ValidName,
            TaskData.ValidTitle,
            MemberData.ValidName
        };
        var command = new ChangeBugAssigneeCommand(testParams, repository);

        var expectedOutput = $"Bug '{TaskData.ValidTitle}' assigned to {MemberData.ValidName}.";
        var result = command.Execute();

        Assert.AreEqual(expectedOutput, result);
    }

    [TestMethod]
    public void Execute_Throw_WhenBugHasSameAssignee()
    {
        var repository = new Repository();

        repository.CreateTeam(TeamData.ValidName);
        repository.CreateBoard(BoardData.ValidName, TeamData.ValidName);
        var bug = repository.CreateBug
            (TaskData.ValidTitle, TaskData.ValidDescription, TeamData.ValidName, BoardData.ValidName);

        bug.Assignee = repository.CreateMember(MemberData.ValidName, TeamData.ValidName);

        var testParams = new List<string>
        {
            TeamData.ValidName,
            BoardData.ValidName,
            TaskData.ValidTitle,
            MemberData.ValidName
        };

        var command = new ChangeBugAssigneeCommand(testParams, repository);

        Assert.ThrowsException<ArgumentException>(() => command.Execute());
    }
}
