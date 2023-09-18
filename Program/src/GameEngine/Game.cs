using Spectre.Console;

using VideoGame.Src.Heros;
using VideoGame.Src.Items;
using VideoGame.Src.Interfaces;

namespace VideoGame.Src.GameEngine
{
    class Game
    {
        IHero ActiveHero { get; set; }
        string[] Choices { get; }
                = {
                    "Display", "Level up", "Get damage", "Change equipment", "Exit"
                };
        Dictionary<string, Weapon> Weapons { get; } = new Dictionary<string, Weapon>()
        {
            // BOWS
            ["Short Bow"] = new Weapon(name: "Short Bow", requiredLevel: 1, damage: 55.0, WeaponType.Bow),
            ["Composite Bow"] = new Weapon(name: "Composite Bow", requiredLevel: 5, damage: 80.0, WeaponType.Bow),
            ["Darkmoon Bow"] = new Weapon(name: "Darkmoon Bow", requiredLevel: 20, damage: 133.0, WeaponType.Bow),
            ["Sniper Crossbow"] = new Weapon(name: "Sniper Crossbow", requiredLevel: 37, damage: 213.0, WeaponType.Bow),
            ["Dragonslayer Greatbow"] = new Weapon(name: "Dragonslayer Greatbow", requiredLevel: 45, damage: 266.0, WeaponType.Bow),
            ["Avelyn"] = new Weapon(name: "Avelyn", requiredLevel: 50, damage: 300.0, WeaponType.Bow),
            // DAGGERS
            ["Parrying Dagger"] = new Weapon(name: "Parrying Dagger", requiredLevel: 1, damage: 63.0, WeaponType.Dagger),
            ["Ghost Blade"] = new Weapon(name: "Ghost Blade", requiredLevel: 37, damage: 277.0, WeaponType.Dagger),
            ["Dark Silver Tracer"] = new Weapon(name: "Dark Silver Tracer", requiredLevel: 45, damage: 342.0, WeaponType.Dagger),
            ["Priscilla's Dagger"] = new Weapon(name: "Priscilla's Dagger", requiredLevel: 50, damage: 399.0, WeaponType.Dagger),
            // HATCHETS
            ["Butcher's Hatchet"] = new Weapon(name: "Butcher's Hatchet", requiredLevel: 37, damage: 477.0, WeaponType.Hatchet),
            ["Black Knight Greataxe"] = new Weapon(name: "Black Knight Greataxe", requiredLevel: 50, damage: 744.0, WeaponType.Hatchet),
            // MACES
            ["Mace"] = new Weapon(name: "Mace", requiredLevel: 1, damage: 77.0, WeaponType.Mace),
            ["Morning Star"] = new Weapon(name: "Morning Star", requiredLevel: 45, damage: 200.0, WeaponType.Mace),
            // STAFFS
            ["Tin Darkmoon Catalyst"] = new Weapon(name: "Tin Darkmoon Catalyst", requiredLevel: 5, damage: 43.0, WeaponType.Staff),
            ["Demon's Catalyst"] = new Weapon(name: "Demon's Catalyst", requiredLevel: 37, damage: 213.0, WeaponType.Staff),
            ["Tin Crystalization Catalyst"] = new Weapon(name: "Tin Crystalization Catalyst", requiredLevel: 50, damage: 300.0, WeaponType.Staff),
            // SWORDS
            ["Longsword"] = new Weapon(name: "Longsword", requiredLevel: 5, damage: 100.0, WeaponType.Sword),
            ["Abyss Greatsword"] = new Weapon(name: "Abyss Greatsword", requiredLevel: 20, damage: 233.0, WeaponType.Sword),
            // WANDS
            ["Sorcerer's Wand"] = new Weapon(name: "Sorcerer's Wand", requiredLevel: 1, damage: 25.0, WeaponType.Wand),
            ["Logan's Wand"] = new Weapon(name: "Logan's Wand", requiredLevel: 20, damage: 125.0, WeaponType.Wand),
            ["Izalith's Wand"] = new Weapon(name: "Izalith's Wand", requiredLevel: 45, damage: 277.0, WeaponType.Wand),
        }.OrderBy(pair => pair.Value.RequiredLevel).ToDictionary(pair => pair.Key, pair => pair.Value);

        Dictionary<string, Armor> Armors { get; } = new Dictionary<string, Armor>()
        {
            ////////// HEAD
            /// Cloth (Wizard)
            ["Wanderer Hood"] = new Armor(name: "Wanderer Hood", requiredLevel: 10, equipSlot: Slot.Head, type: ArmorType.Cloth, armorAttribute: new HeroAttribute(2, 3, 10)),
            ["Sorcerer Hat"] = new Armor(name: "Sorcerer Hat", requiredLevel: 30, equipSlot: Slot.Head, type: ArmorType.Cloth, armorAttribute: new HeroAttribute(10, 8, 18)),
            ["Black Sorcerer Hat"] = new Armor(name: "Black Sorcerer Hat", requiredLevel: 45, equipSlot: Slot.Head, type: ArmorType.Cloth, armorAttribute: new HeroAttribute(20, 17, 45)),
            /// Plate (Barbarian)
            ["Standard Helm"] = new Armor(name: "Standard Helm", requiredLevel: 10, equipSlot: Slot.Head, type: ArmorType.Plate, armorAttribute: new HeroAttribute(20, 8, 3)),
            ["Knight Helm"] = new Armor(name: "Knight Helm", requiredLevel: 30, equipSlot: Slot.Head, type: ArmorType.Plate, armorAttribute: new HeroAttribute(50, 20, 8)),
            ["Helm of Thorns"] = new Armor(name: "Helm of Thorns", requiredLevel: 45, equipSlot: Slot.Head, type: ArmorType.Plate, armorAttribute: new HeroAttribute(90, 50, 20)),
            /// Mail (Barbarian, Archer, SwashBuckler)
            ["Mail helmet"] = new Armor(name: "Mail helmet", requiredLevel: 10, equipSlot: Slot.Head, type: ArmorType.Mail, armorAttribute: new HeroAttribute(8, 8, 5)),
            ["Chain Helm"] = new Armor(name: "Chain Helm", requiredLevel: 30, equipSlot: Slot.Head, type: ArmorType.Mail, armorAttribute: new HeroAttribute(14, 14, 10)),
            ["Helm of Light"] = new Armor(name: "Helm of Light", requiredLevel: 45, equipSlot: Slot.Head, type: ArmorType.Mail, armorAttribute: new HeroAttribute(30, 30, 20)),
            /// Leather (Archer, SwashBuckler)
            ["Sack"] = new Armor(name: "Sack", requiredLevel: 10, equipSlot: Slot.Head, type: ArmorType.Leather, armorAttribute: new HeroAttribute(2, 10, 2)),
            ["Snickering Top Hat"] = new Armor(name: "Snickering Top Hat", requiredLevel: 30, equipSlot: Slot.Head, type: ArmorType.Leather, armorAttribute: new HeroAttribute(6, 20, 6)),
            ["Gold-Hemmed Black Hood"] = new Armor(name: "Gold-Hemmed Black Hood", requiredLevel: 45, equipSlot: Slot.Head, type: ArmorType.Leather, armorAttribute: new HeroAttribute(15, 50, 15)),
            ////////// BODY
            ////// Cloth (Wizard)
            ["Wanderer Shirt"] = new Armor(name: "Wanderer Shirt", requiredLevel: 10, equipSlot: Slot.Body, type: ArmorType.Cloth, armorAttribute: new HeroAttribute(5, 1, 10)),
            ["Sorcerer Cloak"] = new Armor(name: "Sorcerer Cloak", requiredLevel: 30, equipSlot: Slot.Body, type: ArmorType.Cloth, armorAttribute: new HeroAttribute(10, 12, 22)),
            ["Black Sorcerer Gown"] = new Armor(name: "Black Sorcerer Gown", requiredLevel: 45, equipSlot: Slot.Body, type: ArmorType.Cloth, armorAttribute: new HeroAttribute(18, 20, 50)),
            /// Plate (Barbarian)
            ["Standard Armor"] = new Armor(name: "Standard Armor", requiredLevel: 10, equipSlot: Slot.Body, type: ArmorType.Plate, armorAttribute: new HeroAttribute(24, 3, 3)),
            ["Knight Armor"] = new Armor(name: "Knight Armor", requiredLevel: 30, equipSlot: Slot.Body, type: ArmorType.Plate, armorAttribute: new HeroAttribute(54, 16, 5)),
            ["Armor of Thorns"] = new Armor(name: "Armor of Thorns", requiredLevel: 45, equipSlot: Slot.Body, type: ArmorType.Plate, armorAttribute: new HeroAttribute(100, 30, 10)),
            /// Mail (Barbarian, Archer, SwashBuckler)
            ["Mail Armor"] = new Armor(name: "Mail Armor", requiredLevel: 10, equipSlot: Slot.Body, type: ArmorType.Mail, armorAttribute: new HeroAttribute(10, 10, 4)),
            ["Chain Armor"] = new Armor(name: "Chain Armor", requiredLevel: 30, equipSlot: Slot.Body, type: ArmorType.Mail, armorAttribute: new HeroAttribute(16, 16, 6)),
            ["Armor of Light"] = new Armor(name: "Armor of Light", requiredLevel: 45, equipSlot: Slot.Body, type: ArmorType.Mail, armorAttribute: new HeroAttribute(34, 34, 12)),
            /// Leather (Archer, SwashBuckler)
            ["Sage Robe"] = new Armor(name: "Sage Robe", requiredLevel: 10, equipSlot: Slot.Body, type: ArmorType.Leather, armorAttribute: new HeroAttribute(3, 8, 3)),
            ["Suit and Tie"] = new Armor(name: "Suit and Tie", requiredLevel: 30, equipSlot: Slot.Body, type: ArmorType.Leather, armorAttribute: new HeroAttribute(8, 18, 8)),
            ["Gold-Hemmed Cape"] = new Armor(name: "Gold-Hemmed Cape", requiredLevel: 45, equipSlot: Slot.Body, type: ArmorType.Leather, armorAttribute: new HeroAttribute(22, 44, 22)),
            // LEGS
            ////// Cloth (Wizard)
            ["Wanderer Leggings"] = new Armor(name: "Wanderer Leggings", requiredLevel: 10, equipSlot: Slot.Legs, type: ArmorType.Cloth, armorAttribute: new HeroAttribute(2, 5, 10)),
            ["Sorcerer Boots"] = new Armor(name: "Sorcerer Boots", requiredLevel: 30, equipSlot: Slot.Legs, type: ArmorType.Cloth, armorAttribute: new HeroAttribute(4, 9, 15)),
            ["Black Sorcerer Boots"] = new Armor(name: "Black Sorcerer Boots", requiredLevel: 45, equipSlot: Slot.Legs, type: ArmorType.Cloth, armorAttribute: new HeroAttribute(16, 12, 50)),
            /// Plate (Barbarian)
            ["Standard Leggings"] = new Armor(name: "Standard Leggings", requiredLevel: 10, equipSlot: Slot.Legs, type: ArmorType.Plate, armorAttribute: new HeroAttribute(18, 10, 2)),
            ["Knight Leggings"] = new Armor(name: "Knight Leggings", requiredLevel: 30, equipSlot: Slot.Legs, type: ArmorType.Plate, armorAttribute: new HeroAttribute(42, 24, 10)),
            ["Waistcloth of Thorns"] = new Armor(name: "Waistcloth of Thorns", requiredLevel: 45, equipSlot: Slot.Legs, type: ArmorType.Plate, armorAttribute: new HeroAttribute(80, 40, 12)),
            /// Mail (Barbarian, Archer, SwashBuckler)
            ["Mail Leggings"] = new Armor(name: "Mail Leggings", requiredLevel: 10, equipSlot: Slot.Legs, type: ArmorType.Mail, armorAttribute: new HeroAttribute(6, 6, 6)),
            ["Chain Waistcloth"] = new Armor(name: "Chain Waistcloth", requiredLevel: 30, equipSlot: Slot.Legs, type: ArmorType.Mail, armorAttribute: new HeroAttribute(14, 14, 14)),
            ["Leggings Of Light"] = new Armor(name: "Leggings of Light", requiredLevel: 45, equipSlot: Slot.Legs, type: ArmorType.Mail, armorAttribute: new HeroAttribute(22, 22, 22)),
            /// Leather (Archer, SwashBuckler)
            ["Sage Boots"] = new Armor(name: "Sage Boots", requiredLevel: 10, equipSlot: Slot.Legs, type: ArmorType.Leather, armorAttribute: new HeroAttribute(4, 10, 4)),
            ["Pointy Shoes"] = new Armor(name: "Pointy Shoes", requiredLevel: 30, equipSlot: Slot.Legs, type: ArmorType.Leather, armorAttribute: new HeroAttribute(10, 22, 6)),
            ["Gold-Hemmed Black Skirt"] = new Armor(name: "Gold-Hemmed Black Skirt", requiredLevel: 45, equipSlot: Slot.Legs, type: ArmorType.Leather, armorAttribute: new HeroAttribute(20, 48, 18)),
        };

        public Game(IHero hero)
        {
            ActiveHero = hero;
        }

        public void StartGame()
        {
            if (ActiveHero is Archer archer)
            {
                Greeting(archer.Name!, (HeroType)archer.Type!);

                string selection;

                do
                {
                    selection = DisplayMainMenu();

                    switch (selection)
                    {
                        case "Display":
                            {
                                archer.Display(archer.CalculateHeroDamage());
                                break;
                            }
                        case "Level up":
                            {
                                archer.LevelUp();
                                break;
                            }
                        case "Get damage":
                            {
                                AnsiConsole.Write(new Markup($"Your current damage: [red bold]{archer.CalculateHeroDamage()}[/]"));
                                AnsiConsole.WriteLine();
                                break;
                            }
                        case "Change equipment":
                            {
                                DisplayEquipmentMenu(archer);
                                break;
                            }
                        case "Exit":
                            {
                                Environment.Exit(1);
                                break;
                            }
                    }
                } while (selection != "Exit");

            }
            else if (ActiveHero is Barbarian barbarian)
            {
                Greeting(barbarian.Name!, (HeroType)barbarian.Type!);

                string selection;

                do
                {
                    selection = DisplayMainMenu();

                    switch (selection)
                    {
                        case "Display":
                            {
                                barbarian.Display(barbarian.CalculateHeroDamage());
                                break;
                            }
                        case "Level up":
                            {
                                barbarian.LevelUp();
                                break;
                            }
                        case "Get damage":
                            {
                                AnsiConsole.Write(new Markup($"Your current damage: [red bold]{barbarian.CalculateHeroDamage()}[/]"));
                                AnsiConsole.WriteLine();
                                break;
                            }
                        case "Change equipment":
                            {
                                DisplayEquipmentMenu(barbarian);
                                break;
                            }
                        case "Exit":
                            {
                                Environment.Exit(1);
                                break;
                            }
                    }
                } while (selection != "Exit");
            }
            else if (ActiveHero is SwashBuckler swashBuckler)
            {
                Greeting(swashBuckler.Name!, (HeroType)swashBuckler.Type!);

                string selection;

                do
                {
                    selection = DisplayMainMenu();

                    switch (selection)
                    {
                        case "Display":
                            {
                                swashBuckler.Display(swashBuckler.CalculateHeroDamage());
                                break;
                            }
                        case "Level up":
                            {
                                swashBuckler.LevelUp();
                                break;
                            }
                        case "Get damage":
                            {
                                AnsiConsole.Write(new Markup($"Your current damage: [red bold]{swashBuckler.CalculateHeroDamage()}[/]"));
                                AnsiConsole.WriteLine();
                                break;
                            }
                        case "Change equipment":
                            {
                                DisplayEquipmentMenu(swashBuckler);
                                break;
                            }
                        case "Exit":
                            {
                                Environment.Exit(1);
                                break;
                            }
                    }
                } while (selection != "Exit");
            }
            else if (ActiveHero is Wizard wizard)
            {
                Greeting(wizard.Name!, (HeroType)wizard.Type!);

                string selection;

                do
                {
                    selection = DisplayMainMenu();

                    switch (selection)
                    {
                        case "Display":
                            {
                                wizard.Display(wizard.CalculateHeroDamage());
                                break;
                            }
                        case "Level up":
                            {
                                wizard.LevelUp();
                                break;
                            }
                        case "Get damage":
                            {
                                AnsiConsole.Write(new Markup($"Your current damage: [red bold]{wizard.CalculateHeroDamage()}[/]"));
                                AnsiConsole.WriteLine();
                                break;
                            }
                        case "Change equipment":
                            {
                                DisplayEquipmentMenu(wizard);
                                break;
                            }
                        case "Exit":
                            {
                                Environment.Exit(1);
                                break;
                            }
                    }
                } while (selection != "Exit");
            }
        }

        public static void Greeting(string name, HeroType type)
        {
            AnsiConsole.WriteLine();
            AnsiConsole.Write(new Markup($"Greetings [red]{name}[/]"));
            AnsiConsole.WriteLine();
            AnsiConsole.Write(new Markup($"You are a [red]{type}[/]!"));
            AnsiConsole.WriteLine();
            AnsiConsole.WriteLine();
        }

        public string DisplayMainMenu()
        {
            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .PageSize(10)
                    .AddChoices(Choices).HighlightStyle(new Style(Color.Yellow, Color.Red, Decoration.Bold))
                );

            return selection;
        }

        public void DisplayEquipmentMenu(Hero hero)
        {
            List<string> list = new();

            foreach (string name in Enum.GetNames(typeof(Slot)))
            {
                list.Add(name);
            }

            list.Add("Exit");

            var x = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .PageSize(10)
                    .AddChoices(list).HighlightStyle(new Style(Color.Yellow, Color.Red, Decoration.Bold))
            );

            switch (x)
            {
                case "Weapon":
                    {
                        AnsiConsole.Write(new Markup("Select your [red bold]Weapon[/]"));
                        AnsiConsole.WriteLine();

                        string[] options = Weapons.Values.Select(weapon => weapon.Name).ToArray()!;

                        var y = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .PageSize(10)
                                .AddChoices(options).HighlightStyle(new Style(Color.Yellow, Color.Red, Decoration.Bold))
                        );

                        AnsiConsole.WriteLine();

                        hero.Equip(Weapons[y]);
                        break;
                    }
                case "Head":
                    {
                        AnsiConsole.Write(new Markup("Select your [red bold]Head Armor[/]"));
                        AnsiConsole.WriteLine();

                        string[] options
                            = Armors.Values
                                .Where(armor => armor.EquipSlot == Slot.Head)
                                .Select(armor => armor.Name)
                                .ToArray()!;

                        var y = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .PageSize(10)
                                .AddChoices(options).HighlightStyle(new Style(Color.Yellow, Color.Red, Decoration.Bold))
                        );

                        AnsiConsole.WriteLine();

                        hero.Equip(Armors[y]);
                        break;
                    }
                case "Body":
                    {
                        AnsiConsole.Write(new Markup("Select your [red bold]Body Armor[/]"));
                        AnsiConsole.WriteLine();

                        string[] options
                            = Armors.Values
                                .Where(armor => armor.EquipSlot == Slot.Body)
                                .Select(armor => armor.Name)
                                .ToArray()!;

                        var y = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .PageSize(10)
                                .AddChoices(options).HighlightStyle(new Style(Color.Yellow, Color.Red, Decoration.Bold))
                        );

                        AnsiConsole.WriteLine();

                        hero.Equip(Armors[y]);
                        break;
                    }
                case "Legs":
                    {
                        AnsiConsole.Write(new Markup("Select your [red bold]Legs Armor[/]"));
                        AnsiConsole.WriteLine();

                        string[] options
                            = Armors.Values
                                .Where(armor => armor.EquipSlot == Slot.Legs)
                                .Select(armor => armor.Name)
                                .ToArray()!;

                        var y = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .PageSize(10)
                                .AddChoices(options).HighlightStyle(new Style(Color.Yellow, Color.Red, Decoration.Bold))
                        );

                        AnsiConsole.WriteLine();

                        hero.Equip(Armors[y]);
                        break;
                    }
                default:
                    break;
            }

            AnsiConsole.WriteLine();
        }
    }
}