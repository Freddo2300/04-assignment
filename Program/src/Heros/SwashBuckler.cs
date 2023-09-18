using Spectre.Console;

using VideoGame.Src.Items;
using VideoGame.Src.Interfaces;

namespace VideoGame.Src.Heros
{
    class SwashBuckler : Hero, IHero
    {   
        public SwashBuckler(string name)
        {
            Name = name;
            Type = HeroType.SwashBuckler;
            LevelAttribute = new HeroAttribute(2, 6, 1);
            ValidWeaponTypes
                = new[]
                {
                    WeaponType.Dagger,
                    WeaponType.Sword,
                };
            ValidArmorTypes
                = new[]
                {
                    ArmorType.Leather,
                    ArmorType.Mail,
                };
        }

        public void LevelUp()
        {
            Level++;

            LevelAttribute!.IncreaseStat(new HeroAttribute(1, 4, 1));

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

            weaponDamage *= 1.0 + GetTotalStats()[1] / 100.0;

            return weaponDamage;
        }
    }
}