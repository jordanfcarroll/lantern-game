using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTest : MonoBehaviour {

	private DialogueTrigger dialogueTrigger;
	// public UnityScript trigger;



	void OnTriggerEnter2D(Collider2D col) {
    	if (col.gameObject.name == "Player") {
			// trigger.execture();
			// dialogueTrigger = GetComponent<DialogueTrigger>();
			// dialogueTrigger.triggerDialogue();
		}
    }
}
