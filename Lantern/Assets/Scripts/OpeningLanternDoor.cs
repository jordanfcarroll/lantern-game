using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningLanternDoor : Door {
    public GameObject hallwayLight;

    public override void endAction () {
        hallwayLight.SetActive(true);
	}

}
