using UnityEngine;

public class SceneShow : MonoBehaviour
{
    [Header("DormitoryScene")]
    [SerializeField] private GameObject Vampire;
    [SerializeField] private GameObject Maidservant;

    [Header("Gardens")]
    [SerializeField] private GameObject Maidservant1;
    [SerializeField] private GameObject Maidservant2;
    [SerializeField] private GameObject Maidservant3;
    [SerializeField] private GameObject Maidservant4;
    [SerializeField] private GameObject Pontifex;
    [SerializeField] private GameObject Inquisitor;

    [Header("Camera")]
    [SerializeField] private GameObject Camera1;
    [SerializeField] private GameObject Camera2;
    [SerializeField] private GameObject Camera3;

    public GameObject Room4Alt;
    public GameObject Room4;
    public GameObject GuardIgnore;
    public GameObject snowPs;
    public GameObject FiringLine;
    public GameObject FiringLine2;
    private GameObject Player;

    private Animator Vampire_Anim;
    private CharacterMovements charMove;

    [SerializeField] private GameObject D10_affirmation;
    [SerializeField] private GameObject D9_FaceOff;
    [SerializeField] private GameObject Inquisitor_Visit;




    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        charMove = Player.GetComponent<CharacterMovements>();
        FiringLine.SetActive(false);
        FiringLine2.SetActive(false);
        snowPs.SetActive(false);
        GuardIgnore.SetActive(false);
        Vampire.SetActive(false);
        Maidservant1.SetActive(false);
        Maidservant2.SetActive(false);
        Maidservant3.SetActive(false);
        Maidservant4.SetActive(false);
        Camera2.SetActive(false);
        Camera3.SetActive(false);
        D10_affirmation.SetActive(false);
    }

    private void Start()
    {
        Vampire_Anim = Vampire.GetComponent<Animator>();
    }

    public void DialoguePlay(string value)
    {
        switch (value)
        {
            case "Dialogue01_Over":
                Maidservant.SetActive(false);
                break;
            case "Dialogue03":
                Vampire.SetActive(true);
                break;
            case "Dialogue03_Over":
                Room4Alt.SetActive(false);
                Room4.SetActive(true);
                Vampire.SetActive(false);
                break;
            case "Dialogue04_Ignore":
                GuardIgnore.SetActive(true);
                break;
            case "Dialogue05_Start":
                Maidservant1.SetActive(true);
                Maidservant2.SetActive(true);
                Maidservant3.SetActive(true);
                Maidservant4.SetActive(true);
                break;
            case "Dialogue05_Start_1":
                Camera1.SetActive(false);
                Camera2.SetActive(true);
                Pontifex.SetActive(true);
                Inquisitor.SetActive(true);
                break;
            case "Dialogue05_End":
                Camera2.SetActive(false);
                Camera1.SetActive(true);
                break;
            case "Dialogue05_GardenExit":
                Pontifex.SetActive(false);
                Inquisitor.SetActive(false);
                break;
            case "Dialogue65_Start":
                Camera1.SetActive(false);
                Camera3.SetActive(true);
                break;
            case "Dialogue65_Over":
                Camera1.SetActive(true);
                Camera3.SetActive(false);
                break;
            case "Dialogue7_Start":
                FiringLine.SetActive(true);
                snowPs.SetActive(true);
                charMove.jumpPower *= 1.5f;
                charMove.moveSpeed *= 2;
                break;
            case "Dialogue7_GameEnd":
                FiringLine.SetActive(false);
                FiringLine2.SetActive(false);
                charMove.jumpPower /= 1.5f;
                charMove.moveSpeed /= 2;
                break;
            case "Dialogue10_Start":
                D10_affirmation.SetActive(true);
                break;
            case "Dialogue10_End":
                D10_affirmation.SetActive(false);
                break;
            case "StartTrialFail":
                PlayerPrefs.SetInt("InquisitorVisit", 1);
                D9_FaceOff.SetActive(true);
                Inquisitor_Visit.SetActive(true);
                break;
            default:
                Debug.Log("No Dialogue is playing.");
                break;
        }
    }
}
