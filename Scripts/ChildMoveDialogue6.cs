using UnityEngine;

public class ChildMoveDialogue6 : MonoBehaviour
{
    public GameObject ChildSprite;
    public GameObject Endpoint;

    private bool childQueue;
    private float offset = 10.2f;

    public Animator childAnimator;

    private float ChildSpeed = 2f;

    void Start()
    {
        childQueue = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (childQueue != false)
        {
            
            if (ChildSprite.transform.position.x > (Endpoint.transform.position.x + offset))
            {
                ChildSprite.transform.Translate(Vector2.left * ChildSpeed * Time.deltaTime);
                childAnimator.SetFloat("xVelocity", 2);
            }
            else
            {
                ChildSpeed = 0f;
                childAnimator.SetFloat("xVelocity", 0);
            }
        }
    }

    public void ChildQueue(string child_Queue)
    {
        if(child_Queue == "Go")
        {
            childQueue = true;
        }
    }

}
