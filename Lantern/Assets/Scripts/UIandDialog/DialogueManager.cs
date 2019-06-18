using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public delegate void Del();
public delegate void EndDialogueAction();

public class DialogueManager : MonoBehaviour {

	private Action endDialogueCallback;

	public Queue<string> sentences;
	public Queue<string> readings;

	public Text dialogueText;
	public Text readingText;
	public GameObject dialogueBox;
	public GameObject readingBox;
	public GameObject canvasImage;

	private bool textScrolling = false;
	private string currentSentence = "";

    // public delegate void Del();
	public Del m;

	private bool schedulePlayerUpdate = false;
	private bool receiveInput = true;

	// Use this for initialization
	void Start () {
		sentences = new Queue<string>();
		readings = new Queue<string>();

	}

	void LateUpdate()
    {
        // if (schedulePlayerUpdate) {
		// 	FindObjectOfType<PlayerControl>().canMove = !FindObjectOfType<PlayerControl>().canMove;
		// 	schedulePlayerUpdate = false;
		// }
		receiveInput = true;
    }
	
	public void StartDialogue (Dialogue dialogue, Action callback) {
        endDialogueCallback = callback;


		dialogueBox.SetActive(true);
        // canvasImage.GetComponent<Image>().CrossFadeAlpha(0.9f, 3.0f, false);

        // bad but w/e -- use a method?
        // schedulePlayerUpdate = true;
        FindObjectOfType<PlayerControl>().canMove = false;


        sentences.Clear();
        readings.Clear();

		foreach (string sentence in dialogue.sentences) {
			sentences.Enqueue(sentence);
		}

        foreach (string reading in dialogue.readings)
        {
            readings.Enqueue(reading);
        }

		DisplayNextSentence();
	}

	public void StartDialogue (Dialogue dialogue) {
        endDialogueCallback = null;

		dialogueBox.SetActive(true);


        // bad but w/e -- use a method?
        // schedulePlayerUpdate = true;
        FindObjectOfType<PlayerControl>().canMove = false;

		sentences.Clear();

		foreach (string sentence in dialogue.sentences) {
			sentences.Enqueue(sentence);
		}

		foreach (string reading in dialogue.readings) {
			readings.Enqueue(reading);
		}

		DisplayNextSentence();
	}

	IEnumerator startReading() {
		FindObjectOfType<BackgroundUIFader>().startFade();
		yield return new WaitForSeconds(1f);
        FindObjectOfType<ReadBoxControl>().Activate(readings);
	}

	
	public void DisplayNextSentence () {
		// Only allow a single advance for a single keypress
		if (receiveInput) {
			receiveInput = false;
			if (sentences.Count == 0 && !textScrolling) {
				EndDialogue();
				return;
			}

			if (textScrolling) {
				StopAllCoroutines();
				dialogueText.text = currentSentence;
				textScrolling = false;
			} else {
				StopAllCoroutines();
				currentSentence = sentences.Dequeue();
				StartCoroutine(TypeSentence(currentSentence));
			}
		}

	}

	IEnumerator TypeSentence (string sentence) {
		dialogueText.text = "";
		textScrolling = true;

		foreach (char letter in sentence.ToCharArray()) {
			dialogueText.text += letter;
			if (dialogueText.text == sentence) {
				textScrolling = false;
			}

			// At some point we want individual sentences to dictate their read time, among other things
			yield return new WaitForSeconds(0.06f);
		}
	}

	public void EndDialogue () {
		dialogueBox.SetActive(false);

		if (readings.Count > 0) {
			StartCoroutine(startReading());
		}
		else {
			FindObjectOfType<PlayerControl>().canMove = true;
			if (endDialogueCallback != null) {
				endDialogueCallback();
				// endDialogueCallback = null;
			}
		}
		// bad but w/e -- use a method?
		// schedulePlayerUpdate = true;
	}
	
}
