using CodingTracker.m1chael888.Models;
using Spectre.Console;
using static CodingTracker.m1chael888.Enums.SessionViewEnums;
using static CodingTracker.m1chael888.Enums.EnumExtension;

namespace CodingTracker.m1chael888.Views
{
    public interface ISessionView
    {
        SessionType GetType();
        string GetTime(string beginOrEnd, string operation, bool error, string errorMsg = "");
        void ShowSessions(List<SessionDto> sessions);
        long GetEditId(List<SessionDto> sessions);
        long GetDeleteId(List<SessionDto> sessions);
    }
    public class SessionView : ISessionView
    {
        public SessionType GetType()
        {
            Console.Clear();
            AnsiConsole.MarkupLine($"[green]= Adding a coding session =[/]");

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<SessionType>()
                .Title("[green]Would you like to time a new session via stopwatch or manually log a past session??[/]")
                .AddChoices(Enum.GetValues<SessionType>())
                .UseConverter(x => GetDescription(x))
                .HighlightStyle("green")
                .WrapAround()
                );
            return choice;
        }

        public string GetTime(string operation, string beginOrEnd, bool error, string errorMsg = "")
        {
            Console.Clear();
            switch (operation)
            {
                case "create":
                    AnsiConsole.MarkupLine($"[green]= Creating a Session =[/]\n");
                    break;
                case "update":
                    AnsiConsole.MarkupLine($"[green]= Editing a session =[/]\n");
                    break;
            }

            if (error) AnsiConsole.MarkupLine($"[red]{errorMsg}[/]");
            return AnsiConsole.Ask<string>($"[green]What date and time did the session {beginOrEnd} (yyyy/MM/dd hh:mm:ss)??[/]");
        }

        public void ShowSessions(List<SessionDto> sessions)
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[green]= Coding Sessions =[/]\n");
            AnsiConsole.MarkupLine("[green]Id:\tStart Time:\t\tEnd Time:\t\tDuration:[/]");
            foreach (var s in sessions)
            {
                AnsiConsole.MarkupLine($"{s.Id}\t{s.StartTime}\t{s.EndTime}\t{s.Duration}");
            }
        }

        public long GetEditId(List<SessionDto> sessions)
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[green]= Editing a session =[/]");
            return ShowSessionPrompt(sessions);
        }

        public long GetDeleteId(List<SessionDto> sessions)
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[green]= Deleting a session =[/]");
            return ShowSessionPrompt(sessions);
        }

        private long ShowSessionPrompt(List<SessionDto> sessions)
        {
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<SessionDto>()
                .Title("[green]Choose a session::[/]")
                .UseConverter(session => $"{session.Id}\t{session.StartTime}\t{session.EndTime}\t{session.Duration}")
                .AddChoices(sessions)
                .HighlightStyle("green")
                .WrapAround()
                );
            return choice.Id;
        }
    }
}
