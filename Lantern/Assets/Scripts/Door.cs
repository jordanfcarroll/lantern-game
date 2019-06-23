using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Generic Door class, doors with special needs can inherit
[RequireComponent(typeof(BoxCollider2D))]
public class Door : MonoBehaviour
{
    // Individual doors can have different sounds
    public AudioSource lockedSound;
    public AudioSource openSound;
    public AudioSource unlockSound;
    public AudioSource closeSound;
    public AudioSource scarySound;

    public string keyName = "red_key";

    // door lock
    public bool isLocked;
    // interact lock
    private bool locked = false;

    public bool isOpen;
    private Animator animator;
    BoxCollider2D closedCollider;
    private DialogueTrigger unlockDialogueTrigger;
    private DialogueTrigger lockedDialogueTrigger;

    private bool shouldLateUpdate = false;

    void Start()
    {
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

    void Update()
    {
        if (isOpen)
        {
            animator.Play("Open");
            // deactive collider
            closedCollider.enabled = false;
        }
        else
        {
            animator.Play("Closed");
            closedCollider.enabled = true;
        }
    }

    void LateUpdate()
    {
        if (shouldLateUpdate) {
            locked = false;
            shouldLateUpdate = false;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            // NOTE locked != doorLocked, it's a lock on re-interact

            if (Input.GetButtonDown("Fire1") && !locked)
            {
                FindObjectOfType<PlayerControl>().unsetInteractTooltip();
                locked = true;
                // try to open the door
                if (!isOpen)
                {
                    // IT'S LOCKED AND WE DON'T GOT THE KEY OR THERE IS NO KEY
                    if (isLocked && (!FindObjectOfType<GameManager_1>().keyInventory[keyName]))
                    {
                        // do locked things
                        FindObjectOfType<PlayerControl>().TriggerInteract(interactAction);
                        lockedSound.Play();
                        // GetComponent<DialogueTrigger>().triggerDialogue();
                    }
                    // IT'S LOCKED AND OH SHIT WE GOT THE KEY
                    else if (isLocked && FindObjectOfType<GameManager_1>().keyInventory[keyName])
                    {
                        openSound.Play();
                        // unlockSound.Play();
                        unlockDialogueTrigger.triggerDialogue(unlockAndEnd);
                        isOpen = true;
                        isLocked = false;
                    }
                    // OR JUST OPEN IT
                    else
                    {
                        openSound.Play();
                        isOpen = true;
                        scheduleLateUpdate();
                    }
                }
                // close the door
                else
                {
                    closeSound.Play();
                    isOpen = false;
                    scheduleLateUpdate();
                }
            }

        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.name == "Player")
        {
            FindObjectOfType<PlayerControl>().setInteractTooltip();
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.name == "Player")
        {
            FindObjectOfType<PlayerControl>().unsetInteractTooltip();
        }
    }

	private void unlockAndEnd () {
        FindObjectOfType<PlayerControl>().setInteractTooltip();
        locked = false;
		endAction();
	}

    private void scheduleLateUpdate () {
        shouldLateUpdate = true;
        FindObjectOfType<PlayerControl>().setInteractTooltip();
        endAction();
    }

    public virtual void endAction()
    {
		// children can override this for custom behavior
    }

    void interactAction()
    {
        GetComponent<DialogueTrigger>().triggerDialogue(unlockAndEnd);
    }

   
}
