using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenVisitTrigger : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            FindObjectOfType<GameManager_1>().generalFlags[ "shouldTriggerDoorUnlock" ] = true;
        }
    }

}
