using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public Animator transition;
   
    public float transitionTime = 1f;
    public int sceneToLoad;

    LevelLoader levelLoader;

    public void OnTriggerEnter2D(Collider2D other)
    {
        levelLoader = GetComponent<LevelLoader>();
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            PlayerPrefs.SetInt("NewStage", 1);
            StartCoroutine(LoadLevel(sceneToLoad));
            PlayerPrefs.DeleteKey("p_x");
            PlayerPrefs.DeleteKey("p_y");
            PlayerPrefs.SetInt("NextStage", 1);
        }
    }

    IEnumerator LoadLevel(int sceneToLoad)
    {
        Debug.Log(sceneToLoad);
        
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        if(SceneManager.GetActiveScene().buildIndex != 5)
            levelLoader.LoadLevel(sceneToLoad);
        else
            SceneManager.LoadScene(sceneToLoad);
    }
}
