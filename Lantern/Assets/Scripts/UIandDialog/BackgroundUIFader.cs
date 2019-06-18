using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class BackgroundUIFader : MonoBehaviour {

	private Color color;
	public float opacity;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Awake () {
		color = GetComponent<Image>().color;
	}

	public void startFade () {
        StartCoroutine(FadeIn());
    }

	public void endFade (Action callback) {
        StartCoroutine(FadeOut(callback));
    }

	IEnumerator FadeIn() {
		while(color.a < opacity) {

            color.a += 0.01f;
            GetComponent<Image>().color = color;
			yield return null;
		}
	}
	
	IEnumerator FadeOut(Action callback) {
		while(color.a > 0f) {

            color.a -= 0.01f;
            GetComponent<Image>().color = color;
			yield return null;
		}

		callback();
	}
}
