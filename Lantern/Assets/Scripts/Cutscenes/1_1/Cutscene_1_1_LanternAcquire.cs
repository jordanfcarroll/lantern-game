using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cutscene_1_1_LanternAcquire : Cutscene {

	private bool triggered = false;
	private GameObject parhellion;
	private GameObject fixedFlame;
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
        yield return new WaitForSeconds(1);

		// Freeze player and set camera position
		FindObjectOfType<PlayerControl>().lockPlayer();
	
	}
}
