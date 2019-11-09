using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cutscene_1_1_Opening : Cutscene {

	private bool triggered = false;
	public Transform playerMovePoint;
	public GameObject cameraPoint;

	void Awake () {
		// Find all the objects we need references to
		// parhellion = GameObject.FindGameObjectWithTag("Parhellion");
        // fixedFlame = GameObject.FindGameObjectWithTag("Fixed_Flame");
	}

	public override IEnumerator ExecuteCutscene () {
		// Flip triggered flag
		triggered = true;
		// FindObjectOfType<BackgroundUIFader>().start(2f);

		// Freeze player and set camera position
		FindObjectOfType<PlayerControl>().lockPlayer();
		FindObjectOfType<PlayerControl>().disableDefaultLight();
		FindObjectOfType<CameraController>().panToTarget(cameraPoint);

		// Fade in
		// FindObjectOfType<BackgroundUIFader>().endFadeCustom(2f);
		// FindObjectOfType<BackgroundUIFader>().endFadeCustom(2f);
		yield return new WaitForSeconds(3);
		FindObjectOfType<BackgroundUIFader>().startFadeInCustom(2);
		yield return new WaitForSeconds(6);

		// Player enters
		FindObjectOfType<PlayerControl>().forceMoveTo(playerMovePoint,"UP", 1f);
        yield return new WaitForSeconds(3);
        

		// Unlock player
		FindObjectOfType<PlayerControl>().restoreDefaultLight();
		FindObjectOfType<PlayerControl>().unlockPlayer();      

		yield return new WaitForSeconds(3);  
		FindObjectOfType<CameraController>().followPlayer();
	}
}
