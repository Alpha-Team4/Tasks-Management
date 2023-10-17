using TasksManagement.Models;
using TasksManagement.Models.Contracts;
using static TasksManagement_Tests.Helpers.TestData;

namespace TasksManagement_Tests.Helpers;
public class TestHelpers
{
    public static int TaskIdCounter { get; set; }

    public static ITeam InitializeTestTeam()
    {
        return new Team(TeamData.ValidName);
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
                TaskData.ValidDescription,
                InitializeTestBoard()
            );

            TaskIdCounter++;
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
                description,
                board
            );

            TaskIdCounter++;
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
                TaskData.ValidDescription,
                InitializeTestBoard()
            );

            TaskIdCounter++;
            return story;
        }
        catch
        {
            throw;
        }
    }

    public static IStory InitializeTestStory(string title, string description, IBoard board)
    {
        try
        {
            var story = new Story(
                title,
                description,
                board
            );

            TaskIdCounter++;
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

            TaskIdCounter++;
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

            TaskIdCounter++;
            
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
}
