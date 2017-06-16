﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMove : State {

	public StateMove(DecisionTree parent) : base(parent, DecisionTree.STATES.MOVE) {

	}

	public override bool Update(List<Command> commands) {
		if (parent.currentCommand.type == Command.TYPES.MOVE) {
			return true; //don't try to move if we're already moving
		}
		//look through the commands
		bool validCommand = false;
		//if one is type.move && command.numActors == 0
		foreach (Command cmd in commands) {
			if (cmd.type == Command.TYPES.MOVE && cmd.actors.Count == 0 && !cmd.complete) {
			//	cmd.actors.Add (this.parent.GetUnit ());
				this.parent.SetCommand (cmd);
				validCommand = true;
				break;
			}

		}

		return validCommand;
	}

	public override void Act() {
		//TODO implement
		Unit body = parent.GetUnit();
		CmdMoveTo moveCmd = (CmdMoveTo)(parent.currentCommand);

        body.targetPosition = moveCmd.position;// + new Vector3(0, 0.5f, 0);

		//check for command completion/cancellation
		if ((body.targetPosition - body.transform.position).sqrMagnitude < 0.1) {
			this.parent.currentCommand.complete = true;
			parent.ClearCommand ();
			body.targetPosition = body.transform.position;
		}
	}
}