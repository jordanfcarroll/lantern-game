using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedFlameInteract : Interact
{
    public Cutscene cutscene; 

	public override void endAction () {
        cutscene.GetComponent<Cutscene_1_1_SceneEnd>().RunCutscene();
    }
}