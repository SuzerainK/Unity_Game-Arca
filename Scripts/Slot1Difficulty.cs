using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Slot1Difficulty : MonoBehaviour
{
    private TextMeshProUGUI SN1D;
    
    // Start is called before the first frame update
    void Start()
    {
        SN1D = GetComponent<TextMeshProUGUI>();
        if (PlayerPrefs.HasKey("SaveOneCD"))
            SN1D.text = PlayerPrefs.GetString("SaveOneCD");
        else
            SN1D.text = "Unknown Difficulty";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
