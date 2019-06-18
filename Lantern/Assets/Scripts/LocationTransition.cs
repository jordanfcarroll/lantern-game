using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationTransition : MonoBehaviour {

	// private bool active = true;
	public Transform destination;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            FindObjectOfType<PlayerControl>().teleportPlayer(destination);

        }

    }
}
