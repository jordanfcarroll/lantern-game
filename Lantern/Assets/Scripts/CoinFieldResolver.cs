using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinFieldResolver : MonoBehaviour {

	private bool triggered = false;
    private bool active = true;
	public GameObject CoinfieldParent;
    private DialogueTrigger dialogueTrigger;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player" && !triggered && active)
        {
            CoinfieldParent.GetComponent<CoinField>().resolve();
            triggered = true;
            FindObjectOfType<AudioManager>().Play("Bell_1");
            if (FindObjectOfType<GameManager_1>().coins > 0) {
                StartCoroutine(triggerShatter());
                FindObjectOfType<GameManager_1>().coins--;
            }
        }
    }

    private void unlockAndEnd()
    {
        //
    }

	IEnumerator triggerShatter () {
		yield return new WaitForSeconds(1f);
		dialogueTrigger = GetComponent<DialogueTrigger>();
        dialogueTrigger.triggerDialogue(unlockAndEnd);
	}
}
