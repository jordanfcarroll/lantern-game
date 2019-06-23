using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalOpenTrigger : MonoBehaviour {

	private bool triggered = false;
    public GameObject Cutscene_OpenFinal;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player" 
			&& !triggered 
			&& FindObjectOfType<GameManager_1>().WestBurnInActive
			&& FindObjectOfType<GameManager_1>().EastBurnInActive
			&& FindObjectOfType<GameManager_1>().CentralBurnInActive
		)
        {
            triggered = true;
            Cutscene_OpenFinal.GetComponent<Cutscene_Open_Final>().RunCutscene();
        }
    }
}
