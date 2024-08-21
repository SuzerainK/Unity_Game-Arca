using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBGREDBLACK : MonoBehaviour
{
    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(0f, transform.position.y), 10f * Time.deltaTime);
    }
}
