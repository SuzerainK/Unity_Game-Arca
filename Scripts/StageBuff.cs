using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StageBuff : MonoBehaviour
{
    public GameObject BuffScreen;
    public GameObject BuffWindow_1;
    public GameObject BuffWindow_2;

    Button BuffButton_1;
    Button BuffButton_2;

    private TextMeshProUGUI CurrentBuffLevel_1;
    private TextMeshProUGUI CurrentBuffLevel_2;

    private TextMeshProUGUI BuffDescription_1;
    private TextMeshProUGUI BuffDescription_2;

    private TextMeshProUGUI BuffTitle_1;
    private TextMeshProUGUI BuffTitle_2;

    private int Randomizer_1;
    private int Randomizer_2;

    private int buffInitialLevel;
    private int buffNewLevel;

    Animator BuffWindowAnim_1;
    Animator BuffWindowAnim_2;

    private static StageBuff instance;

    public bool stageBuffSelectionOn { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one StageBuff script in the scene!");
        }
        instance = this;
    }

    public static StageBuff GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        if(PlayerPrefs.GetInt("TotalBuffs") < (SceneManager.GetActiveScene().buildIndex - 1))
        {
            BuffScreen.SetActive(true);
            BuffWindowAnim_1 = BuffWindow_1.GetComponent<Animator>();
            BuffWindowAnim_2 = BuffWindow_2.GetComponent<Animator>();
            BuffButton_1 = BuffWindow_1.GetComponent<Button>();
            BuffButton_2 = BuffWindow_2.GetComponent<Button>();
            CurrentBuffLevel_1 = BuffWindow_1.transform.Find("BuffCurrentLevel").GetComponent<TextMeshProUGUI>();
            CurrentBuffLevel_2 = BuffWindow_2.transform.Find("BuffCurrentLevel").GetComponent<TextMeshProUGUI>();
            BuffDescription_1 = BuffWindow_1.transform.Find("BuffDescription").GetComponent<TextMeshProUGUI>();
            BuffDescription_2 = BuffWindow_2.transform.Find("BuffDescription").GetComponent<TextMeshProUGUI>();
            BuffTitle_1 = BuffWindow_1.transform.Find("BuffTitle").GetComponent<TextMeshProUGUI>();
            BuffTitle_2 = BuffWindow_2.transform.Find("BuffTitle").GetComponent<TextMeshProUGUI>();
            BuffWindow_1.SetActive(false);
            BuffWindow_2.SetActive(false);
            stageBuffSelectionOn = false;
            BuffRoll();
        }
        else if(PlayerPrefs.GetString("Difficulty") == "Easy" && PlayerPrefs.GetInt("TotalBuffs") != (SceneManager.GetActiveScene().buildIndex - 1) * 2){
            BuffScreen.SetActive(true);
            BuffWindowAnim_1 = BuffWindow_1.GetComponent<Animator>();
            BuffWindowAnim_2 = BuffWindow_2.GetComponent<Animator>();
            BuffButton_1 = BuffWindow_1.GetComponent<Button>();
            BuffButton_2 = BuffWindow_2.GetComponent<Button>();
            CurrentBuffLevel_1 = BuffWindow_1.transform.Find("BuffCurrentLevel").GetComponent<TextMeshProUGUI>();
            CurrentBuffLevel_2 = BuffWindow_2.transform.Find("BuffCurrentLevel").GetComponent<TextMeshProUGUI>();
            BuffDescription_1 = BuffWindow_1.transform.Find("BuffDescription").GetComponent<TextMeshProUGUI>();
            BuffDescription_2 = BuffWindow_2.transform.Find("BuffDescription").GetComponent<TextMeshProUGUI>();
            BuffTitle_1 = BuffWindow_1.transform.Find("BuffTitle").GetComponent<TextMeshProUGUI>();
            BuffTitle_2 = BuffWindow_2.transform.Find("BuffTitle").GetComponent<TextMeshProUGUI>();
            BuffWindow_1.SetActive(false);
            BuffWindow_2.SetActive(false);
            stageBuffSelectionOn = false;
            BuffRoll();
        }
        else
        {
            BuffScreen.SetActive(false);
        }
        
    }

    int Randomize(int x, int y)
    {
        int RandomizedValue = Random.Range(x, y);
        return RandomizedValue;
    }


    void BuffRoll()
    {
        Randomizer_1 = Randomize(1, 6);
        Randomizer_2 = Randomize(1, 6);
        BuffWindow_1.SetActive(false);
        BuffWindow_2.SetActive(false);
        BuffButton_1.interactable = true;
        BuffButton_2.interactable = true;
        stageBuffSelectionOn = true;
        //yield return new WaitForSeconds(1f);
        BuffWindow_1.SetActive(true);
        BuffWindow_2.SetActive(true);
        while(Randomizer_2 == Randomizer_1)
            Randomizer_2 = Randomize(1, 6);

        switch (Randomizer_1)
        {
            case 1:
                CurrentBuffLevel_1.text = "Current Buff Lv: " + PlayerPrefs.GetInt("UnwaveringAttacker");
                BuffDescription_1.text = "Damage increases by 20% but maximum health points are reduced by 10%";
                BuffTitle_1.text = "Unwavering Attacker";
                break;
            case 2:
                CurrentBuffLevel_1.text = "Current Buff Lv: " + PlayerPrefs.GetInt("Toughness");
                BuffDescription_1.text = "Maximum health points increase by 30% but player damage is reduced by 5%";
                BuffTitle_1.text = "Toughness";
                break;
            case 3:
                CurrentBuffLevel_1.text = "Current Buff Lv: " + PlayerPrefs.GetInt("RagingBull");
                BuffDescription_1.text = "Rage point accumulation increases by 50%";
                BuffTitle_1.text = "Raging Bull";
                break;
            case 4:
                CurrentBuffLevel_1.text = "Current Buff Lv: " + PlayerPrefs.GetInt("BalancedProwess");
                BuffDescription_1.text = "Increases damage by 10% and maximum health points by 10%";
                BuffTitle_1.text = "Balanced Prowess";
                break;
            case 5:
                CurrentBuffLevel_1.text = "Current Buff Lv: " + PlayerPrefs.GetInt("MendWounds");
                BuffDescription_1.text = "Recovers 25% of melee damage dealt as health points";
                BuffTitle_1.text = "Mend Wounds";
                break;
            default:
                Debug.Log("Error in StageBuffs.");
                break;

        }

        switch (Randomizer_2)
        {
            case 1:
                CurrentBuffLevel_2.text = "Current Buff Lv: " + PlayerPrefs.GetInt("UnwaveringAttacker");
                BuffDescription_2.text = "Damage increases by 20% but maximum health points are reduced by 10%";
                BuffTitle_2.text = "Unwavering Attacker";
                break;
            case 2:
                CurrentBuffLevel_2.text = "Current Buff Lv: " + PlayerPrefs.GetInt("Toughness");
                BuffDescription_2.text = "Maximum health points increase by 30% but player damage is reduced by 5%";
                BuffTitle_2.text = "Toughness";
                break;
            case 3:
                CurrentBuffLevel_2.text = "Current Buff Lv: " + PlayerPrefs.GetInt("RagingBull");
                BuffDescription_2.text = "Rage point accumulation increases by 50%";
                BuffTitle_2.text = "Raging Bull";
                break;
            case 4:
                CurrentBuffLevel_2.text = "Current Buff Lv: " + PlayerPrefs.GetInt("BalancedProwess");
                BuffDescription_2.text = "Increases damage by 10% and maximum health points by 10%";
                BuffTitle_2.text = "Balanced Prowess";
                break;
            case 5:
                CurrentBuffLevel_2.text = "Current Buff Lv: " + PlayerPrefs.GetInt("MendWounds");
                BuffDescription_2.text = "Recovers 25% of damage dealt as health points";
                BuffTitle_2.text = "Mend Wounds";
                break;
            default:
                Debug.Log("Error in StageBuffs.");
                break;
        }
        //yield return new WaitForSeconds(1f);
    }

    public void ChooseBuff_1()
    {
        switch (Randomizer_1)
        {
            case 1:
                buffInitialLevel = PlayerPrefs.GetInt("UnwaveringAttacker");
                buffNewLevel = buffInitialLevel + 1;
                PlayerPrefs.SetInt("UnwaveringAttacker", buffNewLevel);
                break;
            case 2:
                buffInitialLevel = PlayerPrefs.GetInt("Toughness");
                buffNewLevel = buffInitialLevel + 1;
                PlayerPrefs.SetInt("Toughness", buffNewLevel);
                break;
            case 3:
                buffInitialLevel = PlayerPrefs.GetInt("RagingBull");
                buffNewLevel = buffInitialLevel + 1;
                PlayerPrefs.SetInt("RagingBull", buffNewLevel);
                break;
            case 4:
                buffInitialLevel = PlayerPrefs.GetInt("BalancedProwess");
                buffNewLevel = buffInitialLevel + 1;
                PlayerPrefs.SetInt("BalancedProwess", buffNewLevel);
                break;
            case 5:
                buffInitialLevel = PlayerPrefs.GetInt("MendWounds");
                buffNewLevel = buffInitialLevel + 1;
                PlayerPrefs.SetInt("MendWounds", buffNewLevel);
                break;
            default:
                Debug.Log("ChooseBuff Error");
                break;
        }

        BuffButton_1.interactable = false;
        BuffButton_2.interactable = false;
        BuffWindowAnim_1.Play("BuffWindowEnd");
        StartCoroutine(EndChooseBuffs());

    }

    public void ChooseBuff_2()
    {
        switch (Randomizer_2)
        {
            case 1:
                buffInitialLevel = PlayerPrefs.GetInt("UnwaveringAttacker");
                buffNewLevel = buffInitialLevel + 1;
                PlayerPrefs.SetInt("UnwaveringAttacker", buffNewLevel);
                break;
            case 2:
                buffInitialLevel = PlayerPrefs.GetInt("Toughness");
                buffNewLevel = buffInitialLevel + 1;
                PlayerPrefs.SetInt("Toughness", buffNewLevel);
                break;
            case 3:
                buffInitialLevel = PlayerPrefs.GetInt("RagingBull");
                buffNewLevel = buffInitialLevel + 1;
                PlayerPrefs.SetInt("RagingBull", buffNewLevel);
                break;
            case 4:
                buffInitialLevel = PlayerPrefs.GetInt("BalancedProwess");
                buffNewLevel = buffInitialLevel + 1;
                PlayerPrefs.SetInt("BalancedProwess", buffNewLevel);
                break;
            case 5:
                buffInitialLevel = PlayerPrefs.GetInt("MendWounds");
                buffNewLevel = buffInitialLevel + 1;
                PlayerPrefs.SetInt("MendWounds", buffNewLevel);
                break;
            default:
                Debug.Log("ChooseBuff Error");
                break;
        }

        BuffButton_1.interactable = false;
        BuffButton_2.interactable = false;
        BuffWindowAnim_2.Play("BuffWindowEnd");
        StartCoroutine(EndChooseBuffs());
    }

    IEnumerator EndChooseBuffs()
    {
        PlayerPrefs.SetInt("TotalBuffs", PlayerPrefs.GetInt("TotalBuffs") + 1);
        stageBuffSelectionOn = false;
        
        if(PlayerPrefs.GetString("Difficulty") == "Easy" && PlayerPrefs.GetInt("TotalBuffs") != (SceneManager.GetActiveScene().buildIndex - 1) * 2)
        {
            yield return new WaitForSeconds(1f);
            BuffRoll();
        }
        else
        {
            yield return new WaitForSeconds(1f);
            BuffScreen.SetActive(false);
        }
    }






}
