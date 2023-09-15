using VideoGame.Src.Items;
using VideoGame.Src.Interfaces;

namespace VideoGame.Src.Heros
{
    class Archer : Hero, IHero
    {   
        public Archer(string name)
        {
            Name = name;
            Type = HeroType.Archer;
            LevelAttribute = new HeroAttribute(1, 7, 1);
            ValidWeaponTypes
                = new[] 
                {
                    WeaponType.Bow,
                };
            ValidArmorTypes
                = new[]
                {
                    ArmorType.Leather,
                    ArmorType.Mail
                };
        }

        public void LevelUp()
        {
            Level++;
            
            LevelAttribute!.IncreaseStat(new HeroAttribute(1, 5, 1));
        }

        public double CalculateHeroDamage()
        {
            double weaponDamage = 0.0;

            if (Equipment[Slot.Weapon] != null)
            {
                Weapon equippedWeapon = (Weapon)Equipment[Slot.Weapon]!;

                weaponDamage += (double)equippedWeapon.Damage!;
            }

            weaponDamage *= 1.0 + (double)LevelAttribute!.Dexterity! / 100.0;

            return weaponDamage;
        }
    }
}