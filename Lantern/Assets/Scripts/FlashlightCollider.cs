using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name);
        // if (col.gameObject.name == "Spotlight2dCollider")
        // {
        //     Debug.Log("Shine");
        //     animator.Play("shine_Shine");
        // }
    }
}
