using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene_OpenEastBookshelf : Cutscene {

    private bool triggered = false;

    public GameObject bookshelf;
    private Animator bookshelfAnimator;

    void Awake()
    {
        // Find all the objects we need references to
        bookshelfAnimator = bookshelf.GetComponent<Animator>();

    }

    public override IEnumerator ExecuteCutscene()
    {
        FindObjectOfType<PlayerControl>().lockPlayer();
        bookshelfAnimator.Play("Bookshelf_Open");

        FindObjectOfType<AudioManager>().Play("Passage_Open");
        FindObjectOfType<CameraController>().minorShake();
       	yield return new WaitForSeconds(5f);


        FindObjectOfType<CameraController>().stopShake();
        FindObjectOfType<PlayerControl>().unlockPlayer();
        FindObjectOfType<AudioManager>().Stop("Passage_Open");
        FindObjectOfType<PlayerControl>().unlockPlayer();

    }
}
