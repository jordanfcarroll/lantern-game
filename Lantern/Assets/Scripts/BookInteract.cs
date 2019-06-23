using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookInteract : MonoBehaviour {

	public bool hasDialogue;
	public bool disabledAfterInteract = false;

    private Animator animator;

	private bool disabled = false;
	private bool locked = false;

	private DialogueTrigger dialogueTrigger;

    void Awake () {
        animator = GetComponent<Animator>();
    }

	void OnTriggerStay2D(Collider2D col) {
		if (col.gameObject.name == "Player" && !disabled && !locked ) {
            FindObjectOfType<PlayerControl>().setInteractTooltip();
			if (Input.GetButtonDown("Fire1")) {
                FindObjectOfType<PlayerControl>().unsetInteractTooltip();
                locked = true;

				if (hasDialogue) {
					dialogueTrigger = GetComponent<DialogueTrigger>();
					dialogueTrigger.triggerDialogue(endAction);	
					if (disabledAfterInteract) {
						disabled = true;
					}
				}
                animator.Play("Open");
			}
		}
	}

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "Player") {
            FindObjectOfType<PlayerControl>().unsetInteractTooltip();
        }
    }

	void endAction () {
		locked = false;
        FindObjectOfType<PlayerControl>().setInteractTooltip();
	}
}
