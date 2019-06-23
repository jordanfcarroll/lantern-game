using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EastWingMiscKeyTrigger1 : MonoBehaviour {
	private bool triggered = false;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player" && !triggered)
        {
            triggered = true;
            FindObjectOfType<GameManager_1>().keyInventory["east_key_misc_1"] = true;
        }
    }
}
