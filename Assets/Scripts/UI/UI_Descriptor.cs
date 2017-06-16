using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using RTS;

public class UI_Descriptor : MonoBehaviour {

	Text description;

	// Use this for initialization
	void Start () {
		description = GetComponentInChildren<Text> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (GameState.hasSelected) {
			description.text = "Selected: " + GameState.Selected.objectName;
		} else {
			description.text = "Selected: " + " None";
		}

	}

}
