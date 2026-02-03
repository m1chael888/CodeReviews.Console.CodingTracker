using System.ComponentModel;

namespace CodingTracker.m1chael888.Enums
{
    public static class MainMenuEnums
    {
        public enum MenuOption
        {
            [Description("Add Session")]
            AddSession,
            [Description("View Sessions")]
            ViewSessions,
            [Description("Edit Session")]
            EditSession,
            [Description("Delete Session")]
            DeleteSession,
            [Description("Exit App")]
            ExitApp
        }
    }
}
