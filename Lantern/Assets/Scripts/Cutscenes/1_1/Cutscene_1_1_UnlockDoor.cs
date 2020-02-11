using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cutscene_1_1_UnlockDoor : Cutscene {

	public GameObject door;

	public override IEnumerator ExecuteCutscene () {
		if (triggered) {
			yield break;
		}
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
