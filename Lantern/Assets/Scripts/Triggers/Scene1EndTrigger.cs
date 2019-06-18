using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1EndTrigger : MonoBehaviour {

    private bool triggered = false;

    public GameObject Cutscene_End_1;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player" && FindObjectOfType<GameManager_1>().bloodRoomVisited && !triggered)
        {
            triggered = true;
            Cutscene_End_1.GetComponent<Cutscene_End_1>().RunCutscene();
        }
    }
}
