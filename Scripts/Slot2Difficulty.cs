using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Slot2Difficulty : MonoBehaviour
{
    private TextMeshProUGUI SN2D;

    // Start is called before the first frame update
    void Start()
    {
        SN2D = GetComponent<TextMeshProUGUI>();
        if (PlayerPrefs.HasKey("SaveTwoCD"))
            SN2D.text = PlayerPrefs.GetString("SaveTwoCD");
        else
            SN2D.text = "Unknown Difficulty";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
