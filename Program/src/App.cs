using Spectre.Console;

using VideoGame.Src.Heros;
using VideoGame.Src.Items;
using VideoGame.Src.Utils;

namespace VideoGame.Src
{
    public class App
    {
        public static void Start()
        {   
            // Show the splash screen
            AnsiUtils.Splash();

            bool confirm;
            string characterSelection;
            string name;

            do
            {
                characterSelection = AnsiUtils.CharacterMenu();

                name = AnsiUtils.AskName();

                confirm = AnsiConsole.Confirm($"type: {characterSelection}\nname: {name}\n\nDo you confirm?", defaultValue: false);
            } while (!confirm);

            AnsiConsole.Clear();

            Wizard hero = AnsiUtils.CreateCharacter(characterSelection, name) as Wizard;
        }
    }
}