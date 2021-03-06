﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour {

	public string sound;
	private bool triggered = false;

    public float duration;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player" && !triggered)
        {
            triggered = true;
            FindObjectOfType<AudioManager>().Play(sound);

            StartCoroutine(TimeOutAudio());
        }
    }

    IEnumerator TimeOutAudio () {
        yield return new WaitForSeconds(duration);
        FindObjectOfType<AudioManager>().Stop(sound);
    }
}
