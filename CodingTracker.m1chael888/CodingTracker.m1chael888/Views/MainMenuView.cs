using Spectre.Console;
using static CodingTracker.m1chael888.Enums.MainMenuEnums;
using static CodingTracker.m1chael888.Enums.EnumExtension;

namespace CodingTracker.m1chael888.Views
{
    public interface IMainMenuView
    {
        MenuOption ShowMenu();
    }

    public class MainMenuView : IMainMenuView
    {
        public MenuOption ShowMenu()
        {
            Console.Clear();
           
            AnsiConsole.MarkupLine($"[green]= Main Menu =[/]");
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<MenuOption>()
                .Title("[green]Choose an option::[/]")
                .AddChoices(Enum.GetValues<MenuOption>())
                .UseConverter(x => GetDescription(x))
                .HighlightStyle("green")
                .WrapAround());

            Console.Clear();
            return choice;
        }
    }
}
