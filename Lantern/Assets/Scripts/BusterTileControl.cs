using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusterTileControl : MonoBehaviour {

	private Animator animator;
	public float lightIntensity = 0.25f;

    public string tileId;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}

	void Awake () {
        GetComponentInChildren<Light>().intensity = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (!FindObjectOfType<GameManager_1>().busterTiles[tileId]) {
			Deactive();
		} else {
			Active();
		}
	}

    void OnTriggerStay2D (Collider2D col)
    {
        if (col.gameObject.name == "Player" && !FindObjectOfType<GameManager_1>().busterTiles[tileId])
        {
		   FindObjectOfType<GameManager_1>().activateBuster(tileId);
		//    GetComponentInChildren<Light>().intensity = lightIntensity;
		//    animator.Play("Active");
        }
    }

	public void Active () {
        animator.Play("Active");
		GetComponentInChildren<Light>().intensity = lightIntensity;
	}

	public void Deactive () {
        animator.Play("Deactive");
		GetComponentInChildren<Light>().intensity = 0;
	}
}
