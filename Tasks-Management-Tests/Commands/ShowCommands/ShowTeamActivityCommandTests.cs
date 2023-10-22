using Microsoft.VisualStudio.TestTools.UnitTesting;
using TasksManagement.Core;
using TasksManagement.Exceptions;
using static TasksManagement_Tests.Helpers.TestData;
using TasksManagement.Commands.ShowCommands;

namespace TasksManagement_Tests.Commands.ShowCommands;

[TestClass]
public class ShowTeamActivityCommandTests
{
    [TestMethod]
    public void Constructor_InitializesCommandInstance()
    {
        var repository = new Repository();

        var command = new ShowTeamActivityCommand(
            new List<string> { TeamData.ValidName },
            repository);

        Assert.IsInstanceOfType(command, typeof(ShowTeamActivityCommand));
    }

    [TestMethod]
    public void Execute_ThrowsOn_InvalidNumberOfParameters()
    {
        var repository = new Repository();

        var command = new ShowTeamActivityCommand(
            new List<string> { },
            repository);

        var command2 = new ShowTeamActivityCommand(
            new List<string> { TeamData.ValidName , TeamData.ValidName },
            repository);

        Assert.ThrowsException<InvalidUserInputException>(() => command.Execute());
        Assert.ThrowsException<InvalidUserInputException>(() => command2.Execute());
    }

    [TestMethod]
    public void Execute_Throw_ThenNoTeamActivity()
    {
        var repository = new Repository();
        var command = new ShowTeamActivityCommand(
            new List<string> { TeamData.ValidName },
            repository);

        var team = repository.CreateTeam(TeamData.ValidName); 

        Assert.ThrowsException<ArgumentException>(()=> command.Execute());
    }

    [TestMethod]
    public void Execute_ValidTeamName_WithHistory_ReturnsOrderedHistory()
    {
        var repository = new Repository();
        var command = new ShowTeamActivityCommand(new List<string> { TeamData.ValidName }, repository);
        
        var team = repository.CreateTeam(TeamData.ValidName);
        team.AddEvent("Test description");
        team.AddEvent("Test description");
       
        var result = command.Execute();
        var orderedHistory = team.History
            .OrderBy(e => e.Time);
        var expectedOutput = string.Join(Environment.NewLine, orderedHistory);

        Assert.IsNotNull(result);
        Assert.AreEqual(expectedOutput, result);
    }
}