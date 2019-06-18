using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneSpotlight : MonoBehaviour {

	public float intensity;
	public float changeSpeed;
	// private Light light;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (GetComponent<Light>().intensity < intensity) {
			GetComponent<Light>().intensity += changeSpeed;
		} else if (GetComponent<Light>().intensity > intensity) {
			GetComponent<Light>().intensity -= changeSpeed;
		}
	}

	public void setNewIntensity (float newIntensity, float newChangeSpeed) {
		intensity = newIntensity;
		changeSpeed = newChangeSpeed;
	}






}
