using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedFlameDialogTrigger : MonoBehaviour
{

    public Dialogue dialogue;
	

    public void triggerDialogue()
    {
        if (GameManager_1.Instance.talkedToParhellion)
        {
            // trigger cutscene
        } else {
        	FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
		}
    }
}
