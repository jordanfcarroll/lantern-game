using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerUnderground : MonoBehaviour {

	private SpriteRenderer blocker;

	// void Awake () {
	// 	blocker = GameObject.FindGameObjectWithTag("Stair_Player_Blocker_Chapel").GetComponent<SpriteRenderer>();
	// }

    // void OnTriggerStay2D(Collider2D col)
    // {
    //     if (col.gameObject.name == "Player")
    //     {
    //         // FindObjectOfType<PlayerControl>().setUnderground();
            
	// 		blocker.enabled = true;
	// 	}

    // }

    // void OnTriggerExit2D(Collider2D col)
    // {
    //     if (col.gameObject.name == "Player")
    //     {
    //         blocker.enabled = false;
    //         // FindObjectOfType<PlayerControl>().resetUnderground();
    //     }

    // }
}
