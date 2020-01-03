using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuGame : MonoBehaviour {

	public static bool GameIsPaused = false;
	public GameObject PauseMenuUi;
	void Update () {
		if (Input.GetButtonDown("Cancel")) {
			if (GameIsPaused) {
				Resume();
			} else {
				Pause();
			}
		}
	}

	void Resume() {
		PauseMenuUi.SetActive(false);
		Time.timeScale = 1f;
		GameIsPaused = false;
	}

	void Pause() {
		PauseMenuUi.SetActive(true);
		Time.timeScale = 0f;
		GameIsPaused = true;
	}
}
