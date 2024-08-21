using UnityEngine;

public class SavePointAuto : MonoBehaviour
{
    ScnSave saveElements;
    SavedPlayerPos playerPosSaved;

    void Start()
    {
        playerPosSaved = GameObject.FindGameObjectWithTag("Manager").GetComponent<SavedPlayerPos>();
        saveElements = GameObject.FindGameObjectWithTag("Manager").GetComponent<ScnSave>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("SavePointAuto");
        if (collision.gameObject.tag == "Player")
        {
            playerPosSaved.PlayerPosSave();
            Debug.Log("Collision Detected 591");
            saveElements.TempSave();
            Debug.Log(PlayerPrefs.GetFloat("SaveTempPosX"));
            Debug.Log(PlayerPrefs.GetFloat("SaveTempPosY"));
        }
    }
}
