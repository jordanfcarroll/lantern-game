using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WestBurnInExitTrigger : MonoBehaviour {

    private bool triggered = false;


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player" && !triggered && FindObjectOfType<GameManager_1>().WestBurnInActive)
        {
            triggered = true;
			FindObjectOfType<AudioManager>().Play("West_Burn_Exit");
        }
    }
}
