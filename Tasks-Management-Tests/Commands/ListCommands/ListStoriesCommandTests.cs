using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TasksManagement_Tests.Helpers.TestData;
using static TasksManagement_Tests.Helpers.TestHelpers;
using TasksManagement.Commands.AssignCommands;
using TasksManagement.Commands.ListCommands;
using TasksManagement.Core;
using TasksManagement.Exceptions;

namespace TasksManagement_Tests.Commands.ListCommands
{
    [TestClass]
    public class ListStoriesCommandTests
    {
        [TestMethod]
        public void Constructor_InitializesCommandInstance()
        {
            var testRepo = new Repository();

            var sut = new ListStoriesCommand(
                    new List<string> { "filter", "assignee" },
                    testRepo
                );

            Assert.IsInstanceOfType(sut, typeof(ListStoriesCommand));
        }


        [TestMethod]
        public void Execute_ThrowsOn_TooManyParameters()
        {
            var testRepo = new Repository();

            ResetTaskLastIssuedIdState();
            testRepo.CreateTeam(TeamData.ValidName);
            testRepo.CreateBoard(BoardData.ValidName, TeamData.ValidName);
            testRepo.CreateStory(
                TaskData.ValidTitle, TaskData.ValidDescription, TeamData.ValidName, BoardData.ValidName
                );

            var sut = new ListStoriesCommand(
                    new List<string> { "1", "2", "3" },
                    testRepo
                );

            Assert.ThrowsException<InvalidUserInputException>(
                    () => sut.Execute()
                ); ;
        }

        [TestMethod]
        public void Execute_ReturnsListOfAllStories()
        {
            ResetTaskLastIssuedIdState();
            var testRepo = new Repository();
            testRepo.CreateTeam(TeamData.ValidName);
            testRepo.CreateBoard(BoardData.ValidName, TeamData.ValidName);
            testRepo.CreateStory(
                TaskData.ValidTitle, TaskData.ValidDescription, TeamData.ValidName, BoardData.ValidName
                );

            ResetTaskLastIssuedIdState();
            var testStory = InitializeTestStory();

            var sut = new ListStoriesCommand(
                    new List<string> {  },
                    testRepo
                );

            Assert.AreEqual(testStory.ToString(), sut.Execute());
        }

        [TestMethod]
        public void Execute_ThrowsOn_NoStoriesFound()
        {
            var testRepo = new Repository();

            var sut = new ListStoriesCommand(
                    new List<string> {  },
                    testRepo
                );

            Assert.ThrowsException<EntityNotFoundException>(() => sut.Execute());
        }

        [TestMethod]
        public void Execute_Returns_ListFilteredBy_Status()
        {
            ResetTaskLastIssuedIdState();
            var testRepo = new Repository();
            testRepo.CreateTeam(TeamData.ValidName);
            testRepo.CreateBoard(BoardData.ValidName, TeamData.ValidName);
            testRepo.CreateStory(TaskData.ValidTitle, TaskData.ValidDescription, TeamData.ValidName, BoardData.ValidName);

            ResetTaskLastIssuedIdState();
            var testStory = InitializeTestStory();

            var sut = new ListStoriesCommand(
                    new List<string> { "NotDone" },
                    testRepo
                );

            Assert.AreEqual(testStory.ToString(), sut.Execute());
        }

        [TestMethod]
        public void Execute_ThrowsOn_InvalidStatusInput()
        {
            ResetTaskLastIssuedIdState();
            var testRepo = new Repository();
            testRepo.CreateTeam(TeamData.ValidName);
            testRepo.CreateBoard(BoardData.ValidName, TeamData.ValidName);
            testRepo.CreateStory(TaskData.ValidTitle, TaskData.ValidDescription, TeamData.ValidName, BoardData.ValidName);

            var sut = new ListStoriesCommand(
                    new List<string> { "InvalidStatus" },
                    testRepo
                );

            Assert.ThrowsException<InvalidUserInputException>(() => sut.Execute());
        }

        [TestMethod]
        public void Execute_ThrowsOn_NoFilteredStoriesFound()
        {
            var testRepo = new Repository();
            testRepo.CreateTeam(TeamData.ValidName);
            testRepo.CreateBoard(BoardData.ValidName, TeamData.ValidName);
            testRepo.CreateStory(TaskData.ValidTitle, TaskData.ValidDescription, TeamData.ValidName, BoardData.ValidName);

            var sut = new ListStoriesCommand(
                    new List<string> { "Done" },
                    testRepo
                );

            Assert.ThrowsException<EntityNotFoundException>(() => sut.Execute());
        }

        [TestMethod]
        public void Execute_Returns_ListFilteredBy_Assignee()
        {
            ResetTaskLastIssuedIdState();
            var testRepo = new Repository();
            testRepo.CreateTeam(TeamData.ValidName);
            testRepo.CreateBoard(BoardData.ValidName, TeamData.ValidName);
            
            var story = testRepo.CreateStory(TaskData.ValidTitle, TaskData.ValidDescription, TeamData.ValidName, BoardData.ValidName);
            var member = testRepo.CreateMember(MemberData.ValidName);

            var assign = new AssignStoryCommand(
            new List<string> { TeamData.ValidName, BoardData.ValidName, TaskData.ValidTitle, member.Name },
            testRepo
             );

            assign.Execute();

            var sut = new ListStoriesCommand(
                    new List<string> { MemberData.ValidName },
                    testRepo
                );

            Assert.AreEqual(story.ToString(), sut.Execute());
        }



        [TestMethod]
        public void Execute_Returns_ListFilteredBy_StatusAndAssignee()
        {
            ResetTaskLastIssuedIdState();
            var testRepo = new Repository();
            testRepo.CreateTeam(TeamData.ValidName);
            testRepo.CreateBoard(BoardData.ValidName, TeamData.ValidName);

            var testStory = testRepo.CreateStory
                (TaskData.ValidTitle, TaskData.ValidDescription, TeamData.ValidName, BoardData.ValidName);
            var member = testRepo.CreateMember(MemberData.ValidName);

            var assign = new AssignStoryCommand(
                    new List<string> { TeamData.ValidName, BoardData.ValidName, TaskData.ValidTitle, member.Name },
                    testRepo
                );

            assign.Execute();

            var sut = new ListStoriesCommand(
                new List<string> { "NotDone", MemberData.ValidName },
                testRepo
            );

            Assert.AreEqual(testStory.ToString(), sut.Execute());
        }

        [TestMethod]
        public void Execute_ThrowsOn_NoAssigneeFound()
        {
            ResetTaskLastIssuedIdState();
            var testRepo = new Repository();
            testRepo.CreateTeam(TeamData.ValidName);
            testRepo.CreateBoard(BoardData.ValidName, TeamData.ValidName);
            testRepo.CreateStory(TaskData.ValidTitle, TaskData.ValidDescription, TeamData.ValidName, BoardData.ValidName);
            testRepo.CreateMember(MemberData.ValidName);

            var sut = new ListStoriesCommand(
                new List<string> { "NotDone", MemberData.ValidName },
                testRepo
            );

            Assert.ThrowsException<EntityNotFoundException>(() => sut.Execute());
        }
    }
}
