using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashControl : MonoBehaviour {

    public float moveSpeed;
    private Vector3 targetPos;
    private bool active = false;

	void Update () {
        if (Input.GetButtonDown("Fire2"))
        {
            active = true;
        }

        if (active) {
            targetPos = new Vector3(transform.position.x, transform.position.y, -1);
            transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
        }
    }
}
    