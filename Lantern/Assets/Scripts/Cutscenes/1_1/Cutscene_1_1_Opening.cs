using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cutscene_1_1_Opening : Cutscene {

	public Transform playerMovePoint;
	public GameObject cameraPoint;
	public GameObject collider;


	public Dialogue dialogue;

	public override IEnumerator ExecuteCutscene () {
		if (triggered) {
			yield return new WaitForSeconds(2);
			FindObjectOfType<BackgroundUIFader>().zeroOut();
			yield break;
		}	
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
		FindObjectOfType<BackgroundUIFader>().GetBrighter(2);
		yield return new WaitForSeconds(1);

		// Player enters
		FindObjectOfType<PlayerControl>().forceMoveTo(playerMovePoint,"UP", 1f);
        yield return new WaitForSeconds(3);
        

		// Unlock player
		FindObjectOfType<PlayerControl>().restoreDefaultLight();
		FindObjectOfType<PlayerControl>().unlockPlayer(); 
		collider.active = true;

		yield return new WaitForSeconds(3);  
		FindObjectOfType<CameraController>().followPlayer();
		
		// FindObjectOfType<DialogueManager>().StartDialogue(dialogue, null, false);

	}
}
