using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	private int LastScene = 1;
	public int selected = 0;
	public int ButtonCount;

	private float lastDirection;
	private float inputTimer = 0f;
	private float INPUT_DELAY = 0.3f;
	public static bool SaveDataExists = false;

	private Color FaderColor;
	public GameObject Fader;

	void Awake () {
		// Check for Save File
		if (SaveLoad.SaveExists("savegame")) {
			SaveDataExists = true;
		}
		Destroy(GameManager_1.Instance);



		FaderColor = Fader.GetComponent<Image>().color;
		Fader.GetComponent<Image>().color = FaderColor;
	}

	private void Update() {
		if (Input.GetButtonDown("Fire3")) {
			if (selected == 0) {
				this.PlayGame();
			}
			else if (selected == 1 && SaveDataExists) {
				this.ContinueGame();
			} else {
				this.QuitGame();
			}
		}

		float direction = Input.GetAxisRaw("Vertical2");
		if (direction == lastDirection) {
			return;
		}
		// StartCoroutine(TimeInput());
		lastDirection = direction;

		if (direction == 1) {
			if (selected == ButtonCount - 1) {
				selected = 0;
			}
			else {
				selected++;
			}
		}
		else if (direction == -1) {
			if (selected == 0) {
				selected = ButtonCount - 1;
			}
			else {
				selected--;
			}
		}
	}

	public void PlayGame () {
		StartCoroutine(FadeOutAndStart());
	}

	public void ContinueGame () {
		StartCoroutine(FadeOutAndContinue());
	}

	public void QuitGame () {
		Debug.Log("Quit!");
		Application.Quit();
	}

	public void activateButton(int button) {
		Debug.Log(button);
		selected = button;
	}

	IEnumerator TimeInput() {
		while (inputTimer < INPUT_DELAY) {
			inputTimer += 0.01f;
			yield return new WaitForSeconds(0.01f);
		}
		inputTimer = 0f;
		lastDirection = 0;
	}

	IEnumerator FadeOutAndStart () {
		Fader.SetActive(true);
		while(FaderColor.a < 1f) {

            FaderColor.a += 0.005f;
            Fader.GetComponent<Image>().color = FaderColor;
			yield return new WaitForSeconds(0f);
		}
		GameManager_Meta.Instance.isNewGame = true;
		SceneManager.LoadScene(1);
	}
	IEnumerator FadeOutAndContinue () {
		Debug.Log("Continue!");
		Fader.SetActive(true);
		while(FaderColor.a < 1f) {

            FaderColor.a += 0.005f;
            Fader.GetComponent<Image>().color = FaderColor;
			yield return new WaitForSeconds(0f);
		}
		GameManager_Meta.Instance.isNewGame = false;
		int sceneIndex = SaveLoad.LoadSaveScene();
		SceneManager.LoadScene(sceneIndex);
	}

}
