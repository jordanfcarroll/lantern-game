using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour {

	public GameObject Cursor;
	public int index;

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
