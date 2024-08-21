using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitButton : MonoBehaviour
{
    private float transitionTime = 1.4f;

    public GameObject mainUI;
    public GameObject menuButton;
    
    public Animator menuAnimate;
    public Animator transition;

    public void OnMenuButton()
    {
        Debug.Log("Button CLICKED");
        mainUI.SetActive(false);
        menuButton.SetActive(true);

        
    }

    public void OnConfirmQuit()
    {
        StartCoroutine(MenuLoadScene());
    }

    IEnumerator MenuLoadScene()
    {
        
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(0);
    }

    public void OnDenyQuit()
    {
        StartCoroutine(BackToGame());
    }

    IEnumerator BackToGame()
    {
        menuAnimate.Play("menuButtonClose");
        yield return new WaitForSeconds(0.3f);
        mainUI.SetActive(true);
        menuButton.SetActive(false);
    }


}
