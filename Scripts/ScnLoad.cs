using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScnLoad : MonoBehaviour
{
    PlayerPosLoad loadPosition;

    public GameObject EmptySlotNotif;

    LevelLoader levelLoader;
    

    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
            levelLoader = GameObject.FindGameObjectWithTag("MainMenu").GetComponent<LevelLoader>();


        if(SceneManager.GetActiveScene().buildIndex == 12)
        {
            levelLoader = GameObject.FindGameObjectWithTag("Manager").GetComponent<LevelLoader>();
            loadPosition = GameObject.FindGameObjectWithTag("Manager").GetComponent<PlayerPosLoad>();

            if (PlayerPrefs.HasKey("InquisitorBoss"))
            {
                if (SceneManager.GetActiveScene().buildIndex == 12 && PlayerPrefs.GetInt("InquisitorBoss") == 1)
                {
                    Debug.Log("ScnLoad1");
                    OnLoadTemp();
                }
                else if (SceneManager.GetActiveScene().buildIndex == 12 && PlayerPrefs.GetInt("InquisitorBoss") == 2)
                {
                    Debug.Log("ScnLoad2");
                    OnLoadTemp();
                }
            }

            if (PlayerPrefs.HasKey("ChildBoss") && !PlayerPrefs.HasKey("InquisitorBoss"))
            {
                if (SceneManager.GetActiveScene().buildIndex == 12 && PlayerPrefs.GetInt("ChildBoss") == 1)
                {
                    Debug.Log("ScnLoad1");
                    OnLoadTemp();
                }
                else if (SceneManager.GetActiveScene().buildIndex == 12 && PlayerPrefs.GetInt("ChildBoss") == 2)
                {
                    Debug.Log("ScnLoad2");
                    OnLoadTemp();
                }
            }

            
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnLoadOneButton()
    {

        StartCoroutine(LoadOne());
    }

    public void OnLoadTwoButton()
    {
        StartCoroutine(LoadTwo());
    }

    public void OnLoadThrButton()
    {
        StartCoroutine(LoadThree());
    }

    public void OnLoadTemp()
    {
        StartCoroutine(LoadTemp());
    }

    private IEnumerator LoadOne()
    {
        if (PlayerPrefs.HasKey("SaveOneHP"))
        {
            

            yield return new WaitForSeconds(1.4f);

            PlayerPrefs.SetInt("NextStage", 1);
            PlayerPrefs.SetInt("HealthBar", PlayerPrefs.GetInt("SaveOneHP"));
            PlayerPrefs.SetInt("MaxHealthPoint", PlayerPrefs.GetInt("SaveOneMHP"));
            PlayerPrefs.SetInt("PlayerLevel", PlayerPrefs.GetInt("SaveOnePL"));
            PlayerPrefs.SetInt("HealthBarRaw", PlayerPrefs.GetInt("SaveOneHPR"));
            PlayerPrefs.SetInt("MaxHealthPointRaw", PlayerPrefs.GetInt("SaveOneMHPR"));
            PlayerPrefs.SetInt("PlayerRawDamage", PlayerPrefs.GetInt("SaveOnePDR"));
            PlayerPrefs.SetFloat("CharPosX", PlayerPrefs.GetFloat("SaveOnePosX"));
            PlayerPrefs.SetFloat("CharPosY", PlayerPrefs.GetFloat("SaveOnePosY"));
            PlayerPrefs.SetInt("UnwaveringAttacker", PlayerPrefs.GetInt("S1UnwaveringAttacker"));
            PlayerPrefs.SetInt("Toughness", PlayerPrefs.GetInt("S1Toughness"));
            PlayerPrefs.SetInt("RagingBull", PlayerPrefs.GetInt("S1RagingBull"));
            PlayerPrefs.SetInt("BalancedProwess", PlayerPrefs.GetInt("S1BalancedProwess"));
            PlayerPrefs.SetInt("MendWounds", PlayerPrefs.GetInt("S1MendWounds"));
            PlayerPrefs.SetInt("TotalBuffs", PlayerPrefs.GetInt("S1TotalBuffs"));
            PlayerPrefs.SetFloat("CharPosX", PlayerPrefs.GetFloat("SaveOnePosX"));
            PlayerPrefs.SetFloat("CharPosY", PlayerPrefs.GetFloat("SaveOnePosY"));
            PlayerPrefs.SetInt("CutsceneView", 0);


            if (PlayerPrefs.GetInt("SaveOneCS") > 11)
            {
                PlayerPrefs.SetInt("InquisitorVisit", PlayerPrefs.GetInt("S1InquisitorVisit"));
                PlayerPrefs.SetInt("VampireBite", PlayerPrefs.GetInt("SaveOneVBite"));
                PlayerPrefs.SetInt("Neutrality", PlayerPrefs.GetInt("SaveOneNeutral"));
                PlayerPrefs.SetInt("Intoxication", PlayerPrefs.GetInt("SaveOneIntox"));
                PlayerPrefs.SetInt("ViewApproval", PlayerPrefs.GetInt("SaveOneVApprov"));
                if (PlayerPrefs.HasKey("SaveOneInquisitorBoss"))
                {
                    PlayerPrefs.SetInt("InquisitorBoss", PlayerPrefs.GetInt("SaveOneInquisitorBoss"));
                }

                if (PlayerPrefs.HasKey("SaveOneChildBoss"))
                {
                    PlayerPrefs.SetInt("ChildBoss", PlayerPrefs.GetInt("SaveOneChildBoss"));
                }
            }

            PlayerPrefs.SetInt("SavedData", 1);
            levelLoader.LoadLevel(PlayerPrefs.GetInt("SaveOneCS"));
            //SceneManager.LoadScene(PlayerPrefs.GetInt("SaveOneCS"));
        }
        else
        {
            EmptySlotNotif.SetActive(true);

            yield return new WaitForSeconds(2f);

            EmptySlotNotif.SetActive(false);
        }
    }

    private IEnumerator LoadTwo()
    {
        if (PlayerPrefs.HasKey("SaveTwoHP"))
        {

            

            yield return new WaitForSeconds(1.4f);

            PlayerPrefs.SetInt("NextStage", 1);
            PlayerPrefs.SetInt("HealthBar", PlayerPrefs.GetInt("SaveTwoHP"));
            PlayerPrefs.SetInt("MaxHealthPoint", PlayerPrefs.GetInt("SaveTwoMHP"));
            PlayerPrefs.SetInt("PlayerLevel", PlayerPrefs.GetInt("SaveTwoPL"));
            PlayerPrefs.SetInt("HealthBarRaw", PlayerPrefs.GetInt("SaveTwoHPR"));
            PlayerPrefs.SetInt("MaxHealthPointRaw", PlayerPrefs.GetInt("SaveTwoMHPR"));
            PlayerPrefs.SetInt("PlayerRawDamage", PlayerPrefs.GetInt("SaveTwoPDR"));
            PlayerPrefs.SetFloat("CharPosX", PlayerPrefs.GetFloat("SaveTwoPosX"));
            PlayerPrefs.SetFloat("CharPosY", PlayerPrefs.GetFloat("SaveTwoPosY"));
            PlayerPrefs.SetInt("UnwaveringAttacker", PlayerPrefs.GetInt("S2UnwaveringAttacker"));
            PlayerPrefs.SetInt("Toughness", PlayerPrefs.GetInt("S2Toughness"));
            PlayerPrefs.SetInt("RagingBull", PlayerPrefs.GetInt("S2RagingBull"));
            PlayerPrefs.SetInt("BalancedProwess", PlayerPrefs.GetInt("S2BalancedProwess"));
            PlayerPrefs.SetInt("MendWounds", PlayerPrefs.GetInt("S2MendWounds"));
            PlayerPrefs.SetInt("TotalBuffs", PlayerPrefs.GetInt("S2TotalBuffs"));
            PlayerPrefs.SetFloat("CharPosX", PlayerPrefs.GetFloat("SaveTwoPosX"));
            PlayerPrefs.SetFloat("CharPosY", PlayerPrefs.GetFloat("SaveTwoPosY"));
            PlayerPrefs.SetInt("CutsceneView", 0);

            if (PlayerPrefs.GetInt("SaveTwoCS") > 11)
            {
                PlayerPrefs.SetInt("VampireBite", PlayerPrefs.GetInt("SaveTwoVBite"));
                PlayerPrefs.SetInt("Neutrality", PlayerPrefs.GetInt("SaveTwoNeutral"));
                PlayerPrefs.SetInt("Intoxication", PlayerPrefs.GetInt("SaveTwoIntox"));
                PlayerPrefs.SetInt("ViewApproval", PlayerPrefs.GetInt("SaveTwoVApprov"));
                PlayerPrefs.SetInt("InquisitorVisit", PlayerPrefs.GetInt("S2InquisitorVisit"));
                if (PlayerPrefs.HasKey("SaveTwoInquisitorBoss"))
                {
                    PlayerPrefs.SetInt("InquisitorBoss", PlayerPrefs.GetInt("SaveTwoInquisitorBoss"));
                }

                if (PlayerPrefs.HasKey("SaveTwoChildBoss"))
                {
                    PlayerPrefs.SetInt("ChildBoss", PlayerPrefs.GetInt("SaveTwoChildBoss"));
                }
            }
            
            PlayerPrefs.SetInt("SavedData", 1);
            levelLoader.LoadLevel(PlayerPrefs.GetInt("SaveTwoCS"));
            //SceneManager.LoadScene(PlayerPrefs.GetInt("SaveTwoCS"));
        }
        else
        {
            EmptySlotNotif.SetActive(true);

            yield return new WaitForSeconds(2f);

            EmptySlotNotif.SetActive(false);
        }
    }

    private IEnumerator LoadThree()
    {
        if (PlayerPrefs.HasKey("SaveThrHP"))
        {
            

            yield return new WaitForSeconds(1.4f);

            PlayerPrefs.SetInt("NextStage", 1);
            PlayerPrefs.SetInt("HealthBar", PlayerPrefs.GetInt("SaveThrHP"));
            PlayerPrefs.SetInt("MaxHealthPoint", PlayerPrefs.GetInt("SaveThrMHP"));
            PlayerPrefs.SetInt("PlayerLevel", PlayerPrefs.GetInt("SaveThrPL"));
            PlayerPrefs.SetInt("HealthBarRaw", PlayerPrefs.GetInt("SaveThrHPR"));
            PlayerPrefs.SetInt("MaxHealthPointRaw", PlayerPrefs.GetInt("SaveThrMHPR"));
            PlayerPrefs.SetInt("PlayerRawDamage", PlayerPrefs.GetInt("SaveThrPDR"));
            PlayerPrefs.SetFloat("CharPosX", PlayerPrefs.GetFloat("SaveThrPosX"));
            PlayerPrefs.SetFloat("CharPosY", PlayerPrefs.GetFloat("SaveThrPosY"));
            PlayerPrefs.SetInt("UnwaveringAttacker", PlayerPrefs.GetInt("S3UnwaveringAttacker"));
            PlayerPrefs.SetInt("Toughness", PlayerPrefs.GetInt("S3Toughness"));
            PlayerPrefs.SetInt("RagingBull", PlayerPrefs.GetInt("S3RagingBull"));
            PlayerPrefs.SetInt("BalancedProwess", PlayerPrefs.GetInt("S3BalancedProwess"));
            PlayerPrefs.SetInt("MendWounds", PlayerPrefs.GetInt("S3MendWounds"));
            PlayerPrefs.SetInt("TotalBuffs", PlayerPrefs.GetInt("S3TotalBuffs"));
            PlayerPrefs.SetFloat("CharPosX", PlayerPrefs.GetFloat("SaveThrPosX"));
            PlayerPrefs.SetFloat("CharPosY", PlayerPrefs.GetFloat("SaveThrPosY"));
            PlayerPrefs.SetInt("CutsceneView", 0);

            if (PlayerPrefs.GetInt("SaveThrCS") > 11)
            {
                PlayerPrefs.SetInt("InquisitorVisit", PlayerPrefs.GetInt("S3InquisitorVisit"));
                PlayerPrefs.SetInt("VampireBite", PlayerPrefs.GetInt("SaveThrVBite"));
                PlayerPrefs.SetInt("Neutrality", PlayerPrefs.GetInt("SaveThrNeutral"));
                PlayerPrefs.SetInt("Intoxication", PlayerPrefs.GetInt("SaveThrIntox"));
                PlayerPrefs.SetInt("ViewApproval", PlayerPrefs.GetInt("SaveThrVApprov"));
                if (PlayerPrefs.HasKey("SaveThrInquisitorBoss")){
                    PlayerPrefs.SetInt("InquisitorBoss", PlayerPrefs.GetInt("SaveThrInquisitorBoss"));
                }

                if (PlayerPrefs.HasKey("SaveThrChildBoss"))
                {
                    PlayerPrefs.SetInt("ChildBoss", PlayerPrefs.GetInt("SaveThrChildBoss"));
                }
            }

            PlayerPrefs.SetInt("SavedData", 1);
            levelLoader.LoadLevel(PlayerPrefs.GetInt("SaveThrCS"));
            //SceneManager.LoadScene(PlayerPrefs.GetInt("SaveThrCS"));
        }
        else
        {
            EmptySlotNotif.SetActive(true);

            yield return new WaitForSeconds(2f);

            EmptySlotNotif.SetActive(false);
        }
    }

    public IEnumerator LoadTemp()
    {
        
        Debug.Log("LoadTempInvoked");
        yield return new WaitForSeconds(1.4f);

        PlayerPrefs.SetInt("NextStage", 1);
        PlayerPrefs.SetInt("MaxHealthPointRaw", PlayerPrefs.GetInt("SaveTempMHPR"));
        PlayerPrefs.SetInt("PlayerRawDamage", PlayerPrefs.GetInt("SaveTempPDR"));
        PlayerPrefs.SetFloat("CharPosX", PlayerPrefs.GetFloat("SaveTempPosX"));
        PlayerPrefs.SetFloat("CharPosY", PlayerPrefs.GetFloat("SaveTempPosY"));
        PlayerPrefs.SetInt("UnwaveringAttacker", PlayerPrefs.GetInt("STUnwaveringAttacker"));
        PlayerPrefs.SetInt("Toughness", PlayerPrefs.GetInt("STToughness"));
        PlayerPrefs.SetInt("RagingBull", PlayerPrefs.GetInt("STRagingBull"));
        PlayerPrefs.SetInt("BalancedProwess", PlayerPrefs.GetInt("STBalancedProwess"));
        PlayerPrefs.SetInt("MendWounds", PlayerPrefs.GetInt("STMendWounds"));
        PlayerPrefs.SetInt("TotalBuffs", PlayerPrefs.GetInt("STTotalBuffs"));
        PlayerPrefs.SetInt("CutsceneView", 0);

        if (PlayerPrefs.GetInt("SaveTempCS") > 11)
        {
            PlayerPrefs.SetInt("VampireBite", PlayerPrefs.GetInt("SaveTempVBite"));
            PlayerPrefs.SetInt("Neutrality", PlayerPrefs.GetInt("SaveTempNeutral"));
            PlayerPrefs.SetInt("Intoxication", PlayerPrefs.GetInt("SaveTempIntox"));
            PlayerPrefs.SetInt("ViewApproval", PlayerPrefs.GetInt("SaveTempVApprov"));
            //PlayerPrefs.SetFloat("Pontifex_X", PlayerPrefs.GetFloat("SaveTemp_Pontifex_X"));
            //PlayerPrefs.SetFloat("Pontifex_Y", PlayerPrefs.GetFloat("SaveTemp_Pontifex_Y"));
            PlayerPrefs.SetFloat("ChildBoss_X", PlayerPrefs.GetFloat("SaveTemp_Child_X"));
            PlayerPrefs.SetFloat("ChildBoss_Y", PlayerPrefs.GetFloat("SaveTemp_Child_Y"));

        }

        PlayerPrefs.SetInt("SavedData", 1);
        if (PlayerPrefs.HasKey("Escape"))
        {
            if(PlayerPrefs.GetInt("Escape") == 1)
            {
                PlayerPrefs.DeleteKey("Escape");
                levelLoader.LoadLevel(PlayerPrefs.GetInt("SaveTempCS"));
                //SceneManager.LoadScene(PlayerPrefs.GetInt("SaveTempCS"));
            }
        }
        


       
    }

    

}
