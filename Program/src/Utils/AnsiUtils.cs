using Spectre.Console;
using VideoGame.Src.Heros;

namespace VideoGame.Src.Utils
{
    public class AnsiUtils
    {
        /// <summary>
        /// The splash screen function for the console program
        /// </summary>
        public static void Splash()
        {
            FigletText dungeons = new("Dungeons");
            FigletText and = new("&");
            FigletText dragons = new("Dragons");

            AnsiConsole.Write(dungeons.LeftJustified().Color(Color.Red));
            AnsiConsole.Write(and.LeftJustified().Color(Color.Orange1));
            AnsiConsole.Write(dragons.LeftJustified().Color(Color.Yellow));

            // Simulated progress bar
            AnsiConsole.Progress()
                .Start(ctx =>
                {
                    var task = ctx.AddTask("[green]Starting the game[/]");

                    Random random = new();

                    while (!ctx.IsFinished)
                    {
                        task.Increment(5);

                        Thread.Sleep(random.Next(1, 250));
                    }
                });

            // Clear the console
            AnsiConsole.Clear();
        }

        public static string AskName()
        {
            string name = AnsiConsole.Ask<string>("What is your [red]name[/]?");

            return name;
        }

        /// <summary>
        /// After the app starts, the player can select their character
        /// </summary>
        public static string CharacterMenu()
        {
            var panel = new Panel(
                new Markup("Choose your character".ToUpper())
                    .Centered())
                .Expand();

            AnsiConsole.Write(panel);

            var names = Enum.GetNames(typeof(HeroType)).Order();

            var table = new Table();

            foreach (string name in names)
            {
                table.AddColumn(
                    new TableColumn(
                        new Text(name).Centered()).Width(Console.WindowWidth / names.Count()));
            }

            table.AddRow(
                new CanvasImage("images/archer_character.png").MaxWidth(16),
                new CanvasImage("images/barbarian_character.png").MaxWidth(16),
                new CanvasImage("images/swashbuckler_character.png").MaxWidth(16),
                new CanvasImage("images/wizard_character.png").MaxWidth(16)
            );

            table.AddRow(
                new Markup("The Archer hails from the mystical forests, honing their skills in the art of precision and stealth."),
                new Markup("The Barbarian is a fierce warrior with mighty strength from the rugged, untamed lands of the North."),
                new Markup("The Swashbuckler is a daring adventurer of the high seas, known for their agility and quick wit."),
                new Markup("The Wizard is a scholar of arcane mysteries, delving deep into ancient tomes and mystic arts.")
            );
            table.AddRow(
                new Markup("[cyan bold underline]Stats[/]").Centered(),
                new Markup("[cyan bold underline]Stats[/]").Centered(),
                new Markup("[cyan bold underline]Stats[/]").Centered(),
                new Markup("[cyan bold underline]Stats[/]").Centered()
            );
            table.AddRow(
                new BarChart()
                    .AddItem("STR", 1, Color.Red)
                    .AddItem("DEX", 7, Color.Green)
                    .AddItem("INT", 1, Color.Blue),
                new BarChart()
                    .AddItem("STR", 5, Color.Red)
                    .AddItem("DEX", 2, Color.Green)
                    .AddItem("INT", 1, Color.Blue),
                new BarChart()
                    .AddItem("STR", 2, Color.Red)
                    .AddItem("DEX", 6, Color.Green)
                    .AddItem("INT", 1, Color.Blue),
                new BarChart()
                    .AddItem("STR", 1, Color.Red)
                    .AddItem("DEX", 1, Color.Green)
                    .AddItem("INT", 8, Color.Blue)
            );

            table.Expand();

            AnsiConsole.Write(table);

            var heroSelection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .PageSize(10)
                    .AddChoices(names)
                );

            return heroSelection;
        }

        public static object CreateCharacter(string characterType, string name)
        {
            try
            {
                switch (characterType)
                {
                    case "Archer":
                        {
                            return new Archer(name);
                        }
                    case "Barbarian":
                        {
                            return new Barbarian(name);
                        }
                    case "SwashBuckler":
                        {
                            return new SwashBuckler(name);
                        }
                    case "Wizard":
                        {
                            return new Wizard(name);
                        }
                    default:
                        throw new Exception($"Invalid character {characterType}");
                }
            }
            catch (Exception e)
            {   
                AnsiConsole.Write(e.Message);
                return false;
            }

        }
    }
}