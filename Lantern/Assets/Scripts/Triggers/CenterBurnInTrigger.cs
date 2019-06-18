using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterBurnInTrigger : MonoBehaviour
{

    private bool triggered = false;

    public GameObject cutscene;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player" && !triggered)
        {
            triggered = true;
            cutscene.GetComponent<Cutscene_BurnInCenter>().RunCutscene();
        }
    }
}
