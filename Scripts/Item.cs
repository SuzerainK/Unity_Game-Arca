using Arca.CharacterStats;

public class Character
{
    public SavedPlayerStats Strength;
}

public class Item
{
    
    public void Equip(Character c)
    {
        c.Strength.AddModifier(new StatModifier(10, StatModType.Flat, this));
        c.Strength.AddModifier(new StatModifier(10, StatModType.PercentMult, this));
    }

    public void Unequip(Character c)
    {
        c.Strength.RemoveAllModifiersFromSource(this);
    }
}
