using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cutscene : MonoBehaviour {
    public string id;
    public bool triggered;

    public void RunCutscene()
        {
            StartCoroutine(ExecuteCutscene());
        }   

        // Can be overwritten by children
        public virtual IEnumerator ExecuteCutscene () {
            yield return new WaitForSeconds(0f);;
        }
}