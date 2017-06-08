using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using RTS;

public class UI_CommandBar : MonoBehaviour {

	//button prefab
	public GameObject button;

	WorldObject selected;
	List<GameObject> buttons;


	// Use this for initialization
	void Start () {
		buttons = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () { }
	/*	//return if nothing changes
		if (selected == GameState.Selected) {
			return;
		}

		DeleteChildren ();

		selected = GameState.Selected;

		if (selected == null || !selected.player || !selected.player.isHuman) {
			return;
		}
		/*

		//we have something new selected - let's update our buttons
		List<string> unitActions = selected.getActions ();
		for (int i = 0; i < unitActions.Count; i++) {
			GameObject nextButton = Instantiate (button) as GameObject;
			nextButton.transform.SetParent (this.transform, false);
			RectTransform trans = nextButton.GetComponent<RectTransform> ();
			//set position on the x axis and text
			trans.position = new Vector3 (trans.position.x + (trans.sizeDelta.x * i), trans.position.y);
			nextButton.GetComponentInChildren<Text> ().text = unitActions [i];
			nextButton.name = unitActions [i];
			buttons.Add (nextButton);
			//add the command script to the button
			nextButton.GetComponent<Button>().onClick.AddListener(delegate {commandButton(nextButton.name); } );
		}
	}


	public void commandButton(string cmd) {
		Debug.Log (cmd);
		selected.doAction (cmd);
	}

	*/
	void DeleteChildren() {
		foreach (Transform child in transform) {
			Destroy (child.gameObject);
		}

	}
}
