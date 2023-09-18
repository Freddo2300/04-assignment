namespace VideoGame.Src.Items
{ 
    public enum WeaponType
    {
        Hatchet,
        Bow,
        CrossBow,
        Dagger,
        Mace,
        Staff,
        Sword,
        Wand
    }

    public class Weapon : Item
    {
        public double? Damage { get; set; }
        public WeaponType? Type { get; set; }

        public Weapon(string name, int requiredLevel, double damage, WeaponType type)
        {
            Name = name; 
            RequiredLevel = requiredLevel;
            EquipSlot = Slot.Weapon;
            Damage = damage;
            Type = type;
        }
    } 
}