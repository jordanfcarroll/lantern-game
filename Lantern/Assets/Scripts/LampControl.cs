using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampControl : MonoBehaviour {

    public bool glows = false;

    private string STANDARD_IDLE_ANIMATION_NAME = "Flame Idle";
    private string GLOW_IDLE_ANIMATION_NAME = "Flame Glow Idle";

    private string STANDARD_IGNITE_ANIMATION_NAME = "Ignite";
    private string GLOW_IGNITE_ANIMATION_NAME = "Glow Ignite";

    private string IgniteAnimationName;
    private string IdleAnimationName;

	private bool isLit = false;

    // Don't allow stacked interacts
    private bool locked = false;

	public string lanternId;

    private float LampLightMaxIntensity = 1f;

    private Animator animator;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        if (glows) {
            IgniteAnimationName = GLOW_IGNITE_ANIMATION_NAME;
            IdleAnimationName =  GLOW_IDLE_ANIMATION_NAME;
        } else {
            IgniteAnimationName = STANDARD_IGNITE_ANIMATION_NAME;
            IdleAnimationName = STANDARD_IDLE_ANIMATION_NAME;
        }
	}

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.name == "Player" && !isLit && !locked)
        {
            FindObjectOfType<PlayerControl>().setInteractTooltip();
            if (Input.GetButtonDown("Fire1"))
            {
                FindObjectOfType<PlayerControl>().unsetInteractTooltip();
                locked = true;
				StartCoroutine(LightingTransition());
            }
        }
    }

	

	IEnumerator LightingTransition () {
        locked = true;
        FindObjectOfType<PlayerControl>().lockPlayer();
        FindObjectOfType<PlayerControl>().setInteract();
		yield return new WaitForSeconds(1f);
		animator.Play(IgniteAnimationName);
        FindObjectOfType<AudioManager>().Play("Flame_Ignite");
		GetComponentInChildren<Light>().enabled = true;
        StartCoroutine(IncreaseLightIntensity());
		yield return new WaitForSeconds(1.2f);

		animator.Play(IdleAnimationName);
		isLit = true;

        if (lanternId != "") {
            FindObjectOfType<GameManager_1>().lightWestLantern(lanternId);
        }
        FindObjectOfType<PlayerControl>().unlockPlayer();
        FindObjectOfType<PlayerControl>().setFree();
        StopCoroutine(IncreaseLightIntensity());
        locked = false;
        
	}

    IEnumerator IncreaseLightIntensity () {
        while (GetComponentInChildren<Light>().intensity <= LampLightMaxIntensity) {
            Debug.Log(GetComponentInChildren<Light>().intensity);
            GetComponentInChildren<Light>().intensity += 0.2f;
            yield return null;
        }
    }
}
