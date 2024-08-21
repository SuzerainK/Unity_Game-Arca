using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverworldUI : MonoBehaviour
{
    CharacterMovements characterMove;
    ScnSave SS;
    QuitButton GM;

    public GameObject MainMenuUI;

    public GameObject MasterVolSlider;
    public Animator MasVolSlidAnim;
    public GameObject SaveFailObj;
    private GameObject transition;

    // Start is called before the first frame update
    void Start()
    {
        //if(SceneManager.GetActiveScene().buildIndex == 12)
            //SaveFailObj = GameObject.FindGameObjectWithTag("SaveFail");

        MasterVolSlider.SetActive(false);
        SS = GameObject.FindGameObjectWithTag("Manager").GetComponent<ScnSave>();
        GM = GameObject.FindGameObjectWithTag("Manager").GetComponent<QuitButton>();
        characterMove = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovements>();
        transition = GameObject.FindGameObjectWithTag("Transition");
        TransitionClose();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenSave()
    {
        if(!DialogueManager.GetInstance().dialogueIsPlaying && !characterMove.gameMenuPane)
        {
            if(PlayerPrefs.GetInt("AllowSave") == 0)
            {
                StartCoroutine(Save_Null());
            }
            else
            {
                characterMove.saveMenuPane = true;
                SS.OnSaveButton();
            }
        }
    }

    public void CloseSave()
    {
        if (characterMove.saveMenuPane)
        {
            characterMove.saveMenuPane = false;
            SS.OnReturnToGame();
        } 
        
    }

    public void OnOpenVM()
    {
        StartCoroutine(OnOpenAudio());
    }

    public void OnCloseVM()
    {
        StartCoroutine(OnCloseAudio());
    }

    IEnumerator OnCloseAudio()
    {
        Debug.Log("Entered Close Audio");
        MasVolSlidAnim.Play("OverworldVolAdjustClose");
        yield return new WaitForSeconds(1f);
        MainMenuUI.SetActive(true);
        MasterVolSlider.SetActive(false);
    }

    IEnumerator OnOpenAudio()
    {
        Debug.Log("Entered Open Audio");
        MainMenuUI.SetActive(false);
        MasterVolSlider.SetActive(true);
        MasVolSlidAnim.Play("OverworldVolAdjustOpen");
        yield return new WaitForSeconds(1f);
    }

    public void Save_One()
    {
        characterMove.saveMenuPane = false;
        SS.OnSaveOneButton();
    }

    public void Save_Two()
    {
        characterMove.saveMenuPane = false;
        SS.OnSaveTwoButton();
    }

    public void Save_Three()
    {
        characterMove.saveMenuPane = false;
        SS.OnSaveThreeButton();
    }

    public IEnumerator Save_Null()
    {
        SaveFailObj.SetActive(true);
        yield return new WaitForSeconds(2f);
        SaveFailObj.SetActive(false);
    }

    public void OpenMenu()
    {
        if (!DialogueManager.GetInstance().dialogueIsPlaying && !characterMove.saveMenuPane)
        {
            characterMove.gameMenuPane = true;
            GM.OnMenuButton();
        }
    }

    public void ChangeVolume()
    {

    }

    public void CloseMenu()
    {
        characterMove.gameMenuPane = false;
        GM.OnDenyQuit();
    }

    public void GoToMainMenu()
    {
        characterMove.gameMenuPane = false;
        GM.OnConfirmQuit();
    }

    public void TransitionClose()
    {
        StartCoroutine(TransitionEnder());
    }

    private IEnumerator TransitionEnder()
    {
        yield return new WaitForSeconds(3f);
        transition.SetActive(false);
    }
}
