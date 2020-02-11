using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Cutscene_1_1_SceneEnd : Cutscene {
	public override IEnumerator ExecuteCutscene () {
		if (triggered) {
			yield break;
		}
		// Flip triggered flag
		triggered = true;

		// Freeze player and open door
		FindObjectOfType<PlayerControl>().lockPlayer();


		FindObjectOfType<BackgroundUIFader>().GetDarker(3);
		yield return new WaitForSeconds(5);
        
		SceneManager.LoadScene(1);


	}
}
