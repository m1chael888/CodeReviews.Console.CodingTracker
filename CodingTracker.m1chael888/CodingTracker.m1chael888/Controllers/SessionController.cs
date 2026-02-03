using CodingTracker.m1chael888.Views;
using Spectre.Console;
using static CodingTracker.m1chael888.Enums.MainMenuEnums;
using static CodingTracker.m1chael888.Enums.SessionViewEnums;
using CodingTracker.m1chael888.Models;
using CodingTracker.m1chael888.Services;
using System.Diagnostics;
using System.Globalization;

namespace CodingTracker.m1chael888.Controllers
{
    public interface ISessionController
    {
        void CallMainMenu();
    }

    public class SessionController : ISessionController
    {
        private readonly IMainMenuView _mainMenuView;
        private readonly ISessionView _sessionView;
        private readonly ISessionService _sessionService;

        public SessionController(IMainMenuView mainMenuView, ISessionView addSessionView, ISessionService addSessionService)
        {
            _mainMenuView = mainMenuView;
            _sessionView = addSessionView;
            _sessionService = addSessionService;
        }

        public void CallMainMenu()
        {
            var choice = _mainMenuView.ShowMenu();

            switch (choice)
            {
                case MenuOption.AddSession:
                    GetSessionType();
                    break;
                case MenuOption.ViewSessions:
                    DisplaySessions();
                    break;
                case MenuOption.EditSession:
                    GetSessionToEdit();
                    break;
                case MenuOption.DeleteSession:
                    GetSessionToDelete();
                    break;
                case MenuOption.ExitApp:
                    Environment.Exit(0);
                    break;
            }
        }

        private void GetSessionType()
        {
            var choice = _sessionView.GetType();

            switch (choice)
            {
                case SessionType.New:
                    TimeNewSession();
                    break;
                case SessionType.Past:
                    GetSessionDetails("create");
                    break;
            }
        }

        private void DisplaySessions()
        {
            var sessions = _sessionService.CallRead();
            
            if (sessions.Count > 0)
            {
                _sessionView.ShowSessions(sessions);
                ReturnToMenu();
            }
            else
            {
                ReturnToMenu("No sessions found, add some first!!");
            }
        }

        private void GetSessionToEdit()
        {
            var sessions = _sessionService.CallRead();

            if (sessions.Count > 0)
            {
                var id = _sessionView.GetEditId(sessions);
                GetSessionDetails("update", editId: id);
            }
            else
            {
                ReturnToMenu("No sessions found, add some first!!");
            }
        }

        private void GetSessionToDelete()
        {
            var sessions = _sessionService.CallRead();
            if (sessions.Count > 0)
            {
                var id = _sessionView.GetDeleteId(sessions);
                _sessionService.CallDelete(id);
                Console.Clear();
                ReturnToMenu("Session deleted successfully.");
            }
            else
            {
                ReturnToMenu("No sessions found, add some first!!");
            }
        }

        private void TimeNewSession()
        {
            var stopwatch = new Stopwatch();
            var startTime = DateTime.Now;
            stopwatch.Start();

            Console.Clear();
            AnsiConsole.MarkupLine("[green]= Timer is currently active =[/]");
            StatusMessage("Press any key to stop the timer and save session");

            var duration = stopwatch.Elapsed;
            duration = new TimeSpan(duration.Hours, duration.Minutes, duration.Seconds);

            stopwatch.Stop();
            var endTime = startTime + duration;

            TrySave(MapDto(startTime.ToString("yyyy/MM/dd HH:mm:ss"), endTime.ToString("yyyy/MM/dd HH:mm:ss"), duration.ToString()));
            Console.Clear();
            ReturnToMenu($"You coded for {duration.ToString()}!! Your session was saved successfully =)");
        }

        private void GetSessionDetails(string operation, bool error = false, long editId = 0)
        {
            string[] errors = { "Please follow the date/time format carefully!! You can omit seconds","Ensure that the starting time is before the ending time!!" };
            string[] formats = { "yyyy/M/d H:mm:ss", "yyyy/M/d H:mm" };
            CultureInfo enUS = new CultureInfo("en-US");

            var startTime = _sessionView.GetTime(operation, "begin", error, errors[1]);
            while (!DateTime.TryParseExact(startTime, formats, enUS, DateTimeStyles.None, out var startTimeOut))
            {
                error = true;
                startTime = _sessionView.GetTime(operation, "begin", error, errorMsg: errors[0]);
            }
            error = false;

            var endTime = _sessionView.GetTime(operation, "end", error);
            while (!DateTime.TryParseExact(endTime, formats, enUS, DateTimeStyles.None, out var endTimeOut))
            {
                error = true;
                endTime = _sessionView.GetTime(operation, "end", error, errorMsg: errors[0]).ToString();
            }
            error = false;

            var duration = (DateTime.Parse(endTime) - DateTime.Parse(startTime));
            duration = new TimeSpan(duration.Days, duration.Hours, duration.Minutes, duration.Seconds);
            startTime = DateTime.Parse(startTime).ToString("yyyy/MM/dd HH:mm:ss");
            endTime = DateTime.Parse(endTime).ToString("yyyy/MM/dd HH:mm:ss");

            var durationstring = duration.ToString();
            if (duration.Days > 0)
            {
                var hours = duration.Days * 24 + duration.Hours;
                var minAndSec = durationstring.Substring(durationstring.IndexOf(":"));
                durationstring = hours + minAndSec;
            }

            switch (operation)
            {
                case "create":
                    TrySave(MapDto(startTime, endTime, durationstring));
                    break;
                case "update":
                    TryEdit(MapDto(startTime, endTime, durationstring, id: editId));
                    break;
            }
            Console.Clear();
            ReturnToMenu("Session saved successfully =)");
        }

        private void TrySave(SessionDto Dto)
        {
            switch (_sessionService.Validate(Dto))
            {
                case true:
                    _sessionService.CallCreate(Dto);
                    break;
                case false:
                    GetSessionDetails("create", error: true);
                    break;
            }
        }

        private void TryEdit(SessionDto Dto)
        {
            switch (_sessionService.Validate(Dto))
            {
                case true:
                    _sessionService.CallUpdate(Dto);
                    break;
                case false:
                    GetSessionDetails("update", error: true, editId: Dto.Id);
                    break;
            }
        }
        
        private SessionDto MapDto(string startTime, string endTime, string duration, long id = 0)
        {
            var dto = new SessionDto();

            dto.StartTime = startTime;
            dto.EndTime = endTime;
            dto.Duration = duration;
            if (id != 0) dto.Id = id;

            return dto;
        }

        private void StatusMessage(string msg)
        {
            AnsiConsole.Status()
                .Spinner(Spinner.Known.Point)
                .SpinnerStyle("white")
                .Start(msg, x =>
                {
                    Console.ReadKey();
                });
        }

        void ReturnToMenu(string msg = "")
        {
            if (msg != "") AnsiConsole.MarkupLine($"[green]{msg}[/]");

            StatusMessage("Press any key to return to menu");
            CallMainMenu();
        }
    }
}
