using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoBGMChange : MonoBehaviour
{
    AudioManager audioManager;
    [SerializeField] string BGMPlay;
    // Start is called before the first frame update
    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            audioManager.Stop("BGM");
            audioManager.Stop("BGMViolinSolo");
            audioManager.Stop("BGMDeath");
            audioManager.Stop("BGMEscapeRoute");
            audioManager.Stop("BGMGardens");
            audioManager.Stop("BGMHouseofVirgins");
            audioManager.Stop("BGMSpeech");
            audioManager.Play(BGMPlay);
        }
    }
}
