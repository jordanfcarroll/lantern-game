using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameManager_1 : MonoBehaviour {

	public static GameManager_1 Instance { get; private set; }
	public List<Cutscene> cutsceneList = new List<Cutscene>();
	public PlayerControl player; 
	public CameraController camera; 
	public SaveData saveGame;

    private void Save() {
        SaveLoad.Save();
    }
    
    public void Load() {
        if (SaveLoad.SaveExists("savegame")) {
			// get save file and assign it to save game
            SaveLoad.Load();

			// Assign variables out to objects

			//Player
			PlayerData playerData = saveGame.MyPlayerData;
            player.transform.position = new Vector3(playerData.xPos, playerData.yPos, playerData.zPos);
			// Set camera to player position
			float cameraZ = camera.transform.position.z;
            camera.transform.position = new Vector3(playerData.xPos, playerData.yPos, cameraZ);

			// Flags
			generalFlags = saveGame.flags;

			// Cutscenes
			for (var i = 0; i < saveGame.cutsceneList.Count; i++) {
				Cutscene cutscene = cutsceneList.Find(x => x.id == saveGame.cutsceneList[i].id);
				cutscene.triggered = saveGame.cutsceneList[i].triggered; 
			}
        }
    }

	void LoadSceneSaveData(Scene scene, LoadSceneMode mode) {
	}

    void LateUpdate () {
        // tempRend.sortingOrder = (int)Camera.main.WorldToScreenPoint (tempRend.bounds.min).y * -1;
    }

    void OnDisable()
    {
        GameEvents.SaveInitiated -= Save;
		SceneManager.sceneLoaded -= LoadSceneSaveData;
    }

    void OnEnable()
    {
		SceneManager.sceneLoaded += LoadSceneSaveData;
    }
	// values

	// World States
	public bool skipCutscenes = false;
	public bool talkedToParhellion = false;
	public bool hasLantern = true;

	// save reference here to where player should load into a scene with multiple loading zones


	public Dictionary<string,bool> keyInventory = new Dictionary<string, bool>() {
		{"red_key", false},
		{"atrium_key", false},
		{"black_book", true},
		{"east_key_misc_1", false},
		// null keys do not exist, for locked doors that can never be opened
		{"null_key", false}
	};

	public Dictionary<string,bool> generalFlags  = new Dictionary<string, bool>() {
		{"shouldTriggerDoorUnlock", false},
	};

	public Dictionary<string,bool> westLamps = new Dictionary<string, bool>() {
		{"w_a", false},
		{"w_b", false},
		{"w_c", false},
		{"w_d", false}
	};

	private string correctBuster = "abcd";
	private string busterCode = "";
	public Dictionary<string,bool> busterTiles = new Dictionary<string, bool>() {
		{"a", false},
		{"b", false},
		{"c", false},
		{"d", false}
	};

	public int coins = 0;

	public bool WestBurnInActive = false;
	public bool EastBurnInActive = false;
	public bool CentralBurnInActive = false;

	// West
	public bool bloodRoomVisited = false;

	// East

	// INVENTORY

	public bool redKey = false;
	
	// cutscenes triggered from gamemanager
	public GameObject Cutscene_BurnInWest;
	public GameObject Cutscene_BurnInEast;


	private void Awake() {
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else {
			Destroy(gameObject);
		}

        GameEvents.SaveInitiated += Save;
		if (!GameManager_Meta.Instance.isNewGame) {
        	Load();
		}
	}

	public void lightWestLantern (string id) {
		westLamps[id] = true;
		if (westLamps["w_a"] && westLamps["w_b"] && westLamps["w_c"] && westLamps["w_d"]) {
            Cutscene_BurnInWest.GetComponent<Cutscene_BurnInWest>().RunCutscene();
			GameObject.Find("Sliding_East_Wall").GetComponent<Animator>().Play("Open");
			GameObject.Find("East Leading Light").GetComponentInChildren<Light>().enabled = true;
            WestBurnInActive = true;
		}
	}

	public void activateBuster (string id) {
		busterTiles[id] = true;
        busterCode += id;

		if (busterCode.Length == 4) {
			// if (busterCode == correctBuster) {
				// do cool stuff
				StartCoroutine(triggerBusters());
			// } else {
				// StartCoroutine(resetBusters());
			// }
		}	
	}

	IEnumerator resetBusters () {
        busterCode = "";
		yield return new WaitForSeconds(1f);
		busterTiles["a"] = false;
        busterTiles["b"] = false;
        busterTiles["c"] = false;
        busterTiles["d"] = false;
	}

	IEnumerator triggerBusters () {
        busterCode = "";
		yield return new WaitForSeconds(1f);
        Cutscene_BurnInEast.GetComponent<Cutscene_BurnInEast>().RunCutscene();

		
		
	}
}
