using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EastBookshelfInteract : Interact {

        public GameObject cutscene;

	public override void endAction () {
                if (FindObjectOfType<GameManager_1>().keyInventory["black_book"])
                {
                        cutscene.GetComponent<Cutscene_OpenEastBookshelf>().RunCutscene();
                }


	}

}
