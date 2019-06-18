using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cutscene_BurnInCenter : MonoBehaviour
{

    private bool triggered = false;
    public GameObject BurnInTileCenter;
    public GameObject BurnInTileCenterMirror;
    public Light BurnInTileCenterLight;
    public Light BurnInTileCenterMirrorLight;
    public GameObject Center_Burn_Hum;
    public GameObject Center_Burn_Hum_Mirror;

    private Animator BurnInAnimator;
    private Animator BurnInMirrorAnimator;


    // void OnTriggerEnter2D(Collider2D col)
    // {
    //     if (col.gameObject.name == "Player" && !triggered)
    //     {
    //         StartCoroutine(RunCutscene());
    //     }
    // }

    void Awake()
    {
        // Find all the objects we need references to
        BurnInAnimator = BurnInTileCenter.GetComponent<Animator>();
        BurnInMirrorAnimator = BurnInTileCenterMirror.GetComponent<Animator>();

    }

    public void RunCutscene()
    {
        StartCoroutine(ExecuteCutscene());
    }

    IEnumerator ExecuteCutscene()
    {
        // Flip triggered flag
        triggered = true;

        // Freeze player
        FindObjectOfType<PlayerControl>().lockPlayer();

        FindObjectOfType<CameraController>().panToTarget(BurnInTileCenter);
        yield return new WaitForSeconds(1);

        BurnInAnimator.SetBool("IsActive", true);
        BurnInMirrorAnimator.Play("Burn_In");
        GameObject.Find("Center_Burn_Hum").GetComponent<AudioSource>().enabled = true;
        GameObject.Find("Center_Burn_Hum_Mirror").GetComponent<AudioSource>().enabled = true;
        // BurnInTileCenterLight.intensity = 2f;
        yield return new WaitForSeconds(6f);
        // BurnInTileCenterLight.intensity = 2.5f;
        // yield return new WaitForSeconds(1f);
        // BurnInTileCenterLight.intensity = 3f;
        // yield return new WaitForSeconds(1f);
        // BurnInTileCenterLight.intensity = 3.5f;

        // Unfreeze player and relock camera
        // yield return new WaitForSeconds(1f);
        FindObjectOfType<PlayerControl>().unlockPlayer();
        FindObjectOfType<CameraController>().resetZoom(1f);
        FindObjectOfType<CameraController>().followPlayer();
    }
}
