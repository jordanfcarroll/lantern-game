using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cutscene_1_1_Parhelion : Cutscene {

	private bool triggered = false;
	public GameObject parhellion;
	public GameObject fixedFlame;
	public GameObject parhellionSpotlight;
	public GameObject fixedFlameSpotlight;

	public override IEnumerator ExecuteCutscene () {
		// Flip triggered flag
		triggered = true;

		// Freeze player and set camera position
		FindObjectOfType<PlayerControl>().lockPlayer();
        yield return new WaitForSeconds(1);

		FindObjectOfType<CameraController>().panToTarget(parhellion);
        yield return new WaitForSeconds(1);
        parhellionSpotlight.GetComponent<CutsceneSpotlight>().setNewIntensity(6f, 0.25f);
        yield return new WaitForSeconds(3);


        FindObjectOfType<CameraController>().panToTarget(fixedFlame);
        yield return new WaitForSeconds(1);
        fixedFlameSpotlight.GetComponent<CutsceneSpotlight>().setNewIntensity(6f, 0.25f);
        yield return new WaitForSeconds(3);

		// Unlock player
		FindObjectOfType<CameraController>().followPlayer();
		FindObjectOfType<PlayerControl>().unlockPlayer();
	}
}
