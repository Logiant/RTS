using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_CmdCraft : CommandState {

	//child states of Gather
	State_Goto move;
    //  State_Transform craft;
    State_GetTools getTool;
    Workshop ws;

	//get the parent tree in the constructor
	public State_CmdCraft(DecisionTree p) : base(p) {

        type = Command.TYPES.CRAFT;
        move = new State_Goto (p);
        getTool = new State_GetTools(p);
		Reset ();
	
	}

	public override bool Update(List<Command> commands) {
        	if (parent.getCommand ().type == this.type) {
                return true; //we have a command of equal priority
            }
        //else, pick a new, higher priority command
        bool avail = false;
        foreach (Command cmd in commands) {
            if (cmd.type == this.type && cmd.actors.Count < parent.GetUnit().player.getWorkshops().Count) {
                CmdCraft ch = (CmdCraft)cmd;
                ws = ch.GetOpenWorkshop();
                if (ws == null) {
                    continue;
                }
                //we've found an open command of this type
                parent.SetCommand(cmd);
                parent.currentState = this;
                avail = true;
                Reset ();
                getTool.SetCommand(ch);
                move.SetPosition (ws.accessPoint.position);
                    break;
                }

            }
        return avail;
        }

	//each state must DO something
	public override void Act () {
        //first do getTool
        if (getTool.getFlow() == FLOW.pass || getTool.getFlow() == FLOW.fail) {
            flow = move.getFlow();
            if (flow == FLOW.fresh || flow == FLOW.run) {
                move.Act();
            } else if (flow == FLOW.pass) {
                //		parent.getCommand ().complete = true;
                //		parent.ClearCommand ();
                move.Reset();
                move.SetPosition(ws.accessPoint.position);

                ws.Craft();

            } else { //failed to move
                parent.ClearCommand();
            }
        } else {
            getTool.Act();
            flow = getTool.getFlow();
        }
	}

	//each sate must reset-able
	public override void Reset() {
		flow = FLOW.fresh;
		move.Reset ();
        getTool.Reset();
	}

}
