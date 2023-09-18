using System.Text;
using System.Globalization;
using Spectre.Console;
using Spectre.Console.Rendering;
using VideoGame.Src.Items;

namespace VideoGame.Src.Heros
{

    public enum HeroType
    {
        Wizard,
        Archer,
        SwashBuckler,
        Barbarian
    }

    public abstract class Hero
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
                if (!ValidWeaponTypes!.Any(w => w == weapon.Type))
                {
                    throw new InvalidWeaponException(
                        message: $"InvalidWeaponException: {weapon.Type} not allowed for hero type {Type}"
                    );
                }

                if (Level < weapon.RequiredLevel)
                {
                    throw new InvalidWeaponException(
                        message: $"InvalidWeaponException: Required level {weapon.RequiredLevel} is too high. Keep training!"
                    );
                }

                Equipment![Slot.Weapon] = weapon;

                AnsiConsole.Write($"Successfully equipped weapon: {weapon.Name}");
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
        public void Equip(Armor armor)
        {
            try
            {
                if (!ValidArmorTypes!.Any(a => a == armor.Type))
                {
                    throw new InvalidArmorException(
                        message: $"InvalidArmorException: {armor.Type} not allowed for hero type {Type}"
                    );
                }

                if (Level < armor.RequiredLevel)
                {
                    throw new InvalidArmorException(
                        message: $"Required level {armor.RequiredLevel} is too high. Keep training!"
                    );
                }

                switch (armor.EquipSlot)
                {
                    case Slot.Head:
                    {
                        Equipment[Slot.Head] = armor;
                        break;
                    }
                    case Slot.Body:
                    {   
                        Equipment[Slot.Body] = armor;
                        break;
                    }
                    case Slot.Legs:
                    {
                        Equipment[Slot.Legs] = armor;
                        break;
                    }
                }

                AnsiConsole.Write($"Successfully equipped armor: {armor.Name}");
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
            int totalStrength = (int)LevelAttribute!.Strength!;
            int totalDexterity = (int)LevelAttribute!.Dexterity!;
            int totalIntelligence = (int)LevelAttribute!.Intelligence!;

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

            return new[] { totalStrength, totalDexterity, totalIntelligence };
        }

        public void Display(double totalDamage)
        {
            StringBuilder sb = new();

            int[] totalStats = GetTotalStats();

            CultureInfo cultureInfo = new("en-GB");
            string format = "|{0,-20}{1,20}|";

            sb.AppendFormat(cultureInfo, "*{0, -18}{1,18}*", new string('-', 20), new string('-', 20));
            sb.AppendLine();
            sb.AppendFormat(cultureInfo, "|{0, -9}{1,31}|", "CHARACTER", new string('+', 31));
            sb.AppendLine();
            sb.AppendFormat(cultureInfo, format, "Name", Name);
            sb.AppendLine();
            sb.AppendFormat(cultureInfo, format, "Type", Type);
            sb.AppendLine();
            sb.AppendFormat(cultureInfo, format, "Level", Level);
            sb.AppendLine();
            sb.AppendFormat(cultureInfo, format, new string('-', 20), new string('-', 20));
            sb.AppendLine();
            sb.AppendFormat(cultureInfo, "|{0, -9}{1,31}|", "EQUIPMENT", new string('+', 31));
            sb.AppendLine();
            sb.AppendFormat(cultureInfo, format, "Weapon", Equipment[Slot.Weapon]?.Name ?? null);
            sb.AppendLine();
            sb.AppendFormat(cultureInfo, format, "Head", Equipment[Slot.Head]?.Name ?? null);
            sb.AppendLine();
            sb.AppendFormat(cultureInfo, format, "Body", Equipment[Slot.Body]?.Name ?? null);
            sb.AppendLine();
            sb.AppendFormat(cultureInfo, format, "Legs", Equipment[Slot.Legs]?.Name ?? null);
            sb.AppendLine();
            sb.AppendFormat(cultureInfo, format, new string('-', 20), new string('-', 20));
            sb.AppendLine();
            sb.AppendFormat(cultureInfo, "|{0, -5}{1,35}|", "STATS", new string('+', 35));
            sb.AppendLine();
            sb.AppendFormat(cultureInfo, "|{0, -20}{1,20:N2}|", "Damage", totalDamage);
            sb.AppendLine();
            sb.AppendFormat(cultureInfo, format, "Strength", $"{LevelAttribute!.Strength} (tot: {totalStats[0]})");
            sb.AppendLine();
            sb.AppendFormat(cultureInfo, format, "Dexterity", $"{LevelAttribute!.Dexterity} (tot: {totalStats[1]})");
            sb.AppendLine();
            sb.AppendFormat(cultureInfo, format, "Intelligence", $"{LevelAttribute!.Intelligence} (tot: {totalStats[2]})");
            sb.AppendLine();
            sb.AppendFormat(cultureInfo, "|{0,40}|", new string(' ', 40));
            sb.AppendLine();
            sb.AppendFormat(cultureInfo, format, "Total", totalStats.Sum());
            sb.AppendLine();
            sb.AppendFormat(cultureInfo, "*{0, -18}{1,18}*", new string('-', 20), new string('-', 20));

            Console.WriteLine(sb);
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