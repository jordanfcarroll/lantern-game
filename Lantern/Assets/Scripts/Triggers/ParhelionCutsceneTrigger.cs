using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParhelionCutsceneTrigger : MonoBehaviour {

    private bool triggered = false;
    public GameObject cutscene;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player" && !triggered)
        {
            cutscene.GetComponent<Cutscene_1_1_Parhelion>().RunCutscene();
            
        }
    }

}
