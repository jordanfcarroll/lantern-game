using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[Serializable]
public class SaveData {
	public PlayerData MyPlayerData { get; set; }
	public List<CutsceneData> cutsceneList = new List<CutsceneData>();
	public Dictionary<string, bool> flags = new Dictionary<string, bool>();
	public Dictionary<string, bool> items = new Dictionary<string, bool>();
	public int SceneIndex;
	public string SceneName;

	public SaveData() {

	}
}

[Serializable]
public class CutsceneData {
	public bool triggered;
	public string id;
}

[Serializable]
public class PlayerData {
	public float xPos { get; set; }
	public float yPos { get; set; }
	public float zPos { get; set; }

	public PlayerData(Transform transform) {
		this.xPos = transform.position.x;
		this.yPos = transform.position.y;
		this.zPos = transform.position.z;

	}

}