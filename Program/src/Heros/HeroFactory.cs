using VideoGame.Src.Interfaces;

namespace VideoGame.Src.Heros
{
    public class HeroFactory
    {
        public static IHero CreateHero(HeroType heroType, string name)
        {
            switch (heroType)
            {
                case HeroType.Archer:
                    return new Archer(name);
                case HeroType.Barbarian:
                    return new Barbarian(name);
                case HeroType.SwashBuckler:
                    return new SwashBuckler(name);
                case HeroType.Wizard:
                    return new Wizard(name);
                default:
                throw new ArgumentException($"{heroType} is an invalid hero type");
            }
        }
    }
}