using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoad : MonoBehaviour {
	public static List<GameManager_1> savedGames = new List<GameManager_1>();
	
	private void Awake() {
		// load save
	}

	//public static void Save<T>(T objectToSave, string key) {
	public static void Save() {
		SaveData save = new SaveData();

		// Player
		Transform playerTransform = GameManager_1.Instance.player.transform;
		PlayerData playerData = new PlayerData(playerTransform);
		save.MyPlayerData = playerData;

		// Cutscenes
		for (var i = 0; i < GameManager_1.Instance.cutsceneList.Count; i++) {
			CutsceneData data = new CutsceneData();
			data.id = GameManager_1.Instance.cutsceneList[i].id;
			data.triggered = GameManager_1.Instance.cutsceneList[i].triggered;
			save.cutsceneList.Add(data);
		}

		// Flags
		save.flags = GameManager_1.Instance.generalFlags;


		// Scene
		Scene currentScene = SceneManager.GetActiveScene();
		save.SceneIndex = currentScene.buildIndex;
		save.SceneName = currentScene.name;

		string path = Application.persistentDataPath + "/saves/";
		Directory.CreateDirectory(path);
		BinaryFormatter formatter = new BinaryFormatter();
		using (FileStream fileStream = new FileStream(path + "savegame" + ".txt", FileMode.Create)) {
			formatter.Serialize(fileStream, save);
		}
	}

	// public static T Load<T>(string key) {
	public static void Load() {
		string path = Application.persistentDataPath + "/saves/";
		BinaryFormatter formatter = new BinaryFormatter();
		using (FileStream fileStream = new FileStream(path + "savegame" + ".txt", FileMode.Open)) {
			SaveData returnValue = (SaveData)formatter.Deserialize(fileStream);
			fileStream.Position = 0;
			GameManager_1.Instance.saveGame = returnValue;
		}
	}

	public static int LoadSaveScene() {
		string path = Application.persistentDataPath + "/saves/";
		BinaryFormatter formatter = new BinaryFormatter();
		using (FileStream fileStream = new FileStream(path + "savegame" + ".txt", FileMode.Open)) {
			SaveData save = (SaveData)formatter.Deserialize(fileStream);
			fileStream.Position = 0;
			// Just returning index means that in future builds we can never change the indexes of scenes in the build. Newely created
			// scenes will always have to go at the end.
			return save.SceneIndex;
		}
	}
	
	public static bool SaveExists(string key) {
		string path = Application.persistentDataPath + "/saves/" + key + ".txt";
		return File.Exists(path);
	}

	public static void SeriouslyDeleteAllSaveFiles() {
		string path = Application.persistentDataPath + "/saves/";
		DirectoryInfo directory = new DirectoryInfo(path);
		directory.Delete();
		Directory.CreateDirectory(path);
	}
}
