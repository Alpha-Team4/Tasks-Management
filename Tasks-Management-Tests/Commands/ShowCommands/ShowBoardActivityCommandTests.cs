using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TasksManagement_Tests.Helpers.TestData;
using static TasksManagement_Tests.Helpers.TestHelpers;
using TasksManagement.Commands.AssignCommands;
using TasksManagement.Commands.ShowCommands;
using TasksManagement.Core;
using TasksManagement.Exceptions;

namespace TasksManagement_Tests.Commands.ShowCommands
{
    [TestClass]
    public class ShowBoardActivityCommandTests
    {
        [TestMethod]
        public void Constructor_InitializesCommandInstance()
        {
            var testRepo = new Repository();

            var sut = new ShowBoardActivityCommand(
                    new List<string> { "filter", "assignee" },
                    testRepo
                );

            Assert.IsInstanceOfType(sut, typeof(ShowBoardActivityCommand));
        }


        [TestMethod]
        public void Execute_ThrowsOn_TooFewParameters()
        {
            var testRepo = new Repository();

            var sut = new ShowBoardActivityCommand(
                    new List<string> { },
                    testRepo
                );

            Assert.ThrowsException<InvalidUserInputException>(() => sut.Execute());
        }

        [TestMethod]
        public void Execute_ThrowsOn_TooManyParameters()
        {
            var testRepo = new Repository();

            var sut = new ShowBoardActivityCommand(
                    new List<string> { "param1", "param2", "param3"},
                    testRepo
                );

            Assert.ThrowsException<InvalidUserInputException>(() => sut.Execute());
        }

        [TestMethod]
        public void Execute_Returns_BoardActivity()
        {
            var testRepo = new Repository();
            testRepo.CreateTeam(TeamData.ValidName);
            var board = testRepo.CreateBoard(BoardData.ValidName, TeamData.ValidName);
            testRepo.CreateBug(
                    TaskData.ValidTitle, TaskData.ValidDescription, TeamData.ValidName, BoardData.ValidName);

            var command = new ShowBoardActivityCommand(
                    new List<string> {TeamData.ValidName, BoardData.ValidName },
                    testRepo
                );

            var expectedOutput = board.History.FirstOrDefault();

            Assert.AreEqual(expectedOutput.ToString(), command.Execute());
        }

        [TestMethod]
        public void Execute_ThrowsOn_NoBoardActivity()
        {
            var testRepo = new Repository();
            testRepo.CreateTeam(TeamData.ValidName);
            var board = testRepo.CreateBoard(BoardData.ValidName, TeamData.ValidName);


            var sut = new ShowBoardActivityCommand(
                    new List<string> { TeamData.ValidName, BoardData.ValidName },
                    testRepo
                );

            Assert.ThrowsException<EntityNotFoundException>(() => sut.Execute());
        }
    }
}
