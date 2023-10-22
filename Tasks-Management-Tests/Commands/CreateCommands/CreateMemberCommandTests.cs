using Microsoft.VisualStudio.TestTools.UnitTesting;
using TasksManagement.Commands.CreateCommands;
using TasksManagement.Core;
using TasksManagement.Exceptions;
using static TasksManagement_Tests.Helpers.TestData;
using static TasksManagement_Tests.Helpers.TestHelpers;

namespace TasksManagement_Tests.Commands.CreateCommands;

[TestClass]
public class CreateMemberCommandTests
{
    [TestMethod]
    public void Constructor_InitializesCommandInstance()
    {
        var repository = new Repository();

        var command = new CreateMemberCommand(
            new List<string> { "member" },
            repository);

        var command2 = new CreateMemberCommand(
            new List<string> { "member", "team" },
            repository);

        Assert.IsInstanceOfType(command, typeof(CreateMemberCommand));
        Assert.IsInstanceOfType(command2, typeof(CreateMemberCommand));
    }

    [TestMethod]
    public void Execute_Throws_WhenNoParameters()
    {
        var repository = new Repository();

        var command = new CreateMemberCommand(
            new List<string> { },
            repository
        );

        Assert.ThrowsException<InvalidUserInputException>(
            () => command.Execute());
    }

    [TestMethod]
    public void Execute_Throws_WhenTooManyParameters()
    {
        var repository = new Repository();

        var command = new CreateMemberCommand(
            new List<string> { "member", "member", "team" },
            repository
        );

        Assert.ThrowsException<InvalidUserInputException>(
            () => command.Execute());
    }

    [TestMethod]
    public void Execute_WithValidParameters_CreatesMember()
    {
        var repository = new Repository();
        var command = new CreateMemberCommand
            (new List<string> { "Katerina" }, repository);

        var result = command.Execute();

        var expected = "Member 'Katerina' was created.";
        Assert.AreEqual(expected, result);
        
        var createdMember = repository.FindMemberByName("Katerina");
        Assert.IsNotNull(createdMember);
    }

    [TestMethod]
    public void Execute_WithTeam_CreatesMemberInTeam()
    {
        var repository = new Repository();
        var command = new CreateMemberCommand
            (new List<string> { "Katerina", "Team4" }, repository);
        repository.CreateTeam("Team4");

        var expected = "Member 'Katerina' was created in team 'Team4'.";
        var result = command.Execute();

        Assert.AreEqual(expected, result);
       
        var createdMember = repository.FindMemberByName("Katerina"); 
        var createdTeam = repository.FindTeamByName("Team4");
        Assert.IsNotNull(createdMember);
        Assert.IsNotNull(createdTeam);
    }
}
