using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TasksManagement_Tests.Helpers.TestData;
using System.Threading.Tasks;
using static TasksManagement_Tests.Helpers.TestHelpers;
using TasksManagement.Commands.Enums;
using TasksManagement.Exceptions;
using TasksManagement.Models.Enums;
using TasksManagement.Models.Contracts;

namespace TasksManagement_Tests.Models
{
    [TestClass]
    public class FeedbackTests
    {
        [TestMethod]
        public void Constructor_Should_AssignCorrectTitle()
        {
            var feedback = InitializeTestFeedback();

            Assert.AreEqual(feedback.Title, TaskData.ValidTitle);
        }

        [TestMethod]
        public void Constructor_Should_AssignCorrectDescription()
        {
            var feedback = InitializeTestFeedback();

            Assert.AreEqual(feedback.Description, TaskData.ValidDescription);
        }

        [TestMethod]
        public void Constructor_Should_ThrowWhen_TitleTooShort()
        {
            Assert.ThrowsException<InvalidUserInputException>(
                () => InitializeTestFeedback(
                    TaskData.TooShortTitle,
                    TaskData.ValidDescription
                ));
        }
        [TestMethod]
        public void Constructor_Should_ThrowWhen_TitleTooLong()
        {
            Assert.ThrowsException<InvalidUserInputException>(
            () => InitializeTestFeedback(
            TaskData.TooLongTitle,
            TaskData.ValidDescription
            ));
        }

        [TestMethod]
        public void Constructor_Should_ThrowWhen_DescriptionTooShort()
        {
            Assert.ThrowsException<InvalidUserInputException>(
                () => InitializeTestFeedback(
                    TaskData.ValidTitle,
                    TaskData.TooShortDescription
                    )
                );

        }

        [TestMethod]
        public void Constructor_Should_ThrowWhen_DescriptionTooLong()
        {
            Assert.ThrowsException<InvalidUserInputException>(
                () => InitializeTestFeedback(
                    TaskData.ValidTitle,
                    TaskData.TooLongDescription
                    )
                );
        }
        [TestMethod]

        public void Constructor_AssignsUniqueIdsToFeedback()
        {
            ResetTaskLastIssuedIdState();

            var feedback1 = InitializeTestFeedback(TaskData.ValidTitle, TaskData.ValidDescription);

            Assert.AreEqual(feedback1.Id, 1);
        }


        [TestMethod]
        public void Title_Setter_ThrowsWhen_SameValue()
        {
            var feedback = InitializeTestFeedback(TaskData.ValidTitle, TaskData.ValidDescription);

            Assert.ThrowsException<InvalidUserInputException>(() => feedback.Title = TaskData.ValidTitle);
        }


        [TestMethod]
        public void Description_Setter_ThrowsWhen_SameValue()
        {
            var feedback = InitializeTestFeedback(TaskData.ValidTitle, TaskData.ValidDescription);

            Assert.ThrowsException<InvalidUserInputException>(() => feedback.Description = TaskData.ValidDescription);
        }

        [TestMethod]
        public void Status_Setter_AssignsCorrectValue()
        {
            var feedback = InitializeTestFeedback(TaskData.ValidTitle, TaskData.ValidDescription);

            feedback.Status = StatusFeedback.Done;

            Assert.AreEqual(feedback.Status, StatusFeedback.Done);
        }

        public void Rating_Setter_AssignsCorrectValue()
        {
            var feedback = InitializeTestFeedback(TaskData.ValidTitle, TaskData.ValidDescription);

            feedback.Rating = Rating.Excellent;

            Assert.AreEqual(feedback.Rating, Rating.Excellent);
        }


        [TestMethod]
        public void GetCurrentStatus_Returns_New()
        {
            var feedback = InitializeTestFeedback(TaskData.ValidTitle, TaskData.ValidDescription);

            Assert.AreEqual(StatusFeedback.New.ToString(), feedback.GetCurrentStatus());
        }
    }
}
