using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {

    public int level;
    public int maxHealth;
    
    
    public PlayerData (Unit unit)
    {
        level = unit.unitLevel;
        maxHealth = unit.maxHP;
        

    }
}
