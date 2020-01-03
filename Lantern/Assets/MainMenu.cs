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

	private Color FaderColor;
	public GameObject Fader;

	void Awake () {
		FaderColor = Fader.GetComponent<Image>().color;
		Fader.GetComponent<Image>().color = FaderColor;
	}

	private void Update() {
		if (Input.GetButtonDown("Fire3")) {
			if (selected == 0) {
				StartCoroutine(FadeOutAndStart());
			}
			else if (selected == 1) {
				this.QuitGame();
			}
		}

		float direction = Input.GetAxisRaw("Vertical2");
		if (direction == lastDirection) {
			return;
		}
		lastDirection = direction;

		if (direction == 1) {
			if (selected == ButtonCount -1) {
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

	public void ContinueGame () {
		SceneManager.LoadScene(LastScene);
	}

	public void QuitGame () {
		Debug.Log("Quit!");
		Application.Quit();
	}

	public void activateButton(int button) {
		selected = button;
	}


	IEnumerator FadeOutAndStart () {
		Debug.Log("Play!");
		while(FaderColor.a < 1f) {

			Debug.Log(FaderColor);
            FaderColor.a += 0.005f;
            Fader.GetComponent<Image>().color = FaderColor;
			yield return new WaitForSeconds(0f);
		}
		// SceneManager.LoadScene(0);
	}

}
