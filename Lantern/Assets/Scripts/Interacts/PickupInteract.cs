using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupInteract : MonoBehaviour
{

    public bool hasDialogue;
    public bool disabledAfterInteract = false;

    private bool disabled = false;

    // Don't allow stacked interacts
    private bool locked = false;

    private DialogueTrigger dialogueTrigger;

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
    // acquire item
    {
        FindObjectOfType<GameManager_1>().coins++;
        GetComponent<Pickup>().Collect();
        locked = false;
    }

}
