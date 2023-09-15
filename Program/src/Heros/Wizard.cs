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
        }

        public double CalculateHeroDamage()
        {
            double weaponDamage = 0.0;

            if (Equipment[Slot.Weapon] != null)
            {
                Weapon equippedWeapon = (Weapon)Equipment[Slot.Weapon]!;

                weaponDamage += (double)equippedWeapon.Damage!;
            }

            weaponDamage *= 1.0 + (double)LevelAttribute!.Intelligence! / 100.0;

            return weaponDamage;
        }
    }
}