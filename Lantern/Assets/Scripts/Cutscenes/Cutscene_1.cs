using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cutscene_1 : Cutscene {

	private bool triggered = false;
	private GameObject parhellion;
	private GameObject fixedFlame;

	void Awake () {
		// Find all the objects we need references to
		parhellion = GameObject.FindGameObjectWithTag("Parhellion");
        fixedFlame = GameObject.FindGameObjectWithTag("Fixed_Flame");
	}

	public override IEnumerator RunCutscene () {
		// Flip triggered flag
		triggered = true;

		// Freeze player
		FindObjectOfType<PlayerControl>().lockPlayer();

		

		// Lerp camera to flame, activate light and pause
		FindObjectOfType<CameraController>().panToTarget(fixedFlame);
        yield return new WaitForSeconds(1);
        GameObject.Find("Fixed_Flame_Spotlight").GetComponent<CutsceneSpotlight>().setNewIntensity(35f, 0.5f);
        FindObjectOfType<CameraController>().zoomTo(1f, 3f);

        // FindObjectOfType<AudioManager>().Play("flame_ignite");

		// FindObjectOfType<CameraController>().majorShake();
        yield return new WaitForSeconds(3);

		// Lerp camera to parhellion, activate light and pause
			
		FindObjectOfType<CameraController>().panToTarget(parhellion);
        yield return new WaitForSeconds(1);
        GameObject.Find("Parhellion_Spotlight").GetComponent<CutsceneSpotlight>().setNewIntensity(24f, 0.5f);
		
		yield return new WaitForSeconds(3);
		// Unfreeze player and relock camera
		FindObjectOfType<PlayerControl>().unlockPlayer();
        FindObjectOfType<CameraController>().resetZoom(1f);
		FindObjectOfType<CameraController>().followPlayer();
	}
}
