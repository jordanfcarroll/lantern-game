using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cutscene_1_1_LanternAcquire : Cutscene {

	private GameObject parhellion;
	private GameObject fixedFlame;
	public Transform playerMovePoint;
	public GameObject cameraPoint;


	public override IEnumerator ExecuteCutscene () {
		if (triggered) {
			yield break;
		}
		// Flip triggered flag
		triggered = true;
        yield return new WaitForSeconds(1);

		// Freeze player and set camera position
		FindObjectOfType<PlayerControl>().lockPlayer();
	
	}
}
