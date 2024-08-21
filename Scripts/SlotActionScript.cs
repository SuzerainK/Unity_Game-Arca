using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotActionScript : MonoBehaviour
{
    bool slotIsSelected = false;
    string slotSelectedName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(slotIsSelected == true) {
            if(Input.GetKeyUp(KeyCode.A)) {
                // Opens the Saved Game
                Debug.Log("Pressed Continue: " + slotSelectedName);
            } else if(Input.GetKeyUp(KeyCode.D)) {
                // Asks for Confirmation. If yes, then delete the Selected Saved Slot.
                Debug.Log("Deleted: " + slotSelectedName);
            }
        }
    }

    private void OnEnable()
    {
        // Loads the Saved Data (not the Game)
    }

    private void OnDisable()
    {
        // Resets the to Default
        slotIsSelected = false;
    }

    public void GetSelectedSlot() {
        slotSelectedName = EventSystem.current.currentSelectedGameObject.name;
        slotIsSelected = true;
        
        for(int count = 1; count <= 3; count++) {
            string savedSlotName = "Slot" + count + "Button";
            GameObject slotGameObject = GameObject.Find(savedSlotName);
            GameObject selectedImageObject = GetChildWithName(slotGameObject, "Selected");
            if(savedSlotName.Contains(slotSelectedName)) {
                selectedImageObject.SetActive(true);
            } else {
                selectedImageObject.SetActive(false);
            }
        }
    }

    GameObject GetChildWithName(GameObject obj, string name) {
        Transform trans = obj.transform;
        Transform childTrans = trans.Find(name);
        if (childTrans != null) {
            return childTrans.gameObject;
        } else {
            return null;
        }
    }
}
