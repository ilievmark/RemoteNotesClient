namespace RemoteNotes.Rules.Contract
{
    public static class RegexRules
    {
        // Only contains alphanumeric characters, underscore and dot.
        // Underscore and dot can't be at the end or start of a username (e.g _username / username_ / .username / username.).
        // Underscore and dot can't be next to each other (e.g user_.name).
        // Underscore or dot can't be used multiple times in a row (e.g user__name / user..name).
        // Number of characters must be between 8 to 20.
        public const string Login = @"^(?=[a-zA-Z0-9._]{8,20}$)(?!.*[_.]{2})[^_.].*[^_.]$";
        
        // Minimum eight characters
        // At least one letter and one number
        public const string Password = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$";
    }
}