using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour {

	public bool hasDialogue;
	public bool disabledAfterInteract = false;
	public bool hasInteractTooltip = true;
	public bool shines = false;
    public GameObject shine;

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
					if (shines) {
						disableShine();
					}
					locked = true;
					dialogueTrigger = GetComponent<DialogueTrigger>();
					dialogueTrigger.triggerDialogue(unlockAndEnd);	

				}
			}
		}
	}

	void OnTriggerExit2D() {
		FindObjectOfType<PlayerControl>().unsetInteractTooltip();
	}

    private void unlockAndEnd()
    {
		if (disabledAfterInteract) {
			disabled = true;
		}
        if (hasInteractTooltip && !disabled)
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
	public void enableShine () {
        shine.GetComponent<Animator>().Play("shine_Shine");
    }

    public void disableShine () {
        shine.GetComponent<Pickup>().Collect();
    }

}
