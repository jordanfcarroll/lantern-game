using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour {

	public string ascendsTo;

    // between 0 & 1.0
	public float grade;

	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.name == "Player") {
			FindObjectOfType<PlayerControl>().setAscendDescend(ascendsTo, grade);
		}
       
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            FindObjectOfType<PlayerControl>().resetAscendDescend();
        }

    }
	
}
