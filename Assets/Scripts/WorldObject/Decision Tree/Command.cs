using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * commands are assigned by the player (or AI)
 * examples:
 * 		move to X,Y,Z
 * 		craft item I
 * 		attack ZZ
 * 		Harvest X
 * 		Build structure b
 * 
 */
public class Command {
	public enum TYPES {
		ATTACK,   // form an attacking army
		GARRISON, // garrison a defensive structure
		BUILD,
		HARVEST, //harvest any resource (including farms)
		CRAFT,
		MOVE,
		NONE //idle, do nothing
	}

	//units working on the current command
	public List<Unit> actors; //units working on the current command
	//command type
	public readonly TYPES type;   //command type
	//command target
	WorldObject target;
	public readonly Vector3 position;

	//TODO this is only for the move command
	public Command(TYPES t, Vector3 position) {
		type = t;
		this.position = position;
		actors = new List<Unit> ();
	}

	public string Text() {
		switch (type) {
		default:
			return "COMMAND: Move to " + position;
			break;
		case (TYPES.NONE):
			return "COMMAND: NONE";
			break;
		}
	}
}
