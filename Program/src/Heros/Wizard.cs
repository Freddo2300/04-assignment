using Spectre.Console;

using VideoGame.Src.Items;
using VideoGame.Src.Interfaces;

namespace VideoGame.Src.Heros
{
    class Wizard : Hero, IHero
    {   
        public Wizard(string name)
        {
            Name = name;
            Type = HeroType.Wizard;
            LevelAttribute = new HeroAttribute(1, 1, 8);
            ValidWeaponTypes
                = new[]
                {
                    WeaponType.Wand,
                    WeaponType.Staff,
                };
            ValidArmorTypes
                = new[]
                {
                    ArmorType.Cloth
                };
        }

        public void LevelUp()
        {
            Level++;

            LevelAttribute!.IncreaseStat(new HeroAttribute(1, 1, 5));

            AnsiConsole.WriteLine($"Level Up: {Level - 1} => {Level}");
            AnsiConsole.WriteLine($"\tSTR: {LevelAttribute.Strength}\n\tDEX: {LevelAttribute.Dexterity}\n\tINT: {LevelAttribute.Intelligence}");
        }

        public double CalculateHeroDamage()
        {
            double weaponDamage = 0.0;

            if (Equipment[Slot.Weapon] != null)
            {
                Weapon equippedWeapon = (Weapon)Equipment[Slot.Weapon]!;

                weaponDamage += (double)equippedWeapon.Damage!;
            }

            weaponDamage *= 1.0 + GetTotalStats()[2] / 100.0;

            return weaponDamage;
        }
    }
}