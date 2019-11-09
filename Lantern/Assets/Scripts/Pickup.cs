using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

	private bool collected = false;
	private bool revealed = false;

	private int revealTimer = 0;

    private Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
 
		void Update () {

		// if distance to player is very low, shine regardless?


		if (revealed && !collected) {
            animator.Play("shine_Shine");
		} else {
         	animator.Play("shine_Disabled");
		}
		
		if (revealTimer <= 0) {
			revealed = false;
		} else {
			revealTimer--;
		}
	}

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.name == "Player" && !collected) {
			Reveal();
		}
        
    }

	public void Reveal() {
        revealed = true;
        revealTimer = 1;
	}

	public void Collect() {
		collected = true;
	}

}
