using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDestroy : MonoBehaviour
{
    public int mobNum;

    public GameObject magus1;
    public Transform magus1p;
    public GameObject magus2;
    public Transform magus2p;
    public GameObject herald1;
    public Transform herald1p;
    public GameObject magus3;
    public Transform magus3p;
    public GameObject herald2;
    public Transform herald2p;
    public static bool magus1Exist = true;
    public static bool magus2Exist = true;
    public static bool herald1Exist = true;
    public static bool magus3Exist = true;
    public static bool herald2Exist = true;
    
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("NewStage") == 1)
        {
            magus1Exist = true;
            magus2Exist = true;
            herald1Exist = true;
            magus3Exist = true;
            herald2Exist = true;
        }
         
        if(magus1Exist)
            Instantiate(magus1, new Vector2(magus1p.position.x, magus1p.position.y), Quaternion.identity);
        if(magus2Exist)
            Instantiate(magus2, new Vector2(magus2p.position.x, magus2p.position.y), Quaternion.identity);
        if(herald1Exist)
            Instantiate(herald1, new Vector2(herald1p.position.x, herald1p.position.y), Quaternion.identity);
        if(magus3Exist)
            Instantiate(magus3, new Vector2(magus3p.position.x, magus3p.position.y), Quaternion.identity);
        if (herald2Exist)
            Instantiate(herald2, new Vector2(herald2p.position.x, herald2p.position.y), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Numeration(int CueNum)
    {
        mobNum = CueNum;
        PlayerPrefs.SetInt("EnemyNum", mobNum);
    }


}
