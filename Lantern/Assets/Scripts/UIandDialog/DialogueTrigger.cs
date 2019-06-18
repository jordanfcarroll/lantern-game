using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	public Dialogue dialogue;

	public void triggerDialogue () {
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
	}

	public void triggerDialogue (Action callback) {
		FindObjectOfType<DialogueManager>().StartDialogue(dialogue, callback);
	}
}
