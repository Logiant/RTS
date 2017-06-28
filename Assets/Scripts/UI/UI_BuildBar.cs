using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using RTS;
using System;

public class UI_BuildBar : UI_Base {

	//button prefab
	public GameObject button;
	//structure foundation prefab
	public GameObject foundation;
	//list of structure prefabs
	public GameObject[] structures;
	//building indicator (square thing)
	public UI_BuildRegion buildIndicator;
    //descriptive text
    public Text costText;
	//buildable structure and button lists
	List<Button> buttons;
	//currently selected structure
	GameObject selected;
	public LayerMask terrainMask;

	// Use this for initialization
	void Start () {
		buttons = new List<Button> ();

		for (int i = 0; i < structures.Length; i++) {
			GameObject go = Instantiate (button, this.transform) as GameObject;
			go.transform.position = go.transform.position + new Vector3 (100f * i, -25, 0);
			Button b = go.GetComponent<Button> ();
			buttons.Add(b);
			//update button appearance and add a callback
			b.GetComponentInChildren<Text> ().text = structures [i].GetComponent<WorldObject>().objectName;
			b.onClick.AddListener (() => BuildStructure (b));
		}
        costText.text = "";
	}

	//command passed from button children
	public void BuildStructure(Button b) {
		//Debug.Log (b.GetComponent<Text> ().text);
        //TODO if GameState.player.bp.wood < buildCost
		selected = structures [buttons.IndexOf(b)];
		buildIndicator.Select (selected);
        //else - send a message, "Requires more structural material!"
	}

    public override void Mouseover(GameObject go) {
        Button b = go.GetComponent<Button>();
        Structure str = structures[buttons.IndexOf(b)].GetComponent<Structure>();
        costText.text = str.objectName + ", " + str.WoodCost + " wood";
        //TODO if cost is too high, color red
   //   if (str.WoodCost > GameState.player.bp.woodQty) {
   //       b.GetComponent<Image>().color = Color.red;
    //  } else {
     //     b.GetComponent<Image>().color = Color.white;
      //}

    }

    public override void MouseExit(GameObject go) {
        if (selected == null) {
            costText.text = "";
        }
    }

    // Update is called once per frame
    void Update () {

		if (selected != null) {
			//check for an escape key press - if so exit
			if (Input.GetKeyUp (KeyCode.Escape)) {
				selected = null;
				buildIndicator.Clear ();
				return;
			}
			//raycast through the mouse to the terrain
			Vector3 position = Raycast();
			//draw a ghost of the selected building

			buildIndicator.transform.position = position;

		}

	}

    public override bool RightClick() {
        bool hadSelected = (selected != null);
        if (hadSelected) {
            Deselect();
        }
        return hadSelected;
    }

	public override bool LeftClick() {
		//do nothing - for now!!
		bool building = (selected != null);

		if (building) {
			//raycast out to the terrain
			Vector3 pt = Raycast();
			//if (building is valid)
			if (pt != RTS.GameState.InvalidPosition && buildIndicator.isValid() && GameState.player.bp.res.wood >= selected.GetComponent<Structure>().WoodCost) {
                //remove the resources
                GameState.player.bp.Remove(selected.GetComponent<Structure>().getCost());
				//place the building
				Foundation wo = Instantiate(foundation, pt, new Quaternion()).GetComponent<Foundation>();
				wo.player = RTS.GameState.player;
				wo.Run (selected);
                Deselect();
			}

		}


		return building;
	}

    private void Deselect() {
        buildIndicator.Clear();
        selected = null;
        costText.text = "";
    }

	private Vector3 Raycast() {
		Vector3 hitPos = RTS.GameState.InvalidPosition;
		RaycastHit hit;

		if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, Mathf.Infinity, terrainMask)) {
			hitPos = hit.point;
		}
		return hitPos;
	}

}
