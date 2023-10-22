using Microsoft.VisualStudio.TestTools.UnitTesting;
using static TasksManagement_Tests.Helpers.TestData;
using static TasksManagement_Tests.Helpers.TestHelpers;
using TasksManagement.Commands.CreateCommands;
using TasksManagement.Core;
using TasksManagement.Exceptions;

namespace TasksManagement_Tests.Commands.CreateCommands
{
    [TestClass]
    public class CreateStoryCommandTests
    {
        [TestMethod]
        public void Constructor_InitializesCommandInstance()
        {
            var repository = new Repository();

            var command = new CreateStoryCommand(
                new List<string> { "title", "description", "team", "board" },
                repository);

            Assert.IsInstanceOfType(command, typeof(CreateStoryCommand));
        }

        [TestMethod]
        public void Execute_Throws_WhenTooManyParameters()
        {
            var repository = new Repository();

            var command = new CreateStoryCommand(
                new List<string> { "title", "description", "team", "board", "other" },
                repository);

            Assert.ThrowsException<InvalidUserInputException>(
                () => command.Execute());
        }

        [TestMethod]
        public void Execute_ThrowsOn_TooFewParameters()
        {
            var repository = new Repository();

            var command = new CreateStoryCommand(
                new List<string> { "title", "description" },
                repository);

            Assert.ThrowsException<InvalidUserInputException>(
                () => command.Execute());
        }

        [TestMethod]
        public void Execute_WithValidParameters_CreatesFeedback()
        {
            ResetTaskLastIssuedIdState();
            var repository = new Repository();
            var command = new CreateStoryCommand
                (new List<string> { TaskData.ValidTitle, TaskData.ValidDescription, TeamData.ValidName, BoardData.ValidName }, repository);
            repository.CreateTeam(TeamData.ValidName);
            repository.CreateBoard(BoardData.ValidName, TeamData.ValidName);


            var result = command.Execute();
            var expected = "Story with ID '1' was created.";
            var createdStory = repository.CreateStory
                (TaskData.ValidTitle, TaskData.ValidDescription, TeamData.ValidName, BoardData.ValidName);

            Assert.AreEqual(expected, result);
            Assert.IsNotNull(createdStory);
        }
    }
}
