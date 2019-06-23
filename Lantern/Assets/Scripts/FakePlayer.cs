using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class could be generalized for NPCs
public class FakePlayer : MonoBehaviour {


	private Animator animator;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	public void winkOut() {
        GetComponent<SpriteRenderer>().enabled = false;
	}

	public void Appear (Transform location) {
		transform.position = location.position;
		GetComponent<SpriteRenderer>().enabled = true;
	}

	public void moveTo (Transform target, string endDirection, float speed) {
        animator.Play("Walking");

		StartCoroutine(advanceToNextPoint(target.GetComponent<Transform>(), endDirection, speed));
		
	}

	IEnumerator advanceToNextPoint (Transform target, string endDirection, float speed) {
        float step = FindObjectOfType<PlayerControl>().walkSpeed * speed * Time.deltaTime;

		Vector3 vector = target.position - transform.position;
		
		Vector3 normalized = Vector3.Normalize(vector);

		animator.SetFloat("FaceX", normalized.x);
		animator.SetFloat("FaceY", normalized.y);

		while (Vector3.Distance(transform.position, target.position) > step) {
        	transform.position = Vector3.MoveTowards(transform.position, target.position, step);
			yield return null;
		}

		animator.Play("Idle");
		if (endDirection == "UP") {
			faceUp();
		} 
		else if (endDirection == "DOWN") {
            faceDown();
		}
		else if (endDirection == "RIGHT") {
            faceRight();
		}
		else if (endDirection == "LEFT") {
            faceLeft();
		}

    }

	public void faceUp() {
        animator.SetFloat("FaceY", 1);
    }

	public void faceDown() {
        animator.SetFloat("FaceY", -1);
	}

	public void faceRight() {
		animator.SetFloat("FaceX", 1);
	}

	public void faceLeft() {
		animator.SetFloat("FaceX", -1);
	}
}
