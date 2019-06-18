using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cutscene_BurnInEast : MonoBehaviour
{

    private bool triggered = false;
    public GameObject BurnInTileEast;
    public GameObject BurnInTileEastMirror;
    public Light BurnInTileEastLight;
    public Light BurnInTileEastMirrorLight;
    public GameObject East_Burn_Hum;
    public GameObject East_Burn_Hum_Mirror;

    public GameObject DownstairsPassage;

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
        BurnInAnimator = BurnInTileEast.GetComponent<Animator>();
        BurnInMirrorAnimator = BurnInTileEastMirror.GetComponent<Animator>();

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

        FindObjectOfType<CameraController>().panToTarget(BurnInTileEast);
        yield return new WaitForSeconds(1);

        BurnInAnimator.SetBool("IsActive", true);
        BurnInMirrorAnimator.Play("Burn_In");
        East_Burn_Hum.GetComponent<AudioSource>().enabled = true;
        East_Burn_Hum_Mirror.GetComponent<AudioSource>().enabled = true;

        // Open third area
        DownstairsPassage.GetComponent<Animator>().Play("Raise");

        yield return new WaitForSeconds(6f);
 
        FindObjectOfType<PlayerControl>().unlockPlayer();
        FindObjectOfType<CameraController>().resetZoom(1f);
        FindObjectOfType<CameraController>().followPlayer();
    }
}
