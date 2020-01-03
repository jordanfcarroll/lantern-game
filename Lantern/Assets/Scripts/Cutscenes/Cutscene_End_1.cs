using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene_End_1 : Cutscene {

    private bool triggered = false;
    public Transform FakePlayerAppear;
    public Transform FakePlayerMove;
    public GameObject CameraTarget;

    public Transform PlayerMove;


    void Awake () {
	}

    public void RunCutscene()
    {
        StartCoroutine(ExecuteCutscene());
    }


    IEnumerator ExecuteCutscene () {
		triggered = true;
		FindObjectOfType<CameraController>().backgroundGlitch(10f, 0.1f);

        // Increase glitching as we approach tree room?
        FindObjectOfType<PlayerControl>().lockPlayer();
        FindObjectOfType<PlayerControl>().forceMoveTo(PlayerMove, "UP", 0.5f);

		yield return new WaitForSeconds(2f);
        FindObjectOfType<CameraController>().panToTarget(CameraTarget);
		FindObjectOfType<CameraController>().Glitch(0.1f, 0.3f);

		yield return new WaitForSeconds(3f);
		FindObjectOfType<CameraController>().Glitch(0.05f, 0.7f);
        FindObjectOfType<CameraController>().Glitch(0.05f, 0.7f);
        FindObjectOfType<FakePlayer>().Appear(FakePlayerAppear);

		yield return new WaitForSeconds(2f);
		FindObjectOfType<CameraController>().ColorGlitch(0.5f, 1f);
        FindObjectOfType<FakePlayer>().moveTo(FakePlayerMove, "DOWN", 0.3f);

		yield return new WaitForSeconds(4f);
		FindObjectOfType<CameraController>().Glitch(0.4f, 1.5f);
		FindObjectOfType<CameraController>().ColorGlitch(0.4f, 1.5f);


		// SceneManager.LoadScene("Scene_3");
	}

}
