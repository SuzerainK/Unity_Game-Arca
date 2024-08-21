using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SavedPlayerPos : MonoBehaviour
{
    public GameObject player;
    
    float pX;
    float pY;

    private void Awake()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }
    private void Update()
    {


        pX = player.transform.position.x;
        pY = player.transform.position.y;

        /*pX = PlayerPrefs.GetFloat("p_x");
        pY = PlayerPrefs.GetFloat("p_y");
        player.transform.position = new Vector2(pX, pY);
        PlayerPrefs.SetInt("TimeToLoad", 0);
        PlayerPrefs.SetInt("Saved", 0);
        PlayerPrefs.Save();*/
    }

    public void PlayerPosSave()
    {
        Debug.Log("SavedPLayerPosSave");
        Debug.Log("SavedPlayerPos");
        PlayerPrefs.SetFloat("p_x", player.transform.position.x);
        PlayerPrefs.SetFloat("p_y", player.transform.position.y);
        PlayerPrefs.SetInt("Saved", 1);
        PlayerPrefs.Save();
        PlayerPrefs.SetInt("Saved", 0);
    }

    public void PlayerPosLoad()
    {
        Debug.Log("SavedPLayerPosLoad");
        PlayerPrefs.SetFloat("p_x", Convert.ToSingle(-1.6));
        PlayerPrefs.SetFloat("p_y", Convert.ToSingle(-3.6));

        

        PlayerPrefs.SetInt("TimeToLoad", 1);
        pX = PlayerPrefs.GetFloat("CharPosX");
        pY = PlayerPrefs.GetFloat("CharPosY");
        if(player != null)
        {
            Debug.Log("Player is present");
        }
        else
        {
            Debug.Log("Player is not present");
        }

        if(SceneManager.GetActiveScene().buildIndex != 12)
        {
            Debug.Log(pX);
            Debug.Log(pY);
            player.transform.position = new Vector2(pX, pY);
        }
        PlayerPrefs.SetInt("TimeToLoad", 0);
        PlayerPrefs.Save();
    }

    
}
