using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cutscene_BurnInWest : MonoBehaviour
{

    private bool triggered = false;
    private GameObject parhellion;
    private GameObject BurnTileWest;
    private GameObject BurnTileWestMirror;


    private Light BurnInTileWestLight;
    private Light BurnInTileWestMirrorLight;

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
        BurnTileWest = GameObject.FindGameObjectWithTag("BurnInTileWest");
        BurnInAnimator = BurnTileWest.GetComponent<Animator>();
        BurnInTileWestLight = BurnTileWest.GetComponentInChildren<Light>();


        BurnTileWestMirror = GameObject.FindGameObjectWithTag("BurnInTileWestMirror");
        BurnInMirrorAnimator = BurnTileWestMirror.GetComponent<Animator>();
        BurnInTileWestMirrorLight = BurnTileWestMirror.GetComponentInChildren<Light>();

    }

    public void RunCutscene () {
        StartCoroutine(ExecuteCutscene());
    }

    IEnumerator ExecuteCutscene()
    {
        BurnTileWest = GameObject.FindGameObjectWithTag("BurnInTileWest");
        BurnInAnimator = BurnTileWest.GetComponent<Animator>();
        // Flip triggered flag
        triggered = true;

        // Freeze player
        FindObjectOfType<PlayerControl>().lockPlayer();

        FindObjectOfType<CameraController>().panToTarget(BurnTileWest);
        yield return new WaitForSeconds(1);

        BurnInAnimator.SetBool("IsActive", true);
        BurnInMirrorAnimator.Play("Burn_In");
        GameObject.Find("West_Burn_Hum").GetComponent<AudioSource>().enabled = true;
        GameObject.Find("West_Burn_Hum_Mirror").GetComponent<AudioSource>().enabled = true;
        // BurnInTileWestLight.intensity = 2f;
        yield return new WaitForSeconds(6f);
        // BurnInTileWestLight.intensity = 2.5f;
        // yield return new WaitForSeconds(1f);
        // BurnInTileWestLight.intensity = 3f;
        // yield return new WaitForSeconds(1f);
        // BurnInTileWestLight.intensity = 3.5f;

        // Unfreeze player and relock camera
        // yield return new WaitForSeconds(1f);

        FindObjectOfType<PlayerControl>().unlockPlayer();
        FindObjectOfType<CameraController>().resetZoom(1f);
        FindObjectOfType<CameraController>().followPlayer();
    }
}
