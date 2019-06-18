using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTooltip : MonoBehaviour {


	public void Display() {
		GetComponent<Animator>().Play("In");
	}

	public void Hide() {
		GetComponent<Animator>().Play("Out");
	}
}
