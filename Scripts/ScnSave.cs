using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScnSave : MonoBehaviour
{
    private int CurrentHealthPoint;
    private int CurrentMaxHealthPoint;
    private int CurrentHealthPointRaw;
    private int CurrentMaxHealthPointRaw;
    private int PlayerLevel;
    private int PlayerDamageRaw;
    private int CurrentStage;
    private float CurrentPositionX;
    private float CurrentPositionY;
    private string CurrentStageName;
    private string CurrentDifficulty;
    private int CurrentIntox = 0;
    private int CurrentVampBite = 0;
    private int CurrentApprovView = 0;
    private int CurrentNeutrality = 50;
    private int UnwaveringAttacker;
    private int Toughness;
    private int RagingBull;
    private int BalancedProwess;
    private int MendWounds;
    private int TotalBuffs;
    private int InquisitorVisit;

    public GameObject MainUI;
    public GameObject SaveMenu;
    public Animator SaveAnimate;
    public Animator transition;
    public GameObject confirmSave;
    private GameObject CharacterSprite;

    private GameObject Pontifex;
    
    private GameObject ChildBoss;

    Unit playerUnit;
    
    private void Start()
    {
        CharacterSprite = GameObject.FindGameObjectWithTag("Player");
        playerUnit = CharacterSprite.GetComponent<Unit>();
    }

    public void Update()
    {
        
        if (SceneManager.GetActiveScene().buildIndex == 12)
        {
            CurrentNeutrality = PlayerPrefs.GetInt("Neutrality");
            CurrentIntox = PlayerPrefs.GetInt("Intoxication");
            CurrentApprovView = PlayerPrefs.GetInt("ViewApproval");
            CurrentVampBite = PlayerPrefs.GetInt("VampireBite");
        }

        CurrentHealthPoint = PlayerPrefs.GetInt("HealthBar");
        CurrentMaxHealthPoint = PlayerPrefs.GetInt("MaxHealthPoint");
        CurrentHealthPointRaw = PlayerPrefs.GetInt("HealthBarRaw");
        CurrentMaxHealthPointRaw = PlayerPrefs.GetInt("MaxHealthPointRaw");
        PlayerLevel = PlayerPrefs.GetInt("PlayerLevel");
        PlayerDamageRaw = PlayerPrefs.GetInt("PlayerRawDamage");
        UnwaveringAttacker = PlayerPrefs.GetInt("UnwaveringAttacker");
        Toughness = PlayerPrefs.GetInt("Toughness");
        RagingBull = PlayerPrefs.GetInt("RagingBull");
        BalancedProwess = PlayerPrefs.GetInt("BalancedProwess");
        MendWounds = PlayerPrefs.GetInt("MendWounds");
        TotalBuffs = PlayerPrefs.GetInt("TotalBuffs");
        InquisitorVisit = PlayerPrefs.GetInt("InquisitorVisit");
        CurrentStage = SceneManager.GetActiveScene().buildIndex;
        CurrentDifficulty = PlayerPrefs.GetString("SelectedDifficulty");
        Pontifex = GameObject.FindGameObjectWithTag("Pontifex");
        ChildBoss = GameObject.FindGameObjectWithTag("ChildBoss");

        
        CurrentPositionX = CharacterSprite.transform.position.x;
        CurrentPositionY = CharacterSprite.transform.position.y;

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            CurrentStageName = "Stage 1: Initial Awakening";
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            CurrentStageName = "Stage 2: The Encounter";
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            CurrentStageName = "Stage 3: Among the Rocks";
        }
        else if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            CurrentStageName = "Stage 4: The Elimination";
        }
        else if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            CurrentStageName = "Stage 5: The Stars' Judgement";
        }
        else if (SceneManager.GetActiveScene().buildIndex == 12)
        {
            CurrentStageName = "Episode 1: The Sanctuary";
        }
    }

    public void OnSaveButton()
    {
        MainUI.SetActive(false);
        SaveMenu.SetActive(true);
    }

    public void OnSaveOneButton()
    {
        PlayerPrefs.SetInt("SaveOneHP", CurrentHealthPoint);
        PlayerPrefs.SetInt("SaveOneMHP", CurrentMaxHealthPoint);
        PlayerPrefs.SetInt("SaveOneHPR", CurrentHealthPointRaw);
        PlayerPrefs.SetInt("SaveOneMHPR", CurrentMaxHealthPointRaw);
        PlayerPrefs.SetInt("SaveOnePL", PlayerLevel);
        PlayerPrefs.SetInt("SaveOnePDR", PlayerDamageRaw);
        PlayerPrefs.SetInt("SaveOneCS", CurrentStage);
        PlayerPrefs.SetString("SaveOneCSN", CurrentStageName);
        PlayerPrefs.SetString("SaveOneCD", CurrentDifficulty);
        PlayerPrefs.SetFloat("SaveOnePosX", CurrentPositionX);
        PlayerPrefs.SetFloat("SaveOnePosY", CurrentPositionY);
        PlayerPrefs.SetInt("S1UnwaveringAttacker", UnwaveringAttacker);
        PlayerPrefs.SetInt("S1Toughness", Toughness);
        PlayerPrefs.SetInt("S1RagingBull", RagingBull);
        PlayerPrefs.SetInt("S1BalancedProwess", BalancedProwess);
        PlayerPrefs.SetInt("S1MendWounds", MendWounds);
        PlayerPrefs.SetInt("S1TotalBuffs", TotalBuffs);
        

        if (SceneManager.GetActiveScene().buildIndex >= 12)
        {
            PlayerPrefs.SetInt("SaveOneIntox", CurrentIntox);
            PlayerPrefs.SetInt("SaveOneVBite", CurrentVampBite);
            PlayerPrefs.SetInt("SaveOneNeutral", CurrentNeutrality);
            PlayerPrefs.SetInt("SaveOneVApprov", CurrentApprovView);
            PlayerPrefs.SetInt("S1InquisitorVisit", InquisitorVisit);
            //PlayerPrefs.SetFloat("SaveOne_Pontifex_X", Pontifex.transform.position.x);
            //PlayerPrefs.SetFloat("SaveOne_Pontifex_Y", Pontifex.transform.position.y);
            PlayerPrefs.SetFloat("SaveOne_Child_X", ChildBoss.transform.position.x);
            PlayerPrefs.SetFloat("SaveOne_Child_Y", ChildBoss.transform.position.y);
            if (PlayerPrefs.HasKey("InquisitorBoss"))
            {
                PlayerPrefs.SetInt("SaveOneInquisitorBoss", PlayerPrefs.GetInt("InquisitorBoss"));
            }
            else
            {
                PlayerPrefs.DeleteKey("SaveOneInquisitorBoss");
            }

            if (PlayerPrefs.HasKey("ChildBoss"))
            {
                PlayerPrefs.SetInt("SaveOneChildBoss", PlayerPrefs.GetInt("ChildBoss"));
            }
            else
            {
                PlayerPrefs.DeleteKey("SaveOneChildBoss");
            }
        }
            
        PlayerPrefs.Save();

        StartCoroutine(BackToGame());
    }

    public void OnSaveTwoButton()
    {
        PlayerPrefs.SetInt("SaveTwoHP", CurrentHealthPoint);
        PlayerPrefs.SetInt("SaveTwoMHP", CurrentMaxHealthPoint);
        PlayerPrefs.SetInt("SaveTwoHPR", CurrentHealthPointRaw);
        PlayerPrefs.SetInt("SaveTwoMHPR", CurrentMaxHealthPointRaw);
        PlayerPrefs.SetInt("SaveTwoPL", PlayerLevel);
        PlayerPrefs.SetInt("SaveTwoPDR", PlayerDamageRaw);
        PlayerPrefs.SetInt("SaveTwoCS", CurrentStage);
        PlayerPrefs.SetString("SaveTwoCSN", CurrentStageName);
        PlayerPrefs.SetString("SaveTwoCD", CurrentDifficulty);
        PlayerPrefs.SetFloat("SaveTwoPosX", CurrentPositionX);
        PlayerPrefs.SetFloat("SaveTwoPosY", CurrentPositionY);
        PlayerPrefs.SetInt("S2UnwaveringAttacker", UnwaveringAttacker);
        PlayerPrefs.SetInt("S2Toughness", Toughness);
        PlayerPrefs.SetInt("S2RagingBull", RagingBull);
        PlayerPrefs.SetInt("S2BalancedProwess", BalancedProwess);
        PlayerPrefs.SetInt("S2MendWounds", MendWounds);
        PlayerPrefs.SetInt("S2TotalBuffs", TotalBuffs);

        if (SceneManager.GetActiveScene().buildIndex >= 12)
        {
            PlayerPrefs.SetInt("S2InquisitorVisit", InquisitorVisit);
            PlayerPrefs.SetInt("SaveTwoIntox", CurrentIntox);
            PlayerPrefs.SetInt("SaveTwoVBite", CurrentVampBite);
            PlayerPrefs.SetInt("SaveTwoNeutral", CurrentNeutrality);
            PlayerPrefs.SetInt("SaveTwoVApprov", CurrentApprovView);
            //PlayerPrefs.SetFloat("SaveTwo_Pontifex_X", Pontifex.transform.position.x);
            //PlayerPrefs.SetFloat("SaveTwo_Pontifex_Y", Pontifex.transform.position.y);
            PlayerPrefs.SetFloat("SaveTwo_Child_X", ChildBoss.transform.position.x);
            PlayerPrefs.SetFloat("SaveTwo_Child_Y", ChildBoss.transform.position.y);
            if (PlayerPrefs.HasKey("InquisitorBoss"))
            {
                PlayerPrefs.SetInt("SaveTwoInquisitorBoss", PlayerPrefs.GetInt("InquisitorBoss"));
            }
            else
            {
                PlayerPrefs.DeleteKey("SaveTwoInquisitorBoss");
            }

            if (PlayerPrefs.HasKey("ChildBoss"))
            {
                PlayerPrefs.SetInt("SaveTwoChildBoss", PlayerPrefs.GetInt("ChildBoss"));
            }
            else
            {
                PlayerPrefs.DeleteKey("SaveTwoChildBoss");
            }
        }
            
        PlayerPrefs.Save();

        StartCoroutine(BackToGame());
    }

    public void OnSaveThreeButton()
    {
        PlayerPrefs.SetInt("SaveThrHP", CurrentHealthPoint);
        PlayerPrefs.SetInt("SaveThrMHP", CurrentMaxHealthPoint);
        PlayerPrefs.SetInt("SaveThrHPR", CurrentHealthPointRaw);
        PlayerPrefs.SetInt("SaveThrMHPR", CurrentMaxHealthPointRaw);
        PlayerPrefs.SetInt("SaveThrPL", PlayerLevel);
        PlayerPrefs.SetInt("SaveThrPDR", PlayerDamageRaw);
        PlayerPrefs.SetInt("SaveThrCS", CurrentStage);
        PlayerPrefs.SetString("SaveThrCSN", CurrentStageName);
        PlayerPrefs.SetString("SaveThrCD", CurrentDifficulty);
        PlayerPrefs.SetFloat("SaveThrPosX", CurrentPositionX);
        PlayerPrefs.SetFloat("SaveThrPosY", CurrentPositionY);
        PlayerPrefs.SetInt("S3UnwaveringAttacker", UnwaveringAttacker);
        PlayerPrefs.SetInt("S3Toughness", Toughness);
        PlayerPrefs.SetInt("S3RagingBull", RagingBull);
        PlayerPrefs.SetInt("S3BalancedProwess", BalancedProwess);
        PlayerPrefs.SetInt("S3MendWounds", MendWounds);
        PlayerPrefs.SetInt("S3TotalBuffs", TotalBuffs);

        if (SceneManager.GetActiveScene().buildIndex >= 12)
        {
            PlayerPrefs.SetInt("S3InquisitorVisit", InquisitorVisit);
            PlayerPrefs.SetInt("SaveThrIntox", CurrentIntox);
            PlayerPrefs.SetInt("SaveThrVBite", CurrentVampBite);
            PlayerPrefs.SetInt("SaveThrNeutral", CurrentNeutrality);
            PlayerPrefs.SetInt("SaveThrVApprov", CurrentApprovView);
            //PlayerPrefs.SetFloat("SaveThr_Pontifex_X", Pontifex.transform.position.x);
            //PlayerPrefs.SetFloat("SaveThr_Pontifex_Y", Pontifex.transform.position.y);
            PlayerPrefs.SetFloat("SaveThr_Child_X", ChildBoss.transform.position.x);
            PlayerPrefs.SetFloat("SaveThr_Child_Y", ChildBoss.transform.position.y);
            if (PlayerPrefs.HasKey("InquisitorBoss"))
            {
                PlayerPrefs.SetInt("SaveThrInquisitorBoss", PlayerPrefs.GetInt("InquisitorBoss"));
            }
            else
            {
                PlayerPrefs.DeleteKey("SaveThrInquisitorBoss");
            }

            if (PlayerPrefs.HasKey("ChildBoss"))
            {
                PlayerPrefs.SetInt("SaveThrChildBoss", PlayerPrefs.GetInt("ChildBoss"));
            }
            else
            {
                PlayerPrefs.DeleteKey("SaveThrChildBoss");
            }
        }
        
        PlayerPrefs.Save();

        StartCoroutine(BackToGame());
    }

    public void TempSave()
    {
        Debug.Log("TempSavedInvoked");
        if (SceneManager.GetActiveScene().buildIndex < 12)
        {
            PlayerPrefs.SetInt("SaveTempHPR", CurrentHealthPointRaw);
            PlayerPrefs.SetInt("SaveTempMHPR", CurrentMaxHealthPointRaw);
            PlayerPrefs.SetInt("SaveTempPL", PlayerLevel);
            PlayerPrefs.SetInt("SaveTempPDR", PlayerDamageRaw);
            PlayerPrefs.SetInt("STUnwaveringAttacker", UnwaveringAttacker);
            PlayerPrefs.SetInt("STToughness", Toughness);
            PlayerPrefs.SetInt("STRagingBull", RagingBull);
            PlayerPrefs.SetInt("STBalancedProwess", BalancedProwess);
            PlayerPrefs.SetInt("STMendWounds", MendWounds);
            PlayerPrefs.SetInt("STTotalBuffs", TotalBuffs);
        }
        else
        {
            
            PlayerPrefs.SetInt("SaveTempHP", CurrentHealthPoint);
            PlayerPrefs.SetInt("SaveTempMHP", CurrentMaxHealthPoint);
            PlayerPrefs.SetInt("SaveTempHPR", CurrentHealthPointRaw);
            PlayerPrefs.SetInt("SaveTempMHPR", CurrentMaxHealthPointRaw);
            PlayerPrefs.SetInt("SaveTempPL", PlayerLevel);
            PlayerPrefs.SetInt("SaveTempPDR", PlayerDamageRaw);
            PlayerPrefs.SetInt("SaveTempCS", CurrentStage);
            PlayerPrefs.SetString("SaveTempCSN", CurrentStageName);
            PlayerPrefs.SetString("SaveTempCD", CurrentDifficulty);
            PlayerPrefs.SetFloat("SaveTempPosX", CurrentPositionX);
            PlayerPrefs.SetFloat("SaveTempPosY", CurrentPositionY);
            PlayerPrefs.SetInt("STUnwaveringAttacker", UnwaveringAttacker);
            PlayerPrefs.SetInt("STToughness", Toughness);
            PlayerPrefs.SetInt("STRagingBull", RagingBull);
            PlayerPrefs.SetInt("STBalancedProwess", BalancedProwess);
            PlayerPrefs.SetInt("STMendWounds", MendWounds);
            PlayerPrefs.SetInt("STTotalBuffs", TotalBuffs);
        }

        if (SceneManager.GetActiveScene().buildIndex >= 12)
        {
            PlayerPrefs.SetInt("SaveTempIntox", CurrentIntox);
            PlayerPrefs.SetInt("SaveTempVBite", CurrentVampBite);
            PlayerPrefs.SetInt("SaveTempNeutral", CurrentNeutrality);
            PlayerPrefs.SetInt("SaveTempVApprov", CurrentApprovView);
            //PlayerPrefs.SetFloat("SaveTemp_Pontifex_X", Pontifex.transform.position.x);
            //PlayerPrefs.SetFloat("SaveTemp_Pontifex_Y", Pontifex.transform.position.y);
            PlayerPrefs.SetFloat("SaveTemp_Child_X", ChildBoss.transform.position.x);
            PlayerPrefs.SetFloat("SaveTemp_Child_Y", ChildBoss.transform.position.y);
        }

        PlayerPrefs.Save();

    }


    public void OnReturnToGame()
    {
        StartCoroutine(BackToGameWOSaving());
    }

    IEnumerator BackToGame()
    {
        SaveAnimate.Play("menuButtonClose");
        yield return new WaitForSeconds(0.3f);
        MainUI.SetActive(true);
        SaveMenu.SetActive(false);
        confirmSave.SetActive(true);
        yield return new WaitForSeconds(2f);
        confirmSave.SetActive(false);
    }

    IEnumerator BackToGameWOSaving()
    {
        SaveAnimate.Play("menuButtonClose");
        yield return new WaitForSeconds(0.3f);
        MainUI.SetActive(true);
        SaveMenu.SetActive(false);
    }
}
