using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedFlameInteract : MonoBehaviour
{
	// private Action callback;

    public bool hasDialogue;
    public bool disabledAfterInteract = true;
	// public GameObject Cutscene_OpenAltar;

    private bool disabled = false;

    private bool shining = true;

    // child object -- shiny interact this boi
    public GameObject shine;


    private DialogueTrigger dialogueTrigger;

	void Awake () {
		
	}

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.name == "Player" && !disabled)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (hasDialogue)
                {
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

	void endAction () {
        // Cutscene_OpenAltar.GetComponent<Cutscene_OpenAltar>().RunCutscene();
        // FindObjectOfType<PlayerControl>().enableLantern();
    }

   
}