using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cutscene_1_1_SceneEnd : Cutscene {

	private bool triggered = false;
	public GameObject door;

	public override IEnumerator ExecuteCutscene () {
		// Flip triggered flag
		triggered = true;

		// Freeze player and open door
		FindObjectOfType<PlayerControl>().lockPlayer();
        door.GetComponent<Door>().isOpen = true;
        door.GetComponent<Door>().isLocked = false;

		// Play door open sound

		yield return new WaitForSeconds(1);
		FindObjectOfType<AudioManager>().Play("Door_Open");
		yield return new WaitForSeconds(2);
		FindObjectOfType<PlayerControl>().unlockPlayer();


	}
}
