using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RageSystemHUD : MonoBehaviour
{
    
 
    public Slider rageSlider;

    //Updates the display of the HUD
    public void SetRageHUD(int rage)
    {
        rageSlider.maxValue = 100;
        rageSlider.value = rage;
    }

}
