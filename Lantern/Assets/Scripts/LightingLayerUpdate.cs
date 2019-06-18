using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LightingLayerUpdate : MonoBehaviour {

	// Offset from transform to collider we're interested in
	public float yOffset;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        Transform playerTransform = FindObjectOfType<PlayerControl>().getTransform();
        if (transform.position.y + yOffset < playerTransform.position.y)
        {
            gameObject.layer = 12;
        }
        else
        {
            gameObject.layer = 0;
        }
    }
}
