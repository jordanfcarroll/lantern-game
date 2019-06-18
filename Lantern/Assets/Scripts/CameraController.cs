using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kino;

public class CameraController : MonoBehaviour {

	// flip this for complete manual comtrol ie during cutscene
	public bool overwriteDefaultCameraBehavior = false;

	// Given a reference to player to return to
	public GameObject player;

	// followTarget is able to be manipulated temporarily by other scripts
	public GameObject previousTarget = null;
    public GameObject followTarget;
    private Vector3 targetPos;
    public float moveSpeed;

	public Queue<GameObject> panTargets;
	private GameObject currentTarget;

	// -- SHAKING
	private bool isShaking = false;
	private float baseX, baseY, baseZ;
	private float intensity = 0.1f;
	private int shakes;

	public float defaultZoom = 3f;

	// background glitching (no sound)
	private float bgGlitchIntensity = 0f;
	// currently manually glitching
	private bool isGlitching = false;

	void Awake () {
        GetComponent<Camera>().orthographicSize = defaultZoom;
	}

	// Update is called once per frame
	void Update () {

		// Might need to deactivate glitch component while not glitching

		// override bg glitch with main glitch
		if (!isGlitching) {
    	    FindObjectOfType<AnalogGlitch>().scanLineJitter = bgGlitchIntensity;	
		}

		

		Camera.main.fieldOfView = 20f;

		if (isShaking) { 
			float randomShakeX = Random.Range(-intensity, intensity);
			float randomShakeY = Random.Range(-intensity, intensity);
			float randomShakeZ = Random.Range(-intensity, intensity);

			transform.position = new Vector3(baseX + randomShakeX, baseY + randomShakeY, baseZ);
			shakes--;
			// if (shakes <= 0) {
			// 	isShaking = false;
			// 	transform.position = new Vector3(baseX, baseY, baseZ);
			// }
		}
		else {
			targetPos = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, transform.position.z);
			transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
		}
	}

	public void panToTarget (GameObject target) 
	{
		previousTarget = followTarget;
		followTarget = target;
	}

    public void followPlayer()
    {
		previousTarget = followTarget;
		followTarget = player;
    }

	public void followPreviousTarget () {
		followTarget = previousTarget;
		previousTarget = null;
	}

	public void minorShake () {
		baseX = transform.position.x;
		baseY = transform.position.y;
		baseZ = transform.position.z;
        isShaking = true;
        intensity = 0.02f;
		return;
	}

	public void majorShake () {
        baseX = transform.position.x;
        baseY = transform.position.y;
        baseZ = transform.position.z;
		isShaking = true;
		intensity = 0.05f;
		return;
	}

	public void stopShake () {
		isShaking = false;
	}
	
	public void zoomTo (float size, float time) {
		StartCoroutine(zoom(size, time));
	}

	public void resetZoom (float time) {
		StartCoroutine(zoom(defaultZoom, time));
	}

	IEnumerator zoom (float size, float time) {

        float initialSize = GetComponent<Camera>().orthographicSize;
		for (float t = 0.0f; t <= 1.0f; t += Time.deltaTime / time) {
			float newSize = Mathf.Lerp(initialSize, size, t);
            GetComponent<Camera>().orthographicSize = newSize;
			yield return null;
		}
	}

	public void Glitch (float duration, float intensity) {
		StartCoroutine(GlitchCoroutine(duration, intensity));
	}

	IEnumerator GlitchCoroutine (float duration, float intensity) {
		isGlitching = true;
        FindObjectOfType<AudioManager>().Play("Glitch_Static");
        // FindObjectOfType<AudioManager>().Play("Tuning_Fork");
        FindObjectOfType<AnalogGlitch>().scanLineJitter = intensity;
		yield return new WaitForSeconds(duration);
        FindObjectOfType<AudioManager>().Stop("Glitch_Static");
        // FindObjectOfType<AudioManager>().Stop("Tuning_Fork");
		isGlitching = false;
        FindObjectOfType<AnalogGlitch>().scanLineJitter = bgGlitchIntensity;
    }

	public void backgroundGlitch (float duration, float intensity) {
		StartCoroutine(backgroundGlitchCoroutine(duration, intensity));
	}

    IEnumerator backgroundGlitchCoroutine(float duration, float intensity)
    {
        bgGlitchIntensity = intensity;
        yield return new WaitForSeconds(duration);
        bgGlitchIntensity = 0f;
    }

    public void ColorGlitch(float duration, float intensity)
    {
        StartCoroutine(ColorGlitchCoroutine(duration, intensity));
    }

    IEnumerator ColorGlitchCoroutine(float duration, float intensity)
    {
        FindObjectOfType<AnalogGlitch>().colorDrift = intensity;
        yield return new WaitForSeconds(duration);
        FindObjectOfType<AnalogGlitch>().colorDrift = 0f;
    }
}
