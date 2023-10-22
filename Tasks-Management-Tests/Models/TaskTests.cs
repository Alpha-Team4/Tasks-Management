using Microsoft.VisualStudio.TestTools.UnitTesting;
using TasksManagement.Exceptions;
using static TasksManagement_Tests.Helpers.TestData;
using static TasksManagement_Tests.Helpers.TestHelpers;

namespace TasksManagement_Tests.Models;

[TestClass]
public class TaskTests
{
    [TestMethod]
    public void AddComment_ShouldAddCommentToTheCollection()
    {
        var story = InitializeTestStory(TaskData.ValidTitle, TaskData.ValidDescription);
        var comment = InitializeTestComment();
        
        story.AddComment(comment);

        Assert.AreEqual(1,story.Comments.Count);
    }

    [TestMethod]
    public void RemoveComment_Should_RemoveCommentFromTheCollection()
    {
        var story = InitializeTestStory(TaskData.ValidTitle, TaskData.ValidDescription);
        var comment = InitializeTestComment();
        story.AddComment(comment);

        story.RemoveComment(comment);

        Assert.AreEqual(0, story.Comments.Count);
    }

    [TestMethod]
    public void RemoveComment_Should_Throw_WhenNoCommentsInTheCollection()
    {
        var story = InitializeTestStory(TaskData.ValidTitle, TaskData.ValidDescription);
        var comment = InitializeTestComment();

        Assert.ThrowsException<EntityNotFoundException>(()=> story.RemoveComment(comment));
    }

    [TestMethod]
    public void ShowAllComments_ReturnsCorrectValues()
    {
        var story = InitializeTestStory();
        var comment = InitializeTestComment();
        story.AddComment(comment);

        var expected = """
                       --COMMENTS--
                       ----------
                           ----------
                         Content
                           Member: Author
                           ----------
                       ----------
                       --COMMENTS--
                       """;

        Assert.AreEqual(expected, story.ShowAllComments());
    }

    [TestMethod]
    public void ShowAllComments_ReturnsCorrectValuesWhenNoComments()
    {
        var story = InitializeTestStory();

        var expected = "--NO COMMENTS--";

        Assert.AreEqual(expected, story.ShowAllComments());
    }

    [TestMethod]
    public void PrintTaskActivity_ReturnsCorrectValues()
    {
        var story = InitializeTestStory();
        story.AddEvent("test description");

        var time = $"[{DateTime.Now:dd-MMM-yy, hh:mm:ss tt}]";
        var expected = $"{time} test description";

        Assert.AreEqual(expected, story.PrintTaskActivity());
    }
}
