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
public class CmdCraft : Command {

	public Workshop workshop;
		
	//TODO this is only for the move command
	public CmdCraft(Workshop building) : base(TYPES.CRAFT, building.transform.position) {
		this.workshop = building;
	}

	public override string Text() {
		return "Craft at " + workshop.objectName + ", " + actors.Count;

	}
}
