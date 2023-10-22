using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TasksManagement_Tests.Helpers.TestData;
using TasksManagement.Commands.ChangeCommands;
using TasksManagement.Core;
using TasksManagement.Exceptions;
using TasksManagement.Models.Enums;
using TasksManagement.Commands.Enums;

namespace TasksManagement_Tests.Commands.ChangeCommands
{
    [TestClass]
    public class ChangeStoryPriorityCommandTests
    {
        [TestMethod]
        public void Constructor_InitializesCommand()
        {
            var testRepo = new Repository();
            var testParams = new List<string>();

            var sut = new ChangeStoryPriorityCommand(testParams, testRepo);

            Assert.IsInstanceOfType(sut, typeof(ChangeStoryPriorityCommand));
        }

        [TestMethod]
        public void Execute_ChangesStoryPriority()
        {
            // arrange
            var testRepo = new Repository();

            testRepo.CreateTeam(TeamData.ValidName);
            testRepo.CreateBoard(BoardData.ValidName, TeamData.ValidName);
            testRepo.CreateStory(
                TaskData.ValidTitle, TaskData.ValidDescription, TeamData.ValidName, BoardData.ValidName
                );

            var testParams = new List<string> {
            TeamData.ValidName,
            BoardData.ValidName,
            TaskData.ValidTitle,
            "High"
        };

            var expectedOutput = $"Story '{TaskData.ValidTitle}' priority changed to '{Priority.High}'.";

            //act
            var sut = new ChangeStoryPriorityCommand(testParams, testRepo);

            Assert.AreEqual(expectedOutput, sut.Execute());
        }

        [TestMethod]
        public void Execute_Throws_When_PriorityIsTheSame()
        {
            // arrange
            var testRepo = new Repository();

            testRepo.CreateTeam(TeamData.ValidName);
            testRepo.CreateBoard(BoardData.ValidName, TeamData.ValidName);
            testRepo.CreateStory(
                TaskData.ValidTitle, TaskData.ValidDescription, TeamData.ValidName, BoardData.ValidName
                );

            var testParams = new List<string> {
            TeamData.ValidName,
            BoardData.ValidName,
            TaskData.ValidTitle,
            "Low"
        };

            //act
            var sut = new ChangeStoryPriorityCommand(testParams, testRepo);

            Assert.ThrowsException<ArgumentException>(() => sut.Execute());
        }

        [TestMethod]
        public void Execute_Throws_When_PriorityCannotBeParsed()
        {
            // arrange
            var testRepo = new Repository();

            testRepo.CreateTeam(TeamData.ValidName);
            testRepo.CreateBoard(BoardData.ValidName, TeamData.ValidName);
            testRepo.CreateStory(
                TaskData.ValidTitle, TaskData.ValidDescription, TeamData.ValidName, BoardData.ValidName
                );

            var testParams = new List<string> {
            TeamData.ValidName,
            BoardData.ValidName,
            TaskData.ValidTitle,
            "wrongPriority"
        };

            //act
            var sut = new ChangeStoryPriorityCommand(testParams, testRepo);

            Assert.ThrowsException<InvalidUserInputException>(() => sut.Execute());
        }

        [TestMethod]
        public void Execute_Throws_When_ParametersTooFew()
        {
            var testRepo = new Repository();
            var testParams = new List<string> { "testparam", "testparam2", "testparam3" };

            var sut = new ChangeStoryPriorityCommand(testParams, testRepo);

            Assert.ThrowsException<InvalidUserInputException>(() => sut.Execute());
        }

        [TestMethod]
        public void Execute_Throws_When_ParametersTooMany()
        {
            var testRepo = new Repository();
            var testParams = new List<string> { "testparam", "testparam2", "testparam3", "testparam4", "testparam5" };

            var sut = new ChangeStoryPriorityCommand(testParams, testRepo);

            Assert.ThrowsException<InvalidUserInputException>(() => sut.Execute());
        }
    }
}
