using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PontifexMoveExecute : MonoBehaviour
{
    //NOTE: Currently only able to move Pontifex Sprite to the right.
    [SerializeField] private float NPCSpeed = 10;
    public GameObject NPCAvatar;
    private GameObject PMover;
    public GameObject Target;

    private bool moveQueue;
    private float offset = 73.83f;

    public Animator NPCanimate;

    PontifexMove pontifexMove;
    void Start()
    {
        PMover = GameObject.FindGameObjectWithTag("Step");
        pontifexMove = PMover.GetComponent<PontifexMove>();
        moveQueue = false;
    }

    void Update()
    {
        //Makes the NPC Move to the right
        if (moveQueue == true)
        {
            //Detects if the NPC reached its destination to stop walking
            if(NPCAvatar.transform.position.x <= (Target.transform.position.x + offset))
            {
                NPCAvatar.transform.Translate(Vector2.right * NPCSpeed * Time.deltaTime);
            }
            else
            {
                NPCSpeed = 0;
                NPCAvatar.transform.Translate(Vector2.right * NPCSpeed * Time.deltaTime);
                NPCanimate.SetFloat("xVelocity", 0);
            }
            
            
        }
        
    }

    //Works to detect if the collider collides with the player collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        NPCanimate.SetFloat("xVelocity", 2);
        if (collision.gameObject.tag == "Player")
        {
            moveQueue = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        NPCanimate.SetFloat("xVelocity", 0);
        if (collision.gameObject.tag == "Player")
        {
            moveQueue = false;
            StartCoroutine(pontifexMove.npcChangeLocation());
        }
    }
}
