using TasksManagement.Models;
using TasksManagement.Models.Contracts;
using static TasksManagement_Tests.Helpers.TestData;

namespace TasksManagement_Tests.Helpers;
public class TestHelpers
{
    public static string GetTestString(int size)
    {
        return new string('x', size);
    }

    public static ITeam InitializeTestTeam()
    {
        return new Team(TeamData.ValidTitle);
    }

    public static IMember InitializeTestMember()
    {
        return new Member(MemberData.ValidTitle);
    }

    public static IBoard InitializeTestBoard()
    {
        return new Board(BoardData.ValidTitle);
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
            InitializeTestMember()
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
}
