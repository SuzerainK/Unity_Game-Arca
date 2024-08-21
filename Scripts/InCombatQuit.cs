using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InCombatQuit : MonoBehaviour
{
    private bool gameMenuPane = false;
    QuitButton GM;
    // Start is called before the first frame update
    void Awake()
    {
        GM = GameObject.FindGameObjectWithTag("Manager").GetComponent<QuitButton>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameMenuPane = true;
            GM.OnMenuButton();
        }


        if (Input.GetKeyDown(KeyCode.Y) && gameMenuPane)
        {
            gameMenuPane = false;
            GM.OnConfirmQuit();
        }
        else if (Input.GetKeyDown(KeyCode.N) && gameMenuPane)
        {
            gameMenuPane = false;
            GM.OnDenyQuit();
        }
    }
}
