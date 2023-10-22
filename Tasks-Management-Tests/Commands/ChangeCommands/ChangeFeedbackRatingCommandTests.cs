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

namespace TasksManagement_Tests.Commands.ChangeCommands
{
    [TestClass]
    public class ChangeFeedbackRatingCommandTests
    {
        [TestMethod]
        public void Constructor_InitializesCommand()
        {
            var testRepo = new Repository();
            var testParams = new List<string>();

            var sut = new ChangeFeedbackRatingCommand(testParams, testRepo);

            Assert.IsInstanceOfType(sut, typeof(ChangeFeedbackRatingCommand));
        }

        [TestMethod]
        public void Execute_ChangesFeedbackRating()
        {
            // arrange
            var testRepo = new Repository();

            testRepo.CreateTeam(TeamData.ValidName);
            testRepo.CreateBoard(BoardData.ValidName, TeamData.ValidName);
            testRepo.CreateFeedback(
                TaskData.ValidTitle, TaskData.ValidDescription, TeamData.ValidName, BoardData.ValidName
                );

            var testParams = new List<string> {
            TeamData.ValidName,
            BoardData.ValidName,
            TaskData.ValidTitle,
            "Excellent"
        };

            var expectedOutput = $"Feedback '{TaskData.ValidTitle}' rating changed to '{Rating.Excellent}'.";

            //act
            var sut = new ChangeFeedbackRatingCommand(testParams, testRepo);

            Assert.AreEqual(expectedOutput, sut.Execute());
        }

        [TestMethod]
        public void Execute_Throws_When_RatingIsTheSame()
        {
            // arrange
            var testRepo = new Repository();

            testRepo.CreateTeam(TeamData.ValidName);
            testRepo.CreateBoard(BoardData.ValidName, TeamData.ValidName);
            testRepo.CreateFeedback(
                TaskData.ValidTitle, TaskData.ValidDescription, TeamData.ValidName, BoardData.ValidName
                );

            var testParams = new List<string> {
            TeamData.ValidName,
            BoardData.ValidName,
            TaskData.ValidTitle,
            "0"
        };

            //act
            var sut = new ChangeFeedbackRatingCommand(testParams, testRepo);

            Assert.ThrowsException<ArgumentException>(() => sut.Execute());
        }

        [TestMethod]
        public void Execute_Throws_When_RatingCannotBeParsed()
        {
            // arrange
            var testRepo = new Repository();

            testRepo.CreateTeam(TeamData.ValidName);
            testRepo.CreateBoard(BoardData.ValidName, TeamData.ValidName);
            testRepo.CreateFeedback(
                TaskData.ValidTitle, TaskData.ValidDescription, TeamData.ValidName, BoardData.ValidName
                );

            var testParams = new List<string> {
            TeamData.ValidName,
            BoardData.ValidName,
            TaskData.ValidTitle,
            "wrongRating"
        };

            //act
            var sut = new ChangeFeedbackRatingCommand(testParams, testRepo);

            Assert.ThrowsException<InvalidUserInputException>(() => sut.Execute());
        }

        [TestMethod]
        public void Execute_Throws_When_ParametersTooFew()
        {
            var testRepo = new Repository();
            var testParams = new List<string> { "testparam", "testparam2", "testparam3" };

            var sut = new ChangeFeedbackRatingCommand(testParams, testRepo);

            Assert.ThrowsException<InvalidUserInputException>(() => sut.Execute());
        }

        [TestMethod]
        public void Execute_Throws_When_ParametersTooMany()
        {
            var testRepo = new Repository();
            var testParams = new List<string> { "testparam", "testparam2", "testparam3", "testparam4", "testparam5" };

            var sut = new ChangeFeedbackRatingCommand(testParams, testRepo);

            Assert.ThrowsException<InvalidUserInputException>(() => sut.Execute());
        }
    }
}
