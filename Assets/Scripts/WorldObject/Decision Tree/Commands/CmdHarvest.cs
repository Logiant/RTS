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
public class CmdHarvest : Command {

	public Resource resource;
		
	//TODO this is only for the move command
	public CmdHarvest(Resource res, Player owner) : base(TYPES.HARVEST, res.transform.position, owner) {
		this.resource = res;
	}

	public override string Text() {
		return "Harvesting " + resource.objectName + ", " + actors.Count;

	}
}
