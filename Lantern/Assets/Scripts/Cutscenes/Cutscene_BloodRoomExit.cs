using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene_BloodRoomExit : MonoBehaviour {

    private bool triggered = false;

    private GameObject Panel;
    private GameObject Whispering;
    private Animator PanelAnimator;
    private AudioSource WhispAudio;

	public Transform inFrontOfBloodRoom;
	public Transform inFrontOfSwitch;


    void Awake()
    {
        // Find all the objects we need references to
        // Whispering = GameObject.FindGameObjectWithTag("Audio_West_Whispering");
        // WhispAudio = Whispering.GetComponent<AudioSource>();
        // Panel = GameObject.FindGameObjectWithTag("WestSlidingPanel");
        // PanelAnimator = Panel.GetComponent<Animator>();
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
        // FindObjectOfType<PlayerControl>().lockPlayer();

        GameObject.Find("Altar_Panel_Whispering").GetComponent<AudioSource>().enabled = true;

        FindObjectOfType<AudioManager>().Play("Bell_1");
		FindObjectOfType<FakePlayer>().Appear(inFrontOfBloodRoom);

		FindObjectOfType<FakePlayer>().moveTo(inFrontOfSwitch, "UP", 1.1f);
		yield return new WaitForSeconds(2f);
        FindObjectOfType<AudioManager>().Play("Tuning_Fork");


		yield return new WaitForSeconds(10f);
        // FindObjectOfType<PlayerControl>().unlockPlayer();

        FindObjectOfType<CameraController>().Glitch(0.1f, .7f);
		FindObjectOfType<FakePlayer>().winkOut();

        FindObjectOfType<AltarSwitchInteract>().enableShine();


        // FindObjectOfType<CameraController>().resetZoom(1f);
        // FindObjectOfType<CameraController>().followPlayer();
    }
}
