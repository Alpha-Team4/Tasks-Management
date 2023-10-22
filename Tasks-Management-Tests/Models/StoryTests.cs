using Microsoft.VisualStudio.TestTools.UnitTesting;
using TasksManagement.Commands.Enums;
using TasksManagement.Exceptions;
using TasksManagement.Models.Enums;
using static TasksManagement_Tests.Helpers.TestData;
using static TasksManagement_Tests.Helpers.TestHelpers;

namespace TasksManagement_Tests.Models;

[TestClass]
public class StoryTests
{
    [TestMethod]
    public void Constructor_AssignsCorrectTitle()
    {
        var story = InitializeTestStory();

        Assert.AreEqual(story.Title, TaskData.ValidTitle);
    }

    [TestMethod]
    public void Constructor_AssignsCorrectDescription()
    {
        var story = InitializeTestBug();

        Assert.AreEqual(story.Description, TaskData.ValidDescription);
    }

    [TestMethod]
    public void Constructor_ThrowsOn_InvalidTitleLength()
    {
        Assert.ThrowsException<InvalidUserInputException>(
            () => InitializeTestBug(
                TaskData.TooShortTitle,
                TaskData.ValidDescription,
                InitializeTestBoard()));

        Assert.ThrowsException<InvalidUserInputException>(
            () => InitializeTestBug(
                TaskData.TooLongTitle,
                TaskData.ValidDescription,
                InitializeTestBoard()));
    }

    [TestMethod]
    public void Constructor_ThrowsOn_InvalidDescriptionLength()
    {
        Assert.ThrowsException<InvalidUserInputException>(
            () => InitializeTestBug(
                TaskData.ValidTitle,
                TaskData.TooShortDescription,
                InitializeTestBoard()));

        Assert.ThrowsException<InvalidUserInputException>(
            () => InitializeTestBug(
                TaskData.ValidTitle,
                TaskData.TooLongDescription,
                InitializeTestBoard()));
    }

    [TestMethod]
    public void Constructor_AssignsUniqueIdsToStories()
    {
        ResetTaskLastIssuedIdState();

        var story1 = InitializeTestStory(TaskData.ValidTitle, TaskData.ValidDescription);

        Assert.AreEqual(story1.Id, 1);
    }

    [TestMethod]
    public void Title_Setter_AssignsCorrectValue()
    {
        var newTitle = "New Valid Title";
        var story = InitializeTestStory(TaskData.ValidTitle, TaskData.ValidDescription);

        story.Title = newTitle;

        Assert.AreEqual(story.Title, newTitle);
    }

    [TestMethod]
    public void Title_Setter_ThrowsOn_InvalidTitleLength()
    {
        var story = InitializeTestStory(TaskData.ValidTitle, TaskData.ValidDescription);

        Assert.ThrowsException<InvalidUserInputException>(() => story.Title = TaskData.TooShortTitle);
        Assert.ThrowsException<InvalidUserInputException>(() => story.Title = TaskData.TooLongTitle);
    }

    [TestMethod]
    public void Title_Setter_ThrowsWhen_SameValue()
    {
        var story = InitializeTestStory(TaskData.ValidTitle, TaskData.ValidDescription);

        Assert.ThrowsException<InvalidUserInputException>(() => story.Title = TaskData.ValidTitle);
    }

    [TestMethod]
    public void Description_Setter_AssignsCorrectValue()
    {
        var newDescription = new string('a', TaskData.DescriptionMinLength);
        var story = InitializeTestStory(TaskData.ValidTitle, TaskData.ValidDescription);

        story.Description = newDescription;

        Assert.AreEqual(story.Description, newDescription);
    }

    [TestMethod]
    public void Description_Setter_ThrowsOn_InvalidDescriptionLength()
    {
        var tooShortDescription = new string('a', TaskData.DescriptionMinLength - 1);
        var tooLongDescription = new string('a', TaskData.DescriptionMaxLength + 1);

        var story = InitializeTestStory(TaskData.ValidTitle, TaskData.ValidDescription);

        Assert.ThrowsException<InvalidUserInputException>(() => story.Description = tooShortDescription);
        Assert.ThrowsException<InvalidUserInputException>(() => story.Description = tooLongDescription);
    }

    [TestMethod]
    public void Description_Setter_ThrowsWhen_SameValue()
    {
        var story = InitializeTestStory(TaskData.ValidTitle, TaskData.ValidDescription);

        Assert.ThrowsException<InvalidUserInputException>(() => story.Description = TaskData.ValidDescription);
    }

    [TestMethod]
    public void Priority_Setter_AssignsCorrectValue()
    {
        var story = InitializeTestStory(TaskData.ValidTitle, TaskData.ValidDescription);

        story.Priority = Priority.Medium;

        Assert.AreEqual(story.Priority, Priority.Medium);
    }

    [TestMethod]
    public void Size_Setter_AssignsCorrectValue()
    {
        var story = InitializeTestStory(TaskData.ValidTitle, TaskData.ValidDescription);

        story.Size = Size.Medium;

        Assert.AreEqual(story.Size, Size.Medium);
    }

    [TestMethod]
    public void Status_Setter_AssignsCorrectValue()
    {
        var story = InitializeTestStory(TaskData.ValidTitle, TaskData.ValidDescription);

        story.Status = StatusStory.InProgress;

        Assert.AreEqual(story.Status, StatusStory.InProgress);
    }

    [TestMethod]
    public void Assignee_Setter_AssignsCorrectValue()
    {
        var story = InitializeTestStory(TaskData.ValidTitle, TaskData.ValidDescription);
        var newMember = InitializeTestMember();

        story.Assignee = newMember;

        Assert.AreEqual(story.Assignee, newMember);
    }

    [TestMethod]
    public void GetCurrentStatus_Returns_NotDone()
    {
        var story = InitializeTestStory(TaskData.ValidTitle, TaskData.ValidDescription);

        Assert.AreEqual(StatusStory.NotDone.ToString(), story.GetCurrentStatus());
    }

    [TestMethod]
    public void ToString_ReturnsCorrectValues()
    {
        ResetTaskLastIssuedIdState();
        var story = InitializeTestStory(TaskData.ValidTitle, TaskData.ValidDescription);

        var expected = """
                       Story (ID: 1)
                         Title: This is a valid title
                         Description: This is a valid description.
                         Status: NotDone
                         Size: Small
                         Priority: Low
                         Assignee: 
                           --NO COMMENTS--
                       """;

        Assert.AreEqual(expected, story.ToString());
    }
}
