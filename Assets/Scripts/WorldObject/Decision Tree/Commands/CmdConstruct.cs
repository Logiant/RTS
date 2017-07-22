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
public class CmdConstruct : Command {

	public Foundation foundation;
		
	//TODO this is only for the move command
	public CmdConstruct(Foundation building) : base(TYPES.BUILD, building.transform.position) {
		this.foundation = building;
	}
    
    public override void Cancel() {
        base.Cancel();
        //TODO refund cost seems wrong
        RTS.GameState.player.bp.Add(foundation.getCost());
        GameObject.Destroy(foundation.gameObject);
    }

	public override string Text() {
		return "Construct " + foundation.GetName() + ", " + actors.Count;

	}
}
