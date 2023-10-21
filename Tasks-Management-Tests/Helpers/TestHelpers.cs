using System.Reflection;
using TasksManagement.Models;
using TasksManagement.Models.Contracts;
using static TasksManagement_Tests.Helpers.TestData;
using Task = TasksManagement.Models.Task;

namespace TasksManagement_Tests.Helpers;
public class TestHelpers
{
    public static void ResetTaskLastIssuedIdState()
    {
        var field = typeof(Task).GetField("lastIssuedId", BindingFlags.Static | BindingFlags.NonPublic);
        if (field != null)
        {
            field.SetValue(null, 0);
        }
    }

    public static ITeam InitializeTestTeam()
    {
        return new Team(TeamData.ValidName);
    }

    public static ITeam InitializeTestTeam(string name)
    {
        try
        {
            var team = new Team(name);

            return team;
        }
        catch
        {
            throw;
        }
        
    }

    public static IMember InitializeTestMember()
    {
        return new Member(MemberData.ValidName);
    }

    public static IBoard InitializeTestBoard()
    {
        return new Board(BoardData.ValidName);
    }

    public static IBug InitializeTestBug()
    {
        try
        {
            var bug = new Bug(
                TaskData.ValidTitle,
                TaskData.ValidDescription
            );

            return bug;
        }
        catch
        {
            throw;
        }
    }

    public static IBug InitializeTestBug(string title, string description, IBoard board)
    {
        try
        {
            var bug = new Bug(
                title,
                description
            );

            return bug;
        }
        catch
        {
            throw;
        }
    }

    public static IStory InitializeTestStory()
    {
        try
        {
            var story = new Story(
                TaskData.ValidTitle,
                TaskData.ValidDescription
            );

            return story;
        }
        catch
        {
            throw;
        }
    }

    public static IStory InitializeTestStory(string title, string description)
    {
        try
        {
            var story = new Story(
                title,
                description
            );

            return story;
        }
        catch
        {
            throw;
        }
    }


    public static IFeedback InitializeTestFeedback()
    {
        try
        {
            var feedback = new Feedback(
                TaskData.ValidTitle,
                TaskData.ValidDescription
                );

            return feedback;
        }
        catch
        {
            throw;
        }


    }

    public static IFeedback InitializeTestFeedback(string title, string description, IBoard board)
    {
        try
        {
            var feedback = new Feedback(
                TaskData.ValidTitle,
                TaskData.ValidDescription
                );

            return feedback;

        }
        catch
        {
            throw;
        }

    }

    public static IEvent InitializeTestEvent()
    {
        return new Event("test description");
    }

    public static IComment InitializeTestComment()
    {
            return new Comment(
                CommentData.ValidContent,
                CommentData.ValidAuthor);
    }
}
