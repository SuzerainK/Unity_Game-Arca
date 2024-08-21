using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PontifexMove : MonoBehaviour
{
    //[SerializeField] private float npcSpeed = 2;

    public GameObject NPCAvatar;
    public Animator NPCanimate;

    public GameObject firstStep;
    public GameObject secondStep;
    public GameObject thirdStep;
    public GameObject fourthStep;
    public GameObject fifthStep;

    [SerializeField] private float npc_x;
    [SerializeField] private float npc_y;

    public void MoveNow(string step)
    {
        switch (step)
        {
            case "firstStep":
                firstStep.GetComponent<BoxCollider2D>().enabled = true;
                npc_x = -81.68f;
                npc_y = -101.59f;
                break;
            case "secondStep":
                secondStep.GetComponent<BoxCollider2D>().enabled = true;
                npc_x = -66.32f;
                npc_y = -95.6f;
                break;
            case "thirdStep":
                thirdStep.GetComponent<BoxCollider2D>().enabled = true;
                npc_x = -52.31f;
                npc_y = -89.6f;
                break;
            case "fourthStep":
                fourthStep.GetComponent<BoxCollider2D>().enabled = true;
                npc_x = -38.49f;
                npc_y = -83.63f;
                break;
            case "fifthStep":
                fifthStep.GetComponent<BoxCollider2D>().enabled = true;
                npc_x = -9.6f;
                npc_y = -77.59f;
                break;
            default:
                Debug.Log("Error in Movement NPC");
                break;
        }
    }


    public IEnumerator npcChangeLocation()
    {

        NPCanimate.Play("NPC_Pontifex_VanishStart");
        yield return new WaitForSeconds(0.2f);
        NPCAvatar.transform.position = new Vector2(npc_x, npc_y);
        NPCanimate.Play("NPC_Pontifex_VanishEnd");
    }
}
