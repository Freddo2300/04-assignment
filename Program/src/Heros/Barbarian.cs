using Spectre.Console;

using VideoGame.Src.Items;
using VideoGame.Src.Interfaces;

namespace VideoGame.Src.Heros
{
    class Barbarian : Hero, IHero
    {   
        public Barbarian(string name)
        {
            Name = name;
            Type = HeroType.Barbarian;
            LevelAttribute = new HeroAttribute(5, 2, 1);
            ValidWeaponTypes
                = new[] 
                {
                    WeaponType.Hatchet,
                    WeaponType.Mace,
                    WeaponType.Sword,
                };
            ValidArmorTypes
                = new[]
                {
                    ArmorType.Mail,
                    ArmorType.Plate,
                };
        }

        public void LevelUp()
        {
            Level++;

            LevelAttribute!.IncreaseStat(new HeroAttribute(3, 2, 1));

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

            weaponDamage *= 1.0 + GetTotalStats()[0] / 100.0;

            return weaponDamage;
        }
    }
}