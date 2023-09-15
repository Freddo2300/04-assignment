namespace VideoGame.Src.Interfaces
{   
    /// <summary>
    /// Hero interface to implement specialised behaviour inside hero sub-classes.
    /// </summary>
    interface IHero
    {   
        /// <summary>
        /// Level up a hero.
        /// </summary>
        void LevelUp();

        /// <summary>
        /// Calculate hero damage using formula:
        /// <para></para>
        /// Damage = WeaponDamage * (1 + DamagingAttribute / 100)
        /// </summary>
        double CalculateHeroDamage();
    }
}