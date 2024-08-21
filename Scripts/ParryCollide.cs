using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryCollide : MonoBehaviour
{

    BoxCollider2D bcParry;
    
    void Start()
    {
        bcParry = GetComponent<BoxCollider2D>();
        bcParry.enabled = false;
    }

    void ParryStart()
    {
        bcParry.enabled = true;
        
        Debug.Log("Parry!");
    }

    void ParryOver()
    {
        bcParry.enabled = false;
    }
}
