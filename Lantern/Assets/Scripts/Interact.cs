using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour {

	public bool hasDialogue;
	public bool disabledAfterInteract = false;
	public bool hasInteractTooltip = true;

	private bool disabled = false;

	private bool locked = false;


	private DialogueTrigger dialogueTrigger;

	void OnTriggerStay2D(Collider2D col) {
		if (col.gameObject.name == "Player" && !disabled && !locked) {
			if (hasInteractTooltip) {
           		FindObjectOfType<PlayerControl>().setInteractTooltip();
			}

            if (Input.GetButtonDown("Fire1")) {
                FindObjectOfType<PlayerControl>().unsetInteractTooltip();

                if (hasDialogue) {
					locked = true;
					dialogueTrigger = GetComponent<DialogueTrigger>();
					dialogueTrigger.triggerDialogue(unlockAndEnd);	
					if (disabledAfterInteract) {
						disabled = true;
					}
				}


			}
		}
	}

	void OnTriggerExit2D() {
		FindObjectOfType<PlayerControl>().unsetInteractTooltip();
	}

    private void unlockAndEnd()
    {
        if (hasInteractTooltip)
        {
            FindObjectOfType<PlayerControl>().setInteractTooltip();
        }
        locked = false;
        endAction();
    }

    public virtual void endAction()
    {
        // children can override this for additional behavior

    }

	
}
