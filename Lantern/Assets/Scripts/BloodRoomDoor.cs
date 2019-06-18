using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodRoomDoor : Door {
    private bool shouldPlayScarySound = true;


    // occurs when player tried to open the door without key
    public override void endAction () {
        if (shouldPlayScarySound)
			{
				StartCoroutine(DelayScarySound());
				shouldPlayScarySound = false;
			}
	}

    IEnumerator DelayScarySound()
    {
        yield return new WaitForSeconds(3f);
        scarySound.Play();
        yield return new WaitForSeconds(.3f);
        scarySound.Play();
        yield return new WaitForSeconds(.3f);
        scarySound.Play();
    }

}
