using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using RTS;

public class UI_ResourceBar : MonoBehaviour {

	Text text;
    Backpack.Resources res;

	// Use this for initialization
	void Start () {
		text = GetComponentInChildren<Text> ();
    }

    // Update is called once per frame
    void Update () {
        text.text = "Resources:\n";
        res = GameState.player.bp.res;


        text.text += "Food: " + res.corn + "\n";
        text.text += "Wood: " + res.wood + "\n";
        text.text += "Metal: " + res.metal + "\n";
        text.text += "Tools: " + res.tools + "\n";
    }
}
