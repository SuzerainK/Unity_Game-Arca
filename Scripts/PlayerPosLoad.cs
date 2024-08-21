using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerPosLoad : MonoBehaviour
{
    public GameObject playerSprite;
    private GameObject pontifexSprite;
    private GameObject childSprite;
    // Start is called before the first frame update
    void Awake()
    {
        PlayerPrefs.SetInt("AllowSave", 1);
        if(SceneManager.GetActiveScene().buildIndex >= 12)
        {
            pontifexSprite = GameObject.FindGameObjectWithTag("Pontifex");
            childSprite = GameObject.FindGameObjectWithTag("ChildBoss");
            //playerSprite = GameObject.FindGameObjectWithTag("Player");
            //if (PlayerPrefs.GetInt("SavedData") == 1)
            //{
                //Debug.Log("AwakePlayerPosLoad");
                //PlayerPrefs.SetInt("SavedData", 0);
                //Debug.Log("AwakePlayerSavedData Reset");
                //Debug.Log(PlayerPrefs.GetInt("SavedData"));
                //playerSprite.transform.position = new Vector2(PlayerPrefs.GetFloat("CharPosX"), PlayerPrefs.GetFloat("CharPosY"));
                //pontifexSprite.transform.position = new Vector2(PlayerPrefs.GetFloat("Pontifex_X"), PlayerPrefs.GetFloat("Pontifex_Y"));
                //childSprite.transform.position = new Vector2(PlayerPrefs.GetFloat("ChildBoss_X"), PlayerPrefs.GetFloat("ChildBoss_Y"));
                
            //}
        }
        
        if(PlayerPrefs.GetInt("SavedData") == 1)
        {
            
            Debug.Log("AwakePlayerPosLoad");
            PlayerPrefs.SetInt("SavedData", 0);
            Debug.Log("AwakePlayerSavedData Reset");
            Debug.Log(PlayerPrefs.GetInt("SavedData"));
            playerSprite.transform.position = new Vector2(PlayerPrefs.GetFloat("CharPosX"), PlayerPrefs.GetFloat("CharPosY"));


        }
        
    }

    //IEnumerator CharacterPlacement()
    //{
        //yield return new WaitForEndOfFrame();
        
    //}

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ManualTransformPosition()
    {
        Debug.Log("ManualTransformInvoked");
        playerSprite = GameObject.FindGameObjectWithTag("Player");
        if (PlayerPrefs.GetInt("SavedData") == 1)
        {
            playerSprite.transform.position = new Vector2(PlayerPrefs.GetFloat("CharPosX"), PlayerPrefs.GetFloat("CharPosY"));
            PlayerPrefs.SetInt("SavedData", 0);
        }
    }


}
