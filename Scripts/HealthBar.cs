
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    public Image fillBar;
    public int health;
    public int maxHealth;
    int lostHealth;
    float newScale;

    void Awake()
    {
        //maxHealth and health gets information from PlayerPrefs in updating the healthbar in the overworld.
        maxHealth = PlayerPrefs.GetInt("MaxHealthPoint");
        health = PlayerPrefs.GetInt("HealthBar");
        newScale = ((float)health / (float)maxHealth) * 5f;
        fillBar.rectTransform.localScale = new Vector3(newScale, 0.05f, 1.0f);
    }

    public void ReUpdate()
    {
        maxHealth = PlayerPrefs.GetInt("MaxHealthPoint");
        health = PlayerPrefs.GetInt("HealthBar");
        newScale = ((float)health / (float)maxHealth) * 5f;
        fillBar.rectTransform.localScale = new Vector3(newScale, 0.05f, 1.0f);
    }

    /*public void LoseHealth(int value)
    {
        if (health <= 0)
            return;

        

        newScale = ((float)health / (float)maxHealth) * 5f;
        fillBar.rectTransform.localScale = new Vector3(newScale, 0.05f, 1.0f);

        if (health <= 0)
        {
            Debug.Log("You Died");
        }
    }

    //For testing purposes
    private void Update()
    {
        StartCoroutine(HealthRecovery());
        LoseHealth(maxHealth - health);
    }

    private IEnumerator HealthRecovery()
    {
        while(health > maxHealth)
        {
            yield return new WaitForSeconds(2f);

            health += 1;

            PlayerPrefs.SetInt()
        }
    }*/
}
