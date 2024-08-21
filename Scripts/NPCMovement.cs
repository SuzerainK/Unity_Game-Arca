using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    private Animator NPCanim;
    float Velocity_X;

    Rigidbody2D RB;
    private void Awake()
    {
        NPCanim = GetComponent<Animator>();
        RB = GetComponent<Rigidbody2D>();
        

    }

    private void Update()
    {
        Velocity_X = RB.velocity.x * 2;
        Debug.Log(Velocity_X);

        if (Velocity_X > 0f)
        {
            Debug.Log("reached first condition");
            NPCanim.SetBool("Walking", true);
            NPCanim.SetFloat("xVelocity", Mathf.Abs(RB.velocity.x));
        }
        else if (Velocity_X < 0f)
        {
            Debug.Log("reached second condition");
            NPCanim.SetBool("Walking", true);
            transform.localScale = new Vector3(-0.27f, 0.27f, 1f);
            NPCanim.SetFloat("xVelocity", Mathf.Abs(RB.velocity.x));
        }
        else if (Velocity_X == 0f)
        {
            Debug.Log("reached third condition");
            NPCanim.SetBool("Walking", false);
            NPCanim.SetFloat("xVelocity", Mathf.Abs(RB.velocity.x));
        }
    }
}
