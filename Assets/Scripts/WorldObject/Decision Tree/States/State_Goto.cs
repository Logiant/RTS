using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Goto : State {

	//position to move to
	Vector3 targetPosition;

	public State_Goto(DecisionTree parent) : base(parent) {
	
	}

	//each state must DO something
	public override void Act () {
		//fail if targetPosition is invalid
		if (targetPosition == RTS.GameState.InvalidPosition) {
			flow = FLOW.fail;
			return;
		}
        //TODO fail out if we aren't moving more than 0.01m or so

		//if the body isn't moving toward the target, move it. flow = running
		if ((parent.GetUnit ().transform.position - targetPosition).magnitude > 2) {
			//TODO don't assume pathfinding will always find a way
			//TODO don't constantly send a MoveTo command to the player
			parent.GetUnit ().MoveTo (targetPosition);
			flow = FLOW.run;
		} else { //if we're at the target we're done!
			parent.GetUnit().Halt();
			flow = FLOW.pass;
		}
	}

	//each sate must reset-able
	public override void Reset() {
		flow = FLOW.fresh;
		targetPosition = RTS.GameState.InvalidPosition;
	}

	//// New functionality ////
	public void SetPosition(Vector3 position) {
		targetPosition = position;
		//TODO set position.y such that the y is the height of the terrain?
	}



}
