using VideoGame.Src.Heros;

namespace VideoGame.Src.Items
{
    public enum ArmorType
    {
        Cloth,
        Leather,
        Mail,
        Plate
    }

    public class Armor : Item
    {
        public ArmorType? Type { get; set; }
        public HeroAttribute? ArmorAttribute { get; set; }
        public Armor(string name, int requiredLevel, Slot equipSlot, ArmorType type, HeroAttribute armorAttribute)
        {
            Name = name;
            RequiredLevel = requiredLevel;
            EquipSlot = equipSlot;
            Type = type;
            ArmorAttribute = armorAttribute;
        }

    }

}