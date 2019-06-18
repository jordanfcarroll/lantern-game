using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class testcutscenetrigger : MonoBehaviour {

    [SerializeField]
    private string newScene;

    public GameObject Cutscene_WestPassageOpen;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            SceneManager.LoadScene(newScene);
        }
    }
}
