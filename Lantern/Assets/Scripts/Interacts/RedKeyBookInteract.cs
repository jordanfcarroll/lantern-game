using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedKeyBookInteract : MonoBehaviour {

    public bool hasDialogue;
    public bool disabledAfterInteract = false;
    public GameObject Cutscene_OpenAltar;

    private bool disabled = false;

    // Don't allow stacked interacts
    private bool locked = false;

    private DialogueTrigger dialogueTrigger;
    private DialogueTrigger acquireDialogTrigger;

    void Awake()
	// get reference to one-off dialogue trigger child for key acquire
    {
		DialogueTrigger[] DialogueTriggers = gameObject.GetComponentsInChildren<DialogueTrigger>();

		foreach (DialogueTrigger DialogueTrigger in DialogueTriggers)
		{
			if (DialogueTrigger.gameObject.transform.parent != null)
			{
				acquireDialogTrigger = DialogueTrigger;
			}
		}
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.name == "Player" && !disabled && !locked)
        {
            FindObjectOfType<PlayerControl>().setInteractTooltip();
            if (Input.GetButtonDown("Fire1"))
            {
                if (hasDialogue)
                {
                    FindObjectOfType<PlayerControl>().unsetInteractTooltip();
                    locked = true;
                    dialogueTrigger = GetComponent<DialogueTrigger>();
                    dialogueTrigger.triggerDialogue(endAction);
                    if (disabledAfterInteract)
                    {
                        disabled = true;
                    }

                }

                GetComponent<Animator>().Play("Open");


            }
        }
    }

    void endAction()
	// acquire key
    {
        if (!FindObjectOfType<GameManager_1>().keyInventory["red_key"]) {
			acquireDialogTrigger.triggerDialogue(endAction2);
			FindObjectOfType<GameManager_1>().keyInventory["red_key"] = true;
		} else {
            locked = false;
        }
    }

    void endAction2() {
        locked = false;
    }
}
