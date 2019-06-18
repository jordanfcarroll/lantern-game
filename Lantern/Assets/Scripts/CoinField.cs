using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinField : MonoBehaviour {

	private bool textTriggered = false;
	private bool resolved = false;
	public GameObject resolver;
	private DialogueTrigger dialogueTrigger;


	public GameObject sound1;
	public GameObject sound2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player" && !resolved && FindObjectOfType<GameManager_1>().coins > 0)
        {
            if (!textTriggered) {
				textTriggered = true;
                dialogueTrigger = GetComponent<DialogueTrigger>();
                dialogueTrigger.triggerDialogue(unlockAndEnd);
			}
			sound1.GetComponent<AudioSource>().enabled = true;
			resolver.active = true;
			// sound2.GetComponent<AudioSource>().enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
			sound1.GetComponent<AudioSource>().enabled = false;
            resolver.active = false;
			// sound2.GetComponent<AudioSource>().enabled = false;
        }
    }

	public void resolve() {
		resolved = true;
        sound1.GetComponent<AudioSource>().enabled = false;
        // sound2.GetComponent<AudioSource>().enabled = false;
	}

    private void unlockAndEnd()
    {
		//
    }
}
