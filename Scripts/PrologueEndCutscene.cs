using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrologueEndCutscene : MonoBehaviour
{
    // Start is called before the first frame update

    LevelLoader LevelLoader;
    void Awake()
    {
        LevelLoader = GetComponent<LevelLoader>();
        StartCoroutine(FinishCut());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(SceneManager.GetActiveScene().buildIndex == 11)
            {
                if(PlayerPrefs.GetInt("CutsceneView") == 1)
                    StartCoroutine(LoadLevel(0));
                else
                    StartCoroutine(LoadLevel(12));
            }
            else if (SceneManager.GetActiveScene().buildIndex == 18)
            {
                StartCoroutine(LoadLevel(1));
            }
            else
            {
                StartCoroutine(LoadLevel(0));
            }

        }
    }

    IEnumerator FinishCut()
    {
        if (SceneManager.GetActiveScene().buildIndex == 11)
        {
            yield return new WaitForSeconds(126f);
            StartCoroutine(LoadLevel(12));
        }else if(SceneManager.GetActiveScene().buildIndex == 15)
        {
            yield return new WaitForSeconds(65f);
            StartCoroutine(LoadLevel(0));
        }else if(SceneManager.GetActiveScene().buildIndex == 16)
        {
            yield return new WaitForSeconds(89f);
            StartCoroutine(LoadLevel(0));
        }else if(SceneManager.GetActiveScene().buildIndex == 17)
        {
            yield return new WaitForSeconds(73f);
            StartCoroutine(LoadLevel(0));
        }else if(SceneManager.GetActiveScene().buildIndex == 18)
        {
            yield return new WaitForSeconds(50f);
            StartCoroutine(LoadLevel(1));
        }else if(SceneManager.GetActiveScene().buildIndex == 19)
        {
            yield return new WaitForSeconds(47f);
            StartCoroutine(LoadLevel(0));
        }


    }

    IEnumerator LoadLevel(int sceneToLoad)
    {

        yield return new WaitForSeconds(1.4f);

        LevelLoader.LoadLevel(sceneToLoad);
    }




}
