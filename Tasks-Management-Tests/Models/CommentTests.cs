using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TasksManagement_Tests.Helpers.TestData;
using static TasksManagement_Tests.Helpers.TestHelpers;
using TasksManagement.Commands.Enums;
using TasksManagement.Exceptions;
using TasksManagement.Models.Enums;
using TasksManagement_Tests.Helpers;

namespace TasksManagement_Tests.Models
{
    [TestClass]
    public class CommentTests
    {
        [TestMethod]
        public void Constructor_Should_AssignsCorrectAuthor()
        {
            var comment = InitializeTestComment();
            Assert.AreEqual(comment.Author, CommentData.ValidAuthor);
        }
        [TestMethod]
        public void Constructor_Should_AssignsCorrectContent()
        {
            var comment = InitializeTestComment();
            Assert.AreEqual(comment.Content, CommentData.ValidContent);
        }

        [TestMethod]    
        public void ConstructorShouldThrow_When_ContentTooShort()
        {
            string tooShortContent = "a";

            Assert.ThrowsException<InvalidUserInputException>
                (() => InitializeTestComment(tooShortContent, CommentData.ValidAuthor));   
        }

        [TestMethod]
        public void ConstructorShouldThrow_When_ContentTooLong()
        {
            string tooLongContent = new string('a', CommentData.ContentMaxLength + 1);

            Assert.ThrowsException<InvalidUserInputException>
                (() => InitializeTestComment(tooLongContent, CommentData.ValidAuthor));
        }

    }
}
