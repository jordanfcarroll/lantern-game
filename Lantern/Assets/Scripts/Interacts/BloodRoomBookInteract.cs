using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodRoomBookInteract : MonoBehaviour {


    public bool hasDialogue;
    public bool disabledAfterInteract = false;

    private bool disabled = false;

    // Don't allow stacked interacts
    private bool locked = false;

    private DialogueTrigger dialogueTrigger;
    private DialogueTrigger acquireDialogTrigger;

    void Awake()
    {
    
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.name == "Player" && !disabled && !locked)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (hasDialogue)
                {
                    locked = true;
                    dialogueTrigger = GetComponent<DialogueTrigger>();
                    dialogueTrigger.triggerDialogue(endAction);
                    if (disabledAfterInteract)
                    {
                        disabled = true;
                    }

                }


            }
        }
    }

    void endAction()
    // acquire key
    {
     
        FindObjectOfType<GameManager_1>().bloodRoomVisited = true;


    }


}
