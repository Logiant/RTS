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
		HARVEST, //harvest any resource (except farms)
        FARM,
		CRAFT,
		MOVE,
		NONE //idle, do nothing
	}

	//units working on the current command
	public List<Unit> actors; //units working on the current command
	//command type
	public readonly TYPES type;   //command type
	//flag for completion
	public bool complete = false;
    //flag for if this command is paused
    public bool paused = false;
    //position for flag location
    public Vector3 position;

	//TODO this is only for the move command
	public Command(TYPES t, Vector3 pos) {
		type = t;
		actors = new List<Unit> ();
        position = pos;
	}

    public virtual void Cancel() {
        complete = true;
    }


	public virtual string Text() {
		return "Do Nothing, " + actors.Count;
	}
}
