namespace TasksManagement_Tests.Helpers;
public static class TestData
{
    public static class TeamData
    {
        public const int NameMinLength = 5;
        public const int NameMaxLength = 15;
        public const string ValidName = "Valid Name";
        public const string TooShortName = "test";
        public const string TooLongName = "This name should be too long.";
    }

    public static class MemberData
    {
        public const int NameMinLength = 5;
        public const int NameMaxLength = 15;
        public const string ValidName = "Valid Name";
        public const string TooShortName = "test";
        public const string TooLongName = "This name should be too long.";
    }

    public static class BoardData
    {
        public const int NameMinLength = 5;
        public const int NameMaxLength = 10;
        public const string ValidName = "ValidName";
        public const string TooShortName = "test";
        public const string TooLongName = "Name too long";
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

    public static class CommentData
    {
        public const int ContentMinLength = 3;
        public const int ContentMaxLength = 300;

        public const string ValidContent = "Content";
        public const string ValidAuthor = "Author";
    }

}
