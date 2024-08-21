using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuScript : MonoBehaviour
{
    int sceneIndex;
    public GameObject mainScreen;
    public GameObject savedScreen;
    public GameObject modeselectScreen;
    public GameObject howtoScreen;
    public GameObject creditsScreen;
    public GameObject illustCorner;
    public GameObject illustCornerMenu;
    public GameObject illustCrnrButton;
    public GameObject p1CharTab;
    public GameObject p2CharTab;
    public GameObject p3CharTab;
    public GameObject transitionUI;
    public GameObject easyRing;
    public GameObject normalRing;
    public GameObject hardRing;
    public GameObject UpdateNotice;
    public GameObject MasterVolSlider;
    public Animator noticeAnim;
    public Animator animateMM;
    public Animator transition;

    AudioManager SFX;
    LevelLoader LevelLoader;

    // Start is called before the first frame update
    void Start()
    {
        LevelLoader = GetComponent<LevelLoader>();
        UpdateNotice.SetActive(true);
        mainScreen.SetActive(false);
        savedScreen.SetActive(false);
        modeselectScreen.SetActive(false);
        howtoScreen.SetActive(false);
        creditsScreen.SetActive(false);
        illustCorner.SetActive(false);
        illustCornerMenu.SetActive(false);
        illustCrnrButton.SetActive(false);
        p1CharTab.SetActive(false);
        p2CharTab.SetActive(false);
        p3CharTab.SetActive(false);
        MasterVolSlider.SetActive(false);
        
        SFX = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        if (PlayerPrefs.HasKey("BossAchievement"))
        {
            if(PlayerPrefs.GetInt("BossAchievement") == 3)
            {
                hardRing.SetActive(true);

                easyRing.SetActive(false);
                normalRing.SetActive(false);
            } else if (PlayerPrefs.GetInt("BossAchievement") == 2)
            {
                hardRing.SetActive(false);
                easyRing.SetActive(false);
                normalRing.SetActive(true);
            }
            else if (PlayerPrefs.GetInt("BossAchievement") == 1)
            {
                hardRing.SetActive(false);
                easyRing.SetActive(true);
                normalRing.SetActive(false);
            }

            illustCrnrButton.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ContinueGame()
    {
        
        mainScreen.SetActive(false);
        savedScreen.SetActive(true);
        
    }

    public void NewGame()
    {
        mainScreen.SetActive(false);
        modeselectScreen.SetActive(true);
    }

    public void SetDifficulty()
    {
        string selectedDifficulty = EventSystem.current.currentSelectedGameObject.name;
        if(selectedDifficulty.Contains("Easy")) {
            PlayerPrefs.SetString("SelectedDifficulty", "Easy");
            StartCoroutine(Easy());
        } else if(selectedDifficulty.Contains("Medium")) {
            PlayerPrefs.SetString("SelectedDifficulty", "Normal");
            StartCoroutine(Normal());
        } else if(selectedDifficulty.Contains("Hard")) {
            PlayerPrefs.SetString("SelectedDifficulty", "Hard");
            StartCoroutine(Hard());
        }
        //Opens First Game Scene
    }

    public void HowToPlay()
    {
        mainScreen.SetActive(false);
        howtoScreen.SetActive(true);
    }
    
    public void returnToMainMenu()
    {
        string bttnName = EventSystem.current.currentSelectedGameObject.name;
        if(bttnName.Equals("savedBackButton")) {
            savedScreen.SetActive(false);
        } else if(bttnName.Equals("modeselectBackButton")) {
            modeselectScreen.SetActive(false);
        } else if(bttnName.Equals("howtoBackButton")) {
            howtoScreen.SetActive(false);
        } else if (bttnName.Equals("creditsBackButton"))
        {
            creditsScreen.SetActive(false);
        }
        else if (bttnName.Equals("illustCornerBackButton"))
        {
            
            illustCornerMenu.SetActive(false);
            p3CharTab.SetActive(false);
            p2CharTab.SetActive(false);
            p1CharTab.SetActive(false);
            illustCorner.SetActive(false);
        }
        mainScreen.SetActive(true);
    }

    public void Credits()
    {
        animateMM.Play("creditsAnim");
        mainScreen.SetActive(false);
        creditsScreen.SetActive(true);
    }
    
    public void QuitGame()
    {
        // Maybe a confirmation UI to Quit the Game
        Application.Quit();
        Debug.Log("Pressed Quit");
    }

    public void NoticeEnd()
    {
        StartCoroutine(NoticeClose());
    }

    public void VolumeOpen()
    {
        if (MasterVolSlider.activeSelf == false)
        {
            MasterVolSlider.SetActive(true);
        }
        else
        {
            MasterVolSlider.SetActive(false);
        }
        

    }

    private IEnumerator NoticeClose()
    {
        noticeAnim.Play("NoticeAnimationEnd");
        yield return new WaitForSeconds(1f);
        UpdateNotice.SetActive(false);
        mainScreen.SetActive(true);
    }
    private IEnumerator Easy()
    {

        yield return new WaitForSeconds(1.2f);
        PlayerPrefs.SetInt("SavedData", 0);
        sceneIndex = 1;
        PlayerPrefs.SetInt("NextStage", 1);
        PlayerPrefs.SetInt("HealthBar", 300);
        PlayerPrefs.SetInt("MaxHealthPoint", 300);
        PlayerPrefs.SetInt("PlayerLevel", 1);
        PlayerPrefs.SetInt("HealthBarRaw", 300);
        PlayerPrefs.SetInt("MaxHealthPointRaw", 300);
        PlayerPrefs.SetInt("PlayerRawDamage", 70);
        PlayerPrefs.SetFloat("CharPosX", 17.6f);
        PlayerPrefs.SetFloat("CharPosY", 7.8f);
        PlayerPrefs.SetInt("UnwaveringAttacker", 0);
        PlayerPrefs.SetInt("Toughness", 0);
        PlayerPrefs.SetInt("RagingBull", 0);
        PlayerPrefs.SetInt("BalancedProwess", 0);
        PlayerPrefs.SetInt("MendWounds", 0);
        PlayerPrefs.SetInt("TotalBuffs", 0);
        PlayerPrefs.SetString("Difficulty", "Easy");
        PlayerPrefs.DeleteKey("ChildBoss");
        PlayerPrefs.DeleteKey("InquisitorBoss");
        PlayerPrefs.SetInt("InquisitorVisit", 0);
        PlayerPrefs.SetInt("CutsceneView", 0);
        LevelLoader.LoadLevel(18);
        //Easy Settings

    }

    private IEnumerator Normal()
    {
        yield return new WaitForSeconds(1.2f);
        PlayerPrefs.SetInt("SavedData", 0);
        sceneIndex = 1;
        PlayerPrefs.SetInt("NextStage", 1);
        PlayerPrefs.SetInt("HealthBar", 200);
        PlayerPrefs.SetInt("MaxHealthPoint", 200);
        PlayerPrefs.SetInt("PlayerLevel", 1);
        PlayerPrefs.SetInt("HealthBarRaw", 200);
        PlayerPrefs.SetInt("MaxHealthPointRaw", 200);
        PlayerPrefs.SetInt("PlayerRawDamage", 50);
        PlayerPrefs.SetFloat("CharPosX", 17.6f);
        PlayerPrefs.SetFloat("CharPosY", 7.8f);
        PlayerPrefs.SetInt("UnwaveringAttacker", 0);
        PlayerPrefs.SetInt("Toughness", 0);
        PlayerPrefs.SetInt("RagingBull", 0);
        PlayerPrefs.SetInt("BalancedProwess", 0);
        PlayerPrefs.SetInt("MendWounds", 0);
        PlayerPrefs.SetInt("TotalBuffs", 0);
        PlayerPrefs.SetString("Difficulty", "Normal");
        PlayerPrefs.DeleteKey("ChildBoss");
        PlayerPrefs.DeleteKey("InquisitorBoss");
        PlayerPrefs.SetInt("InquisitorVisit", 0);
        PlayerPrefs.SetInt("CutsceneView", 0);
        LevelLoader.LoadLevel(18);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + sceneIndex);
        //Normal Settings
    }

    private IEnumerator Hard()
    {
        

        yield return new WaitForSeconds(1.2f);
        PlayerPrefs.SetInt("SavedData", 0);
        sceneIndex = 1;
        PlayerPrefs.SetInt("NextStage", 1);
        PlayerPrefs.SetInt("HealthBar", 100);
        PlayerPrefs.SetInt("MaxHealthPoint", 100);
        PlayerPrefs.SetInt("PlayerLevel", 1);
        PlayerPrefs.SetInt("HealthBarRaw", 100);
        PlayerPrefs.SetInt("MaxHealthPointRaw", 100);
        PlayerPrefs.SetInt("PlayerRawDamage", 30);
        PlayerPrefs.SetFloat("CharPosX", 17.6f);
        PlayerPrefs.SetFloat("CharPosY", 7.8f);
        PlayerPrefs.SetInt("UnwaveringAttacker", 0);
        PlayerPrefs.SetInt("Toughness", 0);
        PlayerPrefs.SetInt("RagingBull", 0);
        PlayerPrefs.SetInt("BalancedProwess", 0);
        PlayerPrefs.SetInt("MendWounds", 0);
        PlayerPrefs.SetInt("TotalBuffs", 0);
        PlayerPrefs.SetString("Difficulty", "Hard");
        PlayerPrefs.DeleteKey("ChildBoss");
        PlayerPrefs.DeleteKey("InquisitorBoss");
        PlayerPrefs.SetInt("InquisitorVisit", 0);
        PlayerPrefs.SetInt("CutsceneView", 0);
        LevelLoader.LoadLevel(18);
        //Hard Settings

    }

    public void PressOption()
    {
        SFX.Play("ButtonClick");
    }

    public void OpenIllustCorner()
    {
        mainScreen.SetActive(false);
        p3CharTab.SetActive(false);
        illustCorner.SetActive(true);
        illustCornerMenu.SetActive(true);
    }

    public void OpenP1CharTab()
    {
        illustCornerMenu.SetActive(false);
        p1CharTab.SetActive(true);
    }

    public void OpenP2CharTab()
    {
        p1CharTab.SetActive(false);
        p2CharTab.SetActive(true);
    }

    public void OpenP3CharTab()
    {
        p2CharTab.SetActive(false);
        p3CharTab.SetActive(true);
    }

    public void ReplayCutscene()
    {
        PlayerPrefs.SetInt("CutsceneView", 1);
        StartCoroutine(CutscenePlay());
    }

    private IEnumerator CutscenePlay()
    {

        yield return new WaitForSeconds(1.4f);

        SceneManager.LoadScene(11);
    }

}
