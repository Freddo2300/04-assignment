using VideoGame.Src.Heros;

namespace VideoGame.Src.Items
{   
    enum ArmorType
    {
        Cloth,
        Leather,
        Mail,
        Plate
    }

    class Armor : Item
    {
        public ArmorType? Type { get; set; }
        public HeroAttribute? ArmorAttribute { get; set; }
    }
}