using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Opening : MonoBehaviour {

	public Dialogue openingDialogue;
	private Queue<string> readings;

    [SerializeField]
    private string newScene;

	void Awake () {
		// FindObjectOfType<AudioManager>().Play("Opening_Music");
		FindObjectOfType<AudioManager>().Play("Wind_Ambience");
		StartCoroutine(WaitToLoadNextScene());


        readings = new Queue<string>();
        foreach (string reading in openingDialogue.readings)
        {
            readings.Enqueue(reading);
        }
    }
	
	IEnumerator WaitToLoadNextScene () {

        FindObjectOfType<BackgroundUIFader>().startFade();
        yield return new WaitForSeconds(1f);
        FindObjectOfType<ReadBoxControl>().Activate(readings);
        yield return new WaitForSeconds(18f);
        // FindObjectOfType<AudioManager>().FadeOut("Opening_Music");
        SceneManager.LoadScene(newScene);
	}
}
