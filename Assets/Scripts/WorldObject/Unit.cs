using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Units can:
 * 	-move
 *  -produce items
 *  -fight
 *  -equip items
 * 
 * Units have the component
 * 	-inventories
 */

//unit type is based on current equpment
//	-this prioritizes tasks for the unit
public enum TYPES {
	CHURL,		//basic human, 		no equipment
	GATHERER,	//gathers resources,equip tools [axe, pick]
	FARMER,		//farms,			equip scythe
	LABORER,	//builds structures,equip hammer
	CRAFTSMAN,	//builds items,		equip crafter
	SOLDIER		//fights,			equip weapon
}

//Begin the Unit class
public class Unit : WorldObject {
	//unit type - defines behavior, 
	TYPES type = TYPES.CHURL;
	//decision tree - controls behavior
	DecisionTree brain;

	//movement TODO should this be in a separate script for readability? probably
	public Vector3 targetPosition;
	public float speed = 3.5f; // m/s

	// Use this for initialization
	public override void Start () {
		targetPosition = transform.position;
		base.Start();
		PickState (); //instantiates brain
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update ();
		Debug.Log (name + " is updating");
		brain.Update (this.player.activeCommands); //feed in active commands from the player
	}

	public void FixedUpdate() {
		Vector3 distance = (targetPosition - transform.position);

		if (distance.magnitude <= speed * Time.fixedDeltaTime) {
			transform.position = targetPosition;
		} else {
			transform.position = transform.position + distance.normalized * speed * Time.fixedDeltaTime;
		}
	}


	// when a new item has been equipped
	// update this units decision tree
	private void PickState() {
		switch (type) {
		case TYPES.CHURL:
		case TYPES.GATHERER:
		case TYPES.FARMER:
		case TYPES.LABORER:
		case TYPES.CRAFTSMAN:
		case TYPES.SOLDIER:
			brain = new TreeChurl (this);
			break;
		}
	}
}