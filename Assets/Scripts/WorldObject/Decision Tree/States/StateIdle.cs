using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIdle : State {

	public StateIdle(DecisionTree parent) : base(parent, DecisionTree.STATES.IDLE) {

	}

	public override bool Update(List<Command> commands) {
		foreach (Command cmd in commands) {
			if (cmd.type == Command.TYPES.NONE && cmd.actors.Count == 0) {
				cmd.actors.Add (this.parent.GetUnit ());
				this.parent.SetCommand (cmd);
				break;
			}
		}
		return true; //we can always idle!
	}

	public override void Act() {
		//TODO implement -- maybe walk to the town center or home?
	}
}