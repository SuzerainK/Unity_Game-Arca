using UnityEngine;

public class EnemyColliderDetector : MonoBehaviour
{
    public bool parryCollission;

    BattleSystem battleSystem;

    // Start is called before the first frame update
    void Start()
    {
        battleSystem = GameObject.FindGameObjectWithTag("Battle System").GetComponent<BattleSystem>();
        parryCollission = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (parryCollission)
        {
            battleSystem.ParryDetection(true);
        }
        else
        {
            battleSystem.ParryDetection(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CombatPlayer")
        {
            Debug.Log("Collision detected.");
            parryCollission = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CombatPlayer")
        {
            parryCollission = false;
        }
    }
}
