using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightCollisionControl : MonoBehaviour {

    void onCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Spotlight_Cone_Collider")
        {
        }
    }
}
