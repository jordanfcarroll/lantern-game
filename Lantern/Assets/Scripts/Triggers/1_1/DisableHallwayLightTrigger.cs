using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableHallwayLightTrigger : MonoBehaviour {

	private bool triggered = false;
    public GameObject HallwayLight;


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player" && !triggered)
        {
            triggered = true;
            FindObjectOfType<AudioManager>().Play("Bell_1");
            HallwayLight.SetActive(false);
        }
    }

}
