using UnityEngine;

public class BulletFire : MonoBehaviour
{
    BulletFireLine BFL;
    public GameObject BulletFireLine;

    // Start is called before the first frame update
    void Start()
    {
        BFL = BulletFireLine.GetComponent<BulletFireLine>();
        //playerCharacter = GameObject.FindGameObjectWithTag("Player");
        //playerUnit = GameObject.FindGameObjectWithTag("Player").GetComponent<Unit>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire()
    {
        BFL.DamagePlayer();
    }
}
