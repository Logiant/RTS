using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using RTS;

public class UI_ResourceBar : MonoBehaviour {

	Text text;

	// Use this for initialization
	void Start () {
		text = GetComponentInChildren<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		text.text = "Resources: " + GameState.player.resources;
	}
}
