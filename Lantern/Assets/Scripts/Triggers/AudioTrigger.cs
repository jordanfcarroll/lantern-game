using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour {

	public string sound;
	private bool triggered = false;

    public float duration;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player" && !triggered)
        {
            triggered = true;
            FindObjectOfType<AudioManager>().Play(sound);

            StartCoroutine(TimeOutAudio());
        }
    }

    IEnumerator TimeOutAudio () {
        yield return new WaitForSeconds(duration);
        FindObjectOfType<AudioManager>().Stop(sound);
    }
}
