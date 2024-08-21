using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public GameObject LoadingScreen;
    public GameObject LoadingCircle;
    public float CircleSpeed = 2;

    public TextMeshProUGUI gameTips;
    public TextMeshProUGUI progressText;

    int tipNumber;
    bool isloading;
    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
       
    }

    public void LoadLevelByName(string sceneName)
    {
        StartCoroutine(LoadAsynchronouslyByName(sceneName));
    }

    IEnumerator LoadAsynchronouslyByName(string sceneName)
    {
        isloading = true;
        StartCoroutine(LoadCircle());
        StartCoroutine(LoadTips());

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        LoadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            progressText.text = Mathf.RoundToInt(progress * 100f) + "%";
            Debug.Log(operation.progress);

            yield return null;
        }
        isloading = false;
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        isloading = true;
        StartCoroutine(LoadCircle());
        StartCoroutine(LoadTips());

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        LoadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            progressText.text = Mathf.RoundToInt(progress * 100f) + "%";
            Debug.Log(operation.progress);

            yield return null;
        }
        isloading = false;
    }

    IEnumerator LoadCircle()
    {
        while (isloading)
        {
            LoadingCircle.transform.Rotate(0, 0, +CircleSpeed);
            yield return null;
        }
    }

    IEnumerator LoadTips()
    {
        while (isloading)
        {
            
            tipNumber = Random.Range(0, 10);
            switch (tipNumber)
            {
                case 0:
                    gameTips.text = "When in battle, you can press any enemy to select a target.";
                    break;
                case 1:
                    gameTips.text = "Switching to blocking stance allows you to parry enemy strikes.";
                    break;
                case 2:
                    gameTips.text = "Press shift to run.";
                    break;
                case 3:
                    gameTips.text = "The world of Ars Cael revolves around the heroes of different eras.";
                    break;
                case 4:
                    gameTips.text = "Those you cannot teach to fly, teach to fall faster.";
                    break;
                case 5:
                    gameTips.text = "Power is the pivot on which everything hinges. He who has the power is always right.";
                    break;
                case 6:
                    gameTips.text = "A wolf does not ask, he takes what is rightfully his.";
                    break;
                case 7:
                    gameTips.text = "The Sanctuary has two eyes of many facets.";
                    break;
                case 8:
                    gameTips.text = "The tower holds the greatest sin of the Empire.";
                    break;
                case 9:
                    gameTips.text = "Be careful with your decisions, for they carve your fate.";
                    break;
                default:
                    gameTips.text = "In this version, levels do not matter much.";
                    break;

            }
            yield return new WaitForSeconds(3f);
        }
    }
}
