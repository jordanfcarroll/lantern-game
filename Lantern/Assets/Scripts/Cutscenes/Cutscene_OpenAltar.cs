using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cutscene_OpenAltar : Cutscene {
    private bool triggered = false;
    private GameObject altarHeadpiece;
  
    private Animator altarAnimator;


    void Awake()
    {
        // Find all the objects we need references to
        altarHeadpiece = GameObject.Find("Altar_Headpiece");
        altarAnimator = altarHeadpiece.GetComponent<Animator>();
    }

    // public void RunCutscene()
    // {
    //     StartCoroutine(ExecuteCutscene());
    // }

    public override IEnumerator ExecuteCutscene()
    {
        // Flip triggered flag
        triggered = true;

        // Freeze player
        FindObjectOfType<PlayerControl>().lockPlayer();
        yield return new WaitForSeconds(1f);

        // FindObjectOfType<CameraController>().panToTarget(altarHeadpiece);
        FindObjectOfType<CameraController>().minorShake();
        FindObjectOfType<AudioManager>().Play("Passage_Open");


        altarAnimator.Play("Open");
        yield return new WaitForSeconds(6f);

        // Unfreeze player and relock camera
        FindObjectOfType<AudioManager>().FadeOut("Passage_Open");
        FindObjectOfType<CameraController>().stopShake();
        FindObjectOfType<PlayerControl>().unlockPlayer();

        GameObject.Find("Altar_Panel_Whispering").GetComponent<AudioSource>().enabled = false;
        // FindObjectOfType<CameraController>().resetZoom(1f);
        // FindObjectOfType<CameraController>().followPlayer();
    }
}
