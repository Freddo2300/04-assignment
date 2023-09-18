using Spectre.Console;

using VideoGame.Src.Heros;
using VideoGame.Src.Items;
using VideoGame.Src.Interfaces;

namespace Test;

public class TestHero : Hero, IHero
{   
    public TestHero(HeroType type)
    {
        switch (type)
        {
            case HeroType.Archer:
            {
                LevelAttribute = new HeroAttribute(1, 7, 1);
                break;
            }
            case HeroType.Barbarian:
            {
                LevelAttribute = new HeroAttribute(5, 2, 1);
                break;
            }
            case HeroType.SwashBuckler:
            {
                LevelAttribute = new HeroAttribute(2, 6, 1);
                break;
            }
            case HeroType.Wizard:
            {
                LevelAttribute = new HeroAttribute(1, 1, 8);
                break;
            }
        }
    }

    public TestHero(
        string name,
        HeroType type,
        HeroAttribute attribute,
        WeaponType[] validWeaponTypes,
        ArmorType[] validArmorTypes)
    {
        Name = name;
        Type = type;
        LevelAttribute = attribute;
        ValidWeaponTypes = validWeaponTypes;
        ValidArmorTypes = validArmorTypes;
    }

    public double CalculateHeroDamage()
    {
        double weaponDamage = 0.0;

        if (Equipment[Slot.Weapon] != null)
        {
            Weapon equippedWeapon = (Weapon)Equipment[Slot.Weapon]!;

            weaponDamage += (double)equippedWeapon.Damage!;
        }
        else
        {
            weaponDamage += 1.0;
        }

        if (Type == HeroType.Archer)
            weaponDamage *= 1.0 + GetTotalStats()[1] / 100.0;
        else if (Type == HeroType.Barbarian)
            weaponDamage *= 1.0 + GetTotalStats()[0] / 100.0;
        else if (Type == HeroType.SwashBuckler)
            weaponDamage *= 1.0 + GetTotalStats()[1] / 100.0;
        else
            weaponDamage *= 1.0 + GetTotalStats()[2] / 100.0;

        return weaponDamage;
    }

    public void LevelUp()
    {
        Level++;

        if (Type == HeroType.Archer)
            LevelAttribute!.IncreaseStat(new HeroAttribute(1, 5, 1));
        else if (Type == HeroType.Barbarian)
            LevelAttribute!.IncreaseStat(new HeroAttribute(3, 2, 1));
        else if (Type == HeroType.SwashBuckler)
            LevelAttribute!.IncreaseStat(new HeroAttribute(1, 4, 1));
        else
            LevelAttribute!.IncreaseStat(new HeroAttribute(1, 1, 5));

        AnsiConsole.WriteLine($"Level Up: {Level - 1} => {Level}");
        AnsiConsole.WriteLine($"\tSTR: {LevelAttribute.Strength}\n\tDEX: {LevelAttribute.Dexterity}\n\tINT: {LevelAttribute.Intelligence}");
    }
}

public class UnitTest
{
    [Fact]
    public void BarbarianCalculateDamageWithoutWeapon()
    {
        // Arrange
        double expected = 1.0 * (1.0 + (5.0 / 100.0)); // 1.05

        TestHero barbarian = new(
           name: "Lord",
           type: HeroType.Barbarian,
           attribute: new HeroAttribute(5, 2, 1),
           validWeaponTypes: new WeaponType[]
           {
                WeaponType.Hatchet,
                WeaponType.Mace,
                WeaponType.Sword,
           },
           validArmorTypes: new ArmorType[]
           {
                ArmorType.Mail,
                ArmorType.Plate
           }
       );

        // Act
        double actual = barbarian.CalculateHeroDamage();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void BarbarianCalculateDamageWithValidWeapon()
    {
        // Arrange
        double expected = 2.0 * (1.0 + (5.0 / 100.0));

        TestHero barbarian = new(
           name: "Lord",
           type: HeroType.Barbarian,
           attribute: new HeroAttribute(5, 2, 1),
           validWeaponTypes: new WeaponType[]
           {
                WeaponType.Hatchet,
                WeaponType.Mace,
                WeaponType.Sword,
           },
           validArmorTypes: new ArmorType[]
           {
                ArmorType.Mail,
                ArmorType.Plate
           }
       );

        Weapon hatchet = new(name: "Common Hatchet", requiredLevel: 1, damage: 2, type: WeaponType.Hatchet);

        // Act
        barbarian.Equip(hatchet);
        double actual = barbarian.CalculateHeroDamage();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void BarbarianCalculateDamageWithValidWeaponAndArmor()
    {
        // Arrange
        double expected = 2.0 * (1.0 + ((5.0 + 1.0) / 100.0));

        TestHero barbarian = new(
           name: "Lord",
           type: HeroType.Barbarian,
           attribute: new HeroAttribute(5, 2, 1),
           validWeaponTypes: new WeaponType[]
           {
                WeaponType.Hatchet,
                WeaponType.Mace,
                WeaponType.Sword,
           },
           validArmorTypes: new ArmorType[]
           {
                ArmorType.Mail,
                ArmorType.Plate
           }
       );

        Weapon hatchet = new(name: "Common Hatchet", requiredLevel: 1, damage: 2, type: WeaponType.Hatchet);
        Armor chestPlate = new(
            name: "Common Plate Chest",
            requiredLevel: 1,
            equipSlot: Slot.Body,
            type: ArmorType.Plate,
            armorAttribute: new HeroAttribute(1, 0, 0)
            );

        // Act
        barbarian.Equip(hatchet);
        barbarian.Equip(chestPlate);
        double actual = barbarian.CalculateHeroDamage();

        // Assert
        Assert.Equal(expected, actual);
    }

    // Test 1: Check if name is correct
    [Fact]
    public void HeroIsCreatedWithCorrectName()
    {
        // Arrange
        string expected = "Thomas KahlenBerg";

        TestHero testHero
            = new(
                name: "Thomas KahlenBerg",
                type: HeroType.Wizard, attribute:
                new HeroAttribute(1, 1, 8),
                validWeaponTypes: new WeaponType[] { WeaponType.Staff, WeaponType.Wand },
                validArmorTypes: new ArmorType[] { ArmorType.Cloth }
            );

        // Act
        string actual = testHero.Name!;

        // Assert
        Assert.Equal(expected, actual);
    }

    // Test 2: Check if level up function works, for each hero type:
    [Fact]
    public void LevelUpTest()         // GENERIC LEVEL UP TEST
    {
        // Arrange
        int expected = 2;

        TestHero testHero = new(HeroType.Archer);

        // Act
        testHero.LevelUp();
        int actual = (int)testHero.Level!;

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ThrowInvalidWeaponExceptionOnTypeTest()
    {
        // Arrange
        TestHero testHero 
            = new(
                name: "Frederik", 
                type: HeroType.Wizard,
                attribute: new HeroAttribute(1, 1, 8),
                validWeaponTypes: new WeaponType[] {WeaponType.Staff, WeaponType.Wand},
                validArmorTypes: new ArmorType[] {ArmorType.Cloth}
            );

        Weapon weapon = new(name: "Common weapon", requiredLevel: 1, damage: 5.0, type: WeaponType.Mace);

        // Assert
        Assert.Throws<Hero.InvalidWeaponException>(() => testHero.Equip(weapon));
    }
}