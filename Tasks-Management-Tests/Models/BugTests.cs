using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TasksManagement.Commands.Enums;
using TasksManagement.Exceptions;
using TasksManagement.Models.Enums;
using static TasksManagement_Tests.Helpers.TestData;
using static TasksManagement_Tests.Helpers.TestHelpers;

namespace TasksManagement.Models.Tests;

[TestClass]
public class BugTests
{
    [TestMethod]
    public void Constructor_AssignsCorrectTitle()
    {
        var bug = InitializeTestBug();

        Assert.AreEqual(bug.Title, TaskData.ValidTitle);
    }

    [TestMethod]
    public void Constructor_AssignsCorrectDescription()
    {
        var bug = InitializeTestBug();

        Assert.AreEqual(bug.Description, TaskData.ValidDescription);
    }

    [TestMethod]
    public void Constructor_ThrowsOn_InvalidTitleLength()
    {
        Assert.ThrowsException<InvalidUserInputException>(
            () => InitializeTestBug(
                TaskData.TooShortTitle,
                TaskData.ValidDescription,
                InitializeTestBoard())
            );

        Assert.ThrowsException<InvalidUserInputException>(
            () => InitializeTestBug(
                TaskData.TooLongTitle,
                TaskData.ValidDescription,
                InitializeTestBoard())
            );
    }

    [TestMethod]
    public void Constructor_ThrowsOn_InvalidDescriptionLength()
    {
        Assert.ThrowsException<InvalidUserInputException>(
            () => InitializeTestBug(
                TaskData.ValidTitle,
                TaskData.TooShortDescription,
                InitializeTestBoard())
            );

        Assert.ThrowsException<InvalidUserInputException>(
            () => InitializeTestBug(
                TaskData.ValidTitle,
                TaskData.TooLongDescription,
                InitializeTestBoard())
            );
    }

    [TestMethod]
    public void Constructor_AssignsUniqueIdsToBugs()
    {
        ResetTaskLastIssuedIdState();

        var bug1 = InitializeTestBug(TaskData.ValidTitle, TaskData.ValidDescription, InitializeTestBoard());

        Assert.AreEqual(bug1.Id, 1);
    }

    [TestMethod]
    public void Title_Setter_AssignsCorrectValue()
    {
        var newTitle = "New Valid Title";
        var bug = InitializeTestBug(TaskData.ValidTitle, TaskData.ValidDescription, InitializeTestBoard());

        bug.Title = newTitle;

        Assert.AreEqual(bug.Title, newTitle);
    }

    [TestMethod]
    public void Title_Setter_ThrowsOn_InvalidTitleLength()
    {
        var bug = InitializeTestBug(TaskData.ValidTitle, TaskData.ValidDescription, InitializeTestBoard());

        Assert.ThrowsException<InvalidUserInputException>(() => bug.Title = TaskData.TooShortTitle);
        Assert.ThrowsException<InvalidUserInputException>(() => bug.Title = TaskData.TooLongTitle);
    }

    [TestMethod]
    public void Title_Setter_ThrowsWhen_SameValue()
    {
        var bug = InitializeTestBug(TaskData.ValidTitle, TaskData.ValidDescription, InitializeTestBoard());

        Assert.ThrowsException<InvalidUserInputException>(() => bug.Title = TaskData.ValidTitle);
    }

    [TestMethod]
    public void Description_Setter_AssignsCorrectValue()
    {
        var newDescription = new string('a', TaskData.DescriptionMinLength);
        var bug = InitializeTestBug(TaskData.ValidTitle, TaskData.ValidDescription, InitializeTestBoard());

        bug.Description = newDescription;

        Assert.AreEqual(bug.Description, newDescription);
    }

    [TestMethod]
    public void Description_Setter_ThrowsOn_InvalidDescriptionLength()
    {
        var TooShortDescription = new string('a', TaskData.DescriptionMinLength - 1);
        var TooLongDescription = new string('a', TaskData.DescriptionMaxLength + 1);

        var bug = InitializeTestBug(TaskData.ValidTitle, TaskData.ValidDescription, InitializeTestBoard());

        Assert.ThrowsException<InvalidUserInputException>(() => bug.Description = TooShortDescription);
        Assert.ThrowsException<InvalidUserInputException>(() => bug.Description = TooLongDescription);
    }

    [TestMethod]
    public void Description_Setter_ThrowsWhen_SameValue()
    {
        var bug = InitializeTestBug(TaskData.ValidTitle, TaskData.ValidDescription, InitializeTestBoard());

        Assert.ThrowsException<InvalidUserInputException>(() => bug.Description = TaskData.ValidDescription);
    }

    [TestMethod]
    public void Status_Setter_AssignsCorrectValue()
    {
        var bug = InitializeTestBug(TaskData.ValidTitle, TaskData.ValidDescription, InitializeTestBoard());

        bug.Status = StatusBug.Fixed;

        Assert.AreEqual(bug.Status, StatusBug.Fixed);
    }

    [TestMethod]
    public void Priority_Setter_AssignsCorrectValue()
    {
        var bug = InitializeTestBug(TaskData.ValidTitle, TaskData.ValidDescription, InitializeTestBoard());

        bug.Priority = Priority.High;

        Assert.AreEqual(bug.Priority, Priority.High);
    }

    [TestMethod]
    public void Severity_Setter_AssignsCorrectValue()
    {
        var bug = InitializeTestBug(TaskData.ValidTitle, TaskData.ValidDescription, InitializeTestBoard());

        bug.Severity = Severity.Major;

        Assert.AreEqual(bug.Severity, Severity.Major);
    }

    [TestMethod]
    public void Assignee_Setter_AssignsCorrectValue()
    {
        var bug = InitializeTestBug(TaskData.ValidTitle, TaskData.ValidDescription, InitializeTestBoard());
        var newMember = InitializeTestMember();

        bug.Assignee = newMember;

        Assert.AreEqual(bug.Assignee, newMember);
    }

    [TestMethod]
    public void ReproductionSteps_Setter_AssignsCorrectValue()
    {
        var bug = InitializeTestBug(TaskData.ValidTitle, TaskData.ValidDescription, InitializeTestBoard());
        var steps = "1. first step\r\n2. second step";

        bug.AddReproductionStep("first step");
        bug.AddReproductionStep("second step");

        Assert.AreEqual(steps, bug.PrintReproductionSteps());
    }

    [TestMethod]
    public void GetCurrentStatus_Returns_Active()
    {
        var bug = InitializeTestBug(TaskData.ValidTitle, TaskData.ValidDescription, InitializeTestBoard());

        Assert.AreEqual(StatusBug.Active.ToString(), bug.GetCurrentStatus());
    }

    [TestMethod]
    public void ToString_ReturnsCorrectValues()
    {
        ResetTaskLastIssuedIdState();
        var bug = InitializeTestBug(TaskData.ValidTitle, TaskData.ValidDescription, InitializeTestBoard());
        
        var testOutput = $"""
                          Bug (ID: 1)
                            Title: This is a valid title
                            Description: This is a valid description.
                            Status: Active
                            Reproduction Steps: 
                            Priority: Low
                            Severity: Minor
                            Assignee: 
                              --NO COMMENTS--
                          """;

        Assert.AreEqual(testOutput, bug.ToString());
    }
}