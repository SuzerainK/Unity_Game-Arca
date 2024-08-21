using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Slot3Difficulty : MonoBehaviour
{
    private TextMeshProUGUI SN3D;

    // Start is called before the first frame update
    void Start()
    {
        SN3D = GetComponent<TextMeshProUGUI>();
        if (PlayerPrefs.HasKey("SaveThrCD"))
            SN3D.text = PlayerPrefs.GetString("SaveThrCD");
        else
            SN3D.text = "Unknown Difficulty";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
