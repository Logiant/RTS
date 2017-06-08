using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//WorldObject is the main entity everything selectable in the world
public abstract class WorldObject : MonoBehaviour {
	
	/*
	 * All world objects:
	 * 	-are selectable
	 *  -are commandable
	 * 
	 * All world objects have the component:
	 *  -health -> this is special enough to warrent its own component script
	 */

	public string objectName;
	public Player player;
	private bool currentlySelected = false;

	//script that manages health, armor, etc
	Health health;

	public virtual void Start() {
		health = new Health ();
	}

	//update HP
	public virtual void Update() {
		
	}


/*	//comes from user input -- do a command here!
	public virtual void command(GameObject other, Vector3 position) {
		//do some command to the supplied GameObject and position
		//GameObject could be terrain, another WorldObject, or something else entirely!

	}

	//action commands from the UI
	public void doAction(string action) {


	}


	public List<string> getActions() {
		return null;
	}
*/
	public void SetSelected(bool selected) {
		currentlySelected = selected;
		if (currentlySelected) {
			//TODO draw a selection indicator
		} else {
			//TODO kill selection indicator
		}
	}

}