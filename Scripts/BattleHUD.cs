using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BattleHUD : MonoBehaviour
{
    //Editable Objects in the unity interface
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public Slider hpSlider;

    //Updates the display of the HUD
    public void SetHUD(Unit unit)
    {
        StartCoroutine(Set_HUD(unit));
    }

    //Updates the current HP of enemy or player
    public void SetHP(int hp)
    {
        StartCoroutine(Set_HP(hp));
    }

    IEnumerator Set_HUD(Unit unit)
    {
        yield return new WaitForSeconds(1f);
        nameText.text = unit.unitName;
        levelText.text = "Lv " + unit.unitLevel;
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;
    }

    IEnumerator Set_HP(int hp)
    {
        yield return new WaitForSeconds(1f);
        hpSlider.value = hp;
    }
}
