using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WestAmbientAudioTrigger : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            FindObjectOfType<AudioManager>().Play("Dark_Empty");
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            FindObjectOfType<AudioManager>().FadeOut("Dark_Empty");
        }
    }
}
