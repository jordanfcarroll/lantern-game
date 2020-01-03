using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternInteract : Interact
{
	public override void endAction () {
        FindObjectOfType<GameManager_1>().shouldTriggerDoorUnlock = true;
        FindObjectOfType<PlayerControl>().enableLantern();
    }
}