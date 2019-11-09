using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTrigger : MonoBehaviour {

    public GameObject HallwayLight;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            HallwayLight.GetComponent<GenericLightControl>().setNewIntensity(1f, 0.20f);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            HallwayLight.GetComponent<GenericLightControl>().setNewIntensity(0f, 0.25f);
        }
    }

}
