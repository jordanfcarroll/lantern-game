using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwayFlicker : GenericLightControl {

	// Use this for initialization
	void Start () {
		StartCoroutine(Flicker());		
	}

	IEnumerator Flicker () {
		while (true) {
			setNewIntensity(4.0f, 0.02f);
			yield return new WaitForSeconds(3f);
			setNewIntensity(0f, 0.2f);
			yield return new WaitForSeconds(1f);
		}

	}


}
