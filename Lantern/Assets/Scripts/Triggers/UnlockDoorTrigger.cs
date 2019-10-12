using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDoorTrigger : MonoBehaviour {

    private bool triggered = false;
    public GameObject cutscene;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player" && FindObjectOfType<GameManager_1>().shouldTriggerDoorUnlock && !triggered)
        {
            cutscene.GetComponent<Cutscene_1_1_UnlockDoor>().RunCutscene();
        }
    }

}
