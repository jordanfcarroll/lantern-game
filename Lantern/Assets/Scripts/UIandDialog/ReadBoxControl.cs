using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class ReadBoxControl : MonoBehaviour {

	private bool isActive = false;
	public Text text;
	public Color color;

    public Queue<string> readings;

    // Use this for initialization
    void Start()
    {
        // hide dialogue on init
        // gameObject.SetActive(false);
        // readings = new Queue<string>();
    }

    void Update()
    {
        if (isActive && Input.GetButtonDown("Fire1"))
        {
			StartCoroutine(Advance());
			isActive = false;
        }
        // hide dialogue on init
        // gameObject.SetActive(false);
    }
	
	public void Activate (Queue<string> input) {
        // fade text in
		readings = input;
		StartCoroutine(FadeIn());
	}

	public void DisplayNextReading() {
		if (readings.Count > 0) {
			StartCoroutine(FadeIn());
		} else {
			// end readings
            FindObjectOfType<BackgroundUIFader>().endFade(endAction);
		}
	}

	IEnumerator FadeIn () {

        string reading = readings.Dequeue();
        text.text = reading;

		Debug.Log(text.text);

		color = text.GetComponent<Text>().color;
        while (color.a < 1f)
        {
            color.a += 0.02f;
			Debug.Log(color.a);
            text.GetComponent<Text>().color = color;
            yield return null;
        }
		isActive = true;
	}

	IEnumerator Advance () {
		color = text.GetComponent<Text>().color;
		while (color.a > 0f) {
			color.a -= 0.02f;
            text.GetComponent<Text>().color = color;
			yield return null;
		}
        DisplayNextReading();
	}

	void endAction () {
		FindObjectOfType<DialogueManager>().EndDialogue();
	}
}
