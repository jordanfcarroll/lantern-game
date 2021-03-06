﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene_Open_Final : MonoBehaviour {

    public GameObject spotlight;
    public Transform finalOpenPlayerPoint;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RunCutscene()
    {
        StartCoroutine(ExecuteCutscene());
    }    

    IEnumerator ExecuteCutscene()
    {
        FindObjectOfType<CameraController>().zoomTo(2.8f, 2f);


        // Freeze player
        FindObjectOfType<PlayerControl>().lockPlayer();
        FindObjectOfType<PlayerControl>().forceMoveTo(finalOpenPlayerPoint, "UP", 0.7f);
		yield return new WaitForSeconds(2f);

        FindObjectOfType<CameraController>().panToTarget(spotlight);

		yield return new WaitForSeconds(1f);
        GameObject.Find("Final_Open_Spotlight").GetComponent<CutsceneSpotlight>().setNewIntensity(6f, 0.25f);
		yield return new WaitForSeconds(1f);

        FindObjectOfType<CameraController>().majorShake();
        FindObjectOfType<AudioManager>().Play("Passage_Open");
		yield return new WaitForSeconds(2f);
        GameObject.Find("Sliding_North_Wall").GetComponent<Animator>().Play("Open North Wall");
        GameObject.Find("Sliding_North_Wall_2").GetComponent<Animator>().Play("Open North Wall 2");

		yield return new WaitForSeconds(13f);

        // reset everything
        FindObjectOfType<AudioManager>().Stop("Passage_Open");
        FindObjectOfType<CameraController>().stopShake();
        FindObjectOfType<PlayerControl>().unlockPlayer();
        FindObjectOfType<CameraController>().resetZoom(1f);
        FindObjectOfType<CameraController>().followPlayer();

        // enable music
        FindObjectOfType<AudioManager>().Play("Creepy_Ambience");

        // turn off light
        GameObject.Find("Final_Open_Spotlight").GetComponent<CutsceneSpotlight>().setNewIntensity(0f, 0.5f);


	}
}
