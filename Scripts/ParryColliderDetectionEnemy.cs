using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryColliderDetectionEnemy : MonoBehaviour
{
    private bool playerParryUp;

    private void Awake()
    {

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            playerParryUp = true;
            Debug.Log("Parry Up true");
        }
    }
}
