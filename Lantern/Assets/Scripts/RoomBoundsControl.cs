using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Bounds for rooms -- allows for camera locking and fading in/out
public class RoomBoundsControl : MonoBehaviour {

    public bool shouldLockCamera;
    public bool shouldFade;
    public GameObject cameraPoint;

    // Localized room sounds
    public TriggerSound[] triggerSounds;

    void Start()
    {
        if (shouldFade) {
            GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1);
        }
    }

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.name == "Player") {

            if (shouldFade) {
                StartCoroutine(FadeTo(0.0f, 1f));
            }

            if (shouldLockCamera)
            {
                FindObjectOfType<CameraController>().panToTarget(cameraPoint);
            }
            
            foreach (TriggerSound sound in triggerSounds) {
                PlaySound(sound.name, sound.duration);
            }

        }

		
    }

    void PlaySound (string name, float duration) {
        FindObjectOfType<AudioManager>().PlayDelayStop(name, duration);
    }

    void StopSound (string sound) {
        FindObjectOfType<AudioManager>().FadeOut(sound);
    }

	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.name == "Player") {

            if (shouldFade)
            {
                StartCoroutine(FadeTo(1f, 1f));
            }

            if (shouldLockCamera && FindObjectOfType<CameraController>().followTarget.GetInstanceID() == cameraPoint.GetInstanceID()) {
                FindObjectOfType<CameraController>().followPlayer();
            }

            // Turn off local sounds on room exit
            foreach (TriggerSound sound in triggerSounds)
            {
                StopSound(sound.name);
            }

		}
	}

    IEnumerator FadeTo(float aValue, float aTime)
    {
        float alpha = GetComponent<SpriteRenderer>().color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(0, 0, 0, Mathf.Lerp(alpha, aValue, t));
            GetComponent<SpriteRenderer>().color = newColor;
            yield return null;
        }
    }
}
