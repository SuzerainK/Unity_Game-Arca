using UnityEngine;

public class CharacterDVariables : MonoBehaviour
{
    
    public int intoxicationLevel;
    public int neutrality;
    public int viewApproval;
    public int vampireBite;
    public int helpAccept;

    public GameObject PathWayDialogues;
    
    [Header("Dialogue 6.5")]
    public GameObject dialogue_65_a;
    public GameObject dialogue_65_b;
    public GameObject dialogue_65_ab;
    public GameObject dialogue_65_ba;

    [Header("Dialogue 6.6")]
    public GameObject dialogue_66;
    public GameObject dialogue_66a;
    public GameObject dialogue_66b;
    public GameObject dialogue_66ba;
    public GameObject Child_Boss;
    public GameObject playerChar;
    private Animator childBossAnim;
    private Animator playerAnim;
    public GameObject pontifexAfter;

    [Header("Dialogue 9")]
    public GameObject dialogue_106;
    public GameObject dialogue_105;
    public GameObject dialogue_93a;
    public GameObject dialogue_9FO;
    public GameObject Inquisitor_Avatar;



    // Start is called before the first frame update
    void Awake()
    {
        helpAccept = 0;
        childBossAnim = Child_Boss.GetComponent<Animator>();
        playerAnim = playerChar.GetComponent<Animator>();
        dialogue_65_a.SetActive(false);
        dialogue_65_b.SetActive(false);
        dialogue_65_ab.SetActive(false);
        dialogue_65_ba.SetActive(false);
        dialogue_66.SetActive(false);
        dialogue_66a.SetActive(false);
        dialogue_66b.SetActive(false);
        dialogue_66ba.SetActive(false);
        dialogue_105.SetActive(false);
        dialogue_106.SetActive(false);
        pontifexAfter.SetActive(false);
        PathWayDialogues.SetActive(true);

        neutrality = 100;
        intoxicationLevel = 0;
        viewApproval = 0;
        PlayerPrefs.SetInt("VampireBite", vampireBite);
        PlayerPrefs.SetInt("Neutrality", neutrality);
        PlayerPrefs.SetInt("Intoxication", intoxicationLevel);
        PlayerPrefs.SetInt("ViewApproval", viewApproval);

    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("ChildBoss"))
        {
            if (PlayerPrefs.GetInt("ChildBoss") == 1)
            {
                PathWayDialogues.SetActive(false);
                childBossAnim.Play("NPC_ChildBoy_Fall");
                Debug.Log("PH1");
            }
            else if (PlayerPrefs.GetInt("ChildBoss") == 2)
            {
                PathWayDialogues.SetActive(false);
                playerAnim.Play("Amara_Neutral_Defeat");
                Debug.Log("PH2");
            }
            else
            {
                PathWayDialogues.SetActive(true);
            }
        }
        if (PlayerPrefs.HasKey("InquisitorBoss"))
        {
            if (PlayerPrefs.GetInt("InquisitorBoss") == 1 || PlayerPrefs.GetInt("InquisitorBoss") == 2)
            {
                Inquisitor_Avatar.SetActive(true);
                playerAnim.Play("Amara_Neutral_Defeat");
                dialogue_93a.SetActive(true);

            }
        }

        if(PlayerPrefs.GetInt("InquisitorVisit") == 1)
        {
            Inquisitor_Avatar.SetActive(true);
            dialogue_9FO.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetInt("VampireBite", vampireBite);
        PlayerPrefs.SetInt("Neutrality", neutrality);
        PlayerPrefs.SetInt("Intoxication", intoxicationLevel);
        PlayerPrefs.SetInt("ViewApproval", viewApproval);
  
        if (PlayerPrefs.GetInt("ChildBoss") == 1 && viewApproval >= 3)
        {
            dialogue_65_a.SetActive(false);
            dialogue_65_b.SetActive(false);
            dialogue_65_ab.SetActive(false);
            dialogue_65_ba.SetActive(false);
            dialogue_66.SetActive(false);
            dialogue_66a.SetActive(true);
            pontifexAfter.SetActive(true);

        }
        else if (PlayerPrefs.GetInt("ChildBoss") == 1 && viewApproval < 3)
        {
            dialogue_65_a.SetActive(false);
            dialogue_65_b.SetActive(false);
            dialogue_65_ab.SetActive(false);
            dialogue_65_ba.SetActive(false);
            dialogue_66.SetActive(true);
            dialogue_66a.SetActive(false);
            pontifexAfter.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("ChildBoss") == 2 && vampireBite == 0)
        {
            dialogue_65_a.SetActive(false);
            dialogue_65_b.SetActive(false);
            dialogue_65_ab.SetActive(false);
            dialogue_65_ba.SetActive(false);
            dialogue_66.SetActive(false);
            dialogue_66a.SetActive(false);
            dialogue_66ba.SetActive(false);
            dialogue_66b.SetActive(true);
            pontifexAfter.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("ChildBoss") == 2 && vampireBite == 1)
        {
            dialogue_65_a.SetActive(false);
            dialogue_65_b.SetActive(false);
            dialogue_65_ab.SetActive(false);
            dialogue_65_ba.SetActive(false);
            dialogue_66.SetActive(false);
            dialogue_66a.SetActive(false);
            dialogue_66ba.SetActive(true);
            dialogue_66b.SetActive(false);
            pontifexAfter.SetActive(true);
        }
        else
        {
            if (viewApproval >= 3 && intoxicationLevel == 2)
            {
                if (vampireBite == 1)
                {
                    dialogue_65_ab.SetActive(true);
                    dialogue_65_a.SetActive(false);
                    dialogue_65_b.SetActive(false);
                    dialogue_65_ba.SetActive(false);
                }
                else
                {
                    dialogue_65_a.SetActive(true);
                    dialogue_65_b.SetActive(false);
                    dialogue_65_ab.SetActive(false);
                    dialogue_65_ba.SetActive(false);
                }
            }
            else
            {
                if (vampireBite == 1)
                {
                    dialogue_65_ba.SetActive(true);
                    dialogue_65_ab.SetActive(false);
                    dialogue_65_a.SetActive(false);
                    dialogue_65_b.SetActive(false);
                }
                else
                {
                    dialogue_65_b.SetActive(true);
                    dialogue_65_ab.SetActive(false);
                    dialogue_65_ba.SetActive(false);
                    dialogue_65_a.SetActive(false);
                }
            }
        }


        if(helpAccept == 1 && neutrality >= 80)
        {
            dialogue_106.SetActive(true);
            dialogue_105.SetActive(false);
        }
        else if(helpAccept == 1 && neutrality < 80)
        {
            dialogue_105.SetActive(true);
            dialogue_106.SetActive(false);
        }

    }

    public void Intoxicated(int value)
    {
        intoxicationLevel += value;
    }

    public void NeutralityChange(int value)
    {
        neutrality += value;
        Debug.Log("neutrality added");
        Debug.Log(neutrality);
    }

    public void ViewApprove(int value)
    {
        viewApproval += value;
    }

    public void VampireBite(int value)
    {
        vampireBite += 1;
    }
}
