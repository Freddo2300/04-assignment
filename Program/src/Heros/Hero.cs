using System.Text;
using Spectre.Console;

using VideoGame.Src.Items;

namespace VideoGame.Src.Heros
{

    enum HeroType
    {
        Wizard,
        Archer,
        SwashBuckler,
        Barbarian
    }

    abstract class Hero
    {
        public string? Name { get; set; }
        public int? Level { get; set; } = 1;
        public HeroType? Type { get; set; }
        public HeroAttribute? LevelAttribute { get; set; }
        public Dictionary<Slot, Item?> Equipment { get; set; }
                    = new()
                    {
                        {Slot.Head, null},
                        {Slot.Body, null},
                        {Slot.Legs, null},
                        {Slot.Weapon, null}
                    };
        public WeaponType[]? ValidWeaponTypes { get; set; }
        public ArmorType[]? ValidArmorTypes { get; set; }

        /// <summary>
        /// Overriden equip method to equip a weapon
        /// </summary>
        /// <param name="weapon"></param>
        public void Equip(Weapon weapon)
        {
            try
            {
                if (ValidWeaponTypes!.Any(w => w == weapon.Type))
                {
                    throw new InvalidWeaponException(
                        message: $"InvalidWeaponException: {weapon.Type} not allowed for hero type {Type}");
                }

                Equipment![Slot.Weapon] = weapon;
            }
            catch (InvalidWeaponException e)
            {
                AnsiConsole.Write(e.Message);
            }
            catch (Exception e)
            {
                AnsiConsole.Write(e.Message);
            }
        }

        /// <summary>
        /// Overriden equip method to equip an armor
        /// </summary>
        /// <param name="slot"></param>
        /// <param name="armor"></param>
        public void Equip(Slot slot, Armor armor)
        {
            try
            {
                if (ValidArmorTypes!.Any(a => a == armor.Type))
                {
                    throw new InvalidArmorException(
                        message: $"InvalidArmorException: {armor.Type} not allowed for hero type {Type}"
                    );
                }

                switch (slot)
                {
                    case Slot.Weapon:
                        {
                            throw new InvalidArmorException(
                                message: $"InvalidArmorException: cannot wear armor at slot {slot}"
                            );
                        }
                    case Slot.Body:
                        {
                            Equipment![Slot.Body] = armor;
                            break;
                        }
                    case Slot.Head:
                        {
                            Equipment![Slot.Head] = armor;
                            break;
                        }
                    case Slot.Legs:
                        {
                            Equipment![Slot.Legs] = armor;
                            break;
                        }
                    default: break;
                }
            }
            catch (InvalidArmorException e)
            {
                AnsiConsole.Write(e.Message);
            }
            catch (Exception e)
            {
                AnsiConsole.Write(e.Message);
            }
        }

        /// <summary>
        /// Calculate total hero attributes using formula:
        /// <para></para>
        /// Total = LevelAttributes + (Sum of ArmorAttribute for all Armor in Equipment)
        /// </summary>
        /// <returns></returns>
        public int CalculateHeroAttributes()
        {
            int totalHeroAttributes = LevelAttribute!.AttributeSum();

            foreach (KeyValuePair<Slot, Item?> pair in Equipment)
            {
                if (pair.Value != null && pair.Value.GetType() == typeof(Armor))
                {
                    Armor equippedArmor = (Armor)pair.Value;

                    totalHeroAttributes += equippedArmor.ArmorAttribute!.AttributeSum();
                }
            }

            return totalHeroAttributes;
        }

        public int[] GetTotalStats()
        {
            int totalStrength = 0;
            int totalDexterity = 0;
            int totalIntelligence = 0;

            foreach (KeyValuePair<Slot, Item?> pair in Equipment)
            {
                if (pair.Value != null && pair.Value.GetType() == typeof(Armor))
                {
                    Armor equippedArmor = (Armor)pair.Value;

                    totalStrength += (int)equippedArmor.ArmorAttribute!.Strength!;
                    totalDexterity += (int)equippedArmor.ArmorAttribute!.Dexterity!;
                    totalIntelligence += (int)equippedArmor.ArmorAttribute!.Intelligence!;
                }
            }

            return new[] {totalStrength, totalDexterity, totalIntelligence};
        }

        public void Display(double totalDamage)
        {   
            // Initialise AnsiTable
            Table table = new()
            {
                Title = new TableTitle(Name!)
            };

            // Add two blank table columns
            table.AddColumn("").LeftAligned();
            table.AddColumn("").RightAligned();

            table.AddRow("Type:", Type.ToString()!);    // Add Type
            table.AddRow("Level:", Level.ToString()!);  // Add Level
            table.HorizontalBorder();                   // Draw border
            
            // Get total stats (including armorstats)
            int[] totalStats = GetTotalStats();

            // Make barchartitems
            var items = new List<BarChartItem>
            {
                new("strength", (double)LevelAttribute!.Strength!, Color.Red),
                new("dexterity", (double)LevelAttribute!.Dexterity!, Color.Green),
                new("intelligence", (double)LevelAttribute!.Intelligence!, Color.Blue),
            };

            // Add barchartitems to barchart
            var barChart = new BarChart().AddItems(items);

            // Add stats and barchart
            table.AddRow(new Text("Stats:"), barChart);

            // Finally, add damage
            table.AddRow("Damage", totalDamage.ToString());

            // Display
            AnsiConsole.Write(table);
        }

        /// <summary>
        /// Custom exception for handling attempted equipping invalid weapon types
        /// </summary>
        public class InvalidWeaponException : Exception
        {
            public InvalidWeaponException() { }

            public InvalidWeaponException(string message) : base(message) { }

            public InvalidWeaponException(string message, Exception inner) : base(message, inner) { }
        }

        /// <summary>
        /// Custom exception for handling attempted equipping invalid armor types
        /// </summary>
        public class InvalidArmorException : Exception
        {
            public InvalidArmorException() { }

            public InvalidArmorException(string message) : base(message) { }

            public InvalidArmorException(string message, Exception inner) : base(message, inner) { }
        }
    }
}