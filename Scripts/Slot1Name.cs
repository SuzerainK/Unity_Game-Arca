using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Slot1Name : MonoBehaviour
{
    private TextMeshProUGUI SN1;
    
    // Start is called before the first frame update
    void Start()
    {
        SN1 = GetComponent<TextMeshProUGUI>();
        if (PlayerPrefs.HasKey("SaveOneCSN"))
            SN1.text = PlayerPrefs.GetString("SaveOneCSN");
        else
            SN1.text = "Empty Slot";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
