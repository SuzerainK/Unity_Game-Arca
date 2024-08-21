using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private float typingSpeed = 0.04f;
    [SerializeField] private float transitionTime = 1f;
    [Header("Dialogue UI")]

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject continueIcon;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject mainUI;
    [SerializeField] private TextMeshProUGUI displayNameText;
    [SerializeField] private Animator portraitAnimator;
    [SerializeField] private Animator dialogueImage;
    [SerializeField] private Animator gameOver;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject AudioMGame;
    [SerializeField] private GameObject VoiceMGame;

    public GameObject SceneShowManager;
    private string sceneName;
    private Animator layoutAnimator;
    public Animator transition;
    
    AudioManager audioManager;
    VoiceManager voiceManager;
    SceneShow scenePlay;

    [Header("Choices UI")]

    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;
    private Story currentStory;

    private static DialogueManager instance;
    public bool dialogueIsPlaying { get; private set; }

    private bool canContinueToNextLine = false;
    private Coroutine displayLineCoroutine;

    //Tags used to send variables inside the dialogues to communicate to other game scripts.
    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    private const string LAYOUT_TAG = "layout";
    private const string COMBAT_TAG = "combat";
    private const string MOVE_TAG = "move";
    private const string IMAGE_TAG = "image";
    private const string OVER_TAG = "over";
    private const string ACTION_TAG = "action";
    private const string SFX_TAG = "sfx";
    private const string BGM_TAG = "bgm";
    private const string INTOX_TAG = "intox";
    private const string NEUTRAL_TAG = "neutrality";
    private const string AGREE_TAG = "agree";
    private const string STEP_TAG = "step";
    private const string CHILD_ATTACK_TAG = "child_attack";
    private const string VAMPIRE_BITE_TAG = "vampire";
    private const string CAMERA_TAG = "camera";
    private const string LOAD_TAG = "load";
    private const string HELP_TAG = "help";
    private const string ESCAPE_TAG = "escape";
    private const string CUTSCENE_TAG = "cutscene";
    private const string SAVESTOP_TAG = "nosave";
    private const string POSITION_TAG = "position";
    private const string VOICE_TAG = "voice";
    private const string VOLUME_TAG = "volume";

    private GameObject InterStageManager;
    public GameObject DialogueStatsManager;
    private GameObject CurrentStep;
    private Camera mainCamera;

    SavedPlayerPos playerPosData;
    InterStageMovement changePosition;
    CharacterDVariables dialogueStats;
    PontifexMove moveThyNPC;
    ChildMoveDialogue6 childmoveD6;
    CameraScaleChanger cameraScale;
    ScnLoad sceneLoader;
    public LevelLoader levelLoader;

    public GameObject CharacterPlayer;
    
    private void Awake()
    {
        gameOverUI.SetActive(false);
        if(instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene!");
        }
        instance = this;
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex >= 12)
        {
            if(PlayerPrefs.GetInt("ChildBoss") == 0)
            {
                childmoveD6 = GameObject.FindGameObjectWithTag("ChildQ").GetComponent<ChildMoveDialogue6>();
            }

            CurrentStep = GameObject.FindGameObjectWithTag("Step");
            moveThyNPC = CurrentStep.GetComponent<PontifexMove>();
            dialogueStats = DialogueStatsManager.GetComponent<CharacterDVariables>();
            scenePlay = SceneShowManager.GetComponent<SceneShow>();
            
            InterStageManager = GameObject.FindGameObjectWithTag("InterMovement");
            changePosition = InterStageManager.GetComponent<InterStageMovement>();
        }
        
        audioManager = AudioMGame.GetComponent<AudioManager>();
        if(SceneManager.GetActiveScene().buildIndex < 12)
            voiceManager = VoiceMGame.GetComponent<VoiceManager>();

        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        PlayerPrefs.SetInt("Saved", 0);
        playerPosData = FindObjectOfType<SavedPlayerPos>();
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        mainUI.SetActive(true);

        layoutAnimator = dialoguePanel.GetComponent<Animator>();
        cameraScale = mainCamera.GetComponent<CameraScaleChanger>();
        sceneLoader = GameObject.FindGameObjectWithTag("Manager").GetComponent<ScnLoad>();


        //get all of the choices text
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach(GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {
        // return right away if dialogue isn't playing
        if (!dialogueIsPlaying)
        {
            return;
        }
        else
        {
            //Debug.Log("Dialogue is Playing");
        }
        //handle continuing to the next line in the dialogue when submit is pressed
        if (canContinueToNextLine &&
            currentStory.currentChoices.Count == 0 &&
            InputManager.GetInstance().GetSubmitPressed())
        {
            ContinueStory();
        }
    }
    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        mainUI.SetActive(false);

        displayNameText.text = "???";
        portraitAnimator.Play("default");
        layoutAnimator.Play("right");

        ContinueStory();

    }

    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.2f);

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        mainUI.SetActive(true);
        dialogueText.text = "";
        
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            if(displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }
            // set text for the current dialogue line
            displayLineCoroutine = StartCoroutine(DisplayLine(currentStory.Continue()));
            
            HandleTags(currentStory.currentTags);
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    private IEnumerator DisplayLine(string line)
    {
        // empty the dialogue text
        dialogueText.text = "";

        //hide items while text is typing
        continueIcon.SetActive(false);
        HideChoices();

        canContinueToNextLine = false;

        // display each letter one at a time
        foreach(char letter in line.ToCharArray())
        {
            // if the submit button is pressed, finish up displaying the line right away
            if (InputManager.GetInstance().GetSubmitPressed())
            {
                dialogueText.text = line;
                break;
            }
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        //actions to take after the entire line has finished displaying
        continueIcon.SetActive(true);

        // display choices, if any, for this dialogue line
        DisplayChoices();

        canContinueToNextLine = true;
    }

    private void HideChoices()
    {
        foreach(GameObject choiceButton in choices)
        {
            choiceButton.SetActive(false);
        }
    }

    private void HandleTags(List<string> currentTags)
    {
        // loop through each tag and handle it accordingly
        foreach(string tag in currentTags)
        {
            //parse the tag
            string[] splitTag = tag.Split(':');
            if(splitTag.Length != 2)
            {
                Debug.LogError("Tag could not be appropriately parsed: " + tag);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            
            //handle the tag
            switch (tagKey)
            {
                case SPEAKER_TAG:
                    displayNameText.text = tagValue;
                    break;
                case PORTRAIT_TAG:
                    portraitAnimator.Play(tagValue);
                    break;
                case LAYOUT_TAG:
                    layoutAnimator.Play(tagValue);
                    break;
                case COMBAT_TAG:
                    sceneName = tagValue;
                    StartCoroutine(LoadCombatScene(sceneName));
                    break;
                case MOVE_TAG:
                    DoorsMovement(tagValue);
                    break;
                case IMAGE_TAG:
                    dialogueImage.Play(tagValue);
                    break;
                case OVER_TAG:
                    StartCoroutine(GameOverInstance(tagValue));
                    break;
                case ACTION_TAG:
                    scenePlay.DialoguePlay(tagValue);
                    break;
                case SFX_TAG:
                    audioManager.Play(tagValue);
                    break;
                case BGM_TAG:
                    AudioPlay(tagValue);
                    break;
                case INTOX_TAG:
                    dialogueStats.Intoxicated(int.Parse(tagValue));
                    break;
                case NEUTRAL_TAG:
                    dialogueStats.NeutralityChange(int.Parse(tagValue));
                    break;
                case AGREE_TAG:
                    dialogueStats.ViewApprove(int.Parse(tagValue));
                    break;
                case STEP_TAG:
                    Debug.Log("PontifexStepped");
                    moveThyNPC.MoveNow(tagValue);
                    break;
                case CHILD_ATTACK_TAG:
                    childmoveD6.ChildQueue(tagValue);
                    break;
                case VAMPIRE_BITE_TAG:
                    dialogueStats.VampireBite(int.Parse(tagValue));
                    break;
                case CAMERA_TAG:
                    cameraScale.CameraFieldOfView(int.Parse(tagValue));
                    break;
                case LOAD_TAG:
                    sceneLoader.OnLoadTemp();
                    break;
                case HELP_TAG:
                    dialogueStats.helpAccept += int.Parse(tagValue);
                    break;
                case ESCAPE_TAG:
                    PlayerPrefs.SetInt("Escape", int.Parse(tagValue));
                    break;
                case CUTSCENE_TAG:
                    StartCoroutine(LoadCutscene(int.Parse(tagValue)));
                    break;
                case SAVESTOP_TAG:
                    PlayerPrefs.SetInt("AllowSave", int.Parse(tagValue));
                    break;
                case POSITION_TAG:
                    SavePosition(int.Parse(tagValue));
                    break;
                case VOICE_TAG:
                    VoicePlay(tagValue);
                    break;
                case VOLUME_TAG:
                    VolumeChange(int.Parse(tagValue));
                    break;
                default:
                    Debug.LogWarning("Tag came in but is not currently being handled: " + tag);
                    break;
            }
        }
    }

    private void SavePosition(int yes)
    {
        if (yes == 1)
        {
            PlayerPrefs.SetFloat("CharPosX", CharacterPlayer.transform.position.x);
            PlayerPrefs.SetFloat("CharPosY", CharacterPlayer.transform.position.y);
            PlayerPrefs.SetInt("SavedData", 1);
        }
    }

    private void VoicePlay(string voice_name)
    {
        audioManager.LowerVolume();
        voiceManager.StopAll();
        voiceManager.Play(voice_name);
        audioManager.RaiseVolume();
    }

    private void VolumeChange(int enabled)
    {
        if(enabled == 0)
        {
            audioManager.LowerVolume();
        }
        else
        {
            audioManager.RaiseVolume();
        }
    }
    private void AudioPlay(string audio_name)
    {
        audioManager.Stop("BGM");
        audioManager.Stop("BGMViolinSolo");
        audioManager.Stop("BGMDeath");
        audioManager.Stop("BGMEscapeRoute");
        audioManager.Stop("BGMGardens");
        audioManager.Stop("BGMHouseofVirgins");
        audioManager.Stop("BGMSpeech");
        audioManager.Stop("BGMChildBoss");
        audioManager.Stop("BGMSadPiano");
        audioManager.Stop("BGMInquisitorBoss");
        audioManager.Stop("BGMHouseofVirgins");
        audioManager.Stop("BGMCathedral");
        audioManager.Play(audio_name);
    }
    private IEnumerator LoadCombatScene(string sceneName)
    {
        playerPosData.PlayerPosSave();
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);
        levelLoader.LoadLevelByName(sceneName);
    }

    private IEnumerator LoadCutscene(int BuildNumber)
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("Cutscene is Loading");
        SceneManager.LoadScene(BuildNumber);
       
    }

    private void DoorsMovement(string door_name)
    {
        
        playerPosData.PlayerPosSave();
        changePosition.ChangeLocation(door_name);
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        //Defensive check to make sure our UI can support the number of choices coming in
        if(currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices given: " + currentChoices.Count);
        }

        int index = 0;
        // enable and initialize the choices up to the amount of choices for this line of dialogue
        foreach(Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        // go through the remaining choices the UI supports and make sure they're hidden.
        for(int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }
        StartCoroutine(SelectFirstChoice());
    }
    private IEnumerator GameOverInstance(string tagValue)
    {

        gameOverUI.gameObject.SetActive(true);
        gameOver.Play(tagValue);
        yield return new WaitForSeconds(5f);
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(0);

    }
    private IEnumerator SelectFirstChoice()
    {
        //Event System requires we clear it first, then wait
        // for at least one frame before we set the current selected object.
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);

    }

    public void MakeChoice(int choiceIndex)
    {
        if (canContinueToNextLine)
        {
            currentStory.ChooseChoiceIndex(choiceIndex);

            InputManager.GetInstance().RegisterSubmitPressed();
            ContinueStory();
        }
        
    }
}
