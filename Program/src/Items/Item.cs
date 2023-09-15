namespace VideoGame.Src.Items
{
    public enum Slot
    {
        Weapon,
        Head,
        Body,
        Legs
    }

    public abstract class Item
    {
        public string? Name { get; set; }
        public int? RequiredLevel { get; set; }
        public Slot? EquipSlot { get; set; }

        /*
        public Item() {}

        public Item(string name, int requiredLevel, Slot equipSlot)
        {
            Name = name;
            RequiredLevel = requiredLevel;
            EquipSlot = equipSlot;
        }
        */
    }
}