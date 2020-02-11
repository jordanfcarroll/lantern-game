using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour {

	public GameObject Cursor;
	public int index;
	public Text text;


	private void Start() {
		// Disabled continue button
		if ( index == 1 && !MainMenu.SaveDataExists) {
			Debug.Log(MainMenu.SaveDataExists);
			Color color = text.color;
			color.a = 0.3f;
			text.color = color;
		}
	}

	private void Update() {

		if (FindObjectOfType<MainMenu>().selected == index) {
			displayCursor();
		}
		else {
			hideCursor();
		}
	}

	public void displayCursor() {
		Cursor.SetActive(true);
	}
	public void hideCursor() {
		Cursor.SetActive(false);
	}
}
