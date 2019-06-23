using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EastAmbientAudioTrigger : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            FindObjectOfType<AudioManager>().Play("Drafty_Ambience");
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            FindObjectOfType<AudioManager>().FadeOut("Drafty_Ambience");
        }
    }
}
