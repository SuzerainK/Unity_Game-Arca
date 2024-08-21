using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovements : MonoBehaviour
{

    private Rigidbody2D rb;
    
    private Animator animate;
    [SerializeField] private Transform groundCheckCollider;
    [SerializeField] LayerMask groundLayer;

    public float jumpPower = 250;
    private Vector2 movement;
    float groundCheckRadius = 0.2f;
    float runSpeedModifier = 2f;
    [SerializeField] bool isGrounded;
    bool jump;
    public float moveSpeed = 1f;
    private float jumpForce;
    bool isRunning = false;
    [SerializeField] private bool isJumping;
    private bool isOnSlope;
    [SerializeField] private float slopeCheckDistance;
    private float moveHorizontal;
    private float moveVertical;
    private int extraJumps;
    public int extraJumpsValue;
    private int savedSceneIndex;
    private CapsuleCollider2D cc;
    

    SavedPlayerPos playerPosData;

    public bool gameMenuPane = false;
    public bool saveMenuPane = false;
    public bool audioMenuPane = false;

    private float slopeDownAngle;
    private float slopeDownAngleOld;

    private float veloX;
    private float veloY;

    private float targetYVelocity;

    private Vector2 colliderSize;
    private Vector2 slopeNormalPerp;

    public GameObject Audio;

    QuitButton GM;

    AudioManager SFX;

    ScnSave SS;

    OverworldUI OUI;

    [SerializeField] MasterVolume MV;

    bool StageBuffOn;


    void Awake()
    {
        StageBuffOn = false;
        extraJumps -= 1;

        GM = GameObject.FindGameObjectWithTag("Manager").GetComponent<QuitButton>();
        SS = GameObject.FindGameObjectWithTag("Manager").GetComponent<ScnSave>();
        OUI = GameObject.FindGameObjectWithTag("UI").GetComponent<OverworldUI>();
        //playerPosData = GameObject.FindGameObjectWithTag("Manager").GetComponent<SavedPlayerPos>();

        if (playerPosData == null || savedSceneIndex != SceneManager.GetActiveScene().buildIndex)
        {
            
            savedSceneIndex = SceneManager.GetActiveScene().buildIndex;
            playerPosData = FindObjectOfType<SavedPlayerPos>();
        }
        
        
        
        if (PlayerPrefs.GetInt("NextStage") == 0)
        {
            if(PlayerPrefs.GetInt("ChildBoss") == 1 || PlayerPrefs.GetInt("InquisitorBoss") == 1)
            {
                playerPosData.PlayerPosLoad();
                playerPosData = null;
            }
            
        }
        else
        {
            PlayerPrefs.SetInt("NextStage", 0);
        }

        
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CapsuleCollider2D>();
        colliderSize = cc.size;
        
        animate = GetComponent<Animator>();
        SFX = Audio.GetComponent<AudioManager>();
        
        isJumping = false;
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            groundCheckRadius = 1f;
        }
        else if (SceneManager.GetActiveScene().buildIndex >= 2 && SceneManager.GetActiveScene().buildIndex <= 11)
        {
            groundCheckRadius = 0.2f;
        }
    }


    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex >= 2 && SceneManager.GetActiveScene().buildIndex <= 5)
        {
            StageBuffOn = StageBuff.GetInstance().stageBuffSelectionOn;
        }
        else
        {
            StageBuffOn = DialogueManager.GetInstance().dialogueIsPlaying;
        }

        movement = new Vector2(Input.GetAxis("Horizontal"), 0).normalized;
        moveHorizontal = Input.GetAxisRaw("Horizontal");

        //"Jump" Controls script
        if (Input.GetButtonDown("Jump") && extraJumps > 0 && (!DialogueManager.GetInstance().dialogueIsPlaying && !StageBuffOn))
        {
            if(rb.velocity.x == 0f)
            {
                if(SceneManager.GetActiveScene().buildIndex >= 2 && SceneManager.GetActiveScene().buildIndex <= 11 && (!DialogueManager.GetInstance().dialogueIsPlaying && !StageBuffOn))
                {
                    Debug.Log("IfYes");
                    targetYVelocity = 10f;
                }
                else
                {
                    Debug.Log("IfNo");
                    targetYVelocity = 5f;
                }
                rb.velocity = new Vector3(rb.velocity.x, targetYVelocity, 0f);
            }
            rb.AddForce(new Vector2(0f, jumpPower));
            animate.SetBool("Jump", true);
            jump = true;
            extraJumps--;
            
        }
        else if(Input.GetButtonDown("Jump") && extraJumps == 0 && (!DialogueManager.GetInstance().dialogueIsPlaying && !StageBuffOn))
        {

            rb.AddForce(new Vector2(0f, jumpPower));
            animate.SetBool("Jump", true);
            jump = true;
            extraJumps--;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && (!DialogueManager.GetInstance().dialogueIsPlaying && !StageBuffOn) && !saveMenuPane && !audioMenuPane)
        {
            gameMenuPane = true;
            GM.OnMenuButton();
        }


        if(Input.GetKeyDown(KeyCode.Y) && gameMenuPane)
        {
            gameMenuPane = false;
            GM.OnConfirmQuit();
        }
        else if(Input.GetKeyDown(KeyCode.N) && gameMenuPane)
        {
            gameMenuPane = false;
            GM.OnDenyQuit();
        }

        if (Input.GetKeyDown(KeyCode.H) && (!DialogueManager.GetInstance().dialogueIsPlaying && !StageBuffOn) && !gameMenuPane && !saveMenuPane)
        {
            Debug.Log("Entered Open Audio 0");
            audioMenuPane = true;
            OUI.OnOpenVM();
        }

        if (Input.GetKeyDown(KeyCode.Y) && audioMenuPane)
        {
            Debug.Log("Entered Close Audio 0");
            audioMenuPane = false;
            OUI.OnCloseVM();
        }

        if (Input.GetKeyDown(KeyCode.J) && audioMenuPane)
        {
            MV.DecreaseVolume();
        }
        else if(Input.GetKeyDown(KeyCode.K) && audioMenuPane)
        {
            MV.IncreaseVolume();
        }


        if (Input.GetKeyDown(KeyCode.Tab) && (!DialogueManager.GetInstance().dialogueIsPlaying && !StageBuffOn) && !gameMenuPane && !audioMenuPane)
        {
            if(SceneManager.GetActiveScene().buildIndex == 12)
            {
                if(PlayerPrefs.GetInt("AllowSave") == 0)
                {
                    StartCoroutine(OUI.Save_Null());
                }
                else
                {
                    saveMenuPane = true;
                    SS.OnSaveButton();
                }
            }
            else
            {
                saveMenuPane = true;
                SS.OnSaveButton();
            }
        }

        if(Input.GetKeyDown(KeyCode.Alpha1) && saveMenuPane)
        {
            Debug.Log("KeyPad1 works!");
            saveMenuPane = false;
            SS.OnSaveOneButton();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && saveMenuPane)
        {
            Debug.Log("KeyPad2 works!");
            saveMenuPane = false;
            SS.OnSaveTwoButton();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && saveMenuPane)
        {
            Debug.Log("KeyPad3 works!");
            saveMenuPane = false;
            SS.OnSaveThreeButton();
        }
        if (DialogueManager.GetInstance().dialogueIsPlaying || StageBuffOn)
        {
            //Debug.Log("Dialog is playing...");
            if (rb.velocity.y > 0f)
            {
                targetYVelocity = -3f;
                rb.velocity = new Vector3(rb.velocity.x, targetYVelocity, 0f);
                
            }
            //animate.SetBool("Jump", false);
            //jump = false;
        }

        
        if(Input.GetKeyDown(KeyCode.Tab) && saveMenuPane && Input.GetKeyDown(KeyCode.Q))
        {
            saveMenuPane = false;
            SS.OnReturnToGame();
        }

        /*else if (Input.GetButtonUp("Jump"))
        {
            jump = false;
        }
        */
        //"Running" Script
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
        }

        animate.SetFloat("yVelocity", rb.velocity.y);
        animate.SetFloat("yVelocityAbs", Math.Abs(rb.velocity.y));



        //Left and Right direction flipping
        if (movement != Vector2.zero)
        {
            bool flipped = movement.x < 0;
            this.transform.rotation = Quaternion.Euler(new Vector3(0f, flipped ? 180f : 0f, 0f));
        }
    }

    void FixedUpdate()
    {
        if (DialogueManager.GetInstance().dialogueIsPlaying || StageBuffOn)
        {
            animate.SetFloat("xVelocity", 0);
            animate.SetFloat("yVelocity", 0);
            animate.SetFloat("yVelocityAbs", 0);
            StopFootSteps();
            return;
        }
        GroundCheck();
        //SlopeCheck();
        Move(moveHorizontal, jump);

        veloX = Math.Abs(rb.velocity.x);
        veloY = Math.Abs(rb.velocity.y);

        //Debug.Log(veloX);
        //Debug.Log(veloY);

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (veloX < 0.1 && veloY < 2.3 && isGrounded)
            {
                //Debug.Log("This is working.");
                //Debug.Log(isGrounded);
                rb.isKinematic = true;
                rb.velocity = new Vector3(0f, 0f, 0f);
            }
            else
            {
                //Debug.Log("This is not working.");
                //Debug.Log(isGrounded);
                rb.isKinematic = false;
            }
        }
        else if (SceneManager.GetActiveScene().buildIndex >= 2 && SceneManager.GetActiveScene().buildIndex <= 11)
        {
            if (veloX < 0.1 && veloY < 2 && isGrounded)
            {
                //Debug.Log("This is working.");
                //Debug.Log(isGrounded);
                rb.isKinematic = true;
                rb.velocity = new Vector3(0f, 0f, 0f);
            }
            else
            {
                //Debug.Log("This is not working.");
                //Debug.Log(isGrounded);
                rb.isKinematic = false;
            }
        }
        else
        {
            if (veloX < 0.1 && veloY < 0.1 && isGrounded)
            {
                //Debug.Log("This is working.");
                rb.isKinematic = true;
                rb.velocity = new Vector3(0f, 0f, 0f);
            }
            else
            {
                //Debug.Log("This is not working.");
                rb.isKinematic = false;
            }
        }


        

    }

    //private void SlopeCheck()
    //{
    //    Vector2 checkPos = transform.position - new Vector3(0.0f, colliderSize.y / 2);
    //    SlopeCheckVertical(checkPos);
    //}

    //private void SlopeCheckHorizontal(Vector2 checkPos)
    //{

    //}

    //private void SlopeCheckVertical(Vector2 checkPos)
    //{
    //    RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, slopeCheckDistance, groundLayer);

    //    if (hit)
    //    {
    //        slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;
    //        slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

    //        if (slopeDownAngle != slopeDownAngleOld)
    //        {
    //            isOnSlope = true;
    //        }

    //        slopeDownAngleOld = slopeDownAngle;
    //        Debug.DrawRay(hit.point, slopeNormalPerp, Color.red);
    //        Debug.DrawRay(hit.point, hit.normal, Color.green);
    //    }
    //}

    void Move(float dir, bool jumpFlag)
    {
        if (isGrounded && jumpFlag)
        {
            extraJumps = 1;
            jumpFlag = false;
            //isGrounded = false;
            
        }
        #region Move & Run
        float xVal = dir * moveSpeed * 100 * Time.deltaTime;
        
        if (isRunning)
        {
            
            xVal *= runSpeedModifier;
            FasterFootSteps(true);
        }
            
        Vector2 targetVelocity = new Vector2(xVal, rb.velocity.y);
        rb.velocity = targetVelocity;

        if(rb.velocity.x == 0)
        {
            StopFootSteps();
        }
        else if (!isRunning)
        {
            FasterFootSteps(false);
        }

        
        // 0 Idle, 10 walking, 20 running
        animate.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));

        //if(isGrounded && !isOnSlope)
        //{
        //    targetVelocity.Set(moveSpeed * moveHorizontal, 0.0f);
        //    rb.velocity = targetVelocity;
        //}else if(isGrounded && isOnSlope)
        //{
        //    targetVelocity.Set(moveSpeed * slopeNormalPerp.x * -moveHorizontal, moveSpeed * slopeNormalPerp.y * -moveHorizontal);
        //    rb.velocity = targetVelocity;
        //}else if (!isGrounded)
        //{
        //    targetVelocity.Set(moveSpeed * moveHorizontal, rb.velocity.y);
        //    rb.velocity = targetVelocity;
        //}


        #endregion
    }

    void GroundCheck()
    {
        isGrounded = false;
        //Checks if the GroundCheckObject is colliding with other
        //2D Colliders that are in the "Ground" Layer
        //If yes (isGrounded true) else (isGrounded false)
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer);
        if (colliders.Length > 0)
            isGrounded = true;

        animate.SetBool("Jump", !isGrounded);
    }

    void PlayFootSteps()
    {
        if (!isGrounded)
        {
            return;
        }
        else
        {
            SFX.Play("Footsteps");
        }
        
    }

    void StopFootSteps()
    {
        SFX.Stop("Footsteps");
    }

    void FasterFootSteps(bool enabled)
    {
        if(enabled)
            SFX.RaisePitch("Footsteps", true);
        else
            SFX.RaisePitch("Footsteps", false);
    }

}
