using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Meta : MonoBehaviour {

	public static GameManager_Meta Instance { get; private set; }
	public bool isNewGame = true;

	private void Awake() {
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else {
			Destroy(gameObject);
		}

	}
}
