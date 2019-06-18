using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class AtriumCameraTrigger : MonoBehaviour {

	public GameObject atriumCenter;

	void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player" ) {
			FindObjectOfType<CameraController>().panToTarget(atriumCenter);
			FindObjectOfType<CameraController>().zoomTo(5f, 1f);
        }
    }

	void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
			FindObjectOfType<CameraController>().followPlayer();
            FindObjectOfType<CameraController>().resetZoom(1f);
        }
    }
}
