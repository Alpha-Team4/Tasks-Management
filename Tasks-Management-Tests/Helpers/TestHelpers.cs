using TasksManagement.Models;
using TasksManagement.Models.Contracts;
using static TasksManagement_Tests.Helpers.TestData;

namespace TasksManagement_Tests.Helpers;
public class TestHelpers
{
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

    public static ITask InitializeTestBug()
    {
        return new Bug(
            TaskData.ValidTitle,
            TaskData.ValidDescription,
            InitializeTestBoard()
            );
    }

    public static ITask InitializeTestStory()
    {
        return new Story(
            TaskData.ValidTitle,
            TaskData.ValidDescription,
            InitializeTestBoard()
            );
    }

    public static ITask InitializeTestFeedback()
    {
        return new Feedback(
            TaskData.ValidTitle,
            TaskData.ValidDescription,
            0
            );
    }

    public static IEvent InitializeTestEvent()
    {
        return new Event("test description");
    }
}
