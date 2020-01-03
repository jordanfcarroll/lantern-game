using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class BackgroundUIFader : MonoBehaviour {

	private Color color;
	public float opacity;
	public float initialA;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Awake () {
		color = GetComponent<Image>().color;
		color.a = initialA;
		GetComponent<Image>().color = color;
	}

	public void startFade (Action callback) {
        StartCoroutine(FadeOut(callback));
    }

	public void GetDarker (float seconds) {
        StartCoroutine(GetDarkerCor(seconds));
    }

	public void endFade (Action callback) {
        StartCoroutine(FadeIn(callback));
    }

	public void GetBrighter (float seconds) {
		StartCoroutine(GetBrighterCor(seconds));
	}

	IEnumerator FadeIn(Action callback) {
		while(color.a > 0f) {

            color.a -= 0.001f;
            GetComponent<Image>().color = color;
			yield return new WaitForSeconds(0f);;
		}

		callback();
	}
	
	IEnumerator FadeOut(Action callback) {
		while(color.a < 0.2f) {

            color.a += 0.005f;
            GetComponent<Image>().color = color;
			yield return new WaitForSeconds(0f);;
		}

		callback();
	}

	IEnumerator GetBrighterCor(float seconds) {
		while(color.a > 0f) {

			float interval = seconds / 100f;



            color.a -= 0.01f;
            GetComponent<Image>().color = color;
			yield return new WaitForSeconds(interval);
		}

	}

	IEnumerator GetDarkerCor(float seconds) {
		while(color.a < 1f) {

			float interval = seconds / 100f;



            color.a += 0.01f;
            GetComponent<Image>().color = color;
			yield return new WaitForSeconds(interval);
		}

	}
}
