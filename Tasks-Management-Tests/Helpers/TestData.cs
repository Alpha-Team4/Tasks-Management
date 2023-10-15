using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksManagement_Tests.Helpers;
public static class TestData
{
    public static class TeamData
    {
        public const int TitleMinLength = 5;
        public const int TitleMaxLength = 15;
        public const string ValidTitle = "Valid Title";
        public const string TooShortTitle = "test";
        public const string TooLongTitle = "This title should be too long.";
    }

    public static class MemberData
    {
        public const int TitleMinLength = 5;
        public const int TitleMaxLength = 15;
        public const string ValidTitle = "Valid Title";
        public const string TooShortTitle = "test";
        public const string TooLongTitle = "This title should be too long.";
    }

    public static class BoardData
    {
        public const int TitleMinLength = 5;
        public const int TitleMaxLength = 10;
        public const string ValidTitle = "ValidTitle";
        public const string TooShortTitle = "test";
        public const string TooLongTitle = "Title too long";
    }

    public static class TaskData
    {
        public const int TitleMinLength = 10;
        public const int TitleMaxLength = 50;
        public const int DescriptionMinLength = 10;
        public const int DescriptionMaxLength = 500;
        
        public const string ValidTitle = "This is a valid title";
        public const string TooShortTitle = "short";
        public const string TooLongTitle = "This title is too long. This title is too long. This title is too long. ";
        public const string ValidDescription = "This is a valid description.";
        public const string TooShortDescription = "short";
        public const string TooLongDescription = """
                                                  This description is too long. This description is too long. This description is too long.
                                                  This description is too long. This description is too long. This description is too long.
                                                  This description is too long. This description is too long. This description is too long.
                                                  This description is too long. This description is too long. This description is too long.
                                                  This description is too long. This description is too long. This description is too long.
                                                  This description is too long. This description is too long. This description is too long.
                                                  """;
    }



}
