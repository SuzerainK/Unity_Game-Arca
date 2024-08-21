using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFireLine : MonoBehaviour
{
    private int voiceSwitch;
    public Transform targetPlayer;
    [Range(1, 10)]
    public float smoothFactor;
    public Vector3 minValues, maxValues;
    public GameObject Player;
    public GameObject BG;
    public GameObject GameOver;

    public Animator bulletLine;
    public Animator bulletFire;
    public GameObject HPBar;
    Animator PlayerAnim;
    Animator BGAnim;

    AudioManager Audio;
    HealthBar healthBar;
    Unit playerUnit;

    bool OnRange;

    private void Start()
    {
        GameOver.SetActive(false);
        BGAnim = BG.GetComponent<Animator>();
        PlayerAnim = Player.GetComponent<Animator>();
        playerUnit = GameObject.FindGameObjectWithTag("Player").GetComponent<Unit>();
        healthBar = HPBar.GetComponent<HealthBar>();
        Audio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }
    private void Update()
    {
        
    }
    private void FixedUpdate()
    {
        FollowAim();
    }

    void FollowAim()
    {
        //Define minimum x,y,z values and maximum x,y,z values

        //Vector3 additionalOffset = new Vector3(0, 0, -10);
        Vector3 targetPosition = targetPlayer.position;
        //Verify if the targetPosition is out of bound or not
        //Limit it to the minimum and max values
        Vector3 boundPosition = new Vector3(
            Mathf.Clamp(targetPosition.x, minValues.x, maxValues.x),
            Mathf.Clamp(targetPosition.y, minValues.y, maxValues.y),
            Mathf.Clamp(targetPosition.z, minValues.z, maxValues.z));

        Vector3 smoothPosition = Vector3.Lerp(transform.position, boundPosition, smoothFactor * Time.fixedDeltaTime);
        transform.position = smoothPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            OnRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            OnRange = false;
        }
    }

    public void DamagePlayer()
    {
        voiceSwitch = Random.Range(1, 7);
        Audio.Play("BulletWhiz");
        bulletFire.Play("bulletLine_Fire");
        if (OnRange)
        {
            if (voiceSwitch <= 3)
            {
                Audio.Play("AmaraHurt_Hard_01");
            }
            else
            {
                Audio.Play("AmaraHurt_Hard_02");
            }

            bool isDead = playerUnit.TakeDamage(30);
            PlayerPrefs.SetInt("HealthBar", playerUnit.currentHP);
            healthBar.ReUpdate();
            if (isDead)
            {
                GameOver.SetActive(true);
                PlayerAnim.Play("Amara_Neutral_Defeat");
                BGAnim.Play("DeathAnimation");

            }
            else
            {
                BGAnim.Play("DamageTaken");
            }
        }
    }



}
