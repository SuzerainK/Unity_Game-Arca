using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public enum BattleState { START, ACTIONCHOSEN, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    //Editable Objects
    [SerializeField] private string unitType;
    [SerializeField] private int sceneNumber;
    public Animator transition;
    public GameObject transitionUI;
    public float transitionTime = 1.2f;
    public bool human;
    public bool boss_1st;
    public bool child;
    public bool inquisitor;
    public int enemyNumbers;
    private int bulletsRemaining = 5;
    private int thisScene;
    private BoxCollider2D enemyCollider1;
    private BoxCollider2D enemyCollider2;
    private BoxCollider2D enemyCollider3;

    public ParticleSystem ps;
    public ParticleSystem Playerps;
    public GameObject playerPrefab;
    public GameObject enemyPrefab1;
    public GameObject enemyPrefab2;
    public GameObject enemyPrefab3;
    public GameObject playerAvatar;
    public GameObject enemyAvatar;

    //Animators for Enemies on the Field
    private Animator animate;
    private Animator animateEnemy1;
    private Animator animateEnemy2;
    private Animator animateEnemy3;
    public Animator animateShield;

    //Combat UI Additives
    public GameObject CombatGauge;
    public GameObject rageOptions;
    public GameObject bulletAmmo;
    public GameObject EnemyHUDs;
        
    public GameObject Shield;

    public int BossMPAccumulated = 0;
    public int TurnsUntilBossAttack = 5;

    public Transform avatarPlacement;
    public Transform playerBattleStation;
    public Transform enemyBattleStation1;
    public Transform enemyBattleStation2;
    public Transform enemyBattleStation3;

    //Unit script references
    Unit playerUnit;
    Unit enemyUnit1;
    Unit enemyUnit2;
    Unit enemyUnit3;

    //In-combat variables
    public int numeration;
    public int rageLevel = 0;
    public int enemySelected = 1;
    public int[] enemyTurn;

    //HUD UI Additives
    public BattleHUD playerHUD;
    public BattleHUD enemyHUD1;
    public BattleHUD enemyHUD2;
    public BattleHUD enemyHUD3;
    public GameObject HUD_UI1;
    public GameObject HUD_UI2;
    public GameObject HUD_UI3;
    public GameObject Select_1;
    public GameObject Select_2;
    public GameObject Select_3;
    public RageSystemHUD rageBar;
    
    //TextMeshPro items
    public TextMeshProUGUI moveText;
    public TextMeshProUGUI dialogueText;

    public BattleState state;

    bool HealCharge = false;
    bool MagicCharge = false;
    bool EnemyMagicCharge1 = false;
    bool EnemyMagicCharge2 = false;
    bool EnemyMagicCharge3 = false;
    [SerializeField] bool EnemyBlock1 = false;
    [SerializeField] bool EnemyBlock2 = false;
    [SerializeField] bool EnemyBlock3 = false;
    [SerializeField] bool PBlock = false;
    [SerializeField] bool EnemyAlive1 = false;
    [SerializeField] bool EnemyAlive2 = false;
    [SerializeField] bool EnemyAlive3 = false;
    [SerializeField] bool EnemyMoved = false;
    
    
    private int TurnRandomizer;
    private int DamageRandomizer;
    private int MPGainRandomizer;
    private int TurnReqRandomizer;
    private int defeatValue;
    private int bossAchievement;
    private int rageShieldToughness = 0;
    private float enemyAddPosY = 0.5f;
    private float enemyAddPosX = 1f;

    SpawnDestroy sd;

    private int BossMP = 0;
    private int TurnsMade = 5;

    [SerializeField] private bool parryAble = false;
    private bool parryAction = false;
    private bool counterExecution = false;
    [SerializeField] private bool enemyAttacking = false;
    
    
    static ArrayList moveList = new ArrayList();

    public GameObject BlockButton;
    public GameObject ParryButton;

    public GameObject MGatherButton;
    public GameObject MReleaseButton;

    ScnLoad sceneLoader;
    Slider ammoSlider;

    private bool firstArt;
    private bool secondArt;
    private bool thirdArt;

    private int powerUP = 0;

    //Reference for EnemyColliderDetector Script
    EnemyColliderDetector EnemyGO1Component;
    EnemyColliderDetector EnemyGO2Component;
    EnemyColliderDetector EnemyGO3Component;

    LevelLoader levelLoader;


    void Start()
    {
        levelLoader = GetComponent<LevelLoader>();
        sceneLoader = GameObject.FindGameObjectWithTag("Battle System").GetComponent<ScnLoad>();
        
        thisScene = SceneManager.GetActiveScene().buildIndex;
        if (thisScene == 13)
        {
            child = true;
            ParryButton.SetActive(false);
        }
        else if(thisScene == 14)
        {
            ParryButton.SetActive(false);
        }
        else if(thisScene < 13)
        {
            MReleaseButton.SetActive(false);
            ParryButton.SetActive(false);
        }

        rageLevel = 0;
        rageBar.SetRageHUD(rageLevel);

        Debug.Log("BattleStartHPRaw = " + PlayerPrefs.GetInt("HealthBarRaw").ToString());
        PlayerPrefs.SetInt("NewStage", 0);
        enemyAvatar.SetActive(false);
        playerAvatar.SetActive(false);
        numeration = PlayerPrefs.GetInt("EnemyNum");
        state = BattleState.START;
        StartCoroutine(SetupBattle());
        //parryAble = false;
        ParticleSystem ps = GetComponent<ParticleSystem>();
        if(thisScene >= 13)
        {
            ammoSlider = bulletAmmo.GetComponent<Slider>();
            ammoSlider.maxValue = 5;
            ammoSlider.minValue = 0;
            ammoSlider.value = bulletsRemaining;
        }

        
        


    }
    private void Update()
    {
        if(bulletsRemaining > 5)
        {
            bulletsRemaining = 5;
        }

        if (child || inquisitor)
        {
            if (state == BattleState.ENEMYTURN && PBlock)
            {
                if (BlockButton != null && ParryButton != null)
                {
                    BlockButton.SetActive(false);
                    ParryButton.SetActive(true);
                }

            }
            else
            {
                BlockButton.SetActive(true);
                ParryButton.SetActive(false);
            }
        }

        if (state == BattleState.ENEMYTURN && PBlock)
        {
            if (BlockButton != null && ParryButton != null)
            {
                BlockButton.SetActive(false);
                ParryButton.SetActive(true);
            }

        }
        else
        {
            BlockButton.SetActive(true);
            ParryButton.SetActive(false);
        }

        if (thisScene >= 13)
        {
            ammoSlider.value = bulletsRemaining;
        }

        if(enemySelected == 1)
        {
            unitType = enemyUnit1.unitType;
        }
        else if(enemySelected == 2)
        {
            unitType = enemyUnit2.unitType;
        }
        else if(enemySelected == 3)
        {
            unitType = enemyUnit3.unitType;
        }
    }

    IEnumerator WaitForEndOfFrame()
    {
        

        if (EnemyAlive1 && !EnemyAlive2 && !EnemyAlive3)
        {
            enemyTurn = new int[] { 1 };
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        else if (EnemyAlive1 && EnemyAlive2 && !EnemyAlive3)
        {
            enemyTurn = new int[] { 1, 2 };
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        else if (EnemyAlive1 && EnemyAlive2 && EnemyAlive3)
        {
            enemyTurn = new int[] { 1, 2, 3 };
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        else if (EnemyAlive1 && !EnemyAlive2 && EnemyAlive3)
        {
            enemyTurn = new int[] { 1, 3 };
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        else if (!EnemyAlive1 && EnemyAlive2 && EnemyAlive3)
        {
            enemyTurn = new int[] { 2, 3 };
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        else if (!EnemyAlive1 && EnemyAlive2 && !EnemyAlive3)
        {
            enemyTurn = new int[] { 2 };
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        else if (!EnemyAlive1 && !EnemyAlive2 && EnemyAlive3)
        {
            enemyTurn = new int[] { 3 };
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        else
        {
            state = BattleState.WON;
            if (state == BattleState.WON)
            {
                Debug.Log("battle state won");
            }
            StartCoroutine(EndBattle());
        }

        yield return new WaitForEndOfFrame();
    }

    IEnumerator SetupBattle()
    {
        rageOptions.SetActive(false);
        Shield.SetActive(false);
        yield return new WaitForSeconds(transitionTime);
        transitionUI.SetActive(false);

        //Summons sprites in the screen
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();
        if (enemyNumbers == 1)
        {
            GameObject enemyGO1 = Instantiate(enemyPrefab1, enemyBattleStation1);
            enemyCollider1 = enemyPrefab1.GetComponent<BoxCollider2D>();
            enemyUnit1 = enemyGO1.GetComponent<Unit>();
            animate = playerGO.GetComponent<Animator>();
            animateEnemy1 = enemyGO1.GetComponent<Animator>();
            EnemyAlive1 = true;
            HUD_UI2.SetActive(false);
            HUD_UI3.SetActive(false);
            //Assigns the unit stats to be shown in the HUD
            playerHUD.SetHUD(playerUnit);
            enemyHUD1.SetHUD(enemyUnit1);
            Select_1 = enemyGO1.transform.Find("Selected_UI").gameObject;
            EnemyGO1Component = enemyGO1.GetComponent<EnemyColliderDetector>();
            Select_1.SetActive(false);
            enemyBattleStation1.transform.localPosition = new Vector3(
                enemyBattleStation1.transform.localPosition.x, 
                enemyBattleStation1.transform.localPosition.y + enemyAddPosY, 0);
            enemyTurn = new int[] { 1 };

        } else if (enemyNumbers == 2)
        {
            GameObject enemyGO1 = Instantiate(enemyPrefab1, enemyBattleStation1);
            enemyCollider1 = enemyPrefab1.GetComponent<BoxCollider2D>();
            enemyUnit1 = enemyGO1.GetComponent<Unit>();
            animateEnemy1 = enemyGO1.GetComponent<Animator>();
            GameObject enemyGO2 = Instantiate(enemyPrefab2, enemyBattleStation2);
            enemyCollider2 = enemyPrefab2.GetComponent<BoxCollider2D>();
            enemyUnit2 = enemyGO2.GetComponent<Unit>();
            animate = playerGO.GetComponent<Animator>();
            animateEnemy2 = enemyGO2.GetComponent<Animator>();
            EnemyAlive1 = true;
            EnemyAlive2 = true;
            HUD_UI3.SetActive(false);
            //Assigns the unit stats to be shown in the HUD
            playerHUD.SetHUD(playerUnit);
            enemyHUD1.SetHUD(enemyUnit1);
            enemyHUD2.SetHUD(enemyUnit2);
            Select_1 = enemyGO1.transform.Find("Selected_UI").gameObject;
            Select_2 = enemyGO2.transform.Find("Selected_UI").gameObject;
            EnemyGO1Component = enemyGO1.GetComponent<EnemyColliderDetector>();
            EnemyGO2Component = enemyGO2.GetComponent<EnemyColliderDetector>();
            Select_2.SetActive(false);
            Select_1.SetActive(false);
            enemyBattleStation1.transform.localPosition = new Vector3(
                enemyBattleStation1.transform.localPosition.x,
                enemyBattleStation1.transform.localPosition.y + enemyAddPosY, 0);
            enemyBattleStation2.transform.localPosition = new Vector3(
                enemyBattleStation2.transform.localPosition.x,
                enemyBattleStation2.transform.localPosition.y + enemyAddPosY, 0);
            enemyTurn = new int[] { 1, 2 };


        } else if (enemyNumbers == 3)
        {
            GameObject enemyGO1 = Instantiate(enemyPrefab1, enemyBattleStation1);
            enemyCollider1 = enemyPrefab1.GetComponent<BoxCollider2D>();
            enemyUnit1 = enemyGO1.GetComponent<Unit>();
            animateEnemy1 = enemyGO1.GetComponent<Animator>();
            GameObject enemyGO2 = Instantiate(enemyPrefab2, enemyBattleStation2);
            enemyCollider2 = enemyPrefab2.GetComponent<BoxCollider2D>();
            enemyUnit2 = enemyGO2.GetComponent<Unit>();
            animateEnemy2 = enemyGO2.GetComponent<Animator>();
            GameObject enemyGO3 = Instantiate(enemyPrefab3, enemyBattleStation3);
            enemyCollider3 = enemyPrefab3.GetComponent<BoxCollider2D>();
            enemyUnit3 = enemyGO3.GetComponent<Unit>();
            animate = playerGO.GetComponent<Animator>();
            animateEnemy3 = enemyGO3.GetComponent<Animator>();
            EnemyAlive1 = true;
            EnemyAlive2 = true;
            EnemyAlive3 = true;
            //Assigns the unit stats to be shown in the HUD
            playerHUD.SetHUD(playerUnit);
            enemyHUD1.SetHUD(enemyUnit1);
            enemyHUD2.SetHUD(enemyUnit2);
            enemyHUD3.SetHUD(enemyUnit3);
            Select_1 = enemyGO1.transform.Find("Selected_UI").gameObject;
            Select_2 = enemyGO2.transform.Find("Selected_UI").gameObject;
            Select_3 = enemyGO3.transform.Find("Selected_UI").gameObject;
            EnemyGO1Component = enemyGO1.GetComponent<EnemyColliderDetector>();
            EnemyGO2Component = enemyGO2.GetComponent<EnemyColliderDetector>();
            EnemyGO3Component = enemyGO3.GetComponent<EnemyColliderDetector>();
            Select_3.SetActive(false);
            Select_2.SetActive(false);
            Select_1.SetActive(false);
            // For automatic readjustment of the enemy positions
            enemyBattleStation1.transform.localPosition = new Vector3(
                enemyBattleStation1.transform.localPosition.x,
                enemyBattleStation1.transform.localPosition.y + enemyAddPosY, 0);
            enemyBattleStation2.transform.localPosition = new Vector3(
                enemyBattleStation2.transform.localPosition.x,
                enemyBattleStation2.transform.localPosition.y + enemyAddPosY, 0);
            enemyBattleStation3.transform.localPosition = new Vector3(
                enemyBattleStation3.transform.localPosition.x,
                enemyBattleStation3.transform.localPosition.y + enemyAddPosY, 0);
            enemyTurn = new int[] { 1, 2, 3 };
        }
        else
        {
            Debug.Log("Error in Enemy Generation.");
        }

        if(sceneNumber < 12)
        {
            if(enemyNumbers < 3)
            {
                EnemyHUDs.transform.localPosition = new Vector3(0, 20, 0);
            }
        }

        //Dialogue that appears on top
        dialogueText.text = "HOSTILES APPROACHING!";

        //Delay time
        yield return new WaitForSeconds(2f);

        playerHUD.SetHP(playerUnit.currentHP);
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    //Physical Type damage
    IEnumerator PlayerPhysAttack()
    {
        DamageRandomizer = UnityEngine.Random.Range(-10, 10);
        if (enemySelected == 1)
        {
            if (thisScene == 13)
            {
                if (counterExecution)
                {
                    yield return new WaitForSeconds(2f);
                    bool isDead = enemyUnit1.TakeDamage(playerUnit.damage + DamageRandomizer);
                    playerUnit.HealHealth((int)(playerUnit.MendWounds * (playerUnit.damage * .25)));
                    playerHUD.SetHP(playerUnit.currentHP);
                    enemyHUD1.SetHP(enemyUnit1.currentHP);
                    bulletsRemaining -= 1;
                    animateEnemy1.Play("childEnemy_Hurt");
                    yield return new WaitForSeconds(0.3f);
                    isDead = enemyUnit1.TakeDamage(playerUnit.damage + DamageRandomizer);
                    playerUnit.HealHealth((int)(playerUnit.MendWounds * (playerUnit.damage * .25)));
                    playerHUD.SetHP(playerUnit.currentHP);
                    enemyHUD1.SetHP(enemyUnit1.currentHP);
                    bulletsRemaining -= 1;
                    animateEnemy1.Play("childEnemy_Hurt");
                    yield return new WaitForSeconds(2f);
                    //Checks if the enemy is dead or not
                    counterExecution = false;
                    if (isDead)
                    {
                        animateEnemy1.Play("childEnemy_Fall");
                        EnemyAlive1 = false;
                        StartCoroutine(WaitForEndOfFrame());
                    }
                }
                else
                {
                    yield return new WaitForSeconds(2f);
                    bool isDead = enemyUnit1.TakeDamage(playerUnit.damage + DamageRandomizer);
                    playerUnit.HealHealth((int)(playerUnit.MendWounds * (playerUnit.damage * .25)));
                    playerHUD.SetHP(playerUnit.currentHP);
                    animateEnemy1.Play("childEnemy_Hurt");
                    //Updates the current HP of the enemy
                    enemyHUD1.SetHP(enemyUnit1.currentHP);
                    bulletsRemaining -= 1;
                    yield return new WaitForSeconds(2f);
                    //Checks if the enemy is dead or not
                    if (isDead)
                    {
                        animateEnemy1.Play("childEnemy_Fall");
                        EnemyAlive1 = false;
                        StartCoroutine(WaitForEndOfFrame());
                    }
                    else
                    {
                        StartCoroutine(WaitForEndOfFrame());
                    }
                }


            }
            if (thisScene == 14)
            {
                if (counterExecution)
                {
                    yield return new WaitForSeconds(2f);
                    bool isDead = enemyUnit1.TakeDamage(playerUnit.damage + DamageRandomizer);
                    playerUnit.HealHealth((int)(playerUnit.MendWounds * (playerUnit.damage * .25)));
                    playerHUD.SetHP(playerUnit.currentHP);
                    animateEnemy1.Play("inquisitorEnemy_Hurt");
                    enemyHUD1.SetHP(enemyUnit1.currentHP);
                    bulletsRemaining -= 1;
                    yield return new WaitForSeconds(0.3f);
                    isDead = enemyUnit1.TakeDamage(playerUnit.damage + DamageRandomizer);
                    playerUnit.HealHealth((int)(playerUnit.MendWounds * (playerUnit.damage * .25)));
                    playerHUD.SetHP(playerUnit.currentHP);
                    animateEnemy1.Play("inquisitorEnemy_Hurt");
                    enemyHUD1.SetHP(enemyUnit1.currentHP);
                    bulletsRemaining -= 1;
                    yield return new WaitForSeconds(2f);
                    //Checks if the enemy is dead or not
                    counterExecution = false;
                    if (isDead)
                    {
                        animateEnemy1.Play("inquisitorEnemy_Defeat");
                        EnemyAlive1 = false;
                        StartCoroutine(WaitForEndOfFrame());
                    }
                }
                else
                {
                    yield return new WaitForSeconds(2f);
                    bool isDead = enemyUnit1.TakeDamage(playerUnit.damage + DamageRandomizer);
                    playerUnit.HealHealth((int)(playerUnit.MendWounds * (playerUnit.damage * .25)));
                    playerHUD.SetHP(playerUnit.currentHP);
                    //Updates the current HP of the enemy
                    enemyHUD1.SetHP(enemyUnit1.currentHP);
                    bulletsRemaining -= 1;
                    yield return new WaitForSeconds(2f);
                    //Checks if the enemy is dead or not
                    if (isDead)
                    {
                        animateEnemy1.Play("inquisitorEnemy_Defeat");
                        EnemyAlive1 = false;
                        StartCoroutine(WaitForEndOfFrame());
                    }
                    else
                    {
                        StartCoroutine(WaitForEndOfFrame());
                    }
                }


            }
            if (thisScene < 13)
            {
                if (counterExecution)
                {
                    if (unitType == "magus")
                    {
                        yield return new WaitForSeconds(0.05f);
                        bool isDead = enemyUnit1.TakeDamage(playerUnit.damage * 2 + DamageRandomizer);
                        playerUnit.HealHealth((int)(playerUnit.MendWounds * (playerUnit.damage * .25)));
                        playerHUD.SetHP(playerUnit.currentHP);
                        animateEnemy1.Play("ReceiveHit");
                        enemyHUD1.SetHP(enemyUnit1.currentHP);
                        yield return new WaitForSeconds(2f);
                        //Checks if the enemy is dead or not
                        counterExecution = false;
                        if (isDead)
                        {
                            animateEnemy1.Play("Fall");
                            EnemyAlive1 = false;
                            StartCoroutine(WaitForEndOfFrame());
                        }
                    }
                    else
                    {
                        yield return new WaitForSeconds(1f);
                        bool isDead = enemyUnit1.TakeDamage(playerUnit.damage + DamageRandomizer);
                        playerUnit.HealHealth((int)(playerUnit.MendWounds * (playerUnit.damage * .25)));
                        playerHUD.SetHP(playerUnit.currentHP);
                        animateEnemy1.Play("ReceiveHit");
                        enemyHUD1.SetHP(enemyUnit1.currentHP);
                        yield return new WaitForSeconds(0.3f);
                        isDead = enemyUnit1.TakeDamage(playerUnit.damage + DamageRandomizer);
                        playerUnit.HealHealth((int)(playerUnit.MendWounds * (playerUnit.damage * .25)));
                        playerHUD.SetHP(playerUnit.currentHP);
                        animateEnemy1.Play("ReceiveHit");
                        yield return new WaitForSeconds(0.05f);
                        animateEnemy1.Play("ReceiveHit");
                        yield return new WaitForSeconds(0.05f);
                        animateEnemy1.Play("ReceiveHit");
                        enemyHUD1.SetHP(enemyUnit1.currentHP);
                        yield return new WaitForSeconds(2f);
                        //Checks if the enemy is dead or not
                        counterExecution = false;
                        if (isDead)
                        {
                            animateEnemy1.Play("Fall");
                            EnemyAlive1 = false;
                            StartCoroutine(WaitForEndOfFrame());
                        }
                    }

                }
                else
                {
                    //Damages the enemyHP
                    bool isDead = enemyUnit1.TakeDamage(playerUnit.damage + DamageRandomizer);
                    playerUnit.HealHealth((int)(playerUnit.MendWounds * (playerUnit.damage * .25)));
                    playerHUD.SetHP(playerUnit.currentHP);
                    yield return new WaitForSeconds(0.5f);
                    animateEnemy1.Play("ReceiveHit");
                    //Updates the current HP of the enemy
                    enemyHUD1.SetHP(enemyUnit1.currentHP);

                    yield return new WaitForSeconds(2f);

                    //Checks if the enemy is dead or not
                    if (isDead)
                    {
                        animateEnemy1.Play("Fall");
                        EnemyAlive1 = false;
                        StartCoroutine(WaitForEndOfFrame());
                    }
                    else
                    {
                        StartCoroutine(WaitForEndOfFrame());
                    }
                }

            }
        }
        else if (enemySelected == 2)
        {
            if (thisScene < 13)
            {
                if (counterExecution)
                {
                    if (unitType == "magus")
                    {
                        yield return new WaitForSeconds(0.05f);
                        bool isDead = enemyUnit2.TakeDamage(playerUnit.damage * 2 + DamageRandomizer);
                        playerUnit.HealHealth((int)(playerUnit.MendWounds * (playerUnit.damage * .25)));
                        playerHUD.SetHP(playerUnit.currentHP);
                        animateEnemy2.Play("ReceiveHit");
                        enemyHUD2.SetHP(enemyUnit2.currentHP);
                        yield return new WaitForSeconds(2f);
                        //Checks if the enemy is dead or not
                        counterExecution = false;
                        if (isDead)
                        {
                            animateEnemy2.Play("Fall");
                            EnemyAlive2 = false;  
                        }
                    }
                    else
                    {
                        yield return new WaitForSeconds(1f);
                        bool isDead = enemyUnit2.TakeDamage(playerUnit.damage + DamageRandomizer);
                        playerUnit.HealHealth((int)(playerUnit.MendWounds * (playerUnit.damage * .25)));
                        playerHUD.SetHP(playerUnit.currentHP);
                        animateEnemy2.Play("ReceiveHit");
                        enemyHUD2.SetHP(enemyUnit2.currentHP);
                        yield return new WaitForSeconds(0.3f);
                        isDead = enemyUnit2.TakeDamage(playerUnit.damage + DamageRandomizer);
                        playerUnit.HealHealth((int)(playerUnit.MendWounds * (playerUnit.damage * .25)));
                        playerHUD.SetHP(playerUnit.currentHP);
                        animateEnemy2.Play("ReceiveHit");
                        yield return new WaitForSeconds(0.05f);
                        animateEnemy2.Play("ReceiveHit");
                        yield return new WaitForSeconds(0.05f);
                        animateEnemy2.Play("ReceiveHit");
                        enemyHUD2.SetHP(enemyUnit2.currentHP);
                        yield return new WaitForSeconds(2f);
                        //Checks if the enemy is dead or not
                        counterExecution = false;
                        if (isDead)
                        {
                            animateEnemy2.Play("Fall");
                            EnemyAlive2 = false;
                        }
                    }

                }
                else
                {
                    //Damages the enemyHP
                    bool isDead = enemyUnit2.TakeDamage(playerUnit.damage + DamageRandomizer);
                    playerUnit.HealHealth((int)(playerUnit.MendWounds * (playerUnit.damage * .25)));
                    playerHUD.SetHP(playerUnit.currentHP);
                    yield return new WaitForSeconds(0.5f);
                    animateEnemy2.Play("ReceiveHit");
                    //Updates the current HP of the enemy
                    enemyHUD2.SetHP(enemyUnit2.currentHP);

                    yield return new WaitForSeconds(2f);

                    //Checks if the enemy is dead or not
                    if (isDead)
                    {
                        animateEnemy2.Play("Fall");
                        EnemyAlive2 = false;
                        StartCoroutine(WaitForEndOfFrame());

                    }
                    else
                    {
                        StartCoroutine(WaitForEndOfFrame());
                    }
                }

            }
        }
        else if (enemySelected == 3)
        {
            if (thisScene < 13)
            {
                if (counterExecution)
                {
                    if (unitType == "magus")
                    {
                        yield return new WaitForSeconds(0.05f);
                        bool isDead = enemyUnit3.TakeDamage(playerUnit.damage * 2 + DamageRandomizer);
                        playerUnit.HealHealth((int)(playerUnit.MendWounds * (playerUnit.damage * .25)));
                        playerHUD.SetHP(playerUnit.currentHP);
                        animateEnemy3.Play("ReceiveHit");
                        enemyHUD3.SetHP(enemyUnit3.currentHP);
                        yield return new WaitForSeconds(2f);
                        //Checks if the enemy is dead or not
                        counterExecution = false;
                        if (isDead)
                        {
                            animateEnemy3.Play("Fall");
                            EnemyAlive3 = false;
                            StartCoroutine(WaitForEndOfFrame());

                        }
                    }
                    else
                    {
                        yield return new WaitForSeconds(1f);
                        bool isDead = enemyUnit3.TakeDamage(playerUnit.damage + DamageRandomizer);
                        playerUnit.HealHealth((int)(playerUnit.MendWounds * (playerUnit.damage * .25)));
                        playerHUD.SetHP(playerUnit.currentHP);
                        animateEnemy3.Play("ReceiveHit");
                        enemyHUD3.SetHP(enemyUnit3.currentHP);
                        yield return new WaitForSeconds(0.3f);
                        isDead = enemyUnit3.TakeDamage(playerUnit.damage + DamageRandomizer);
                        playerUnit.HealHealth((int)(playerUnit.MendWounds * (playerUnit.damage * .25)));
                        playerHUD.SetHP(playerUnit.currentHP);
                        animateEnemy3.Play("ReceiveHit");
                        yield return new WaitForSeconds(0.05f);
                        animateEnemy3.Play("ReceiveHit");
                        yield return new WaitForSeconds(0.05f);
                        animateEnemy3.Play("ReceiveHit");
                        enemyHUD3.SetHP(enemyUnit3.currentHP);
                        yield return new WaitForSeconds(2f);
                        //Checks if the enemy is dead or not
                        counterExecution = false;
                        if (isDead)
                        {
                            animateEnemy3.Play("Fall");
                            EnemyAlive3 = false;
                            StartCoroutine(WaitForEndOfFrame());

                        }
                    }

                }
                else
                {
                    //Damages the enemyHP
                    bool isDead = enemyUnit3.TakeDamage(playerUnit.damage + DamageRandomizer);
                    playerUnit.HealHealth((int)(playerUnit.MendWounds * (playerUnit.damage * .25)));
                    playerHUD.SetHP(playerUnit.currentHP);
                    yield return new WaitForSeconds(0.5f);
                    animateEnemy3.Play("ReceiveHit");
                    //Updates the current HP of the enemy
                    enemyHUD3.SetHP(enemyUnit3.currentHP);

                    yield return new WaitForSeconds(2f);

                    //Checks if the enemy is dead or not
                    if (isDead)
                    {
                        animateEnemy3.Play("Fall");
                        EnemyAlive3 = false;
                        StartCoroutine(WaitForEndOfFrame());

                    }
                    else
                    {
                        StartCoroutine(WaitForEndOfFrame());
                    }
                }

            }
        }


    }

    IEnumerator PlayerRageAttack()
    {
        rageOptions.SetActive(false);
        if (EnemyBlock1)
        {
            //Damages the enemyHP
            bool isDead = enemyUnit1.RageAttack(playerUnit.damage/ 2 + DamageRandomizer);
            yield return new WaitForSeconds(1.05f);
            animateEnemy1.Play("ReceiveHit");
            enemyHUD1.SetHP(enemyUnit1.currentHP);

            yield return new WaitForSeconds(2f);

            //Checks if the enemy is dead or not
            if (isDead)
            {
                animateEnemy1.Play("Fall");
                EnemyAlive1 = false;
                StartCoroutine(WaitForEndOfFrame());
            }
            else
            {
                StartCoroutine(WaitForEndOfFrame());
            }
        }
        else if (EnemyAlive1)
        {
            //Damages the enemyHP
            bool isDead = enemyUnit1.RageAttack(playerUnit.damage + DamageRandomizer);
            animateEnemy1.Play("ReceiveHit");
            enemyHUD1.SetHP(enemyUnit1.currentHP);

            yield return new WaitForSeconds(2f);

            //Checks if the enemy is dead or not
            if (isDead)
            {
                animateEnemy1.Play("Fall");
                EnemyAlive1 = false;
                StartCoroutine(WaitForEndOfFrame());
            }
            else
            {
                StartCoroutine(WaitForEndOfFrame());
            }
        }


        if (EnemyBlock2)
        {
            //Damages the enemyHP
            bool isDead = enemyUnit2.RageAttack(playerUnit.damage / 2 + DamageRandomizer);
            yield return new WaitForSeconds(1.05f);
            animateEnemy2.Play("ReceiveHit");
            enemyHUD2.SetHP(enemyUnit2.currentHP);

            yield return new WaitForSeconds(2f);

            //Checks if the enemy is dead or not
            if (isDead)
            {
                animateEnemy2.Play("Fall");
                EnemyAlive2 = false;
                StartCoroutine(WaitForEndOfFrame());
            }
            else
            {
                StartCoroutine(WaitForEndOfFrame());
            }
        }
        else if (EnemyAlive2)
        {
            //Damages the enemyHP
            bool isDead = enemyUnit2.RageAttack(playerUnit.damage + DamageRandomizer);
            animateEnemy1.Play("ReceiveHit");
            enemyHUD2.SetHP(enemyUnit2.currentHP);

            yield return new WaitForSeconds(2f);

            //Checks if the enemy is dead or not
            if (isDead)
            {
                animateEnemy2.Play("Fall");
                EnemyAlive2 = false;
                StartCoroutine(WaitForEndOfFrame());
            }
            else
            {
                StartCoroutine(WaitForEndOfFrame());
            }
        }

        if (EnemyBlock3)
        {
            //Damages the enemyHP
            bool isDead = enemyUnit3.RageAttack(playerUnit.damage / 2 + DamageRandomizer);
            yield return new WaitForSeconds(1.05f);
            animateEnemy3.Play("ReceiveHit");
            enemyHUD3.SetHP(enemyUnit3.currentHP);

            yield return new WaitForSeconds(2f);

            //Checks if the enemy is dead or not
            if (isDead)
            {
                animateEnemy3.Play("Fall");
                EnemyAlive3 = false;
                StartCoroutine(WaitForEndOfFrame());
            }
            else
            {
                StartCoroutine(WaitForEndOfFrame());
            }
        }
        else if(EnemyAlive3)
        {
            //Damages the enemyHP
            bool isDead = enemyUnit3.RageAttack(playerUnit.damage + DamageRandomizer);
            animateEnemy1.Play("ReceiveHit");
            enemyHUD3.SetHP(enemyUnit3.currentHP);

            yield return new WaitForSeconds(2f);

            //Checks if the enemy is dead or not
            if (isDead)
            {
                animateEnemy3.Play("Fall");
                EnemyAlive3 = false;
                StartCoroutine(WaitForEndOfFrame());
            }
            else
            {
                StartCoroutine(WaitForEndOfFrame());
            }
        }

    }
    
    //Magic Type damage
    IEnumerator PlayerMagAttack()
    {
        yield return new WaitForSeconds(1f);
        if (EnemyBlock1 || !EnemyAlive1)
        {
            //moveText.text = enemyUnit1.unitName + " completely blocked the attack!";
        }
        else
        {
            bool isDead = enemyUnit1.TakeDamageMag(playerUnit.damage + DamageRandomizer);
            animateEnemy1.Play("ReceiveHit");
            enemyHUD1.SetHP(enemyUnit1.currentHP);
            if (isDead)
            {
                animateEnemy1.Play("Fall");
                EnemyAlive1 = false;
                StartCoroutine(WaitForEndOfFrame());
            }
        }


        if (EnemyBlock2 || !EnemyAlive2)
        {
            //moveText.text = enemyUnit2.unitName + " completely blocked the attack!";
        }
        else
        {
            bool isDead = enemyUnit2.TakeDamageMag(playerUnit.damage + DamageRandomizer);
            animateEnemy2.Play("ReceiveHit");
            enemyHUD2.SetHP(enemyUnit2.currentHP);

            if (isDead)
            {
                animateEnemy2.Play("Fall");
                EnemyAlive2 = false;
                StartCoroutine(WaitForEndOfFrame());
            }
        }

        if (EnemyBlock3 || !EnemyAlive3)
        {
            //moveText.text = enemyUnit3.unitName + " completely blocked the attack!";
        }
        else
        {
            bool isDead = enemyUnit3.TakeDamageMag(playerUnit.damage + DamageRandomizer);
            //yield return new WaitForSeconds(1f);
            animateEnemy3.Play("ReceiveHit");
            enemyHUD3.SetHP(enemyUnit3.currentHP);

            //yield return new WaitForSeconds(2f);
            if (isDead)
            {
                animateEnemy3.Play("Fall");
                EnemyAlive3 = false;
                StartCoroutine(WaitForEndOfFrame());
            }
        }

        if(EnemyBlock1 && EnemyBlock2 && EnemyBlock3)
        {
            moveText.text = "All enemies have completely blocked the attack!";
        }
        yield return new WaitForSeconds(2f);
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());

    }

    IEnumerator PlayerHeal()
    {
        bool isDead = playerUnit.HealHealth(playerUnit.damage + DamageRandomizer);
        playerHUD.SetHP(playerUnit.currentHP);

        yield return new WaitForSeconds(2f);
        
        state = BattleState.ENEMYTURN;

        StartCoroutine(EnemyTurn());
        
    }

    IEnumerator PlayerBlock()
    {
        
        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;

        StartCoroutine(EnemyTurn());

    }

    IEnumerator PlayerRageShield()
    {
        rageOptions.SetActive(false);

        rageShieldToughness = 3;

        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;

        StartCoroutine(EnemyTurn());
    }

    IEnumerator PlayerReload()
    {
        bulletsRemaining += 3;
        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;

        StartCoroutine(EnemyTurn());
    }
    //Settings for enemy turn
    IEnumerator EnemyTurn()
    {

        EnemyBlock1 = false;
        EnemyBlock2 = false;
        EnemyBlock3 = false;

        if (state == BattleState.ENEMYTURN)
        {
            for (int i = 0; i < enemyTurn.Length; i++)
            {
                enemySelected = enemyTurn[i];

                if(enemyNumbers == 3)
                {
                    if (enemyTurn[i] == 1)
                    {
                        Select_3.SetActive(false);
                        Select_2.SetActive(false);
                        Select_1.SetActive(true);
                        EnemyGO1Component.enabled = true;
                        EnemyGO2Component.enabled = false;
                        EnemyGO3Component.enabled = false;
                    }
                    else if (enemyTurn[i] == 2)
                    {
                        Select_3.SetActive(false);
                        Select_2.SetActive(true);
                        Select_1.SetActive(false);
                        EnemyGO1Component.enabled = false;
                        EnemyGO2Component.enabled = true;
                        EnemyGO3Component.enabled = false;

                    }
                    else if (enemyTurn[i] == 3)
                    {
                        Select_3.SetActive(true);
                        Select_2.SetActive(false);
                        Select_1.SetActive(false);
                        EnemyGO1Component.enabled = false;
                        EnemyGO2Component.enabled = false;
                        EnemyGO3Component.enabled = true;
                    }
                }else if(enemyNumbers == 2){
                    if (enemyTurn[i] == 1)
                    {
                        Select_2.SetActive(false);
                        Select_1.SetActive(true);
                        EnemyGO1Component.enabled = true;
                        EnemyGO2Component.enabled = false;
                    }
                    else if (enemyTurn[i] == 2)
                    {
                        Select_2.SetActive(true);
                        Select_1.SetActive(false);
                        EnemyGO1Component.enabled = false;
                        EnemyGO2Component.enabled = true;
                    }
                }else if(enemyNumbers == 1)
                {
                    if (enemyTurn[i] == 1)
                    {
                        Select_1.SetActive(true);
                        EnemyGO1Component.enabled = true;
                    }
                    
                }
                
                DamageRandomizer = UnityEngine.Random.Range(-10, 10);
                TurnReqRandomizer = UnityEngine.Random.Range(3, 6);
                MPGainRandomizer = UnityEngine.Random.Range(0, 400);
                TurnRandomizer = UnityEngine.Random.Range(1, 21);

                playerAvatar.SetActive(false);
                enemyAvatar.SetActive(true);

                dialogueText.text = "Enemy Turn";
                //Updates the bottom HUD with the enemy stat


                yield return new WaitForSeconds(2f);


                if (unitType == "inquisitor")
                {
                    if (TurnRandomizer < 5)
                    {
                        enemyAttacking = true;
                        moveText.text = "Dezvalh: First Art! Luminous Refraction!";
                        animateEnemy1.Play("inquisitorEnemy_FirstArt");
                        Debug.Log("AttackFromIf");

                        if (PBlock)
                        {

                            yield return new WaitForSeconds(3f);
                            moveText.text = playerUnit.unitName + " tries to dodge the attack!";
                            EnemyMoved = true;

                            if (counterExecution)
                            {
                                yield return new WaitForSeconds(4.3f);
                            }
                            EnemyMoved = true;

                        }
                        else
                        {
                            bool isDead = playerUnit.TakeDamage(enemyUnit1.damage + powerUP + DamageRandomizer);
                            yield return new WaitForSeconds(1.5f);
                            animate.Play("amaraNeutral_Hurt");
                            yield return new WaitForSeconds(0.8f);
                            animate.Play("amaraNeutral_Hurt");
                            playerHUD.SetHP(playerUnit.currentHP);
                            powerUP = 0;
                            yield return new WaitForSeconds(2f);

                            if (isDead)
                            {
                                state = BattleState.LOST;
                                animate.Play("amaraNeutral_Fall");
                                enemyAttacking = false;
                                StartCoroutine(EndBattle());
                            }

                            EnemyMoved = true;
                        }
                    }
                    else if (TurnRandomizer >= 5 && TurnRandomizer < 12 && enemyUnit1.currentHP < (enemyUnit1.maxHP * 0.5))
                    {
                        enemyAttacking = true;
                        thirdArt = true;
                        moveText.text = "Dezvalh: Third Art! Successive Shots!";

                        animateEnemy1.Play("inquisitorEnemy_ThirdArt");

                        yield return new WaitForSeconds(5.1f);

                        if (PBlock)
                        {
                            moveText.text = playerUnit.unitName + " tries to dodge the attacks!";
                            animate.Play("amaraNeutral_Dodge");
                            yield return new WaitForSeconds(2.1f);
                            if (PBlock)
                            {
                                animate.Play("amaraNeutral_Dodge");
                                yield return new WaitForSeconds(1.4f);
                                if (PBlock)
                                {
                                    animate.Play("amaraNeutral_Dodge");

                                    if (counterExecution)
                                    {
                                        yield return new WaitForSeconds(4.3f);
                                    }
                                }
                            }
                            
                            if (counterExecution)
                            {
                                yield return new WaitForSeconds(4.3f);
                            }

                            yield return new WaitForSeconds(0.5f);
                            EnemyMoved = true;

                        }
                        else
                        {
                            playerUnit.TakeDamage(enemyUnit1.damage + powerUP + DamageRandomizer);
                            animate.Play("amaraNeutral_Hurt");
                            playerHUD.SetHP(playerUnit.currentHP);

                            yield return new WaitForSeconds(2.1f);

                            playerUnit.TakeDamage(enemyUnit1.damage + powerUP + DamageRandomizer);
                            animate.Play("amaraNeutral_Hurt");
                            playerHUD.SetHP(playerUnit.currentHP);

                            yield return new WaitForSeconds(1.4f);

                            bool isDead = playerUnit.TakeDamage(enemyUnit1.damage + powerUP + DamageRandomizer);
                            playerHUD.SetHP(playerUnit.currentHP);
                            animate.Play("amaraNeutral_Hurt");

                            yield return new WaitForSeconds(2f);
                            powerUP = 0;
                            if (isDead)
                            {
                                state = BattleState.LOST;
                                animate.Play("amaraNeutral_Fall");
                                enemyAttacking = false;
                                StartCoroutine(EndBattle());
                            }
                            EnemyMoved = true;

                        }
                        thirdArt = false;
                    }
                    else if (TurnRandomizer >= 12 && TurnRandomizer < 17 && enemyUnit1.currentHP < (enemyUnit1.maxHP * 0.75))
                    {
                        enemyAttacking = true;
                        secondArt = true;
                        moveText.text = "Dezvalh: Second Art! Ground Splitting Edge!";
                        animateEnemy1.Play("inquisitorEnemy_SecondArt");

                        yield return new WaitForSeconds(2.5f);

                        if (PBlock)
                        {
                            animate.Play("amaraNeutral_Dodge");
                            yield return new WaitForSeconds(2f);
                            moveText.text = playerUnit.unitName + " tries to dodge the attack!";
                            powerUP = 0;
                            if (counterExecution)
                            {
                                yield return new WaitForSeconds(4.3f);
                            }
                            EnemyMoved = true;
                        }
                        else
                        {
                            bool isDead = playerUnit.TakeDamage(enemyUnit1.damage + powerUP + DamageRandomizer);
                            playerHUD.SetHP(playerUnit.currentHP);
                            animate.Play("amaraNeutral_Hurt");

                            yield return new WaitForSeconds(2f);
                            powerUP = 0;
                            EnemyMoved = true;
                            if (isDead)
                            {
                                state = BattleState.LOST;
                                animate.Play("amaraNeutral_Fall");
                                enemyAttacking = false;
                                StartCoroutine(EndBattle());
                            }
                        }
                        secondArt = false;
                    }
                    else if (TurnRandomizer >= 17 && TurnRandomizer < 21 && enemyUnit1.currentHP < (enemyUnit1.maxHP * 0.25))
                    {
                        enemyAttacking = true;
                        moveText.text = "Dezvalh: Come Forth! Millenium Blades!";
                        animateEnemy1.Play("inquisitorEnemy_SpecialAttack");

                        yield return new WaitForSeconds(2.5f);

                        if (PBlock)
                        {
                            moveText.text = playerUnit.unitName + " tries to dodge the attack, but received some damage!";
                            bool isDead = playerUnit.TakeDamage(enemyUnit1.damage + powerUP + DamageRandomizer);
                            playerHUD.SetHP(playerUnit.currentHP);
                            animate.Play("amaraNeutral_Hurt");
                            yield return new WaitForSeconds(2f);
                            powerUP = 0;
                            EnemyMoved = true;
                            if (counterExecution)
                            {
                                yield return new WaitForSeconds(4.3f);
                            }

                            if (isDead)
                            {
                                state = BattleState.LOST;
                                animate.Play("amaraNeutral_Fall");
                                enemyAttacking = false;
                                StartCoroutine(EndBattle());
                            }

                        }
                        else
                        {
                            moveText.text = playerUnit.unitName + " received the damage head on!";
                            bool isDead = playerUnit.TakeDamage((enemyUnit1.damage + powerUP) * 5 + DamageRandomizer);
                            playerHUD.SetHP(playerUnit.currentHP);
                            animate.Play("amaraNeutral_Hurt");
                            yield return new WaitForSeconds(2f);
                            powerUP = 0;
                            EnemyMoved = true;
                            if (isDead)
                            {
                                state = BattleState.LOST;
                                animate.Play("amaraNeutral_Fall");
                                enemyAttacking = false;
                                StartCoroutine(EndBattle());
                            }
                        }
                    }
                    else
                    {
                        if (TurnRandomizer > 10)
                        {
                            enemyAttacking = true;
                            moveText.text = "Dezvalh: First Art! Luminous Refraction!";
                            animateEnemy1.Play("inquisitorEnemy_FirstArt");
                            Debug.Log("AttackFromElse");
                            EnemyMoved = true;
                            if (PBlock)
                            {
                                yield return new WaitForSeconds(2f);
                                moveText.text = playerUnit.unitName + " tries to dodge the attack!";
                                powerUP = 0;
                                if (counterExecution)
                                {
                                    yield return new WaitForSeconds(4.3f);
                                }
                                EnemyMoved = true;

                            }
                            else
                            {
                                bool isDead = playerUnit.TakeDamage(enemyUnit1.damage + powerUP + DamageRandomizer);
                                yield return new WaitForSeconds(1.5f);
                                animate.Play("amaraNeutral_Hurt");
                                yield return new WaitForSeconds(0.8f);
                                animate.Play("amaraNeutral_Hurt");
                                playerHUD.SetHP(playerUnit.currentHP);

                                yield return new WaitForSeconds(3f);

                                powerUP = 0;

                                if (isDead)
                                {
                                    state = BattleState.LOST;
                                    animate.Play("amaraNeutral_Fall");
                                    enemyAttacking = false;
                                    StartCoroutine(EndBattle());
                                }
                                EnemyMoved = true;
                            }
                        }
                        else
                        {
                            EnemyMoved = true;
                            moveText.text = "Dezhval: Enhancement Art! Power Up!";
                            animateEnemy1.Play("inquisitorEnemy_PowerUp");
                            yield return new WaitForSeconds(2f);
                            powerUP += 25;
                        }
                    }
                    enemyAttacking = false;
                    //yield return new WaitForSeconds(2f);
                }
                else if (unitType == "child" && state == BattleState.ENEMYTURN)
                {
                    if (TurnRandomizer < 13)
                    {
                        enemyAttacking = true;
                        moveText.text = enemyUnit1.unitName + " tries to stab " + playerUnit.unitName + "!";
                        animateEnemy1.Play("childEnemy_Slash");

                        if (PBlock)
                        {
                            moveText.text = playerUnit.unitName + " tries to dodge the attack!";
                            animate.Play("amaraNeutral_Dodge");
                            yield return new WaitForSeconds(2f);
                            EnemyMoved = true;
                            if (counterExecution)
                            {
                                yield return new WaitForSeconds(2.3f);
                            }

                        }
                        else
                        {
                            bool isDead = playerUnit.TakeDamage(enemyUnit1.damage + DamageRandomizer);
                            yield return new WaitForSeconds(0.6f);
                            animate.Play("amaraNeutral_Hurt");
                            playerHUD.SetHP(playerUnit.currentHP);
                            EnemyMoved = true;
                            yield return new WaitForSeconds(2f);

                            if (isDead)
                            {
                                state = BattleState.LOST;
                                animate.Play("amaraNeutral_Fall");
                                enemyAttacking = false;
                                StartCoroutine(EndBattle());
                            }
                        }


                    }
                    else if (enemyUnit1.currentHP < (enemyUnit1.maxHP / 2) && TurnRandomizer >= 13 && TurnRandomizer < 17)
                    {
                        enemyAttacking = true;
                        moveText.text = enemyUnit1.unitName + " tries to stab " + playerUnit.unitName + "!";
                        animateEnemy1.Play("childEnemy_Feint");
                        EnemyMoved = true;
                        if (PBlock)
                        {
                            moveText.text = playerUnit.unitName + " tries to dodge the attack!";
                            animate.Play("amaraNeutral_Dodge");
                            yield return new WaitForSeconds(2f);
                            if (counterExecution)
                            {
                                yield return new WaitForSeconds(2.3f);
                            }

                        }
                        else
                        {
                            bool isDead = playerUnit.TakeDamage(enemyUnit1.damage + DamageRandomizer);
                            yield return new WaitForSeconds(1f);
                            animate.Play("amaraNeutral_Hurt");
                            playerHUD.SetHP(playerUnit.currentHP);

                            yield return new WaitForSeconds(2f);
                            EnemyMoved = true;
                            if (isDead)
                            {
                                state = BattleState.LOST;
                                animate.Play("amaraNeutral_Fall");
                                enemyAttacking = false;
                                StartCoroutine(EndBattle());
                            }
                        }
                    }
                    else
                    {
                        moveText.text = enemyUnit1.unitName + " keeps his guard up!";
                        animateEnemy1.Play("childEnemy_Dodge");
                        EnemyBlock1 = true;
                        enemyAttacking = false;

                        yield return new WaitForSeconds(1f);
                        EnemyMoved = true;
                    }
                    enemyAttacking = false;
                    //yield return new WaitForSeconds(2f);
                }
                else if (unitType == "issei") //moveset for the Boss Issei Kai
                {

                    if (TurnsMade <= 0) //Takes note of the turns
                    {
                        if (moveList.Count == 0)
                        {
                            int[] val = { 5, 3, 2, 1 }; //value of each attacks
                            int[] wt = { 800, 300, 200, 275 }; //weight or mp cost of each attack
                            int W = BossMP; //Accumulated Boss MP is set as the maximum weight
                            int n = val.Length;
                            BossMoveKnapSack(W, wt, val, n); //Sends the values to the Knapsack Function
                            Debug.Log("Movelist = null");
                        }




                        if ((int)moveList[0] == 200)
                        {
                            enemyAttacking = true;
                            Debug.Log("LowAttack!");
                            moveText.text = enemyUnit1.unitName + " uses Sonic Slash against " + playerUnit.unitName + "!";
                            animateEnemy1.Play("LowAttack");


                            if (PBlock || rageShieldToughness > 0)
                            {
                                rageLevel += (int)(5 + (playerUnit.RagingBull * (5 * .5)));
                                rageBar.SetRageHUD(rageLevel);
                                moveText.text = playerUnit.unitName + "managed to block the attack!";
                                yield return new WaitForSeconds(2f);
                                rageLevel += (int)(5 + (playerUnit.RagingBull * (5 * .5)));
                                rageBar.SetRageHUD(rageLevel);
                                rageShieldToughness -= 1;
                                EnemyMoved = true;
                                if (rageLevel >= 100)
                                {
                                    rageOptions.SetActive(true);
                                }


                                if (rageShieldToughness == 0)
                                {
                                    animateShield.Play("ShieldAnimBreak");
                                    yield return new WaitForSeconds(2.5f);
                                    Shield.SetActive(false);
                                }
                                BossMP -= 200;
                                BossMPAccumulated = BossMP; ;
                                TurnsMade -= 1;
                                TurnsUntilBossAttack = TurnsMade;
                                moveList.RemoveAt(0);

                            }
                            else
                            {
                                bool isDead = playerUnit.LowAttack(enemyUnit1.damage);
                                yield return new WaitForSeconds(0.19f);
                                animate.Play("CombatReceiveHit");
                                playerHUD.SetHP(playerUnit.currentHP);
                                rageLevel += (int)(10 + (playerUnit.RagingBull * (10 * .5)));
                                rageBar.SetRageHUD(rageLevel);

                                yield return new WaitForSeconds(2f);
                                EnemyMoved = true;


                                if (isDead)
                                {
                                    state = BattleState.LOST;
                                    animate.Play("CombatFailAnim");
                                    StartCoroutine(EndBattle());
                                }
                                else
                                {
                                    rageLevel += (int)(10 + (playerUnit.RagingBull * (10 * .5)));
                                    rageBar.SetRageHUD(rageLevel);
                                    rageShieldToughness -= 1;
                                    if (rageLevel >= 100)
                                    {
                                        rageOptions.SetActive(true);
                                    }
                                    BossMP -= 200;
                                    BossMPAccumulated = BossMP;
                                    TurnsMade -= 1;
                                    TurnsUntilBossAttack = TurnsMade;
                                    moveList.RemoveAt(0);
                                }
                            }

                            Debug.Log("SlashPassed");
                            if (moveList.Count == 0)
                            {
                                TurnsMade = TurnReqRandomizer;
                                TurnsUntilBossAttack = TurnsMade;
                            }

                            enemyAttacking = false;

                        }
                        else if ((int)moveList[0] == 300)
                        {
                            enemyAttacking = true;
                            Debug.Log("SwordRain");
                            moveText.text = enemyUnit1.unitName + " uses Falling Blades against " + playerUnit.unitName + "!";
                            animateEnemy1.Play("SwordSummon");

                            EnemyMoved = true;
                            if (PBlock || rageShieldToughness > 0)
                            {
                                moveText.text = playerUnit.unitName + " managed to swiftly dodge the blades!";
                                yield return new WaitForSeconds(2f);

                                rageLevel += (int)(5 + (playerUnit.RagingBull * (5 * .5)));
                                rageBar.SetRageHUD(rageLevel);
                                rageShieldToughness -= 1;
                                if (rageLevel >= 100)
                                {
                                    rageOptions.SetActive(true);
                                }


                                if (rageShieldToughness == 0)
                                {
                                    animateShield.Play("ShieldAnimBreak");
                                    yield return new WaitForSeconds(2.5f);
                                    Shield.SetActive(false);
                                }
                                BossMP -= 300;
                                BossMPAccumulated = BossMP;
                                TurnsMade -= 1;
                                TurnsUntilBossAttack = TurnsMade;
                                moveList.RemoveAt(0);

                            }
                            else
                            {
                                bool isDead = playerUnit.SwordSummon(enemyUnit1.damage);
                                yield return new WaitForSeconds(1f);
                                animate.Play("CombatReceiveHit");
                                playerHUD.SetHP(playerUnit.currentHP);

                                yield return new WaitForSeconds(2f);
                                EnemyMoved = true;


                                if (isDead)
                                {
                                    state = BattleState.LOST;
                                    animate.Play("CombatFailAnim");
                                    StartCoroutine(EndBattle());
                                }
                                else
                                {
                                    rageLevel += (int)(10 + (playerUnit.RagingBull * (10 * .5)));
                                    rageBar.SetRageHUD(rageLevel);
                                    rageShieldToughness -= 1;
                                    if (rageLevel >= 100)
                                    {
                                        rageOptions.SetActive(true);
                                    }
                                    BossMP -= 300;
                                    BossMPAccumulated = BossMP;
                                    TurnsMade -= 1;
                                    TurnsUntilBossAttack = TurnsMade;
                                    moveList.RemoveAt(0);
                                }
                            }


                            if (moveList.Count == 0)
                            {
                                TurnsMade = TurnReqRandomizer;
                                TurnsUntilBossAttack = TurnsMade;
                            }

                            enemyAttacking = false;

                        }
                        else if ((int)moveList[0] == 800)
                        {
                            enemyAttacking = true;
                            Debug.Log("JudgmentCut!");
                            moveText.text = enemyUnit1.unitName + " uses Dominate Cut against " + playerUnit.unitName + "!";
                            animateEnemy1.Play("JudgementCut");

                            EnemyMoved = true;

                            if (PBlock || rageShieldToughness > 0)
                            {
                                moveText.text = playerUnit.unitName + " tried to survive the assault!";
                                if (rageShieldToughness > 0)
                                {
                                    yield return new WaitForSeconds(2f);
                                    rageLevel += (int)(5 + (playerUnit.RagingBull * (5 * .5)));
                                    rageBar.SetRageHUD(rageLevel);
                                    rageShieldToughness = 0;
                                    if (rageLevel >= 100)
                                    {
                                        rageOptions.SetActive(true);
                                    }


                                    if (rageShieldToughness == 0)
                                    {
                                        animateShield.Play("ShieldAnimBreak");
                                        yield return new WaitForSeconds(2.5f);
                                        Shield.SetActive(false);
                                    }
                                    BossMP -= 800;
                                    BossMPAccumulated = BossMP;
                                    TurnsMade -= 1;
                                    TurnsUntilBossAttack = TurnsMade;
                                    moveList.RemoveAt(0);
                                }
                                else
                                {
                                    bool isDead = playerUnit.JudgementCutHalved(enemyUnit1.damage / 2 + DamageRandomizer);
                                    yield return new WaitForSeconds(1.7f);
                                    animate.Play("CombatReceiveHit");
                                    yield return new WaitForSeconds(0.1f);
                                    animate.Play("CombatReceiveHit");
                                    yield return new WaitForSeconds(0.1f);
                                    animate.Play("CombatReceiveHit");
                                    yield return new WaitForSeconds(0.1f);
                                    animate.Play("CombatReceiveHit");
                                    yield return new WaitForSeconds(0.5f);
                                    animate.Play("CombatReceiveHit");
                                    playerHUD.SetHP(playerUnit.currentHP);

                                    yield return new WaitForSeconds(2f);



                                    if (isDead)
                                    {
                                        state = BattleState.LOST;
                                        animate.Play("CombatFailAnim");
                                        StartCoroutine(EndBattle());
                                    }
                                    else
                                    {
                                        rageLevel += (int)(5 + (playerUnit.RagingBull * (5 * .5)));
                                        rageBar.SetRageHUD(rageLevel);
                                        rageShieldToughness -= 1;
                                        if (rageLevel >= 100)
                                        {
                                            rageOptions.SetActive(true);
                                        }



                                        BossMP -= 800;
                                        BossMPAccumulated = BossMP;
                                        TurnsMade -= 1;
                                        TurnsUntilBossAttack = TurnsMade;
                                        moveList.RemoveAt(0);
                                    }
                                }

                            }
                            else
                            {
                                bool isDead = playerUnit.JudgementCut(enemyUnit1.damage + DamageRandomizer);
                                yield return new WaitForSeconds(1.7f);
                                animate.Play("CombatReceiveHit");
                                yield return new WaitForSeconds(0.1f);
                                animate.Play("CombatReceiveHit");
                                yield return new WaitForSeconds(0.1f);
                                animate.Play("CombatReceiveHit");
                                yield return new WaitForSeconds(0.1f);
                                animate.Play("CombatReceiveHit");
                                yield return new WaitForSeconds(0.5f);
                                animate.Play("CombatReceiveHit");
                                playerHUD.SetHP(playerUnit.currentHP);
                                playerHUD.SetHP(playerUnit.currentHP);

                                yield return new WaitForSeconds(2f);
                                EnemyMoved = true;


                                if (isDead)
                                {
                                    state = BattleState.LOST;
                                    animate.Play("CombatFailAnim");
                                    StartCoroutine(EndBattle());
                                }
                                else
                                {
                                    rageLevel += (int)(10 + (playerUnit.RagingBull * (10 * .5)));
                                    rageBar.SetRageHUD(rageLevel);
                                    rageShieldToughness -= 1;
                                    if (rageLevel >= 100)
                                    {
                                        rageOptions.SetActive(true);
                                    }
                                    BossMP -= 800;
                                    BossMPAccumulated = BossMP;
                                    TurnsMade -= 1;
                                    TurnsUntilBossAttack = TurnsMade;
                                    moveList.RemoveAt(0);
                                }
                            }

                            if (moveList.Count == 0)
                            {
                                TurnsMade = TurnReqRandomizer;
                                TurnsUntilBossAttack = TurnsMade;
                            }
                            enemyAttacking = false;

                        }
                        else if ((int)moveList[0] == 275)
                        {
                            enemyAttacking = true;
                            Debug.Log("LowAttack!2");
                            moveText.text = enemyUnit1.unitName + " uses Weaker Sonic Slash against " + playerUnit.unitName + "!";
                            animateEnemy1.Play("LowAttack");
                            EnemyMoved = true;
                            yield return new WaitForSeconds(2f);
                            if (PBlock || rageShieldToughness > 0)
                            {


                                moveText.text = playerUnit.unitName + " managed to block the attack!";
                                yield return new WaitForSeconds(2f);
                                rageLevel += (int)(5 + (playerUnit.RagingBull * (5 * .5)));
                                rageBar.SetRageHUD(rageLevel);
                                rageShieldToughness -= 1;
                                if (rageLevel >= 100)
                                {
                                    rageOptions.SetActive(true);
                                }


                                if (rageShieldToughness == 0)
                                {
                                    animateShield.Play("ShieldAnimBreak");
                                    yield return new WaitForSeconds(2.5f);
                                    Shield.SetActive(false);
                                }
                                BossMP -= 275;
                                BossMPAccumulated = BossMP;
                                TurnsMade -= 1;
                                TurnsUntilBossAttack = TurnsMade;
                                moveList.RemoveAt(0);

                            }
                            else
                            {
                                bool isDead = playerUnit.LowAttackHalved(enemyUnit1.damage + DamageRandomizer);
                                yield return new WaitForSeconds(0.19f);
                                animate.Play("CombatReceiveHit");
                                playerHUD.SetHP(playerUnit.currentHP);
                                rageLevel += (int)(10 + (playerUnit.RagingBull * (10 * .5)));
                                rageBar.SetRageHUD(rageLevel);
                                EnemyMoved = true;
                                if (rageLevel >= 100)
                                {
                                    rageOptions.SetActive(true);
                                }

                                yield return new WaitForSeconds(2f);

                                if (isDead)
                                {
                                    state = BattleState.LOST;
                                    animate.Play("CombatFailAnim");
                                    StartCoroutine(EndBattle());
                                }
                                else
                                {
                                    BossMP -= 200;
                                    BossMPAccumulated = BossMP;
                                    TurnsMade -= 1;
                                    TurnsUntilBossAttack = TurnsMade;
                                    moveList.RemoveAt(0);
                                }
                            }

                            Debug.Log("SlashPassed");
                            if (moveList.Count == 0)
                            {
                                TurnsMade = TurnReqRandomizer;
                                TurnsUntilBossAttack = TurnsMade;
                            }

                            enemyAttacking = false;

                        }



                    }

                    else
                    {
                        if (TurnRandomizer < 15)
                        {
                            moveText.text = enemyUnit1.unitName + " is silently staring...";
                            animateEnemy1.Play("Prepare");
                            BossMP += MPGainRandomizer;
                            BossMPAccumulated = BossMP;
                            EnemyMoved = true;
                            if (enemyTurn.Length == 1)
                            {
                                BossMP += MPGainRandomizer;
                                BossMPAccumulated = BossMP;
                                TurnsMade -= 1;
                            }

                            yield return new WaitForSeconds(2f);

                            TurnsMade -= 1;
                            TurnsUntilBossAttack = TurnsMade;
                        }
                        else
                        {
                            moveText.text = enemyUnit1.unitName + " is regenerating himself!";

                            bool isDead = enemyUnit1.HealHealth(enemyUnit1.damage);
                            enemyHUD1.SetHP(enemyUnit1.currentHP);
                            BossMP += 75;
                            BossMPAccumulated = BossMP;

                            if (enemyTurn.Length == 1)
                            {
                                BossMP += MPGainRandomizer;
                                BossMPAccumulated = BossMP;
                                TurnsMade -= 1;

                                if(enemyUnit1.currentHP < (int)(enemyUnit1.maxHP * .5))
                                {
                                    Debug.Log("Here");
                                    BossMP += MPGainRandomizer * 4;
                                    BossMPAccumulated = BossMP;
                                    TurnsMade -= 1;
                                }
                            }

                            TurnsMade -= 1;
                            TurnsUntilBossAttack = TurnsMade;
                            yield return new WaitForSeconds(2f);
                            EnemyMoved = true;
                        }

                    }
                }
                else if (unitType == "magus")
                {
                    if (TurnRandomizer < 15)
                    {
                        if (enemySelected == 1)
                        {
                            if (!EnemyMagicCharge1)
                            {
                                moveText.text = enemyUnit1.unitName + " has started to charge up their power...";
                                animateEnemy1.Play("AttackPrep");
                                EnemyMagicCharge1 = true;
                                EnemyMoved = true;
                            }
                            else
                            {
                                enemyAttacking = true;
                                moveText.text = enemyUnit1.unitName + " unleashes Magical Attack against " + playerUnit.unitName + "!";
                                animateEnemy1.Play("Attack");
                                ps.Play();
                                EnemyMoved = true;

                                if (PBlock || rageShieldToughness > 0)
                                {
                                    moveText.text = playerUnit.unitName + " managed to swiftly dodge the blades!";
                                    yield return new WaitForSeconds(2f);

                                    rageLevel += (int)(5 + (playerUnit.RagingBull * (5 * .5)));
                                    rageBar.SetRageHUD(rageLevel);
                                    rageShieldToughness -= 1;
                                    if (rageLevel >= 100)
                                    {
                                        rageOptions.SetActive(true);
                                    }


                                    if (rageShieldToughness == 0)
                                    {
                                        animateShield.Play("ShieldAnimBreak");
                                        yield return new WaitForSeconds(2.5f);
                                        Shield.SetActive(false);
                                    }

                                }
                                else
                                {
                                    bool isDead = playerUnit.TakeDamage(enemyUnit1.damage + DamageRandomizer);
                                    yield return new WaitForSeconds(1f);
                                    animate.Play("CombatReceiveHit");
                                    playerHUD.SetHP(playerUnit.currentHP);
                                    rageLevel += (int)(10 + (playerUnit.RagingBull * (10 * .5)));
                                    rageBar.SetRageHUD(rageLevel);
                                    EnemyMoved = true;
                                    if (rageLevel >= 100)
                                    {


                                        rageOptions.SetActive(true);
                                    }

                                    yield return new WaitForSeconds(2f);

                                    if (isDead)
                                    {
                                        state = BattleState.LOST;
                                        animate.Play("CombatFailAnim");
                                        StartCoroutine(EndBattle());
                                    }
                                }

                                enemyAttacking = false;
                                EnemyMagicCharge1 = false;
                            }
                        }
                        if (enemySelected == 2)
                        {
                            if (!EnemyMagicCharge2)
                            {
                                moveText.text = enemyUnit2.unitName + " has started to charge up their power...";
                                animateEnemy2.Play("AttackPrep");
                                EnemyMagicCharge2 = true;
                                EnemyMoved = true;
                            }
                            else
                            {
                                enemyAttacking = true;
                                moveText.text = enemyUnit2.unitName + " unleashes Magical Attack against " + playerUnit.unitName + "!";
                                animateEnemy2.Play("Attack");
                                EnemyMoved = true;
                                ps.Play();

                                if (PBlock || rageShieldToughness > 0)
                                {
                                    moveText.text = playerUnit.unitName + " managed to swiftly dodge the blades!";
                                    yield return new WaitForSeconds(2f);

                                    rageLevel += (int)(5 + (playerUnit.RagingBull * (5 * .5)));
                                    rageBar.SetRageHUD(rageLevel);
                                    rageShieldToughness -= 1;
                                    if (rageLevel >= 100)
                                    {
                                        rageOptions.SetActive(true);
                                    }


                                    if (rageShieldToughness == 0)
                                    {
                                        animateShield.Play("ShieldAnimBreak");
                                        yield return new WaitForSeconds(2.5f);
                                        Shield.SetActive(false);
                                    }
                                }
                                else
                                {
                                    bool isDead = playerUnit.TakeDamage(enemyUnit2.damage + DamageRandomizer);
                                    yield return new WaitForSeconds(1f);
                                    animate.Play("CombatReceiveHit");
                                    playerHUD.SetHP(playerUnit.currentHP);
                                    rageLevel += (int)(10 + (playerUnit.RagingBull * (10 * .5)));
                                    EnemyMoved = true;
                                    rageBar.SetRageHUD(rageLevel);
                                    if (rageLevel >= 100)
                                    {


                                        rageOptions.SetActive(true);
                                    }

                                    yield return new WaitForSeconds(2f);

                                    if (isDead)
                                    {
                                        state = BattleState.LOST;
                                        animate.Play("CombatFailAnim");
                                        StartCoroutine(EndBattle());
                                    }
                                }

                                enemyAttacking = false;
                                EnemyMagicCharge2 = false;
                            }
                        }
                        if (enemySelected == 3)
                        {
                            if (!EnemyMagicCharge3)
                            {
                                moveText.text = enemyUnit3.unitName + " has started to charge up their power...";
                                animateEnemy3.Play("AttackPrep");
                                EnemyMagicCharge3 = true;
                                EnemyMoved = true;
                            }
                            else
                            {
                                enemyAttacking = true;
                                moveText.text = enemyUnit3.unitName + " unleashes Magical Attack against " + playerUnit.unitName + "!";
                                animateEnemy3.Play("Attack");
                                ps.Play();
                                EnemyMoved = true;

                                if (PBlock || rageShieldToughness > 0)
                                {
                                    moveText.text = playerUnit.unitName + " managed to swiftly dodge the blades!";
                                    yield return new WaitForSeconds(2f);

                                    rageLevel += (int)(5 + (playerUnit.RagingBull * (5 * .5)));
                                    rageBar.SetRageHUD(rageLevel);
                                    rageShieldToughness -= 1;
                                    if (rageLevel >= 100)
                                    {
                                        rageOptions.SetActive(true);
                                    }


                                    if (rageShieldToughness == 0)
                                    {
                                        animateShield.Play("ShieldAnimBreak");
                                        yield return new WaitForSeconds(2.5f);
                                        Shield.SetActive(false);
                                    }


                                }
                                else
                                {
                                    bool isDead = playerUnit.TakeDamage(enemyUnit3.damage + DamageRandomizer);
                                    yield return new WaitForSeconds(1f);
                                    animate.Play("CombatReceiveHit");
                                    playerHUD.SetHP(playerUnit.currentHP);
                                    rageLevel += (int)(10 + (playerUnit.RagingBull * (10 * .5)));
                                    rageBar.SetRageHUD(rageLevel);
                                    if (rageLevel >= 100)
                                    {


                                        rageOptions.SetActive(true);
                                    }

                                    yield return new WaitForSeconds(2f);

                                    if (isDead)
                                    {
                                        state = BattleState.LOST;
                                        animate.Play("CombatFailAnim");
                                        StartCoroutine(EndBattle());
                                    }
                                }

                                enemyAttacking = false;
                                EnemyMagicCharge3 = false;
                            }
                        }
                    }
                    else
                    {
                        if (enemySelected == 1)
                        {
                            moveText.text = enemyUnit1.unitName + " has healed himself!";
                            animateEnemy1.Play("AttackPrep");
                            bool isDead = enemyUnit1.HealHealth(enemyUnit1.damage);
                            enemyHUD1.SetHP(enemyUnit1.currentHP);

                            yield return new WaitForSeconds(2f);
                        }
                        else if (enemySelected == 2)
                        {
                            moveText.text = enemyUnit2.unitName + " has healed himself!";
                            animateEnemy2.Play("AttackPrep");
                            bool isDead = enemyUnit2.HealHealth(enemyUnit2.damage);
                            enemyHUD2.SetHP(enemyUnit2.currentHP);

                            yield return new WaitForSeconds(2f);
                        }
                        else if (enemySelected == 3)
                        {
                            moveText.text = enemyUnit3.unitName + " has healed himself!";
                            animateEnemy3.Play("AttackPrep");
                            bool isDead = enemyUnit3.HealHealth(enemyUnit3.damage);
                            enemyHUD3.SetHP(enemyUnit3.currentHP);

                            yield return new WaitForSeconds(2f);
                        }
                        EnemyMoved = true;

                    }
                }
                else if (unitType == "monster")
                {
                    if (enemySelected == 1 && EnemyAlive1)
                    {
                        if (TurnRandomizer < 15)
                        {
                            enemyAttacking = true;
                            moveText.text = enemyUnit1.unitName + " uses Physical Attack against " + playerUnit.unitName + "!";
                            animateEnemy1.Play("Attack");


                            if (PBlock || rageShieldToughness > 0 || parryAction)
                            {

                                moveText.text = playerUnit.unitName + " has successfully blocked the attack!";
                                yield return new WaitForSeconds(2f);
                                rageLevel += (int)(5 + (playerUnit.RagingBull * (5 * .5)));
                                rageBar.SetRageHUD(rageLevel);
                                rageShieldToughness -= 1;
                                EnemyMoved = true;
                                if (rageLevel >= 100)
                                {
                                    rageOptions.SetActive(true);
                                }


                                if (rageShieldToughness == 0)
                                {
                                    animateShield.Play("ShieldAnimBreak");
                                    yield return new WaitForSeconds(2.5f);
                                    Shield.SetActive(false);
                                }

                                if (parryAction)
                                {
                                    Debug.Log("ParryConfirmed");
                                }

                            }

                            else
                            {

                                bool isDead = playerUnit.TakeDamage(enemyUnit1.damage + DamageRandomizer);
                                yield return new WaitForSeconds(0.65f);
                                animate.Play("CombatReceiveHit");
                                playerHUD.SetHP(playerUnit.currentHP);

                                yield return new WaitForSeconds(2f);
                                rageLevel += (int)(10 + (playerUnit.RagingBull * (10 * .5)));
                                EnemyMoved = true;
                                rageBar.SetRageHUD(rageLevel);
                                if (rageLevel >= 100)
                                {


                                    rageOptions.SetActive(true);
                                }

                                if (isDead)
                                {
                                    state = BattleState.LOST;
                                    animate.Play("CombatFailAnim");
                                    StartCoroutine(EndBattle());
                                }
                            }
                            enemyAttacking = false;

                        }
                        else
                        {
                            moveText.text = enemyUnit1.unitName + " adapts a blocking stance!";
                            animateEnemy1.Play("Block");
                            EnemyMoved = true;
                            EnemyBlock1 = true;

                        }
                    }
                    else if (enemySelected == 2 && EnemyAlive2)
                    {
                        if (TurnRandomizer < 15)
                        {
                            enemyAttacking = true;
                            moveText.text = enemyUnit2.unitName + " uses Physical Attack against " + playerUnit.unitName + "!";
                            animateEnemy2.Play("Attack");


                            if (PBlock || rageShieldToughness > 0 || parryAction)
                            {

                                moveText.text = playerUnit.unitName + " has successfully blocked the attack!";
                                yield return new WaitForSeconds(2f);
                                rageLevel += (int)(5 + (playerUnit.RagingBull * (5 * .5)));
                                rageBar.SetRageHUD(rageLevel);
                                EnemyMoved = true;
                                rageShieldToughness -= 1;
                                if (rageLevel >= 100)
                                {
                                    rageOptions.SetActive(true);
                                }


                                if (rageShieldToughness == 0)
                                {
                                    animateShield.Play("ShieldAnimBreak");
                                    yield return new WaitForSeconds(2.5f);
                                    Shield.SetActive(false);
                                }

                                if (parryAction)
                                {
                                    Debug.Log("ParryConfirmed");
                                }
                            }

                            else
                            {

                                bool isDead = playerUnit.TakeDamage(enemyUnit2.damage + DamageRandomizer);
                                yield return new WaitForSeconds(0.65f);
                                animate.Play("CombatReceiveHit");
                                playerHUD.SetHP(playerUnit.currentHP);

                                yield return new WaitForSeconds(2f);
                                rageLevel += (int)(10 + (playerUnit.RagingBull * (10 * .5)));
                                EnemyMoved = true;
                                rageBar.SetRageHUD(rageLevel);
                                if (rageLevel >= 100)
                                {


                                    rageOptions.SetActive(true);
                                }

                                if (isDead)
                                {
                                    state = BattleState.LOST;
                                    animate.Play("CombatFailAnim");
                                    StartCoroutine(EndBattle());
                                }
                            }
                            enemyAttacking = false;

                        }
                        else
                        {
                            moveText.text = enemyUnit2.unitName + " adapts a blocking stance!";
                            animateEnemy2.Play("Block");
                            EnemyBlock2 = true;
                            EnemyMoved = true;

                        }
                    }
                    else if (enemySelected == 3 && EnemyAlive3)
                    {
                        if (TurnRandomizer < 15)
                        {
                            enemyAttacking = true;
                            moveText.text = enemyUnit3.unitName + " uses Physical Attack against " + playerUnit.unitName + "!";
                            animateEnemy3.Play("Attack");
                            EnemyMoved = true;

                            if (PBlock || rageShieldToughness > 0 || parryAction)
                            {

                                moveText.text = playerUnit.unitName + " has successfully blocked the attack!";
                                yield return new WaitForSeconds(2f);
                                rageLevel += (int)(5 + (playerUnit.RagingBull * (5 * .5)));
                                rageBar.SetRageHUD(rageLevel);
                                rageShieldToughness -= 1;
                                if (rageLevel >= 100)
                                {
                                    rageOptions.SetActive(true);
                                }


                                if (rageShieldToughness == 0)
                                {
                                    animateShield.Play("ShieldAnimBreak");
                                    yield return new WaitForSeconds(2.5f);
                                    Shield.SetActive(false);
                                }

                                if (parryAction)
                                {
                                    Debug.Log("ParryConfirmed");
                                }
                            }

                            else
                            {

                                bool isDead = playerUnit.TakeDamage(enemyUnit3.damage + DamageRandomizer);
                                yield return new WaitForSeconds(0.65f);
                                animate.Play("CombatReceiveHit");
                                playerHUD.SetHP(playerUnit.currentHP);

                                yield return new WaitForSeconds(2f);
                                rageLevel += (int)(10 + (playerUnit.RagingBull * (10 * .5)));
                                EnemyMoved = true;
                                rageBar.SetRageHUD(rageLevel);
                                if (rageLevel >= 100)
                                {


                                    rageOptions.SetActive(true);
                                }

                                if (isDead)
                                {
                                    state = BattleState.LOST;
                                    animate.Play("CombatFailAnim");
                                    StartCoroutine(EndBattle());
                                }
                            }
                            enemyAttacking = false;

                        }
                        else
                        {
                            moveText.text = enemyUnit3.unitName + " adapts a blocking stance!";
                            animateEnemy3.Play("Block");
                            EnemyMoved = true;
                            EnemyBlock3 = true;

                        }
                    }

                }

                yield return new WaitForSeconds(2f);
            }
            if(EnemyMoved == true)
            {
                yield return new WaitForSeconds(1.5f);
                //PBlock = false;
                state = BattleState.PLAYERTURN;
                enemyAttacking = false;
                PlayerTurn();
            }
            EnemyMoved = false;
        }
    }

    //Battle results once ended
    private IEnumerator EndBattle()
    {
        yield return new WaitForEndOfFrame();
        if(state == BattleState.WON)
        {
            dialogueText.text = "Battle Cleared";
            PlayerPrefs.SetInt("HealthBar", playerUnit.currentHP);
            PlayerPrefs.SetInt("MaxHealthPoint", playerUnit.maxHP);
            PlayerPrefs.SetInt("PlayerLevel", playerUnit.unitLevel + 1);
            int lostHP = playerUnit.maxHP - playerUnit.currentHP;
            int calculatedLostHP = PlayerPrefs.GetInt("MaxHealthPointRaw") - lostHP;
            Debug.Log("BattleEndHPRaw = " + calculatedLostHP.ToString());
            PlayerPrefs.SetInt("HealthBarRaw", calculatedLostHP);

            if (unitType == "magus")
            {

                switch (numeration)
                {
                    case 0:
                        SpawnDestroy.magus1Exist = false;
                        break;
                    case 1:
                        SpawnDestroy.magus2Exist = false;
                        SpawnDestroy.herald1Exist = false;
                        break;
                    case 2:
                        SpawnDestroy.magus3Exist = false;
                        SpawnDestroy.herald2Exist = false;
                        break;
                    default:
                        Debug.Log("Error Human");
                        break;
                }
            }

            if (unitType == "child")
            {

                PlayerPrefs.SetInt("NextStage", 1);
                PlayerPrefs.SetInt("ChildBoss", 1);
                sceneLoader.OnLoadTemp();
                    
            }

            if (unitType == "inquisitor")
            {
                yield return new WaitForSeconds(2f);
                StartCoroutine(LoadLevel(15));
            }

            if (unitType == "issei")
            {
                //Gives Achievement Medal upon victory
                if(PlayerPrefs.GetString("SelectedDifficulty") == "Easy")
                {
                    defeatValue = 1;
                } else if(PlayerPrefs.GetString("SelectedDifficulty") == "Normal")
                {
                    defeatValue = 2;
                }
                else if (PlayerPrefs.GetString("SelectedDifficulty") == "Hard")
                {
                    defeatValue = 3;
                }


                if (PlayerPrefs.HasKey("BossAchievement"))
                {
                    bossAchievement = Mathf.Max(PlayerPrefs.GetInt("BossAchievement"), defeatValue);
                    PlayerPrefs.SetInt("BossAchievement", bossAchievement);
                }
                else
                {
                    PlayerPrefs.SetInt("BossAchievement", defeatValue);
                }
                    


                yield return new WaitForSeconds(2f);
                StartCoroutine(LoadLevel(11));
            }

            if (unitType == "monster")
            {
                switch (numeration)
                {
                    case 1:
                        SpawnDestroy.herald1Exist = false;
                        SpawnDestroy.magus2Exist = false;
                        break;
                    case 2:
                        SpawnDestroy.magus3Exist = false;
                        SpawnDestroy.herald2Exist = false;
                        break;
                    default:
                        Debug.Log("Error Monster");
                        break;
                }
            }

            yield return new WaitForSeconds(2f);
            StartCoroutine(LoadLevel(sceneNumber));

        } else if(state == BattleState.LOST)
        {
            dialogueText.text = "Battle Failed";

            if (inquisitor)
            {
                yield return new WaitForSeconds(2f);
                StartCoroutine(LoadLevel(15));
            }

            if (child)
            {
                PlayerPrefs.SetInt("NextStage", 1);
                PlayerPrefs.SetInt("ChildBoss", 2);
                sceneLoader.OnLoadTemp();
            }

            if (boss_1st)
            {
                yield return new WaitForSeconds(2f);
                StartCoroutine(LoadLevel(11));
            }

            yield return new WaitForSeconds(2f);
            StartCoroutine(LoadLevel(sceneNumber));
        }
        else
        {
            yield return new WaitForSeconds(2f);
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    
    //Player turn commences
    void PlayerTurn()
    {
        
        moveText.text = "Choose an action...";
        if (PBlock && !child && !inquisitor)
        {
            animate.Play("CombatBlockIdleEnd");
        }

        
        playerAvatar.SetActive(true);
        enemyAvatar.SetActive(false);

        dialogueText.text = "Your Turn";
        //Updates the bottom HUD with the player stat
        

    }

    public void OnAttackButton()
    {
        //Checks if its the player turn or not
        if (state != BattleState.PLAYERTURN)
            return;
        PBlock = false;
        if (enemySelected == 1)
        {
            //Animates the attack animation
            animate.Play("CombatAttackAnim");
            moveText.text = playerUnit.unitName + " uses Physical Attack against " + enemyUnit1.unitName + "!";
            //Calls the physical atk
            state = BattleState.ACTIONCHOSEN;
            if (EnemyBlock1)
            {
                moveText.text = enemyUnit1.unitName + " completely blocked the attack!";
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
            else
            {
                StartCoroutine(PlayerPhysAttack());
            }
        }
        else if(enemySelected == 2)
        {
            //Animates the attack animation
            animate.Play("CombatAttackAnim");
            moveText.text = playerUnit.unitName + " uses Physical Attack against " + enemyUnit2.unitName + "!";
            //Calls the physical atk
            state = BattleState.ACTIONCHOSEN;
            if (EnemyBlock2)
            {
                moveText.text = enemyUnit2.unitName + " completely blocked the attack!";
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
            else
            {
                StartCoroutine(PlayerPhysAttack());
            }
        }
        else if(enemySelected == 3)
        {
            //Animates the attack animation
            animate.Play("CombatAttackAnim");
            moveText.text = playerUnit.unitName + " uses Physical Attack against " + enemyUnit3.unitName + "!";
            //Calls the physical atk
            state = BattleState.ACTIONCHOSEN;
            if (EnemyBlock3)
            {
                moveText.text = enemyUnit3.unitName + " completely blocked the attack!";
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
            else
            {
                StartCoroutine(PlayerPhysAttack());
            }
        }

    }
    public void OnMagicButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        state = BattleState.ACTIONCHOSEN;
        PBlock = false;
        if (MagicCharge == false)
        {
            
            moveText.text = playerUnit.unitName + " has started to charge up her power...";
            animate.Play("CombatMagicAnim");
            MagicCharge = true;
            MGatherButton.SetActive(false);
            MReleaseButton.SetActive(true);
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        else
        {
            
            moveText.text = playerUnit.unitName + " unleashes Magical Attack!";
            animate.Play("CombatMagicRelease");

            Playerps.Play();
            MagicCharge = false;
            MGatherButton.SetActive(true);
            MReleaseButton.SetActive(false);
            StartCoroutine(PlayerMagAttack());

        }
              
    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        state = BattleState.ACTIONCHOSEN;
        PBlock = false;
        if (HealCharge == false)
        {
            moveText.text = playerUnit.unitName + " has started to cast healing spell!";
            animate.Play("CombatMagicAnim");
            HealCharge = true;
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        else
        {
            animate.Play("CombatHeal");
            moveText.text = playerUnit.unitName + " regenerates her HP!";
            HealCharge = false;

            StartCoroutine(PlayerHeal());
        }
        
        
        
    }

    public void OnBlockButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        PBlock = true;
        animate.Play("CombatBlockAnim");
        moveText.text = playerUnit.unitName + " adapts a blocking stance!";

        state = BattleState.ACTIONCHOSEN;
        StartCoroutine(PlayerBlock());


    }

    public void OnRageAttackButton()
    {
        //Checks if its the player turn or not
        if (state != BattleState.PLAYERTURN)
            return;
        //Animates the attack animation
        
        animate.Play("CombatRageAttackSwordFall");
        moveText.text = playerUnit.unitName + " made a huge sword from the sky and throws it at all the enemies!";
        rageLevel = 0;
        rageBar.SetRageHUD(rageLevel);
        //Calls the physical atk
        state = BattleState.ACTIONCHOSEN;

        StartCoroutine(PlayerRageAttack());
        
    }

    public void OnRageShieldButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        animate.Play("CombatMagicAnim");
        moveText.text = playerUnit.unitName + " has formed a strong barrier around her!";
        rageLevel = 0;
        rageBar.SetRageHUD(rageLevel);

        Shield.SetActive(true);
        animateShield.Play("ShieldAnimStart");

        state = BattleState.ACTIONCHOSEN;
        StartCoroutine(PlayerRageShield());
    }

    public void OnAimandFireButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        counterExecution = false;

        if (bulletsRemaining > 0)
        {
            animate.Play("amaraNeutral_AimandFire");
            moveText.text = playerUnit.unitName + " fired a shot from her flintlock!";
            state = BattleState.ACTIONCHOSEN;
            if (EnemyBlock1)
            {
                moveText.text = enemyUnit1.unitName + " has dodged the bullet!";
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
            else
            {
                StartCoroutine(PlayerPhysAttack());
            }

        }
        else
        {
            moveText.text = playerUnit.unitName + " ran out of ammo!";
            state = BattleState.ACTIONCHOSEN;
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }

    }

    public void OnReloadButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        PBlock = false;
        animate.Play("amaraNeutral_Reload");
        moveText.text = playerUnit.unitName + " reloads her gun.";
        StartCoroutine(PlayerReload());
    }

    public void OnGuardButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        PBlock = true;
        animate.Play("amaraNeutral_GuardStance");
        moveText.text = playerUnit.unitName + " adapts a guarding stance!";
        state = BattleState.ACTIONCHOSEN;
        StartCoroutine(PlayerBlock());

    }

    public void OnParryButton()
    {
        if (state != BattleState.ENEMYTURN)
            return;

        if (enemyAttacking)
        {
            Debug.Log("OPB EnemyAttacking");
            GameObject.FindGameObjectWithTag("CombatPlayer").GetComponent<BoxCollider2D>().enabled = true;
            StartCoroutine(PlayerParry());
        }
        else
        {
            Debug.Log("OPB Else");
            if (thisScene > 12)
            {
                //PBlock = false;
                animate.Play("amaraNeutral_FailParry");
                //state = BattleState.PLAYERTURN;
                //PlayerTurn();
            }
            else
            {
                //PBlock = false;
                animate.Play("CombatParryFail");
                
            }
            PBlock = false;
        }
        
    }

    public void OnDodgeButton()
    {
        if (enemyAttacking)
        {
            if(secondArt || thirdArt)
            {
                GameObject.FindGameObjectWithTag("CombatPlayer").GetComponent<BoxCollider2D>().enabled = true;
            }
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    public void SelectFirstEnemy()
    {
        if (state != BattleState.PLAYERTURN || !EnemyAlive1)
            return;
        Select_1.SetActive(true);
        Select_2.SetActive(false);
        Select_3.SetActive(false);
        enemySelected = 1;
    }

    public void SelectSecondEnemy()
    {
        if (state != BattleState.PLAYERTURN || !EnemyAlive2)
            return;
        Select_2.SetActive(true);
        Select_3.SetActive(false);
        Select_1.SetActive(false);
        enemySelected = 2;
    }

    public void SelectThirdEnemy()
    {
        if (state != BattleState.PLAYERTURN || !EnemyAlive3)
            return;
        Select_3.SetActive(true);
        Select_2.SetActive(false);
        Select_1.SetActive(false);
        enemySelected = 3;
    }

    public IEnumerator PlayerDodge()
    {
        if (parryAble)
        {
            moveText.text = playerUnit.unitName + " successfully dodged!";
            animate.Play("amaraNeutral_Dodge");
            yield return new WaitForSeconds(1f);
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
        else
        {
            moveText.text = playerUnit.unitName + " failed to dodge on time!";
            animate.Play("amaraNeutral_Hurt");
            bool isDead = playerUnit.TakeDamage(enemyUnit1.damage + DamageRandomizer);
            playerHUD.SetHP(playerUnit.currentHP);

            yield return new WaitForSeconds(2f);

            if (isDead)
            {
                state = BattleState.LOST;
                animate.Play("amaraNeutral_Fall");
                StartCoroutine(EndBattle());
            }
            else
            {
                state = BattleState.PLAYERTURN;
                PlayerTurn();
            }

        }
    }

    public IEnumerator PlayerParry()
    {
        if (state == BattleState.PLAYERTURN)
            yield return PlayerParry();

        Debug.Log("PlayerParryPassed");
        //PBlock = false;
        if(SceneManager.GetActiveScene().buildIndex > 12)
        {
            if (parryAble && bulletsRemaining > 0)
            {

                moveText.text = playerUnit.unitName + " successfully counterattacked!";
                counterExecution = true;
                if (unitType == "inquisitor")
                {
                    animate.Play("amaraNeutral_Dodge");
                    yield return new WaitForSeconds(0.83f);
                    animate.Play("amaraNeutral_ParryCounter");
                }
                else
                {
                    animate.Play("amaraNeutral_ParryCounter");
                }
                powerUP = 0;
                yield return new WaitForSeconds(1f);
                //playerCollider.enabled = false;
                StartCoroutine(PlayerPhysAttack());


            }
            else if (bulletsRemaining <= 0)
            {
                counterExecution = false;
                //playerCollider.enabled = false;
                moveText.text = playerUnit.unitName + " ran out of bullets!";
                animate.Play("amaraNeutral_FailParry");
                bool isDead = playerUnit.TakeDamage(enemyUnit1.damage + DamageRandomizer);
                playerHUD.SetHP(playerUnit.currentHP);

                yield return new WaitForSeconds(2f);

                if (isDead)
                {
                    state = BattleState.LOST;
                    animate.Play("amaraNeutral_Fall");
                    StartCoroutine(EndBattle());
                }
                else
                {
                    state = BattleState.PLAYERTURN;
                    PlayerTurn();
                }
            }
            else
            {
                counterExecution = false;
                //playerCollider.enabled = false;
                moveText.text = playerUnit.unitName + " failed to parry!";
                animate.Play("amaraNeutral_FailParry");
                bool isDead = playerUnit.TakeDamage(enemyUnit1.damage + DamageRandomizer);
                playerHUD.SetHP(playerUnit.currentHP);
                animate.Play("amaraNeutral_Hurt");

                yield return new WaitForSeconds(2f);

                if (isDead)
                {
                    state = BattleState.LOST;
                    animate.Play("amaraNeutral_Fall");
                    StartCoroutine(EndBattle());
                }
                else
                {
                    state = BattleState.PLAYERTURN;
                    PlayerTurn();
                }
            }
        }
        else
        {
            Debug.Log("ParryElse");
            if(parryAble && unitType == "monster")
            {
                Debug.Log("Not HUman");
                moveText.text = playerUnit.unitName + " counterattacks!";
                counterExecution = true;
                animate.Play("CombatParryPhys");
                yield return new WaitForSeconds(2f);
                //playerCollider.enabled = false;
                StartCoroutine(PlayerPhysAttack());
            }
            else if(parryAble && unitType == "issei")
            {
                Debug.Log("Not HUman");
                moveText.text = playerUnit.unitName + " counterattacks!";
                counterExecution = true;
                animate.Play("CombatParryPhys");
                yield return new WaitForSeconds(2f);
                //playerCollider.enabled = false;
                StartCoroutine(PlayerPhysAttack());
            }
            else if(parryAble && unitType == "magus")
            {
                Debug.Log("Human");
                moveText.text = playerUnit.unitName + " counterattacks!";
                counterExecution = true;
                animate.Play("CombatParry");
                yield return new WaitForSeconds(1f);
                //playerCollider.enabled = false;
                StartCoroutine(PlayerPhysAttack());
            }
            else
            {
                if (parryAble)
                {
                    Debug.Log("ParryAble Yes");
                }
                else if(!parryAble)
                {
                    //Debug.Log("ParryAble No");
                    //Debug.Log(parryAble);
                }
                else
                {
                    Debug.Log("ParryAble Maybe");
                }

                if (unitType == "magus")
                {
                    Debug.Log("Human Yes");
                }
                else if (unitType == "monster")
                {
                    //Debug.Log("Human No");
                    //Debug.Log(human);
                }
                else
                {
                    Debug.Log("Human Maybe");
                }
                counterExecution = false;
                //playerCollider.enabled = false;
                moveText.text = playerUnit.unitName + " failed to parry!";
                animate.Play("amaraNeutral_FailParry");
                PBlock = false;
                bool isDead = playerUnit.TakeDamage(enemyUnit1.damage + DamageRandomizer);
                playerHUD.SetHP(playerUnit.currentHP);

                yield return new WaitForSeconds(2f);

                if (isDead)
                {
                    state = BattleState.LOST;
                    animate.Play("amaraNeutral_Fall");
                    StartCoroutine(EndBattle());
                }
                //else
                //{
                    //state = BattleState.PLAYERTURN;
                    //PlayerTurn();
                //}
            }
        }
    }

    IEnumerator LoadLevel(int sceneToLoad)
    {

        yield return new WaitForSeconds(transitionTime);

        levelLoader.LoadLevel(sceneToLoad);
    }

    public void ParryDetection(bool collide)
    {
        parryAble = collide;
        if (parryAble)
        {
            //Debug.Log(parryAble);
        }
        else
        {
            //Debug.Log(parryAble);
        }
       
    }

    static void BossMoveKnapSack(int W, int[] wt,
                            int[] val, int n)
    {
        int i, w;
        int[,] K = new int[n + 1, W + 1];

        // Build table K[][] in bottom up manner
        for (i = 0; i <= n; i++)
        {
            for (w = 0; w <= W; w++)
            {
                if (i == 0 || w == 0)
                    K[i, w] = 0;
                else if (wt[i - 1] <= w)
                    K[i, w] = Math.Max(val[i - 1] +
                            K[i - 1, w - wt[i - 1]], K[i - 1, w]);
                else
                    K[i, w] = K[i - 1, w];
            }
        }

        // stores the result of Knapsack
        int res = K[n, W];


        w = W;
        for (i = n; i > 0 && res > 0; i--)
        {

            // either the result comes from the top
            // (K[i-1][w]) or from (val[i-1] + K[i-1]
            // [w-wt[i-1]]) as in Knapsack table. If
            // it comes from the latter one/ it means
            // the item is included.
            if (res == K[i - 1, w])
                continue;
            else
            {

                // This item is included.
                // Creates a new array of moves and sends it back to the Boss.
                moveList.Add(wt[i - 1]);
                
                

                // Since this weight is included its
                // value is deducted
                res = res - val[i - 1];
                w = w - wt[i - 1];
            }
        }
    }
}
