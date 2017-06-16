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
public class CmdMoveTo : Command {

	public Vector3 position;
		
	//TODO this is only for the move command
	public CmdMoveTo(Vector3 position) : base(TYPES.MOVE) {
        this.position = position;
	}

	public override string Text() {
		return "Move " + position + ", " + actors.Count;

	}
}
