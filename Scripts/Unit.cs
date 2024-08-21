using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Unit : MonoBehaviour
{
    public Animator animate;
    public string unitName;
    public int unitLevel;

    public int damage;

    public int maxHP;
    public int currentHP;

    public string unitType;

    private int additionalDmg = 0;
    private int additionalHpMax = 0;

    private int healthBarMonitor;

    public int UnwaveringAttacker = 0;
    public int Toughness = 0;
    public int RagingBull = 0;
    public int BalancedProwess = 0;
    public int MendWounds = 0;


    
    //Physical Attack
    void Awake()
    {
        if(gameObject.tag == "CombatPlayer")
        {
            if(SceneManager.GetActiveScene().buildIndex < 12)
            {
                UnwaveringAttacker = PlayerPrefs.GetInt("UnwaveringAttacker");
                Toughness = PlayerPrefs.GetInt("Toughness");
                RagingBull = PlayerPrefs.GetInt("RagingBull");
                BalancedProwess = PlayerPrefs.GetInt("BalancedProwess");
                MendWounds = PlayerPrefs.GetInt("MendWounds");
                unitLevel = PlayerPrefs.GetInt("PlayerLevel");

                additionalDmg = (unitLevel - 1) * 5;
                additionalHpMax = (unitLevel - 1) * 10;

                currentHP = PlayerPrefs.GetInt("HealthBarRaw") + (int)additionalHpMax;
                maxHP = PlayerPrefs.GetInt("MaxHealthPointRaw") + (int)additionalHpMax;

                damage = PlayerPrefs.GetInt("PlayerRawDamage") + (int)additionalDmg;

                damage += (int)((UnwaveringAttacker * (damage * .2)) 
                    + (BalancedProwess * (damage *.1)) + (-Toughness * (damage * .05)));
                maxHP += (int)((-UnwaveringAttacker * (maxHP * .1)) 
                    + (Toughness * (maxHP * .3)) + (BalancedProwess * (maxHP * .1)));

                Debug.Log("FromUnitScript");
                Debug.Log(unitLevel);
                Debug.Log(currentHP);
            }
            else
            {
                unitLevel = PlayerPrefs.GetInt("SaveTempPL");

                additionalDmg = (unitLevel - 1) * 5;
                additionalHpMax = (unitLevel - 1) * 10;


                currentHP = PlayerPrefs.GetInt("SaveTempHPR") + additionalHpMax;
                maxHP = PlayerPrefs.GetInt("SaveTempMHPR") + additionalHpMax;


                if (SceneManager.GetActiveScene().buildIndex > 12)
                {
                    currentHP = maxHP;
                }

                damage = PlayerPrefs.GetInt("SaveTempPDR") + additionalDmg;
            }
            
        }
    }

    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
            return true;
        else
            return false;
    }

    //Magical Attack
    public bool TakeDamageMag(int dmg)
    {
        currentHP -= dmg/2 + dmg*2;

        if (currentHP <= 0)
            return true;
        else
            return false;
    }

    public bool HealHealth(int dmg)
    {
        currentHP += dmg;

        if(currentHP > maxHP)
        {
            healthBarMonitor = currentHP - maxHP;
            currentHP -= healthBarMonitor;
        }

        if (currentHP <= 0)
            return true;
        else
            return false;

    }

    public bool LowAttack(int dmg)
    {
        currentHP -= dmg * 2;

        if (currentHP <= 0)
            return true;
        else
            return false;
    }

    public bool LowAttackHalved(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
            return true;
        else
            return false;
    }

    public bool SwordSummon(int dmg)
    {
        currentHP -= dmg * 3;

        if (currentHP <= 0)
            return true;
        else
            return false;
    }

    public bool JudgementCut(int dmg)
    {
        currentHP -= dmg * 5;

        if (currentHP <= 0)
            return true;
        else
            return false;
    }

    public bool JudgementCutHalved(int dmg)
    {
        currentHP -= dmg * 5;

        if (currentHP <= 0)
            return true;
        else
            return false;
    }

    public bool RageAttack(int dmg)
    {
        currentHP -= dmg * 7;

        if (currentHP <= 0)
            return true;
        else
            return false;


    }



}
