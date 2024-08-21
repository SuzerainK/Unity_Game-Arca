using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Slot2Name : MonoBehaviour
{
    private TextMeshProUGUI SN2;

    // Start is called before the first frame update
    void Start()
    {
        SN2 = GetComponent<TextMeshProUGUI>();
        if (PlayerPrefs.HasKey("SaveTwoCSN"))
            SN2.text = PlayerPrefs.GetString("SaveTwoCSN");
        else
            SN2.text = "Empty Slot";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
