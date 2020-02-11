using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSceneStart1_1 : MonoBehaviour {


	public GameObject Opening_Cutscene;
	// Use this for initialization
	void Start () {
		Opening_Cutscene.GetComponent<Cutscene_1_1_Opening>().RunCutscene();
	}
	
}
