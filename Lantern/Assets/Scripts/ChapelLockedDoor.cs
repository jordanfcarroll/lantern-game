using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ChapelLockedDoor : MonoBehaviour {

    public AudioSource lockedSound;
    public AudioSource openSound;
    public AudioSource unlockSound;
    public AudioSource closeSound;
    public AudioSource scarySound;

    // door lock
	public bool isLocked;
    // interact lock
	public bool locked = false;
	// public string keyName = "red_key";
	public bool isOpen;
    private bool shouldPlayScarySound = true;
    private Animator animator;
    BoxCollider2D closedCollider;
    private DialogueTrigger unlockDialogueTrigger;
    private DialogueTrigger lockedDialogueTrigger;

    void Start () {
        animator = GetComponent<Animator>();
    }

    void Awake()
    {

        BoxCollider2D[] BoxCollider2Ds = gameObject.GetComponentsInChildren<BoxCollider2D>();

        foreach (BoxCollider2D BoxCollider2D in BoxCollider2Ds)
        {
            if (BoxCollider2D.gameObject.transform.parent != null)
            {
                closedCollider = BoxCollider2D;
            }
        }

        DialogueTrigger[] DialogueTriggers = gameObject.GetComponentsInChildren<DialogueTrigger>();

        foreach (DialogueTrigger DialogueTrigger in DialogueTriggers)
        {
            if (DialogueTrigger.gameObject.transform.parent != null)
            {
                unlockDialogueTrigger = DialogueTrigger;
            }
        }
    }

    void Update () {
        if (isOpen) {
            animator.Play("Open");
            // deactive collider
            closedCollider.enabled = false;
        } else {
            animator.Play("Closed");
            closedCollider.enabled = true;
        }
    }

    void LateUpdate() {
        locked = false;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.name == "Player" && Input.GetButtonDown("Fire1") && !locked)
        {   
            locked = true;
            // try to open the door
            if (!isOpen)
            {
                if (isLocked && !FindObjectOfType<GameManager_1>().redKey) {
                    // do locked things
					FindObjectOfType<PlayerControl>().TriggerInteract(interactAction);
                    lockedSound.Play();
                    // GetComponent<DialogueTrigger>().triggerDialogue();
                    
                    
				} else if (isLocked && FindObjectOfType<GameManager_1>().redKey) {
                    openSound.Play();
                    // unlockSound.Play();
                    unlockDialogueTrigger.triggerDialogue();
                    isOpen = true;
                    isLocked = false;
                } else {
                    openSound.Play();
                    isOpen = true;
                }
            } 
            // close the door
            else {
                closeSound.Play();
                isOpen = false;
            }
        }
    }


    void endAction () {
        if (shouldPlayScarySound)
        {
            StartCoroutine(DelayScarySound());
            shouldPlayScarySound = false;
        }
    }

    void interactAction () {
        GetComponent<DialogueTrigger>().triggerDialogue(endAction);
    }

    IEnumerator DelayScarySound () {
        yield return new WaitForSeconds(3f);
        scarySound.Play();
        yield return new WaitForSeconds(.3f);
        scarySound.Play();
        yield return new WaitForSeconds(.3f);
        scarySound.Play();
    }
}
