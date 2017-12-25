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

		if (GameState.hasSelected && GameState.Selected.GetComponent<Generic>() == null) {
			description.text = "Selected: " + GameState.Selected.objectName;
            Unit u = GameState.Selected.GetComponent<Unit>();
            if (u != null) {
                description.text += ": " + u.GetUnitType() + "\n(" + u.tool.ToString() + ")";
            }
            Warehouse w = GameState.Selected.GetComponent<Warehouse>();
            if (w != null) {
                int[] tools = w.player.ist.GetQty();
                description.text += "\nAxe: " + tools[0] + " Hammer: " + tools[1] + " Saw: " + tools[2] + " Scythe: " + tools[3];
            }
		} else {
			description.text = "Selected: " + " None";
		}
	}

}
