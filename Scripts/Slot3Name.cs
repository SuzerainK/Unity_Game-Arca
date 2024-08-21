using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Slot3Name : MonoBehaviour
{
    private TextMeshProUGUI SN3;

    // Start is called before the first frame update
    void Start()
    {
        SN3 = GetComponent<TextMeshProUGUI>();
        if (PlayerPrefs.HasKey("SaveThrCSN"))
            SN3.text = PlayerPrefs.GetString("SaveThrCSN");
        else
            SN3.text = "Empty Slot";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
