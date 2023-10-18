using Microsoft.VisualStudio.TestTools.UnitTesting;
using TasksManagement.Core;
using TasksManagement.Exceptions;

namespace TasksManagement.Commands.CreateCommands.Tests;

[TestClass]
public class CreateTeamCommandTests
{
    [TestMethod]
    public void Constructor_InitializesCommand()
    {
        var testRepo = new Repository();
        var testParams = new List<string> { "testparam" };

        var sut = new CreateTeamCommand(testParams, testRepo);

        Assert.IsInstanceOfType(sut, typeof(CreateTeamCommand));
    }

    [TestMethod]
    public void Execute_CreatesTeam_WithValidParameters()
    {
        var testTeamName = "teamName";
        var testRepo = new Repository();
        var testParams = new List<string> { testTeamName };
        var expectedOutput = $"Team with name '{testTeamName}' was created.";

        var sut = new CreateTeamCommand(testParams, testRepo);

        Assert.AreEqual(expectedOutput, sut.Execute());
    }

    [TestMethod]
    public void Execute_Throws_When_ParameterCountIncorrect()
    {
        var testRepo = new Repository();
        var testParams = new List<string> { "testparam", "testparam2", "testparam3" };

        var sut = new CreateTeamCommand(testParams, testRepo);

        Assert.ThrowsException<InvalidUserInputException>(() => sut.Execute());
    }
}