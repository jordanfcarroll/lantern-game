using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour {

	public string sound;

	void OnTriggerEnter2D(Collider2D col) {
    	if (col.gameObject.name == "Player") {
			FindObjectOfType<AudioManager>().Play(sound);
		}
    }

}
