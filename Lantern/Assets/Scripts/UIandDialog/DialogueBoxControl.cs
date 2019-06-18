using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBoxControl : MonoBehaviour {


	// Use this for initialization
	void Start () {
		// hide dialogue on init
		gameObject.SetActive(false);
	}

	// Update is called once per frame
	void Update () {
		// Only register input if player is in dialogue
		if (!FindObjectOfType<PlayerControl>().canMove) {
				if (Input.GetButtonDown("Fire1")) {
					FindObjectOfType<DialogueManager>().DisplayNextSentence();
			}
		}	
	}
}
