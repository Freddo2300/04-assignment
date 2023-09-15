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

        public Weapon()
        {
            EquipSlot = Slot.Weapon;
        }
    } 
}