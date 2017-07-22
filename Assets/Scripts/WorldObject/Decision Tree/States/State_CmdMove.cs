using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_CmdMove : CommandState {

	//child states of Gather
	State_Goto move;

	//current command type
	Command.TYPES type = Command.TYPES.MOVE;

	//get the parent tree in the constructor
	public State_CmdMove(DecisionTree p) : base(p) {
	
		move = new State_Goto (p);
		Reset ();
	
	}

	public override bool Update(List<Command> commands) {
		if (parent.getCommand ().type == this.type) {
			return true; //we have a command of equal priority
		}
		//else, pick a new, higher priority command
		bool avail = false;
		foreach (Command cmd in commands) {
			if (cmd.type == this.type && cmd.actors.Count < 1 && !cmd.complete) {
				//we've found an open command of this type
				parent.SetCommand(cmd);
				parent.currentState = this;
				avail = true;
				Reset ();
				CmdMoveTo ch = (CmdMoveTo)cmd;
				move.SetPosition (ch.position);

				break;
			}

		}
		return avail;
	}

	//each state must DO something
	public override void Act () {
		flow = move.getFlow ();
		if (flow == FLOW.fresh || flow == FLOW.run) {
			move.Act ();
		} else if (flow == FLOW.pass) {
			parent.getCommand ().complete = true;
			parent.ClearCommand ();
		} else { //failed to move
			parent.ClearCommand();
		}
	}

	//each sate must reset-able
	public override void Reset() {
		flow = FLOW.fresh;
		move.Reset ();
	}

}
