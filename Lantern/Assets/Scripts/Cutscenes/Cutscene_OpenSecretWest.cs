using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cutscene_OpenSecretWest : MonoBehaviour
{

    private bool triggered = false;

    private GameObject Panel;
    private GameObject Whispering;
    private Animator PanelAnimator;
    private AudioSource WhispAudio;

    void Awake()
    {
        // Find all the objects we need references to
        Whispering = GameObject.FindGameObjectWithTag("Audio_West_Whispering");
        WhispAudio = Whispering.GetComponent<AudioSource>();
        Panel = GameObject.FindGameObjectWithTag("WestSlidingPanel");
        PanelAnimator = Panel.GetComponent<Animator>();
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

        FindObjectOfType<AudioManager>().Play("Passage_Open");
        FindObjectOfType<CameraController>().minorShake();
        PanelAnimator.Play("Moving_Panel_Open");
        yield return new WaitForSeconds(8);
        WhispAudio.enabled = false;
        FindObjectOfType<AudioManager>().FadeOut("Passage_Open");



        // Unfreeze player and relock camera
        // yield return new WaitForSeconds(1f);
        FindObjectOfType<PlayerControl>().unlockPlayer();
        FindObjectOfType<CameraController>().resetZoom(1f);
        FindObjectOfType<CameraController>().followPlayer();
    }
}
