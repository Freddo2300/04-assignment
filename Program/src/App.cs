using System.Reflection;
using Spectre.Console;

using VideoGame.Src.Heros;
using VideoGame.Src.Items;
using VideoGame.Src.Utils;
using VideoGame.Src.Interfaces;
using VideoGame.Src.GameEngine;

namespace VideoGame.Src
{
    public class App
    {
        public static void Start()
        {   
            // Show the splash screen
            AnsiUtils.Splash();

            // Select character
            string[] selections = AnsiUtils.SelectCharacter();

            string name = selections[0];
            string heroType = selections[1];

            HeroType characterEnum = (HeroType)Enum.Parse(typeof(HeroType), heroType);

            IHero playerHero = HeroFactory.CreateHero(characterEnum, name);

            Game game = new(playerHero);

            game.StartGame();
        }
    }
}