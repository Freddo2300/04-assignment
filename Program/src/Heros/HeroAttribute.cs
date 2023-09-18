namespace VideoGame.Src.Heros
{
    public class HeroAttribute
    {
        public int? Strength { get; set; }
        public int? Dexterity { get; set; }
        public int? Intelligence { get; set; }

        public Dictionary<string, int> GetAttributes()
        {
            return new Dictionary<string, int>()
            {
                {"strength:", (int)Strength!},
                {"dexterity:", (int)Dexterity!},
                {"intelligence:", (int)Intelligence!},
            };
        }

        public int AttributeSum()
        {
            return (int)(Strength + Dexterity + Intelligence)!;
        }

        public void IncreaseStat(HeroAttribute attribute)
        {
            Strength += attribute.Strength;
            Dexterity += attribute.Dexterity;
            Intelligence += attribute.Intelligence;
        }

        public HeroAttribute()
        {
            Strength = null;
            Dexterity = null;
            Intelligence = null;
        }

        public HeroAttribute(int strength, int dexterity, int intelligence)
        {
            Strength = strength;
            Dexterity = dexterity;
            Intelligence = intelligence;
        }
    }
}