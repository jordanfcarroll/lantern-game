using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

	private bool collected = false;
	private bool revealed = false;
	private bool shining = false;

	private int revealTimer = 0;

    private Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
 
		void Update () {

		// if distance to player is very low, shine regardless?


	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player" && !collected) {
			animator.Play("shine_Shine");
		}
        
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "Player" && !collected) {

			animator.Play("shine_Disabled");
		}
        
    }

	public void Reveal() {
        revealed = true;
	}

	public void Collect() {
		collected = true;
	}

}
