using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdle : State {

	public StateIdle(DecisionTree parent) : base(parent, DecisionTree.STATES.IDLE) {

	}

	public override bool Update(List<Command> commands) {
		if (parent.getCommand().type == Command.TYPES.NONE) {
			return true;
		}
		parent.SetCommand (parent.GetUnit ().player.nothing);
        parent.GetUnit().MoveTo(parent.GetUnit().transform.position);
		return true; //we can always idle!
	}

	public override void Act() {
        //TODO do something
        //if resources in inventory are nonzero
            //return them to the nearest warehouse
        //otherwise
            //go home or something
	}
}