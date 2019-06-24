﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodRoomTrigger : MonoBehaviour {

	private bool triggered = false;

    public GameObject Cutscene_BloodRoomExit;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player" && FindObjectOfType<GameManager_1>().bloodRoomVisited && !triggered)
        {
			triggered = true;
            Cutscene_BloodRoomExit.GetComponent<Cutscene_BloodRoomExit>().RunCutscene();
        }
    }
}
